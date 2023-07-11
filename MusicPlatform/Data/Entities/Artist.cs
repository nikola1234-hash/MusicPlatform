using System.ComponentModel.DataAnnotations;

namespace MusicPlatform.Data.Entities
{
    public class Artist : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? Url { get; set; }
        public string? Listeners { get; set; }
        public string? Plays { get; set; }

        public ICollection<Song> Songs { get; set; }
        public ICollection<ArtistImages> Images { get; set; }
        public ICollection<FanBase> FanBases { get; set; }
        public Artist()
        {
            Id = Guid.NewGuid();
            Songs = new List<Song>();
            FanBases = new List<FanBase>();
            Images = new List<ArtistImages>();
        }
    }
}
