using OLT.Core;

namespace Example.Container.Host.WebApi.Models
{
    public class AppSettings : OltAspNetAppSettings
    {
        public string? JwtSecret { get; set; }
    }
}
