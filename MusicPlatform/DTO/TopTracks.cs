namespace MusicPlatform.DTO
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class TopTracks
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("track")]
        public List<Track> Tracks { get; set; }
    }

    public class Track
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mbid")]
        public string Mbid { get; set; }

        [JsonPropertyName("playcount")]
        public int Playcount { get; set; }

        [JsonPropertyName("listeners")]
        public int Listeners { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("image")]
        public List<TrackImage> Images { get; set; }
    }

    public class TrackImage
    {
        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("#text")]
        public string Value { get; set; }
    }

}
