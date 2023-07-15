using MusicPlatform.CommonUtils.Exceptions;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;

namespace MusicPlatform.Services.Authentication
{


    public class AuthenticationService : IAuthenticationService
    {

        private readonly ISessionService _sessionService;
        private readonly AppDbContext _dbContext;

        public AuthenticationService(ISessionService sessionService, AppDbContext dbContext)
        {
            _sessionService = sessionService;
            _dbContext = dbContext;
        }

        public AuthenticationResult Login(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be empty");
            }


            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return AuthenticationResult.UserNotFound;
            }


            var isPasswordValid = PasswordEncrypt.VerifyHashedPassword(user.Password, password);
            if (!isPasswordValid)
            {
                return AuthenticationResult.InvalidUsernameOrPassword;
            }

            _sessionService.StoreToSession(user.Id.ToString());

            return AuthenticationResult.Success;
        }

        public void Logout()
        {
            _sessionService.RemoveFromSession();
        }

        public bool IsLoggedIn()
        {
            return _sessionService.GetFromSession() != null;
        }

        public bool UsernameExist(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);
            return user != null;
        }

        public bool EmailExist(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            return user != null;
        }

        public void Register(string username, string password, string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new UserRegistrationException("Username, password and email cannot be empty");
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);


            if (user != null)
            {
                throw new UserRegistrationException(nameof(username));
            }


            var userEmail = _dbContext.Users.FirstOrDefault(x => x.Email == email);


            if (userEmail != null)
            {
                throw new UserRegistrationException(nameof(email));
            }

            // If all is okay we can create new user
            // lets hash the password

            string hashedPassword = PasswordEncrypt.HashPassword(password);

            var newUser = new User(username, email, hashedPassword);


            var result = _dbContext.Users.Add(newUser);

            // If result is not added something went wrong
            if (result.State != Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                throw new UserRegistrationException("Something went wrong");
            }


            //If we came this far all is okay save changes
            _dbContext.SaveChanges();

        }

    }
}
