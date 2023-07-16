using MusicPlatform.Models.ArtistModels;

namespace MusicPlatform.Models.SongModels
{
    public class SongDetailsViewModel
    {
        public SongModel Song { get; set; }
        public ArtistModel Artist { get; set; }
        public SongDetailsViewModel()
        {
            Song = new SongModel();
            Artist = new ArtistModel();
        }

        public SongDetailsViewModel(SongModel song, ArtistModel artist)
        {
            Song = song;
            Artist = artist;
        }
    }
}
