using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MusicPlatform.CommonUtils.Attributes;
using MusicPlatform.Data;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.Progress;
using MusicPlatform.Services;
using MusicPlatform.Models.AdminModels;

namespace MusicPlatform.Controllers
{
    [Auth(true)]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IApiService _apiService;
        private readonly AppDbContext _dbContext;
        private readonly IInitialDbService _initialDbService;
        private readonly IHubContext<ProgressHub> _hubContext;
        private readonly IMapper _mapper;

        public AdminController(ILogger<AdminController> logger, IApiService apiService, AppDbContext dbContext, IInitialDbService initialDbService, IHubContext<ProgressHub> hubContext, IMapper mapper)
        {
            _logger = logger;
            _apiService = apiService;
            _dbContext = dbContext;
            _initialDbService = initialDbService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {

            var users = _dbContext.Users.ToList();
            UsersViewModel usersViewModel = new UsersViewModel(users);
            return View(usersViewModel);
        }
        public IActionResult Songs()
        {
            return View();
        }
    }
}
