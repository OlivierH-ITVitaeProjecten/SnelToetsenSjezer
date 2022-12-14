using SnelToetsenSjezer.Domain.Models;
using SnelToetsenSjezer.Domain.Types;

namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeyGameService
    {
        bool GetAutoResume();
        bool GetKeyResume();
        void ConfigureGame(List<HotKey> hotKeys, bool autoResume, bool keyResume);
        void SetGameStateUpdatedCallback(Action<string, GameStateCallbackData> callback);
        void SetGameTimerCallback(Action<int, bool> callback);
        void StartGame();
        void StopGame(bool forceStop = false);
        void PauseGame(int duration);
        void ResumeGame();
        void GameTimer_Tick(object sender, EventArgs e);
        string GameTicksToTimeStr();
        string GameTicksToTimeStr(int gameTicks);
        void KeyDown(string keyName);
        void KeyUp(string keyName);
        void StringInput(string input);
        void CheckForProgressOrFail();
        void HotKeyIsCorrect();
        void HotKeyIsFailed();
        void NextHotKey();
        List<HotKey> GetGameHotKeys();
        int GetGameDuration();
    }
}
