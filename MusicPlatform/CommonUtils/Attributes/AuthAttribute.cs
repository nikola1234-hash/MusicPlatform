using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicPlatform.Services;
using MusicPlatform.Services.Authentication;

namespace MusicPlatform.CommonUtils.Attributes
{
    public class AuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly ISessionService sessionService = ObjectResolverService.Resolve<ISessionService>();

        public bool Admin { get; set; }
        public AuthAttribute(bool value)
        {
            Admin = value;
        }
        public AuthAttribute()
        {
            Admin = false;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = sessionService.IsAuthenticated();
            if (!isAuthenticated)
            {
                context.Result = new RedirectResult("/Authentication/Login");
                return;
            }

            if(Admin)
            {
                var isAdmin = sessionService.IsAdmin();
                if (!isAdmin)
                {
                    context.Result = new RedirectResult("/Authentication/Login");
                    return;
                }
            }

            
          

        }
    }
}
