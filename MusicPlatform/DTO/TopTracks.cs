namespace MusicPlatform.DTO
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class TopTracks
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("track")]
        public List<Track> Tracks { get; set; }
    }

    public class Track
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mbid")]
        public string Mbid { get; set; }

        [JsonProperty("playcount")]
        public int Playcount { get; set; }

        [JsonProperty("listeners")]
        public int Listeners { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("image")]
        public List<TrackImage> Images { get; set; }
    }

    public class TrackImage
    {
        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("#text")]
        public string Value { get; set; }
    }

}
