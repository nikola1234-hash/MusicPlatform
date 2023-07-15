
using MusicPlatform.Services;
using MusicPlatform.Services.Authentication;
using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.Models.AuthenticationModels
{
    public class RegisterForm : IValidatableObject
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReapeatPassword { get; set; }


        private readonly IAuthenticationService _authService = ObjectResolverService.Resolve<IAuthenticationService>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Password != ReapeatPassword)
            {
                yield return new ValidationResult("Passwords do not match", new[] { nameof(Password), nameof(ReapeatPassword) });
            }
            Exception e = null;

            try
            {
                _authService.Register(Username, Email, Password);
            }
            catch (Exception ex)
            {
               e = ex;
            }

            if (e != null)
            {
                yield return new ValidationResult(e.Message, new[] { nameof(Username), nameof(Email), nameof(Password) });
            }
        }
    }
}
