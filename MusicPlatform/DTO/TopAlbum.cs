using Newtonsoft.Json;




namespace MusicPlatform.DTO
{
  
    public class TopAlbum
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("album")]
        public List<Album> Albums { get; set; }
    }

    public class Album
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mbid")]
        public string Mbid { get; set; }

        [JsonProperty("listeners")]
        public int Listeners { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("image")]
        public List<AlbumImage> Images { get; set; }
    }

    public class AlbumImage
    {
        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("#text")]
        public string Value { get; set; }
    }

}
