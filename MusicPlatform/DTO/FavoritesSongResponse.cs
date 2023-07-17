namespace MusicPlatform.DTO
{
    public class FavoritesSongResponse
    {
        public string Id { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public int Count { get; set; }
        public string ArtistId { get; set; }
        public FavoritesSongResponse()
        {
            
        }
    }
}
