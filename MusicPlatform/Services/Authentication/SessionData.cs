namespace MusicPlatform.Services.Authentication
{
    public class SessionData
    {
        public SessionData(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
        public SessionData()
        {
            UserId = string.Empty;
            Role = string.Empty;
        }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
