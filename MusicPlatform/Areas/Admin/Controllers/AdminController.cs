using Microsoft.AspNetCore.Mvc;
using MusicPlatform.CommonUtils.Attributes;

namespace MusicPlatform.Areas.Admin.Controllers
{
    [Auth(true)]
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
