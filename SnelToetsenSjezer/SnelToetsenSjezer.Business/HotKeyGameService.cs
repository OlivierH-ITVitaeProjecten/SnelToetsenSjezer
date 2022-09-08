using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using System.Diagnostics;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyGameService : IHotKeyGameService
    {
        private List<HotKey> _gameHotKeys = new() { };
        private static Dictionary<string, bool> _currentlyPressedKeys = new Dictionary<string, bool>();

        private Action<string, Dictionary<string, string>> gameStateUpdatedCallback = null;
        private Action<int, bool> gameTimerCallback = null;

        private static readonly System.Windows.Forms.Timer _gameTimer = new System.Windows.Forms.Timer();
        private static int _gameSeconds = 0;

        private static bool _isPaused = false;
        private static int _pauseDurationDefault = 3;
        private static int _pauseDuration = 0;

        private List<List<string>> _userInputSteps = new List<List<string>>();

        private int _currHotKey = 0;
        private bool _dealingWithFails = false;

        public void SetHotKeys(List<HotKey> hotKeys)
        {
            _gameHotKeys = hotKeys;
        }

        public void SetGameStateUpdatedCallback(Action<string, Dictionary<string, string>> callback)
        {
            gameStateUpdatedCallback = callback;
        }
        public void SetGameTimerCallback(Action<int, bool> callback)
        {
            gameTimerCallback = callback;
        }

        public void StartGame()
        {
            Debug.WriteLine("Starting game!");
            _gameTimer.Interval = 1000;
            _gameTimer.Tick += new EventHandler(GameTimer_Tick);
            _gameTimer.Start();

            Dictionary<string, string> myStatusDictionary = new Dictionary<string, string>()
            {
                { "index", "1" },
                { "count", _gameHotKeys.Count().ToString() },
                { "category", _gameHotKeys[_currHotKey].Category },
                { "description", _gameHotKeys[_currHotKey].Description }
            };

            gameStateUpdatedCallback("playing", myStatusDictionary);
        }
        public void StopGame()
        {
            Debug.WriteLine("Stopping game!");
            _gameTimer.Stop();
            _currHotKey = 0;
            gameStateUpdatedCallback("finished", new Dictionary<string, string>());
        }

        public void PauseGame()
        {
            Debug.WriteLine("Pausing game!");
            _isPaused = true;
            _pauseDuration = _pauseDurationDefault;
        }

        public void ResumeGame()
        {
            Debug.WriteLine("Resuming game!");
            _isPaused = false;
            NextHotKey();
        }

        public void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!_isPaused)
            {
                _gameSeconds++;
                _gameHotKeys[_currHotKey].Seconds++;
            }
            else
            {
                if (_pauseDuration > 0)
                {
                    _pauseDuration--;
                }
                else
                {
                    ResumeGame();
                }
            }
            gameTimerCallback(_gameSeconds, _isPaused);
        }

        public void KeyDown(string keyName)
        {
            if (!_currentlyPressedKeys.ContainsKey(keyName) || !_currentlyPressedKeys[keyName])
            {
                Debug.WriteLine("KeyDown: " + keyName);
                _currentlyPressedKeys[keyName] = true;
            }
        }
        public void KeyUp(string keyName)
        {
            if (_currentlyPressedKeys.ContainsKey(keyName))
            {
                Debug.WriteLine("KeyUp: " + keyName);
                Debug.WriteLine("- _currentlyPressedKeys.Keys: " + String.Join("+", _currentlyPressedKeys.Keys));

                bool dontAdd = false;
                int containsKeyCount = 0;
                if (_userInputSteps.Count() > 0)
                {
                    List<string> previousStep = _userInputSteps[_userInputSteps.Count() - 1];
                    _currentlyPressedKeys.Keys.ToList().ForEach(key =>
                    {
                        if (previousStep.Contains(key) && previousStep.Count() > 1) containsKeyCount++;
                    });
                    if (containsKeyCount == _currentlyPressedKeys.Keys.Count())
                    {
                        dontAdd = true;
                    }
                }
                if (!dontAdd) _userInputSteps.Add(_currentlyPressedKeys.Keys.ToList());
                _currentlyPressedKeys.Remove(keyName);

                CheckForProgressOrFail();
            }
        }

        public void CheckForProgressOrFail()
        {
            HotKey myHotKey = _gameHotKeys[_currHotKey];
            List<List<List<string>>> hotKeySolutions = myHotKey.Solutions;

            bool hasAnyMatches = false;

            hotKeySolutions.ForEach(hkSolution =>
            {
                int hkSolutionStepIndex = 0;
                double matchingKeys = 0;

                hkSolution.ForEach(hkSolutionStep =>
                {
                    if (hkSolutionStepIndex <= _userInputSteps.Count() - 1)
                    {
                        if (hkSolutionStep.SequenceEqual(_userInputSteps[hkSolutionStepIndex]))
                        {
                            hasAnyMatches = true;
                            matchingKeys++;
                        }
                    }
                    hkSolutionStepIndex++;
                });
                if (matchingKeys == hkSolution.Count())
                {
                    HotKeyIsCorrect();
                }
            });
            if (!hasAnyMatches)
            {
                HotKeyIsFailed();
            }
        }

        public void HotKeyIsCorrect()
        {
            gameStateUpdatedCallback("correct", new Dictionary<string, string>() { });
            _gameHotKeys[_currHotKey].Failed = false;
            PauseGame();
        }
        public void HotKeyIsFailed()
        {
            _gameHotKeys[_currHotKey].Failed = true;

            string hotKeySolutionStr = "";
            List<List<List<string>>> hotKeySolutions = _gameHotKeys[_currHotKey].Solutions;
            hotKeySolutions[0].ForEach(solutionStep =>
            {
                if (hotKeySolutionStr != "") hotKeySolutionStr += ",";
                hotKeySolutionStr += String.Join("+", solutionStep);
            });

            Dictionary<string, string> myStatusDictionary = new Dictionary<string, string>()
            {
                { "solution", hotKeySolutionStr }
            };
            gameStateUpdatedCallback("failed", myStatusDictionary);
            PauseGame();
        }

        public void NextHotKey()
        {
            _userInputSteps = new List<List<string>>();
            _currentlyPressedKeys = new Dictionary<string, bool>();
            bool finished = false;

            if (!_dealingWithFails && _currHotKey < _gameHotKeys.Count() - 1)
            {
                _currHotKey++;
            }
            else
            {
                int failsCount = _gameHotKeys.Where(hk => hk.Failed == true).ToList().Count();
                if (failsCount > 0)
                {
                    if (!_dealingWithFails) _dealingWithFails = true;

                    for (int i = 0; i < _gameHotKeys.Count(); i++)
                    {
                        if (_gameHotKeys[i].Failed && (i != _currHotKey))
                        {
                            _currHotKey = i;
                            _gameHotKeys[i].Attempt += 1;
                            break;
                        }
                    }
                }
                else
                {
                    finished = true;
                    StopGame();
                }
            }
            if (!finished)
            {
                Dictionary<string, string> myStatusDictionary = new Dictionary<string, string>()
                {
                    { "index", (_currHotKey+1).ToString() },
                    { "count", _gameHotKeys.Count().ToString() },
                    { "attempt", _gameHotKeys[_currHotKey].Attempt.ToString() },
                    { "category", _gameHotKeys[_currHotKey].Category },
                    { "description", _gameHotKeys[_currHotKey].Description }
                };
                gameStateUpdatedCallback("playing", myStatusDictionary);
            }
        }
        public List<HotKey> GetGameHotKeys()
        {
            return _gameHotKeys;
        }
        public int GetGameDuration()
        {
            return _gameSeconds;
        }
    }
}
