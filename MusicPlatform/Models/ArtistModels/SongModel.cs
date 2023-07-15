namespace MusicPlatform.Models.ArtistModels
{
    public class SongModel
    {
        public SongModel(string name, string lyrics)
        {
            Name = name;
            Lyrics = lyrics;
        }
        public SongModel()
        {
            
        }
        public string Name { get; set; }
        public string Lyrics { get; set; }
    }
}
