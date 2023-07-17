namespace MusicPlatform.DTO
{
    public class ArtistResponse
    {
        public string Id { get; set; }
        public string ArtistName { get; set; }
        public int Count { get; set; }
        public int SongsCount { get; set; }
        public ArtistResponse()
        {
            
        }
    }
}
