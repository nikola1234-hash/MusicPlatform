using Microsoft.AspNetCore.Mvc;

namespace MusicPlatform.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
