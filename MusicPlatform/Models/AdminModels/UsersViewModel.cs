using MusicPlatform.Data.Entities;

namespace MusicPlatform.Models.AdminModels
{
    public class UsersViewModel
    {
        public UsersViewModel(List<User> users)
        {
            this.Users = users;
        }

        public List<User> Users { get; set; }
    }
}
