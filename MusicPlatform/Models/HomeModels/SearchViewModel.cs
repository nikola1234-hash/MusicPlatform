namespace MusicPlatform.Models.HomeModels
{
    public class SearchViewModel
    {
        public string Title { get; set; }
        public List<ArtistModel> Artists { get; set; } 
        public List<SongModel> Songs { get; set; }
        public SearchViewModel(string title, List<ArtistModel> artists, List<SongModel> songs)
        {
            Artists = artists;
            Songs = songs;
            Title = title;
        }
        public SearchViewModel()
        {
            Title = string.Empty;
            Artists = new List<ArtistModel>();
            Songs = new List<SongModel>();
        }
    }
}
