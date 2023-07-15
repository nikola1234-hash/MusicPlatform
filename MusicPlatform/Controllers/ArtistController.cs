using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Data;
using MusicPlatform.Models.ArtistModels;

namespace MusicPlatform.Controllers
{

    public class ArtistController : Controller
    {
        private readonly ILogger<ArtistController> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ArtistController(AppDbContext dbContext, 
                                ILogger<ArtistController> logger,
                                IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            try
            {
                bool success = Guid.TryParse(id, out Guid artistId);
                if(!success)
                {
                    _logger.LogInformation(nameof(InvalidCastException));
                    return View("Error");
                }

                var artist = _dbContext.Artists.FirstOrDefault(s=> s.Id == artistId);

                if (artist == null)
                {
                    _logger.LogInformation("Artist not found");
                    return View("Error");
                }

                var songs = _dbContext.Songs.Where(s => s.ArtistId == artistId).ToList();


                List<SongModel> songList = _mapper.Map<List<SongModel>>(songs);
                ArtistModel artistModel = _mapper.Map<ArtistModel>(artist);

                ArtistViewModel artistViewModel = new ArtistViewModel(artistModel, songList);

                return View(artistViewModel);

            }
            catch (System.Exception e)
            {
                _logger.LogCritical(message: e.Message);
                return View("Error");   
            }
        
        }
       
    }
}
