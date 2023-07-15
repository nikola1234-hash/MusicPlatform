
using Microsoft.AspNet.Identity;
namespace MusicPlatform.Services
{
    public static class PasswordEncrypt
    {
        private static readonly IPasswordHasher _passwordHasher = ObjectResolverService.Resolve<IPasswordHasher>();

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty");
            }
            return _passwordHasher.HashPassword(password);
        }


        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentException("Password cannot be empty");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty");
            }

            var result = _passwordHasher.VerifyHashedPassword(hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
