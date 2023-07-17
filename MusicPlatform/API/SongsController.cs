using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.DTO;
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

        [HttpPost]// or remove
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
                    _dbContext.Favorites.RemoveRange(_dbContext.Favorites.Where(f => f.Song == song && f.User == user));
                    _dbContext.SaveChanges();
                    return Ok();
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
        [HttpPost("comment")]
        public IActionResult AddComment(CommentModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var userParsed = Guid.TryParse(model.UserId, out Guid userIdParsed);
                if (!userParsed)
                {
                    return BadRequest();
                }
                var songParsed = Guid.TryParse(model.SongId, out Guid songIdParsed);
                if (!songParsed)
                {
                    return BadRequest();
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userIdParsed);
                if (user is null)
                {
                    return BadRequest();
                }
                var song = _dbContext.Songs.FirstOrDefault(s => s.Id == songIdParsed);
                if (song is null)
                {
                    return BadRequest();
                }
                var commentExists = _dbContext.Comments.Any(c => c.Song == song && c.User == user);
                if (commentExists)
                {
                    return BadRequest();
                }
                Comment comment = new Comment();
                comment.UserId = user.Id;
                comment.SongId = song.Id;
                comment.Text = model.Comment;

                _dbContext.Comments.Add(comment);

                song.Comments.Add(comment);
                _dbContext.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest();
            }


        }


        [HttpPost("getcomments")]
        public IActionResult GetComments(BaseDto dto)
        {
            var id = dto.Id;
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var songParsed = Guid.TryParse(id, out Guid songIdParsed);
            if (!songParsed)
            {
                return BadRequest();
            }
            var song = _dbContext.Songs.Include(s=> s.Comments).ThenInclude(s=> s.User).FirstOrDefault(s => s.Id == songIdParsed);
            if(song is null)
            {
                return BadRequest();
            }

            var comments = song.Comments.Select(c => new CommentDto
            {
                Text = c.Text,
                Username = c.User.Username
            }).ToList();

            if(comments.Count == 0)
            {
                return NotFound();
            }

            return Ok(comments);
        }


    }
}
