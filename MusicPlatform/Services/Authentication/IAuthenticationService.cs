namespace MusicPlatform.Services.Authentication
{


    public enum AuthenticationResult
    {
        Success,
        InvalidUsernameOrPassword,
        UserNotFound
    }
    public interface IAuthenticationService
    {

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthenticationResult Login(string username, string password);
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <exception cref="UserRegistrationException">If username or email already used or arguments are null or empty</exception>
        void Register(string username, string password, string email);
        void Logout();
        bool IsLoggedIn();
        bool UsernameExist(string username);
        bool EmailExist(string email);
    }
}