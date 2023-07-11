namespace MusicPlatform.Data.Entities
{
    public class Favorite : BaseEntity
    {

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid SongId { get; set; }
        public Song Song { get; set; }
        public Favorite()
        {
            Id = Guid.NewGuid();
        }
    }
}
