using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.DTO;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.EnrichArtist;
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
        public ArtistController(AppDbContext dbContext, IApiService apiService, IMapper mapper)
        {
            _dbContext = dbContext;
            _apiService = apiService;
            _mapper = mapper;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                EnrichService enrichService = new EnrichService();
                var artists = _dbContext.Artists.Include(s => s.Images).Take(5);

                foreach (var artist in artists)
                {
                    if (string.IsNullOrEmpty(artist.Url))
                    {
                        ArtistResponseDto artistResponse = await _apiService.GetArtistDetails(artist.Name);
                        if (artistResponse == null)
                        {
                            continue;
                        }
                        enrichService.Enrich(artist, artistResponse, _dbContext);
                        _dbContext.Entry(artist).State = EntityState.Modified;
                    }

                }

                await _dbContext.SaveChangesAsync();

                List<ArtistDto> artistsDto = new List<ArtistDto>();
                foreach (var artist in artists)
                {
                    artistsDto.Add(new ArtistDto(artist));
                }

                return Ok(artistsDto);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
