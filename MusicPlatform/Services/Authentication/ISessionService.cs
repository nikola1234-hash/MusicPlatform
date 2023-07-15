using MusicPlatform.Enums;
using MusicPlatform.Services.Authentication.Models;

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