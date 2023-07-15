using MusicPlatform.Data;
using MusicPlatform.Enums;
using MusicPlatform.Services.Authentication.Models;

namespace MusicPlatform.Services.Authentication
{
    public class SessionService : ISessionService
    {

        private readonly IHttpContextAccessor _context;
        private readonly AppDbContext _dbContext;

        public SessionService(IHttpContextAccessor context, AppDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public void StoreToSession(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Id cannot be null or empty", nameof(id));


            var success = Guid.TryParse(id, out Guid userId);
            if (success)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                    throw new ArgumentException("User not found", nameof(user));

                _context.HttpContext.Response.Cookies.Append("UserRole", user.Role.ToString(), new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(7),
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

                _context.HttpContext.Session.SetString("UserId", userId.ToString());


            }
        }

        public SessionData GetFromSession()
        {
            var userId = _context.HttpContext.Session.GetString("UserId");
            var userRole = _context.HttpContext.Request.Cookies["UserRole"];
            SessionData sessionData = new SessionData(userId, userRole);
            return sessionData;

        }


        public void RemoveFromSession()
        {
            _context.HttpContext.Session.Remove("UserId");
            _context.HttpContext.Response.Cookies.Delete("UserRole");
        }

        public bool IsAuthenticated()
        {
            var userId = _context.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return false;
            return true;
        }

        public bool IsInRole(Role role)
        {
            var userRole = _context.HttpContext.Request.Cookies["UserRole"];
            if (string.IsNullOrEmpty(userRole))
                return false;

            if (userRole == role.ToString())
                return true;

            return false;
        }


        public bool IsAdmin()
        {
            var userRole = _context.HttpContext.Request.Cookies["UserRole"];
            if (string.IsNullOrEmpty(userRole))
                return false;

            if (userRole == Role.Admin.ToString())
                return true;

            return false;
        }

    }
}
