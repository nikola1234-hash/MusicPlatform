using Newtonsoft.Json;

namespace MusicPlatform.DTO
{
    public class TopTracksResponse
    {
        [JsonProperty("toptracks")]
        public TopTracks TopTracks { get; set; }
    }
}
