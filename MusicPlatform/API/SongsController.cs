using Microsoft.AspNetCore.Mvc;
using MusicPlatform.Data;
using MusicPlatform.Services.Api;

namespace MusicPlatform.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IApiService _apiService;

        public SongsController(AppDbContext dbContext, IApiService apiService)
        {
            _dbContext = dbContext;
            _apiService = apiService;
        }

        // GET: api/Songs
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }




    }
}
