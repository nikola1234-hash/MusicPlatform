using MusicPlatform.Enums;

namespace MusicPlatform.Services.Authentication
{
    public interface ISessionService
    {
        SessionData GetFromSession();
        bool IsAdmin();
        bool IsAuthenticated();
        bool IsInRole(Role role);
        void RemoveFromSession();
        void StoreToSession(string id);
    }
}