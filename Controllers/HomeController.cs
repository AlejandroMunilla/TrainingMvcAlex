using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Mime;
using System.Text.Json.Serialization;
using TrainingAlex.Containers;
using TrainingAlex.Helpers;
using TrainingAlex.Models;


namespace TrainingAlex.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string language)
        {
            //Debug.WriteLine("Language: " + language);
            //if (!string.IsNullOrEmpty(language))
            //{
            //    DictionaryHelper.Instance.SetUpCurrentDictionary(language);
            //}
            HomeModel model = new HomeModel();
            string output = JsonConvert.SerializeObject(model);

            ViewBag.Message = model;


            //return new JsonResult(output);
            return View("~/Views/Home/Index.cshtml");
        }

        protected void btnLanguage_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Event");
            Index("es");
        }


        //public void ChangeLanguage(string language)
        //{
        //    Debug.WriteLine("Language: " + language);
        //    if (string.IsNullOrEmpty(language))
        //    {
        //        DictionaryHelper.Instance.SetUpCurrentDictionary(language);
        //    }

        //    //return new JsonResult(output);
        //    Index();
        //}

        public IActionResult ItServices()
        {
            return View();
        }

        public IActionResult MiningServices()
        {
            return View();
        }

        public IActionResult Documentation()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}