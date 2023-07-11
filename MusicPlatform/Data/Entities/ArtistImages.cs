namespace MusicPlatform.Data.Entities
{
    public class ArtistImages : BaseEntity
    {
        public string Url { get; set; }
        public string Size { get; set; }


        public ArtistImages()
        {
            Id = Guid.NewGuid();
        }

        public ArtistImages(string url, string size)
        {
            Id = Guid.NewGuid();
            Url = url;
            Size = size;
        }

    }
}
