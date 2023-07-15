using MusicPlatform.Data.Entities;

namespace MusicPlatform.Models.ArtistModels
{
    public class ArtistViewModel
    {
        public ArtistModel Artist { get; set; }
        public List<SongModel> Songs { get; set; }
        public ArtistViewModel(ArtistModel artist, List<SongModel> songs)
        {
            Artist = artist;
            Songs = songs;
        }
        public ArtistViewModel()
        {
            Artist = new ArtistModel();
            Songs = new List<SongModel>();
        }
    }
}
