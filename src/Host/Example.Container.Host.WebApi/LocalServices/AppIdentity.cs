using Example.Container.Core.Common.Abstract;
using OLT.Core;
using System.Security.Claims;

namespace Example.Container.Host.WebApi.LocalServices
{
    public class AppIdentity : OltIdentity, IAppIdentity
    {
        private readonly IHttpContextAccessor _httpContext;

        public AppIdentity(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public override ClaimsPrincipal? Identity => _httpContext?.HttpContext?.User;
       
    }
}
