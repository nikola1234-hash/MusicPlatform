namespace MusicPlatform.Models.ArtistModels
{
    public class SongModel
    {
        public SongModel(Guid id, string name, string lyrics)
        {
            Id = id;
            Name = name;
            Lyrics = lyrics;
        }
        public SongModel()
        {
            
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }
    }
}
