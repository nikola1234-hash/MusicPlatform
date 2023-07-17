using Newtonsoft.Json;

namespace MusicPlatform.DTO
{
    public class TopAlbumResponse
    {
        [JsonProperty("topalbums")]
        public TopAlbum TopAlbum { get; set; }
    }
}
