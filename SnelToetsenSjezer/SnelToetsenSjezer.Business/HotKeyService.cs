using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyService : IHotKeyService
    {
        private readonly List<HotKey> _allHotKeys = new() { };

        public void AddHotKey(string category, string description, string hotKeySteps)
        {
            Regex multiSolutionRegex = new Regex("\\[[^\\]]*]\\[[^\\]]*]");
            string[] solutions = hotKeySteps.Contains("[") ? multiSolutionRegex.Split(hotKeySteps) : new string[] { hotKeySteps };
            List<List<Array>> allValidSolutions = new List<List<Array>>();

            solutions.ToList().ForEach(hkSolution => {
                List<Array> newSteps = new List<Array>();
                string[] solutionSteps = hkSolution.Split(",");

                solutionSteps.ToList().ForEach(k => {
                    bool isCombination = hotKeySteps.Contains("+");
                    newSteps.Add(isCombination ? k.Split("+") : new string[] { k });
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
            _allHotKeys.GroupBy(hk => hk.Category).ToList().ForEach(hk_item => {
                Categories.Add(hk_item.ElementAt(1).Category);
            });
            return Categories;
        }

        public List<HotKey> GetAllHotKeys()
        {
            return _allHotKeys;
        }
        public List<HotKey> GetHotKeysInCategory(string category)
        {
            if(category.Length < 1) return new List<HotKey> { };
            return _allHotKeys.Where(hk => hk.Category == category).ToList();
        }
        public List<HotKey> GetHotKeysInCategories(List<string> categories)
        {
            if(categories.Count() < 1) return new List<HotKey> { };
            return _allHotKeys.Where(hk => categories.Contains(hk.Category)).ToList();
        }
    }
}