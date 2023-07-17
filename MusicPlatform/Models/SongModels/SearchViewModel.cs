using MusicPlatform.DTO;
using MusicPlatform.Models.ArtistModels;

namespace MusicPlatform.Models.SongModels
{
    public class SearchViewModel
    {
        public SearchViewModel(List<SearchSongModel> songs)
        {
            Songs = songs;
        }

      public List<SearchSongModel > Songs { get; set; }
    }
}
