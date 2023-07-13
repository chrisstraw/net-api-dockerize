using OLT.Core;

namespace Example.Container.Host.WebApi.LocalServices
{
    public class HelperService : OltCoreService, IHelperService
    {
        public HelperService(IOltHostService hostService)
        {
            this.HostService = hostService;
        }

        public IOltHostService HostService { get; }

    }
}
