using Aspose.Cells;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using TrainingAlex.Containers;

namespace TrainingAlex.Helpers
{
    public class DictionaryHelper
    {
        public static DictionaryHelper Instance = new DictionaryHelper();
        public DictionariesContainer? DictionariesContainer { get; private set; }
        public Dictionary<string, string> CurrentDictionary = new Dictionary<string, string>();
        //public string CurrentLan = "en";
        public string? ContentRoot;
        public XmlDocument? DictionaryXmlDoc { get; private set; }

        public enum AvailableLanguages
        {
            en,
            es
        }
        public AvailableLanguages CurrentLanguage { get; private set; }

        public DictionaryHelper() 
        {
            Instance = this;
        }

        public void Start (string contentRoot)
        {
            Debug.WriteLine("DictionaryHelper called");
            ContentRoot = contentRoot;
            DictionariesContainer = new DictionariesContainer();
            DictionaryXmlDoc = GetXmlDoc(contentRoot);
            SetUpCurrentDictionary(AvailableLanguages.en);

            if (CurrentDictionary != null)
            {
                for (int i = CurrentDictionary.Count - 1; i >= 0; i--)
                {
                    var item = CurrentDictionary.ElementAt(i);
                    var itemKey = item.Key;
                    var itemValue = item.Value;
                    Debug.WriteLine("Final: " + item + "/" + itemKey + "/" + itemValue);
                }
            }
        }


        public XmlDocument? GetXmlDoc(string contentRoot)
        {
            ContentRoot= contentRoot;
            if (contentRoot == null)
            {
                return null;
            }

            string pathToXlsx = Path.Combine(contentRoot, @"Res\DictionaryWebsite.xlsx");

            Workbook workbook = new Workbook(pathToXlsx);
            string pathToXml = Path.Combine(contentRoot, @"Res\Workbook.xml");
            workbook.Save(pathToXml);

            XmlSaveOptions xmlSaveOptions = new XmlSaveOptions();
            string pathToXmlFormatted = Path.Combine(contentRoot, @"Res\data.xml");
            workbook.Save(pathToXmlFormatted, xmlSaveOptions);
            workbook.Dispose();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathToXmlFormatted);

            DictionaryXmlDoc = xmlDoc;
            return DictionaryXmlDoc;
        }

        private XmlDocument? GetXmlDoc()
        {
            if (ContentRoot == null) throw new Exception("DictionaryHelper. ContentRoot is null");
            return GetXmlDoc(ContentRoot);
        }


        public Dictionary<string, string> SetUpCurrentDictionary (string changeLanguateTo) 
        {
            switch (changeLanguateTo)
            {
                case ("es"):
                    return SetUpCurrentDictionary(AvailableLanguages.es);

                case ("en"):
                default:
                    return SetUpCurrentDictionary(AvailableLanguages.en);                   
            }

        }

 
        public Dictionary<string, string> SetUpCurrentDictionary (AvailableLanguages changeLanguageTo)
        {
            CurrentLanguage = changeLanguageTo;
            Debug.WriteLine("SetUpCurrentDictionary: " + changeLanguageTo.ToString());

            if (DictionaryXmlDoc == null) throw new Exception("SetUpCurrentDictionary.DictionaryXmlDoc is null");

            switch (changeLanguageTo)
            {
                case (AvailableLanguages.es):

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    DictionaryExistOrCreateIt(DictionariesContainer.EsDictionary, changeLanguageTo, DictionaryXmlDoc);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    DictionariesContainer.EsDictionary = CurrentDictionary;
                    break;

                case (AvailableLanguages.en):
                default:
                    Debug.WriteLine("case En");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    
                    DictionaryExistOrCreateIt(DictionariesContainer.EnDictionary, changeLanguageTo, DictionaryXmlDoc);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    DictionariesContainer.EnDictionary = CurrentDictionary;
                    break;
            }

            return CurrentDictionary;
        }

        private bool DictionaryExistOrCreateIt(Dictionary<string, string>? dictionaryToCheck, AvailableLanguages languageChosen, XmlDocument xmlDocument)
        {
            //Debug.WriteLine("Check Dictionary Exist");
            //if (dictionaryToCheck != null && dictionaryToCheck.Count > 0)
            //{
            //    Debug.WriteLine("Check True");

            //    return true;
            //}

            //Debug.WriteLine("Check False");
            try
            {
                CreateSpecificDictionaries(xmlDocument, languageChosen);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        private Dictionary<string, string> CreateSpecificDictionaries(XmlDocument xmlDocument, AvailableLanguages chosenLanguage)
        {
            CurrentDictionary.Clear();
            //Debug.WriteLine("Called CreateDictionaries");
            XmlNode? rootNode = xmlDocument.SelectSingleNode("root");

            if (rootNode == null) throw new Exception("Root Node CrateAllDictionaries is null");

            XmlNodeList? tabNodes = rootNode.SelectNodes("worksheet");
  
            if (tabNodes == null) throw new Exception("Tab nodes CrateAllDictionaries is null");

            //Debug.WriteLine(tabNodes.Count);
            foreach (XmlNode tab in tabNodes)
            {
                if (tab == null || tab.Attributes == null || tab.Attributes["name"] == null) throw new Exception("Tab nodes CrateAllDictionaries is null");

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (tab.Attributes["name"].Value != null && tab.Attributes["name"].Value == "Dictionary")
                {
                    if (tab.SelectNodes("Row") == null) throw new Exception("Tab Row nodes CrateAllDictionaries is null");

                    XmlNodeList? rowNodes = tab.SelectNodes("Row");

                    string language = chosenLanguage.ToString();
                    foreach (XmlNode row in rowNodes)
                    {

                         CurrentDictionary.Add(row.SelectSingleNode("Key").InnerText, row.SelectSingleNode(language).InnerText);

                        //Debug.WriteLine(row.SelectSingleNode("Key").InnerText + "/" + row.SelectSingleNode("en").InnerText + "/" + row.SelectSingleNode("es").InnerText);
                        ////dictionariesContainer.EnDictionary.Add(row.SelectSingleNode("Key").InnerText, row.SelectSingleNode("en").InnerText);
                        //dictionariesContainer.EsDictionary.Add(row.SelectSingleNode("Key").InnerText, row.SelectSingleNode("es").InnerText);
                    }
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return CurrentDictionary;
        }

        public string CheckIfKeyExist(string key)
        {
            string? value;
            if (CurrentDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            return string.Empty;
        }

    }
}
