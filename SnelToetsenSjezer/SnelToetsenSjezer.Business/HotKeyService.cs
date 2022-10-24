using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using SnelToetsenSjezer.Domain.Types;
using System.Xml;

namespace SnelToetsenSjezer.Business
{
    public class HotKeyService : IHotKeyService
    {
        private readonly List<HotKey> _allHotKeys = new() { };

        public void AddHotKey(string category, string description, string hotKeySteps)
        {
            bool multipleSolutions = hotKeySteps.Contains("||");
            List<string> solutionsStrings = multipleSolutions ? hotKeySteps.Split("||").ToList() : new List<string>() { hotKeySteps };

            HotKeySolutions allSolutions = new HotKeySolutions();

            solutionsStrings.ToList().ForEach(solutionString =>
            {
                HotKeySolution newSolution = new HotKeySolution();
                List<string> solutionStrParts = solutionString.Split(",").ToList();

                solutionStrParts.ToList().ForEach(keyCombo =>
                {
                    HotKeySolutionStep newSolutionStep = new HotKeySolutionStep();
                    bool isCombination = hotKeySteps.Contains("+");

                    if (isCombination)
                    {
                        keyCombo.Split("+").ToList().ForEach(keyComboPart =>
                        {
                            newSolutionStep.Add(keyComboPart);
                        });
                    }
                    else
                    {
                        newSolutionStep.Add(keyCombo);
                    }

                    newSolution.Add(newSolutionStep);
                });

                allSolutions.Add(newSolution);
            });


            HotKey myNewHotKey = new HotKey()
            {
                Category = category,
                Description = description,
                Solutions = allSolutions
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