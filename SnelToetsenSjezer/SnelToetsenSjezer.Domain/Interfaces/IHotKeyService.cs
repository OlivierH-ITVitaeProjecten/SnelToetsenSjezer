namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeyService
    {
        abstract void AddHotKey(string category, string description, string hotKeys);
        abstract void ProcessHotkeysXmlFile(string filePath);
        abstract bool CheckAgainstExpectedKey(string keyInput);
    }
}
