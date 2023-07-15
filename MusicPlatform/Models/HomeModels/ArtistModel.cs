using MusicPlatform.Data.Entities;

namespace MusicPlatform.Models.HomeModels
{
    public class ArtistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> ImageUrl { get; set; }
        public string Url { get; set; }
        public string Listeners { get; set; }
        public string Plays { get; set; }

        public ArtistModel(Artist artist)
        {
            Id = artist.Id;
            Name = artist.Name;
            ImageUrl = new List<string>();
            foreach (var image in artist.Images)
            {
                ImageUrl.Add(image.Url);
            }
            Url = artist.Url;
            Listeners = artist.Listeners;
            Plays = artist.Plays;

        }
    }
}
