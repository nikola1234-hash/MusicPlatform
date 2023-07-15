using MusicPlatform.DTO;

namespace MusicPlatform.Models.HomeModels
{
    public class FindViewModel
    {
        public FindViewModel(SongDto song, ArtistDto artist)
        {
            Song = song;
            Artist = artist;
        }
        public FindViewModel()
        {
            Song = new SongDto();
            Artist = new ArtistDto();
        }

        public SongDto Song { get; set; }
        public ArtistDto Artist { get; set; }

    }
}
