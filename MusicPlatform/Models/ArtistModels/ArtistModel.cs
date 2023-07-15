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
        }
        public ArtistModel()
        {
            Name = string.Empty;
            Url = string.Empty;
            Listeners = string.Empty;
            Plays = string.Empty;

        }
        public string Name { get; set; }
        public string? Url { get; set; }
        public string? Listeners { get; set; }
        public string? Plays { get; set; }
    }
}
