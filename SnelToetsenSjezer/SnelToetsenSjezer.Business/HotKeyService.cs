using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using System.Xml;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyService : IHotKeyService
    {
        private readonly List<HotKey> _allHotKeys = new() { };

        public void AddHotKey(string category, string description, string hotKeySteps)
        {
            List<string> solutions = hotKeySteps.Contains("||") ? hotKeySteps.Split("||").ToList() : new List<string>() { hotKeySteps };

            List<List<List<string>>> allValidSolutions = new List<List<List<string>>>();

            solutions.ToList().ForEach(hkSolution =>
            {
                List<List<string>> newSteps = new List<List<string>>();
                List<string> solutionSteps = hkSolution.Split(",").ToList();

                solutionSteps.ToList().ForEach(k =>
                {
                    bool isCombination = hotKeySteps.Contains("+");
                    List<string> mySubSteps = isCombination ? k.Split("+").ToList() : new List<string>() { k };

                    newSteps.Add(mySubSteps);
                });

                allValidSolutions.Add(newSteps);
            });


            HotKey myNewHotKey = new HotKey()
            {
                Category = category,
                Description = description,
                Solutions = allValidSolutions
            };
            _allHotKeys.Add(myNewHotKey);
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
        public List<string> GetCategories()
        {
            List<string> Categories = new List<string>();
            _allHotKeys.GroupBy(hk => hk.Category).ToList().ForEach(hk_item =>
            {
                Categories.Add(hk_item.ElementAt(0).Category);
            });
            return Categories;
        }

        public List<HotKey> GetAllHotKeys()
        {
            return _allHotKeys;
        }
        public List<HotKey> GetHotKeysInCategory(string category)
        {
            if (category.Length < 1) return new List<HotKey> { };
            return _allHotKeys.Where(hk => hk.Category == category).ToList();
        }
        public List<HotKey> GetHotKeysInCategories(List<string> categories)
        {
            if (categories.Count() < 1) return new List<HotKey> { };
            return _allHotKeys.Where(hk => categories.Contains(hk.Category)).ToList();
        }
    }
}