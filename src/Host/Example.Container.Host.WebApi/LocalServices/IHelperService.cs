using OLT.Core;

namespace Example.Container.Host.WebApi.LocalServices
{
    public interface IHelperService : IOltCoreService
    {
        IOltHostService HostService { get; }
    }
}
