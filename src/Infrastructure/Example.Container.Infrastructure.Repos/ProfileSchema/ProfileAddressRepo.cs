using Example.Container.Infrastructure.Abstractions;
using Example.Container.Infrastructure.Abstractions.Repo;
using Example.Container.Infrastructure.Repos.Abstract;

namespace Example.Container.Infrastructure.Repos.ProfileSchema
{
    public class ProfileAddressRepo : BaseAppEntityIdRepo<Core.Domain.Entities.Profile.Address>, IProfileAddressRepo
    {
        public ProfileAddressRepo(IRepoServiceManager serviceManager, IDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
