using Newtonsoft.Json;

namespace MusicPlatform.DTO
{
    public class ArtistResponseDto
    {
        [JsonProperty("artist")]
        public ArtistInfo Artist { get; set; }

    }

    public class ArtistInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("stats")]
        public Stats Stats { get; set; }
        public List<Image> Image { get; set; }
    }
    public class Image
    {
        [JsonProperty("#text")]
        public string Url { get; set; }
        public string Size { get; set; }
    }
    public class Stats
    {
        [JsonProperty("listeners")]
        public int Listeners { get; set; }
        [JsonProperty("Playcount ")]
        public int Plays { get; set; }
    }
}
