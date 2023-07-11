using MusicPlatform.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatform.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<FanBase> FanBases { get; set; }

        public Role Role { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Favorites = new List<Favorite>();
        }

        public User(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            Favorites = new List<Favorite>();
            Role = Role.User;
        }
    }
}
