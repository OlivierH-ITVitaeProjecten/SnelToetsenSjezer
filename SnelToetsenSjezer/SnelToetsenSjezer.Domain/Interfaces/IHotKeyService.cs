namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeyService
    {
        abstract void AddHotKey(string category, string description, string hotKeys);
        abstract void ProcessHotkeysXmlFile(string filePath);
        abstract bool CheckAgainstExpectedKey(string keyInput, out bool completedWholeSet, out bool currStepIsStringButIncomplete);
        abstract string CurrHotKey_CategoryName();
        abstract string CurrHotKey_Description();
        abstract int[] GetCurrHotKeyAndCount();
        abstract int[] GetCurrHotKeyStepAndCount();
        abstract int GetCurrHotKeyFails();
        abstract void NextHotKey();
    }
}
