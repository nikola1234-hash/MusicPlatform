using MusicPlatform.Data.Entities;

namespace MusicPlatform.DTO
{
    public class SongDto
    {
        public SongDto(Guid id, string name, string lyrics, List<Comment> comments, List<Favorite> favorites)
        {
            Id = id;
            Name = name;
            Lyrics = lyrics;
            Comments = comments;
            Favorites = favorites;
        }
        public SongDto()
        {
            Id = Guid.NewGuid();
            Comments = new List<Comment>();
            Favorites = new List<Favorite>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Favorite> Favorites { get; set; }

    }
}
