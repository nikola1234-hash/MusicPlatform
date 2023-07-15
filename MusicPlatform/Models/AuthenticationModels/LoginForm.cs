using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.Models.AuthenticationModels
{
    public class LoginForm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public LoginForm()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
