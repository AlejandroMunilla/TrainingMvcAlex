using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using TrainingAlex.Helpers;

namespace TrainingAlex.Models
{
    public class HomeModel
    {
        public string? Alejandro { get; private set; } = "batman";
        public string? Welcome { get; private set; }

        public List<string>? HomeList { get; private set; }

        private string[] homeArray = new string[]
        {
            "Home1",
            "Home2",
            "Home3"
        };

        public HomeModel() 
        {
            //Debug.WriteLine("Model !!!!!!!!!!!!!!!!!!!!!!!!!!1");
            Alejandro = "ALEJANDRO MUNILLA";
            Dictionary<string, string> dictionary = DictionaryHelper.Instance.CurrentDictionary;

            Welcome = CheckIfKeyExist("Welcome", dictionary);
            HomeList = new List<string>();
            foreach (string st in homeArray)
            {
                HomeList.Add(CheckIfKeyExist(st, dictionary));
            }  

        }

        private string CheckIfKeyExist (string key, Dictionary<string, string> dictionary)
        {
            string? value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            return string.Empty;
        }
    }
}
