using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using SnelToetsenSjezer.Domain.Types;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyGameService : IHotKeyGameService
    {
        private List<HotKey> _gameHotKeys = new() { };
        private static PressedKeysDict _currentlyPressedKeys = new PressedKeysDict();

        private Action<string, GameStateCallbackData> gameStateUpdatedCallback = null;
        private Action<int, bool> gameTimerCallback = null;

        private static Timer? _gameTimer = null;
        private static int _gameTicks = 0;

        private static bool _isPaused = false;
        private static int _pauseDurationCorrect = 100;
        private static int _pauseDurationFailed = 250;
        private static int _pauseDuration = 0;

        private List<string> _userInputSteps = new List<string>();

        private int _currHotKey = 0;
        private bool _dealingWithFails = false;

        public void SetHotKeys(List<HotKey> hotKeys)
        {
            hotKeys.ForEach(hotKey =>
            {
                hotKey.ResetForNewGame();
            });
            _gameHotKeys = hotKeys;
        }

        public void SetGameStateUpdatedCallback(Action<string, GameStateCallbackData> callback)
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
            if (_gameTimer != null) _gameTimer.Dispose();
            _gameTicks = 0;
            _gameTimer = new Timer();
            _gameTimer.Interval = 10; // 10 = centiseconds | 100 = deciseconds | 1000 = seconds
            _gameTimer.Tick += new EventHandler(GameTimer_Tick);
            _gameTimer.Start();

            GameStateCallbackData stateData = new GameStateCallbackData()
            {
                { "index", "1" },
                { "count", _gameHotKeys.Count().ToString() },
                { "category", _gameHotKeys[_currHotKey].Category },
                { "description", _gameHotKeys[_currHotKey].Description }
            };

            if (IsExpectingString())
            {
                Debug.WriteLine("Telling GameForm that we expect a string!");
                stateData.Add("expecting_string", "1");
            }
            gameStateUpdatedCallback("playing", stateData);
        }
        public void StopGame(bool forceStop = false)
        {
            Debug.WriteLine("Stopping game!");
            _gameTimer!.Stop();

            _currHotKey = 0;
            _dealingWithFails = false;
            _userInputSteps = new List<string>();

            if (!forceStop) gameStateUpdatedCallback("finished", new GameStateCallbackData());
        }

        public void PauseGame(int duration)
        {
            Debug.WriteLine("Pausing game!");
            _isPaused = true;
            _pauseDuration = duration;
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
                _gameTicks++;
                _gameHotKeys[_currHotKey].Duration++;
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
            gameTimerCallback(_gameTicks, _isPaused);
        }
        public string GameTicksToTimeStr()
        {
            return GameTicksToTimeStr(_gameTicks);
        }
        public string GameTicksToTimeStr(int gameTicks)
        {
            int seconds = gameTicks / 50; // Delen door 100 is te traag??? hoe dan???
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string timeStr = time.ToString((seconds > 3600) ? @"hh\:mm\:ss" : @"mm\:ss");

            int centiseconds = (gameTicks % 100);
            string centiStr = $"{centiseconds}".Length < 2 ? "0" + centiseconds : $"{centiseconds}";

            return timeStr + "." + centiStr;
        }

        public void KeyDown(string keyName)
        {
            if (!_isPaused && (!_currentlyPressedKeys.ContainsKey(keyName) || !_currentlyPressedKeys[keyName]))
            {
                Debug.WriteLine("KeyDown: " + keyName);
                _currentlyPressedKeys[keyName] = true;

                gameStateUpdatedCallback("userinputsteps", new GameStateCallbackData() {
                    { "userinputsteps", GetUserInputSteps(true) }
                });
            }
        }
        public void KeyUp(string keyName)
        {
            if (!_isPaused && (_currentlyPressedKeys.ContainsKey(keyName) || keyName == "F12"))
            {
                Debug.WriteLine("KeyUp: " + keyName);
                Debug.WriteLine("- _currentlyPressedKeys.Keys: " + String.Join("+", _currentlyPressedKeys.Keys));

                _userInputSteps.Add(String.Join("+", _currentlyPressedKeys.Keys.ToList()));
                _currentlyPressedKeys.Remove(keyName);

                // edge-case voor Nvidia GeForce Experience software die bij Alt+F12
                // het F12 keyDown event niet door blijkt te geven
                if (_userInputSteps.Count() == 1 && _userInputSteps[0] == "Menu" && keyName == "F12")
                {
                    Debug.WriteLine("!!! Nvidia Geforce Experience edge-case is in effect !!!");
                    _userInputSteps[0] = "Menu+F12";
                }

                CheckForProgressOrFail();
            }
        }
        public void StringInput(string input)
        {
            if (IsExpectingString())
            {
                HotKeySolution solution = _gameHotKeys[_currHotKey].Solutions[0];
                string expectedInput = solution[solution.correctSteps].ToString();

                Debug.WriteLine($"StringInput - input: {input} | expectedInput: {expectedInput}");

                if ($"'{input}'" == expectedInput)
                {
                    _userInputSteps.Add($"'{input}'");
                    gameStateUpdatedCallback("userinputsteps", new GameStateCallbackData() {
                        { "userinputsteps", GetUserInputSteps(true) },
                        { "expecting_string", "0" }
                    });

                    CheckForProgressOrFail();
                }
            }
        }

        public string GetUserInputSteps(bool includeCurrentlyPressed = false)
        {
            string inputSteps = "";
            _userInputSteps.ToList().ForEach(step =>
            {
                inputSteps += inputSteps.Length > 0 ? "," + step : step;
            });
            if (includeCurrentlyPressed)
            {
                string currentlyPressed = String.Join("+", _currentlyPressedKeys.Keys);
                inputSteps += inputSteps.Length > 0 ? "," + currentlyPressed : currentlyPressed;
            }

            return inputSteps;
        }

        public bool IsExpectingString()
        {
            bool isExpectingString = false;
            HotKeySolutions currHotKeySolutions = _gameHotKeys[_currHotKey].Solutions;

            if (currHotKeySolutions.Count() == 1)
            {
                HotKeySolution solution = _gameHotKeys[_currHotKey].Solutions[0];
                isExpectingString = solution.IsNextStepAString();
            }

            return isExpectingString;
        }

        public void CheckForProgressOrFail()
        {
            Debug.WriteLine("- _userInputSteps: " + GetUserInputSteps());

            HotKeySolutions currHotKeySolutions = _gameHotKeys[_currHotKey].Solutions;

            int failedSolutions = 0;

            currHotKeySolutions.ForEach(solution =>
            {
                int completedSteps = 0;
                bool failedSolution = false;
                int solutionStepIndex = 0;

                solution.ForEach(solutionStep =>
                {
                    if (solutionStepIndex <= _userInputSteps.Count() - 1)
                    {
                        if (solutionStep.CheckForCompletion(_userInputSteps[solutionStepIndex]))
                        {
                            completedSteps++;
                            solution.correctSteps = completedSteps;
                        }
                        else if (solutionStep.CheckForFail(_userInputSteps[solutionStepIndex]))
                        {
                            failedSolution = true;
                        }
                    }
                    solutionStepIndex++;
                });

                if (solution.IsNextStepAString())
                {
                    _currentlyPressedKeys = new PressedKeysDict();
                    GameStateCallbackData stateData = new GameStateCallbackData()
                    {
                        { "expecting_string", "1" }
                    };
                    gameStateUpdatedCallback("userinputsteps", stateData);
                }

                if (completedSteps == solution.Count())
                {
                    HotKeyIsCorrect();
                }
                if (failedSolution)
                {
                    solution.correctSteps = 0;
                    failedSolutions++;
                }
            });
            if (failedSolutions == currHotKeySolutions.Count())
            {
                HotKeyIsFailed();
            }
        }

        public void HotKeyIsCorrect()
        {
            _gameHotKeys[_currHotKey].Failed = false;
            _gameHotKeys[_currHotKey].Completed = true;

            GameStateCallbackData stateData = new GameStateCallbackData() {
                { "userinputsteps", GetUserInputSteps() }
            };
            int numberOfIncomplete = _gameHotKeys.ToList().Where(h => h.Completed == false).Count();
            if (numberOfIncomplete == 0)
            {
                stateData.Add("game_completed", "1");
            }
            gameStateUpdatedCallback("correct", stateData);

            PauseGame(_pauseDurationCorrect);
        }
        public void HotKeyIsFailed()
        {
            _gameHotKeys[_currHotKey].Failed = true;
            _gameHotKeys[_currHotKey].Completed = false;

            string hotKeySolutionStr = "";
            HotKeySolutions hotKeySolutions = _gameHotKeys[_currHotKey].Solutions;
            hotKeySolutions[0].ToList().ForEach(solutionStep =>
            {
                if (hotKeySolutionStr != "") hotKeySolutionStr += ",";
                hotKeySolutionStr += String.Join("+", solutionStep);
            });

            GameStateCallbackData stateData = new GameStateCallbackData()
            {
                { "solution", hotKeySolutionStr },
                { "userinputsteps", GetUserInputSteps() }
            };
            gameStateUpdatedCallback("failed", stateData);
            PauseGame(_pauseDurationFailed);
        }

        public void NextHotKey()
        {
            _userInputSteps = new List<string>();
            _currentlyPressedKeys = new PressedKeysDict();
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
                GameStateCallbackData stateData = new GameStateCallbackData()
                {
                    { "index", (_currHotKey+1).ToString() },
                    { "count", _gameHotKeys.Count().ToString() },
                    { "attempt", _gameHotKeys[_currHotKey].Attempt.ToString() },
                    { "category", _gameHotKeys[_currHotKey].Category },
                    { "description", _gameHotKeys[_currHotKey].Description },
                    { "userinputsteps", "" }
                };

                if (IsExpectingString())
                {
                    Debug.WriteLine("Telling GameForm that we expect a string!");
                    stateData.Add("expecting_string", "1");
                }
                else
                {
                    stateData.Add("expecting_string", "0");
                }

                gameStateUpdatedCallback("playing", stateData);
            }
        }
        public List<HotKey> GetGameHotKeys()
        {
            return _gameHotKeys;
        }
        public int GetGameDuration()
        {
            return _gameTicks;
        }
    }
}
