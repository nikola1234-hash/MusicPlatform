using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatform.Data.Entities
{
    public class Song : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength(int.MaxValue)]
        public string Lyrics { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public Song()
        {
            Id = Guid.NewGuid();
            Favorites = new List<Favorite>();
            Comments = new List<Comment>();
        }

        public Song(string name, string lyrics, Guid artistId)
        {
            Name = name;
            Lyrics = lyrics;
            ArtistId = artistId;
            Id = Guid.NewGuid();
            Favorites = new List<Favorite>();
            Comments = new List<Comment>();
        }
    }
}
