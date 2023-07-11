using Microsoft.AspNetCore.Mvc;

namespace MusicPlatform.Controllers
{
    public class SongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
