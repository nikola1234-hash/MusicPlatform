using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Data;
using MusicPlatform.Models.ArtistModels;
using MusicPlatform.Models.SongModels;

namespace MusicPlatform.Controllers
{
    public class SongController : Controller
    {
        private readonly ILogger<SongController> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public SongController(ILogger<SongController> logger, AppDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error");
            }

            bool parsed = Guid.TryParse(id, out Guid songId);
            if (!parsed)
            {
                return View("Error");
            }
            var song = _dbContext.Songs.FirstOrDefault(s => s.Id == songId);
            if(song == null)
            {
                return View("Error");
            }
            
            var artist = _dbContext.Artists.FirstOrDefault(a => a.Id == song.ArtistId);

            var songModel = _mapper.Map<SongModel>(song);
            var artistModel = _mapper.Map<ArtistModel>(artist);
            var comments = _dbContext.Comments.Where(c => c.SongId == songId).ToList();

            List<CommentModel> commentsList = new List<CommentModel>();
            foreach (var comment in comments)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == comment.UserId);

                CommentModel commentModel = new CommentModel()
                {

                    User = user.Username,
                    Comment = comment.Text
                    
                };
                commentsList.Add(commentModel);
               
            }

            SongDetailsViewModel model = new SongDetailsViewModel(songModel, artistModel, commentsList);


            return View(model);
        }
    }
}
