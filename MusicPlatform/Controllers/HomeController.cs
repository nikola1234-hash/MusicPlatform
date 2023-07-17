using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MusicPlatform.Data;
using MusicPlatform.Models;
using MusicPlatform.Models.HomeModels;
using MusicPlatform.Services;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.Progress;
using System.Diagnostics;

namespace MusicPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;
        private readonly AppDbContext _dbContext;
        private readonly IInitialDbService _initialDbService;
        private readonly IHubContext<ProgressHub> _hubContext;
        private readonly IMapper _mapper;


        private string Title { get; set; }
        private string SubTitle { get; set; }
        private int numberOfRecords = 6;

        private bool isInitialized
        {
            get
            {
                var settings = _dbContext.AppSettings.FirstOrDefault();
                if (settings is null)
                {
                    return false;
                }
                return settings.IsEnriched;
            }
        }

        public HomeController(ILogger<HomeController> logger, IApiService apiService, AppDbContext dbContext, IInitialDbService initialDbService, IHubContext<ProgressHub> hubContext, IMapper mapper)
        {
            _logger = logger;
            _apiService = apiService;
            _dbContext = dbContext;
            _initialDbService = initialDbService;
            _hubContext = hubContext;
            _mapper = mapper;
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

           
                var artists = _dbContext.Artists.Include(s => s.Images).Take(numberOfRecords);

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

        public async Task<IActionResult> Search(string searchTerm)
        {
            SearchViewModel searchViewModel = new SearchViewModel();

            if(string.IsNullOrEmpty(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
            {
                searchViewModel.Title = "Invalid Search term";
                return View(searchViewModel);
            }

            var result = await _dbContext.SearchSongs(searchTerm);
            foreach(var data in result)
            {
                data.Artist = await _dbContext.Artists.FirstOrDefaultAsync(a => a.Id == data.ArtistId);
            }
            if(result.Count == 0)
            {
                searchViewModel.Title = "No results found";
                return View(searchViewModel);
            }
            searchViewModel.Title = "Search Results";
            List<SongModel> songs = _mapper.Map<List<SongModel>>(result);
            searchViewModel.Songs = songs;

            return View(searchViewModel);
        }

    

        public IActionResult Initialize()
        {
            // Just in case
            if(isInitialized)
            {
                return RedirectToAction(nameof(Index));
            }
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