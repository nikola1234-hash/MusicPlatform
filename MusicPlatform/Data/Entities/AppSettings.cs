namespace MusicPlatform.Data.Entities
{
    public class AppSettings : BaseEntity
    {
        public bool IsEnriched { get; set; }
        public AppSettings()
        {
            Id = Guid.NewGuid();
        }
    }
}
