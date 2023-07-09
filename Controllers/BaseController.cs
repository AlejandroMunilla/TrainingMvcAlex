using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Console;
using System.Diagnostics;
using TrainingAlex.Helpers;
using TrainingAlex.Models;

namespace TrainingAlex.Controllers
{
    public class BaseController : Controller
    {
        public string? SimpleMessage { get; set; } 
        public void ChangeLanguage(string language, string action)
        {
            Debug.WriteLine("Language: " + language + "/Action " + action);
            if (!string.IsNullOrEmpty(language))
            {
               
                DictionaryHelper.Instance.SetUpCurrentDictionary(language);
                Response.Redirect("Index");
            }

        }

        public IActionResult DisplaySimpleMessage(string message)
        {
            SimpleMessageModel simpleMessage = new SimpleMessageModel();
            Debug.WriteLine("DisplauSimpleMessage function: " + message);
            simpleMessage.Message = message;
            return View(simpleMessage);
        }  
    }
}
