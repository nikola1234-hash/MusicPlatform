using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.DTO;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.EnrichArtist;
using MusicPlatform.Services.Progress;
using System.Drawing.Text;

namespace MusicPlatform.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly IEnrichService _enrichService;
        private readonly ILogger<ArtistController> _logger;
        private readonly IHubContext<ProgressHub> _hubContext;
        public ArtistController(AppDbContext dbContext, IApiService apiService, IMapper mapper, ILogger<ArtistController> logger, IEnrichService enrichService, IHubContext<ProgressHub> hubContext)
        {
            _dbContext = dbContext;
            _apiService = apiService;
            _mapper = mapper;
            _logger = logger;
            _enrichService = enrichService;
            _hubContext = hubContext;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                if (!_dbContext.Artists.Any())
                {
                    _logger.LogInformation(_logger + "No artists in database");
                    return BadRequest();

                }

                var settings = await _dbContext.AppSettings.FirstOrDefaultAsync();

                if (!(settings is null) && settings.IsEnriched)
                {
                    _logger.LogInformation(_logger + "Database is already enriched");
                    return BadRequest();
                }

                var artists = await _dbContext.Artists.Include(s => s.Images).ToListAsync();
                var progress = new Progress<int>(async percentage =>
                {
                    await _hubContext.Clients.All.SendAsync("EnrichUpdate", percentage);
                });

                await Enrich(progress, artists);
              
                AppSettings appSettings = new();
                appSettings.IsEnriched = true;
                _dbContext.AppSettings.Add(appSettings);

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger + ex.Message);
                return BadRequest();
            }
        }

        private async Task Enrich(IProgress<int> progress, List<Artist> artists)
        {
            int total = artists.Count;
            for(int i = 0; i < artists.Count; i++)
            {
                if (string.IsNullOrEmpty(artists[i].Url))
                {
                    ArtistResponseDto artistResponse = await _apiService.GetArtistDetails(artists[i].Name);
                    if (artistResponse == null)
                    {
                        continue;
                    }
                    _enrichService.Enrich(artists[i], artistResponse, _dbContext);
                    _dbContext.Update(artists[i]);

                }

                var progressPercentage = (int)(((i + 1) / (double)total) * 100);
                progress?.Report(progressPercentage);
                var recordsDone = new
                {
                    current = i + 1,
                    total = total
                };
                await _hubContext.Clients.All.SendAsync("RecordsUpdate", recordsDone);
            }
        }
  
    }

    

}

