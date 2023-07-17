using System.Collections.Generic;
using System.Text.Json.Serialization;



namespace MusicPlatform.DTO
{
  
    public class TopAlbum
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("album")]
        public List<Album> Albums { get; set; }
    }

    public class Album
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mbid")]
        public string Mbid { get; set; }

        [JsonPropertyName("listeners")]
        public int Listeners { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("image")]
        public List<AlbumImage> Images { get; set; }
    }

    public class AlbumImage
    {
        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("#text")]
        public string Value { get; set; }
    }

}
