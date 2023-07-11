namespace MusicPlatform.Data.Entities
{
    public class FanBase : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public FanBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
