using MusicPlatform.Models.ArtistModels;

namespace MusicPlatform.Models.SongModels
{
    public class SongDetailsViewModel
    {
        public SongModel Song { get; set; }
        public ArtistModel Artist { get; set; }
        public List<CommentModel> Comments { get; set; }
        public SongDetailsViewModel()
        {
            Song = new SongModel();
            Artist = new ArtistModel();
            Comments = new List<CommentModel>();
        }

        public SongDetailsViewModel(SongModel song, ArtistModel artist, List<CommentModel> comments)
        {
            Song = song;
            Artist = artist;
            Comments = comments;
        }
    }
}
