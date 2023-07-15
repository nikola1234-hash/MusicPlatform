using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicPlatform.Services;
using MusicPlatform.Services.Authentication;

namespace MusicPlatform.CommonUtils.Helpers
{
    public static class AuthHelper
    {

        private static ISessionService sessionService = ObjectResolverService.Resolve<ISessionService>();

       

        public static bool IsAuthenticated(this IHtmlHelper helper)
        {
            return sessionService.IsAuthenticated();
        }

        public static bool IsAdmin(this IHtmlHelper helper)
        {
            return sessionService.IsAdmin();
        }
      

    }
}
