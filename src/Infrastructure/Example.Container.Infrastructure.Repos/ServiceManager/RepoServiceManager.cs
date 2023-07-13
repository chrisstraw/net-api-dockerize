using Example.Container.Infrastructure.Abstractions.Repo;
using OLT.Core;

namespace Example.Container.Infrastructure.Repos.ServiceManager
{
    public class RepoServiceManager : OltEfCoreServiceManager, IRepoServiceManager
    {
        public RepoServiceManager(
            IOltAdapterResolver adapterResolver) : base(adapterResolver)
        {
        }
    }
}
