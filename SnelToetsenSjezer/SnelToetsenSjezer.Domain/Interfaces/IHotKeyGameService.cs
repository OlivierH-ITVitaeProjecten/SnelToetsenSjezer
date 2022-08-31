using SnelToetsenSjezer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeyGameService
    {
        abstract void SetHotKeys(List<HotKey> hotKeys);
        abstract void SetGameStateUpdatedCallback(Action<string> callback);
        abstract void SetGameTimerCallback(Action<int> callback);
        abstract void StartGame();
        abstract void GameTimer_Tick(object sender, EventArgs e);
        abstract void StopGame();
        abstract void KeyDown(string keyName);
        abstract void KeyUp(string keyName);
        abstract bool CheckAgainstExpectedKey(string keyInput, out bool completedWholeSet, out bool currStepIsStringButIncomplete);
        abstract string CurrHotKey_CategoryName();
        abstract string CurrHotKey_Description();
        abstract int[] GetCurrHotKeyAndCount();
        abstract int[] GetCurrHotKeyStepAndCount();
        abstract int GetCurrHotKeyAttempt();
        abstract void NextHotKey();
    }
}
