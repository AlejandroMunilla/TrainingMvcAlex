using Aspose.Cells;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using TrainingAlex.Containers;
using TrainingAlex.Helpers;
using TrainingAlex.Models;

namespace TrainingAlex
{
    public class StartUp
    {
        public string? contentRoot;
        public static StartUp? Instance;
        public IConfiguration? Configuration;
        public DictionariesContainer? DictionariesContainer;
        public string Lan = "en";
        public MongoDbHelper MongoDbHelper = new MongoDbHelper();

        public void Start (IConfiguration configuration)
        {
            Instance = this;
            Configuration= configuration;

            Debug.WriteLine("Start up Called");
            contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey) + "wwwroot";
            Debug.WriteLine(contentRoot);
            DictionaryHelper dictionaryHelper = new DictionaryHelper();
            dictionaryHelper.Start(contentRoot);

            //for (int i = DictionariesContainer.esDictionary.Count - 1; i >= 0; i--)
            //{
            //    var item = DictionariesContainer.esDictionary.ElementAt(i);
            //    var itemKey = item.Key;
            //    var itemValue = item.Value;
            //    Debug.WriteLine (item+ "/" + itemKey + "/" + itemValue);
            //}
        }


    }
}
