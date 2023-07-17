using Microsoft.AspNetCore.Mvc;
using MusicPlatform.CommonUtils.Attributes;

namespace MusicPlatform.Controllers
{
    [Auth(true)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
