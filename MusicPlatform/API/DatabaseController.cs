using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Services.Progress;
using MusicPlatform.Services;

namespace MusicPlatform.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IInitialDbService _initialDbService;
        private readonly IHubContext<ProgressHub> _hubContext;

        public DatabaseController(AppDbContext dbContext, IInitialDbService initialDbService, IHubContext<ProgressHub> hubContext)
        {
            _dbContext = dbContext;
            _initialDbService = initialDbService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Check if the database is empty
                if (!_dbContext.Songs.Any())
                {
                    var progress = new Progress<int>(async percentage =>
                    {
                        await _hubContext.Clients.All.SendAsync("ProgressUpdate", percentage);
                    });
                    await _initialDbService.InitializeDatabase(progress);

                }
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
            return Ok();
        }
    }
}
