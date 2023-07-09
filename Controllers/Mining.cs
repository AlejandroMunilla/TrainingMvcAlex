using Microsoft.AspNetCore.Mvc;
using TrainingAlex.Helpers;
using TrainingAlex.Models;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Diagnostics;
using MongoDB.Bson.IO;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace TrainingAlex.Controllers
{
    public class Mining : BaseController
    {
        public IActionResult Index()
        {
            var miningModels = GetMiningModels();
            return View(miningModels);
        }

        public  MiningViewModel GetMiningModels ()
        {
            var miningViewModels = new MiningViewModel();
            var dictionaryHelper = DictionaryHelper.Instance;
            List<MiningModel> models = new List<MiningModel> ();

            MongoDbHelper dbHelper = new MongoDbHelper ();

            bool mongoDbWorking = false;
            if (dbHelper.dbClient != null && dbHelper.dbClient.GetDatabase("Projects") != null)
            {
                try
                {
                    var dbClient = dbHelper.dbClient;
                    var db = dbClient.GetDatabase("Projects");
                    //var collection = db.GetCollection<MiningModel>("Projects");
                    var collection = db.GetCollection<MiningModel>("Mining");

                    ////var documents = collection.Find(f => true).ToListAsync();
                    //var filter = Builders<MiningModel>.Filter.Empty;
                    //var allRestaurants = collection.Find(filter);

                    var query =
                        from e in collection.AsQueryable<MiningModel>()
                        select e;

                    foreach (var e in query)
                    {
                        Debug.WriteLine(e.Company.ToString());
                        var model = new MiningModel();
                        model.Project = e.Project;
                        model.Company = e.Company;
                        model.Year = e.Year;
                        models.Add(model);

                    }

                    miningViewModels.ListMiningModel = models;
                    mongoDbWorking = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            //If no Db available, Populate with data from json, this is an app for training purposes who might be download by anyone, to show model view is working 
            if (mongoDbWorking == false) 
            {
                models = GetJson();
                miningViewModels.ListMiningModel = models;
            }

            miningViewModels.Project = dictionaryHelper.CheckIfKeyExist("Project");
            miningViewModels.Company = dictionaryHelper.CheckIfKeyExist("Company");
            miningViewModels.Year = dictionaryHelper.CheckIfKeyExist("Year");
            miningViewModels.SomeExperience = dictionaryHelper.CheckIfKeyExist("SomeExperience");

            return miningViewModels;
        }

        private List<MiningModel> GetJson()
        {
            if (StartUp.Instance == null) throw new Exception("Startup Configuration is null");

            IConfiguration? configuration = StartUp.Instance.Configuration;
            var contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey) + "wwwroot";
            string pathToJson = Path.Combine(contentRoot, @"Res\ProjectMiningDb.txt");

            using (StreamReader r = new StreamReader(pathToJson))
            {
                string json = r.ReadToEnd();
                Debug.WriteLine(json.ToString());

                var models = JsonConvert.DeserializeObject<List<MiningModel>>(json);

                foreach (MiningModel miningModel in models)
                {
                    Debug.WriteLine(miningModel.Project);
                }

                return models;
            }
        }
    }
}
