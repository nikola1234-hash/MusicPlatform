using MusicPlatform.Data.Entities;

namespace MusicPlatform.Models.ArtistModels
{
    public class ArtistModel
    {
        public ArtistModel(string name, string? url, string? listeners, string? plays)
        {
            Name = name;
            Url = url;
            Listeners = listeners;
            Plays = plays;
            Images = new();
        }
        public ArtistModel()
        {
            Name = string.Empty;
            Url = string.Empty;
            Listeners = string.Empty;
            Plays = string.Empty;
            Images = new();

        }
        public List<ArtistImages> Images { get; set; }
        public string Name { get; set; }
        public string? Url { get; set; }
        public string? Listeners { get; set; }
        public string? Plays { get; set; }
    }
}
