
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
        public string RepeatPassword { get; set; }


        private readonly IAuthenticationService _authService = ObjectResolverService.Resolve<IAuthenticationService>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Password != RepeatPassword)
            {
                yield return new ValidationResult("Passwords do not match", new[] { nameof(Password), nameof(RepeatPassword) });
            }
       

       
        }
    }
}
