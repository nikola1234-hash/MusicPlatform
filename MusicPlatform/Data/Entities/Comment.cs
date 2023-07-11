using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.Data.Entities
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid SongId { get; set; }
        public Song Song { get; set; }

        [Required]
        public string Text { get; set; }
        public Comment()
        {
            Id = Guid.NewGuid();
        }
    }
}
