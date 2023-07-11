using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Data;

namespace MusicPlatform.Controllers
{
    public class ArtistController : Controller
    {

        private readonly AppDbContext _dbContext;

        public ArtistController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            try
            {
                Guid.TryParse(id, out Guid artistId);
                var artist = _dbContext.Artists.Find(artistId);
                if (artist == null)
                {
                    return View("Error");
                }






            }
            catch (System.Exception)
            {

                throw;
            }


            return View();
        }
    }
}
