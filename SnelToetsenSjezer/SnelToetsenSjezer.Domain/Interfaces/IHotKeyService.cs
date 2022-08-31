using SnelToetsenSjezer.Domain.Models;

namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeyService
    {
        abstract void AddHotKey(string category, string description, string hotKeys);
        abstract void ProcessHotkeysXmlFile(string filePath);
        abstract List<string> GetCategories();
        abstract List<HotKey> GetAllHotKeys();
        abstract List<HotKey> GetHotKeysInCategory(string category);
        abstract List<HotKey> GetHotKeysInCategories(List<string> categories);
    }
}
