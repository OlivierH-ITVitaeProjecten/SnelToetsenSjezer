using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using System.Diagnostics;
using System.Xml;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyService : IHotKeyService
    {
        private readonly List<HotKey> _hotKeys = new() { };
        private int _currHotKey = 0;
        private int _currHotKeyStep = 0;
        private string _testString = "";
        private int _mistakes = 0;
        private bool _dealingWithFails = false;

        public void AddHotKey(string category, string description, string hotKeySteps)
        {
            List<Array> mySteps = new List<Array>();

            bool isSequential = hotKeySteps.Contains(",");
            string[] keysParts = hotKeySteps.Split(isSequential ? "," : "+");
            keysParts.ToList().ForEach(k =>
            {
                string kLower = k.ToLower();
                switch (kLower)
                {
                    case "ctrl":
                        mySteps.Add(new string[] { "key", Keys.ControlKey.ToString() });
                        break;
                    //case "lctrl":
                    //    mySteps.Add(new string[] { "key", Keys.LControlKey.ToString() });
                    //    break;
                    //case "rctrl":
                    //    mySteps.Add(new string[] { "key", Keys.RControlKey.ToString() });
                    //    break;
                    case "alt":
                        mySteps.Add(new string[] { "key", Keys.Menu.ToString() });
                        break;
                    //case "lalt":
                    //    mySteps.Add(new string[] { "key", Keys.LMenu.ToString() });
                    //    break;
                    //case "ralt":
                    //    mySteps.Add(new string[] { "key", Keys.RMenu.ToString() });
                    //    break;
                    case "shift":
                        mySteps.Add(new string[] { "key", Keys.ShiftKey.ToString() });
                        break;
                    //case "lshift":
                    //    mySteps.Add(new string[] { "key", Keys.LShiftKey.ToString() });
                    //    break;
                    //case "rshift":
                    //    mySteps.Add(new string[] { "key", Keys.RShiftKey.ToString() });
                    //    break;
                    case "enter":
                        mySteps.Add(new string[] { "key", Keys.Enter.ToString() });
                        break;
                    case "return":
                        mySteps.Add(new string[] { "key", Keys.Return.ToString() });
                        break;
                    case "backspace":
                        mySteps.Add(new string[] { "key", Keys.Back.ToString() });
                        break;
                    case "tab":
                        mySteps.Add(new string[] { "key", Keys.Tab.ToString() });
                        break;
                    case "insert":
                        mySteps.Add(new string[] { "key", Keys.Insert.ToString() });
                        break;
                    case "delete":
                        mySteps.Add(new string[] { "key", Keys.Delete.ToString() });
                        break;
                    case "home":
                        mySteps.Add(new string[] { "key", Keys.Home.ToString() });
                        break;
                    case "end":
                        mySteps.Add(new string[] { "key", Keys.End.ToString() });
                        break;
                    case "pageup":
                    case "page up":
                        mySteps.Add(new string[] { "key", Keys.PageUp.ToString() });
                        break;
                    case "pagedown":
                    case "page down":
                        mySteps.Add(new string[] { "key", Keys.Next.ToString() });
                        break;
                    case @"\":
                        mySteps.Add(new string[] { "key", Keys.Oem5.ToString() });
                        break;
                    default:
                        mySteps.Add(new string[] { "string", k.ToUpper() });
                        break;
                }
            });

            HotKey myNewHotKey = new HotKey()
            {
                Category = category,
                Description = description,
                Steps = mySteps,
                Behaviour = (isSequential ? HotKeyBehaviour.Sequential : HotKeyBehaviour.AllAtOnce)
            };
            _hotKeys.Add(myNewHotKey);
        }

        public void ProcessHotkeysXmlFile(string filePath)
        {
            XmlDocument hotKeysXml = new XmlDocument();
            hotKeysXml.Load(filePath);

            if (hotKeysXml.DocumentElement != null)
            {
                foreach (XmlNode node in hotKeysXml.DocumentElement.ChildNodes)
                {
                    string? category = node.Attributes?["name"]?.InnerText;
                    if (!string.IsNullOrEmpty(category))
                    {
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            string? description = childNode.Attributes?["description"]?.InnerText;
                            string? keys = childNode.Attributes?["keys"]?.InnerText;
                            if (!string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(keys))
                            {
                                AddHotKey(category, description, keys);
                            }
                        }
                    }
                }
            }
        }
        public bool CheckAgainstExpectedKey(string keyInput, out bool completedWholeSet, out bool currStepIsStringButIncomplete)
        {
            HotKey myHotKey = _hotKeys[_currHotKey];
            List<Array> hotKeySteps = myHotKey.Steps;
            completedWholeSet = false;
            currStepIsStringButIncomplete = false;

            if (_currHotKeyStep < hotKeySteps.Count())
            {
                Array expectedStep = hotKeySteps[_currHotKeyStep];
                string expectedStepType = expectedStep.GetValue(0).ToString();
                string expectedStepValue = expectedStep.GetValue(1).ToString();

                Debug.WriteLine("expected: " + expectedStepValue);

                switch (expectedStepType)
                {
                    case "key":
                        if (keyInput == expectedStepValue)
                        {
                            _currHotKeyStep++;
                            if (_currHotKeyStep == hotKeySteps.Count()) completedWholeSet = true;
                            return true;
                        }
                        break;
                    case "string":
                        _testString += keyInput;
                        if (_testString == expectedStepValue)
                        {
                            _currHotKeyStep++;
                            if (_currHotKeyStep == hotKeySteps.Count()) completedWholeSet = true;
                            return true;
                        }
                        if (expectedStepValue.StartsWith(_testString))
                        {
                            currStepIsStringButIncomplete = true;
                            return true;
                        }
                        break;
                }
            }
            myHotKey.Failed = true;
            myHotKey.Fails++;
            _mistakes++;
            return false;
        }

        public string CurrHotKey_CategoryName()
        {
            return _hotKeys[_currHotKey].Category;

        }
        public string CurrHotKey_Description()
        {
            return _hotKeys[_currHotKey].Description;
        }
        public int[] GetCurrHotKeyAndCount()
        {
            return new int[2] { _currHotKey, _hotKeys.Count() };
        }
        public int[] GetCurrHotKeyStepAndCount()
        {
            HotKey myHotKey = _hotKeys[_currHotKey];
            List<Array> hotKeySteps = myHotKey.Steps;
            return new int[2] { _currHotKeyStep, hotKeySteps.Count() };
        }
        public int GetCurrHotKeyFails()
        {
            return _hotKeys[_currHotKey].Fails;
        }
        public void NextHotKey()
        {
            _currHotKeyStep = 0;
            _testString = "";
            if (!_dealingWithFails && _currHotKey < _hotKeys.Count()-1) { 
                _currHotKey++;
            }
            else
            {
                Debug.WriteLine("we're done! lets see if we need to backtrack!");
                int firstFailedIndex = _hotKeys.FindIndex(h => h.Failed == true);

                if ((firstFailedIndex >= 0) && (firstFailedIndex <= _hotKeys.Count() - 1))
                {
                    Debug.WriteLine("oh crap we have ourselves a fail! backtracking!");
                    _dealingWithFails = true;
                    _currHotKey = firstFailedIndex;
                    _hotKeys[firstFailedIndex].Failed = false;
                } else
                {
                    Debug.WriteLine("f- yeah we're really done! (show gameover screen with stats etc)");
                }
            }
        }
    }
}