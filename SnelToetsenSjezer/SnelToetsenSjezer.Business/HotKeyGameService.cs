using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyGameService : IHotKeyGameService
    {
        private List<HotKey> _gameHotKeys = new() { };
        private List<HotKey> _failedHotKeys = new() { };
        private static readonly Dictionary<string, bool> _currentlyPressedKeys = new Dictionary<string, bool>();

        private Action<string> gameStateUpdatedCallback = null;
        private Action<int> gameTimerCallback = null;

        private static readonly System.Windows.Forms.Timer _gameTimer = new System.Windows.Forms.Timer();
        private static int _gameSeconds = 0;

        private List<string> _userInputSteps = new List<string>();

        private int _currHotKey = 0;
        private int _currHotKeyStep = 0;
        private string _testString = "";
        private int _mistakes = 0;
        private bool _dealingWithFails = false;

        public void SetHotKeys(List<HotKey> hotKeys)
        {
            _gameHotKeys = hotKeys;
        }

        public void SetGameStateUpdatedCallback(Action<string> callback)
        {
            gameStateUpdatedCallback = callback;
        }
        public void SetGameTimerCallback(Action<int> callback)
        {
            gameTimerCallback = callback;
        }

        public void StartGame()
        {
            Debug.WriteLine("Starting game!");
            _gameTimer.Interval = 1000;
            _gameTimer.Tick += new EventHandler(GameTimer_Tick);
            _gameTimer.Start();
            gameStateUpdatedCallback("");
        }
        public void GameTimer_Tick(object sender, EventArgs e)
        {
            _gameSeconds++;
            gameTimerCallback(_gameSeconds);
        }
        public void StopGame()
        {
            Debug.WriteLine("Stopping game!");
            _gameTimer.Stop();
        }

        public void KeyDown(string keyName)
        {
            if (!_currentlyPressedKeys.ContainsKey(keyName) || !_currentlyPressedKeys[keyName])
            {
                Debug.WriteLine("KeyDown: " + keyName);
                _currentlyPressedKeys[keyName] = true;
            }
            /*
            if (!_currentlyPressedKeys.ContainsKey(keyCodeAsString) || !_currentlyPressedKeys[keyCodeAsString])
            {
                bool keyIsValid = MyHotKeyGameService!.CheckAgainstExpectedKey(keyCodeAsString, out bool wholeSetCorrect, out bool currStepIsStringButIncomplete);
                if (keyIsValid && !currStepIsStringButIncomplete) progbar_correctsteps.PerformStep();
                _currentlyPressedKeys[keyCodeAsString] = true;

                Debug.WriteLine(keyIsValid + " " + e.KeyCode);
                Debug.WriteLine($"keyCode: {e.KeyCode} keyValue: {e.KeyValue} keyData: {e.KeyData}");

                if (!keyIsValid)
                {
                    Debug.WriteLine("You done f-ed up boy!");
                    MyHotKeyGameService.NextHotKey();
                    UpdateForm();
                }
                if (wholeSetCorrect)
                {
                    Debug.WriteLine("The whole hotkey set is correct woohoo!");
                    MyHotKeyGameService.NextHotKey();
                    UpdateForm();
                }
            }
            */
        }
        public void KeyUp(string keyName)
        {
            if (_currentlyPressedKeys.ContainsKey(keyName))
            {
                Debug.WriteLine("KeyUp: " + keyName);
                Debug.WriteLine("- _currentlyPressedKeys.Keys: " + String.Join("+", _currentlyPressedKeys.Keys));
                _userInputSteps.Add(String.Join("+", _currentlyPressedKeys.Keys));
                //_userInputSteps.SetValue(String.Join("+", _currentlyPressedKeys.Keys), _userInputSteps.Count());
                _currentlyPressedKeys.Remove(keyName);

                CheckForProgressOrFail();
            }
        }

        public void CheckForProgressOrFail()
        {
            Debug.WriteLine("CheckForProgressOrFail");
            Debug.WriteLine($"- _userInputSteps({_userInputSteps.Count()}): " + String.Join(",", _userInputSteps));


            HotKey myHotKey = _gameHotKeys[_currHotKey];
            List<List<List<string>>> hotKeySolutions = myHotKey.Solutions;

            hotKeySolutions.ForEach(hkSolution => { // for each possible solution
                _userInputSteps.ForEach(userInputStep => { // for each user input step (key/key-combination)
                    List<string> userInputStepArray = userInputStep.Split("+").ToList();
                    int solutionStepCount = 0;
                    hkSolution.ForEach(hkSolutionStep => { // for each step (key/key-combination) of this solution
                        hkSolutionStep.ForEach(hkSolutionSubStep => { // for each substep (key / part of key-combination) of this solution
                            if(solutionStepCount < userInputStepArray.Count()) {
                                if (userInputStepArray[solutionStepCount] == hkSolutionSubStep) {
                                    Debug.WriteLine("substeps matching!");
                                }
                            }
                        });
                        
                        solutionStepCount++;
                    });
                });
            });
            /*
            int userInputStepCount = 0;
            _userInputSteps.ToList().ForEach(userInputStep => { // for each user input step
                
                Array solutionStepArray = hkSolutionSteps.ElementAt(userInputStepCount);

                Debug.WriteLine("userInputStepArray: " + String.Join(",",userInputStepArray));
                Debug.WriteLine("solutionStepArray: " + String.Join(",", solutionStepArray));

                if (userInputStepArray.Length <= solutionStepArray.Length)
                {

                } else
                {
                    // more input steps vs steps in this solution, fail
                }
                userInputStepCount++;
            });
            */

        }

        public bool CheckAgainstExpectedKey(string keyInput, out bool completedWholeSet, out bool currStepIsStringButIncomplete)
        {
            HotKey myHotKey = _gameHotKeys[_currHotKey];
            List<List<List<string>>> hotKeySteps = myHotKey.Solutions;
            completedWholeSet = false;
            currStepIsStringButIncomplete = false;

            /*
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
            myHotKey.Attempt++;
            _mistakes++;
            */
            return false;
        }

        public string CurrHotKey_CategoryName()
        {
            return _gameHotKeys[_currHotKey].Category;

        }
        public string CurrHotKey_Description()
        {
            return _gameHotKeys[_currHotKey].Description;
        }
        public int[] GetCurrHotKeyAndCount()
        {
            return new int[2] { _currHotKey, _gameHotKeys.Count() };
        }
        public int[] GetCurrHotKeyStepAndCount()
        {
            HotKey myHotKey = _gameHotKeys[_currHotKey];
            List<List<List<string>>> hotKeySteps = myHotKey.Solutions;
            return new int[2] { _currHotKeyStep, hotKeySteps.Count() };
        }
        public int GetCurrHotKeyAttempt()
        {
            return _gameHotKeys[_currHotKey].Attempt;
        }
        public void NextHotKey()
        {
            _currHotKeyStep = 0;
            _testString = "";
            if (!_dealingWithFails && _currHotKey < _gameHotKeys.Count() - 1)
            {
                _currHotKey++;
            }
            else
            {
                Debug.WriteLine("we're done! lets see if we need to backtrack!");
                int firstFailedIndex = _gameHotKeys.FindIndex(h => h.Failed == true);

                if ((firstFailedIndex >= 0) && (firstFailedIndex <= _gameHotKeys.Count() - 1))
                {
                    Debug.WriteLine("oh crap we have ourselves a fail! backtracking!");
                    _dealingWithFails = true;
                    _currHotKey = firstFailedIndex;
                    _gameHotKeys[firstFailedIndex].Failed = false;
                }
                else
                {
                    Debug.WriteLine("f- yeah we're really done! (show gameover screen with stats etc)");
                }
            }
        }
    }
}
