using SnelToetsenSjezer.Domain.Models;

namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeyGameService
    {
        void SetHotKeys(List<HotKey> hotKeys);
        void SetGameStateUpdatedCallback(Action<string, Dictionary<string, string>> callback);
        void SetGameTimerCallback(Action<int, bool> callback);
        void StartGame();
        void StopGame();
        void PauseGame();
        void ResumeGame();
        void GameTimer_Tick(object sender, EventArgs e);
        void KeyDown(string keyName);
        void KeyUp(string keyName);
        void CheckForProgressOrFail();
        void HotKeyIsCorrect();
        void HotKeyIsFailed();
        void NextHotKey();
        List<HotKey> GetGameHotKeys();
        int GetGameDuration();
    }
}
