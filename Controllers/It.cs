using Microsoft.AspNetCore.Mvc;

namespace TrainingAlex.Controllers
{
    public class It : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
