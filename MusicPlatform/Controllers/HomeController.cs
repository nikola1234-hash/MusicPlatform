using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.DTO;
using MusicPlatform.Models;
using MusicPlatform.Services;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.EnrichArtist;
using MusicPlatform.Services.Progress;
using MusicPlatform.ViewModels;
using System.Diagnostics;
using System.Drawing.Text;

namespace MusicPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;
        private readonly AppDbContext _dbContext;
        private readonly IInitialDbService _initialDbService;
        private readonly IHubContext<ProgressHub> _hubContext;


        private string Title { get; set; }
        private string SubTitle { get; set; }
        private int numberOfRecords = 6;

        public HomeController(ILogger<HomeController> logger, IApiService apiService, AppDbContext dbContext, IInitialDbService initialDbService, IHubContext<ProgressHub> hubContext)
        {
            _logger = logger;
            _apiService = apiService;
            _dbContext = dbContext;
            _initialDbService = initialDbService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            if (!_dbContext.Songs.Any())
            {
                return RedirectToAction(nameof(Initialize));
            }


            try
            {
                Title = "Featured Artists";
                SubTitle = "Featured Songs";

                EnrichService enrichService = new EnrichService();
                var artists = _dbContext.Artists.Include(s => s.Images).Take(numberOfRecords);

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

                List<ArtistModel> artistsDto = new List<ArtistModel>();
                foreach (var artist in artists)
                {
                    artistsDto.Add(new ArtistModel(artist));
                }
                var songs = _dbContext.Songs.Include(s => s.Artist).ToList().Take(9);

                List<SongModel> songsList = new List<SongModel>();

                foreach (var song in songs)
                {

                    songsList.Add(new SongModel(song));

                }


                HomeViewModel homeViewModel = new HomeViewModel(Title, SubTitle, artistsDto, songsList);


                return View(homeViewModel);
            }
            catch (Exception ex)
            {
                return Error();
            }




        }
        public IActionResult Initialize()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}