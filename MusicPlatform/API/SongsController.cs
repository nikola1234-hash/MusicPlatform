using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.Models.API.SongsModels;

namespace MusicPlatform.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<SongsController> _logger;

        public SongsController(AppDbContext dbContext, ILogger<SongsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET: api/Songs
        [HttpPost("check")]
        public IActionResult Get(FavoritesModel model)
        {

            if (string.IsNullOrEmpty(model.Id) || string.IsNullOrEmpty(model.UserId))
            {
                return BadRequest();
            }

            var songParsed = Guid.TryParse(model.Id, out Guid songId);
            var userParsed = Guid.TryParse(model.UserId, out Guid userIdParsed);

            if (!songParsed || !userParsed)
            {
                return BadRequest();
            }



            var song = _dbContext.Songs.FirstOrDefault(s => s.Id == songId);
            if (song is null)
            {
                return BadRequest();
            }
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdParsed);
            if (user is null)
            {
                return BadRequest();
            }
            var favoriteAlreadyExists = _dbContext.Favorites.Any(f => f.Song == song && f.User == user);
            if (favoriteAlreadyExists)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddToFavorites(FavoritesModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Id) || string.IsNullOrEmpty(model.UserId))
                {
                    return BadRequest();
                }

                var songParsed = Guid.TryParse(model.Id, out Guid songId);
                var userParsed = Guid.TryParse(model.UserId, out Guid userIdParsed);

                if (!songParsed || !userParsed)
                {
                    return BadRequest();
                }



                var song = _dbContext.Songs.FirstOrDefault(s => s.Id == songId);
                if (song is null)
                {
                    return BadRequest();
                }
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdParsed);
                if (user is null)
                {
                    return BadRequest();
                }
                var favoriteAlreadyExists = _dbContext.Favorites.Any(f => f.Song == song && f.User == user);
                if (favoriteAlreadyExists)
                {
                    return BadRequest();
                }

                var favorite = new Favorite
                {
                    Song = song,
                    User = user
                };
                _dbContext.Favorites.Add(favorite);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }



    }
}
