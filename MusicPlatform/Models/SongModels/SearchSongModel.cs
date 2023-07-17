namespace MusicPlatform.Models.SongModels
{
    public class SearchSongModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }
        public string ArtistName { get; set; }
        public Guid ArtistId { get; set; }
        public SearchSongModel()
        {
            
        }
    }
}
