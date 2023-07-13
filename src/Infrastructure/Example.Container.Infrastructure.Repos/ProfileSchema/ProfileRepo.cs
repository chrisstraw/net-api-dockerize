using Example.Container.Infrastructure.Abstractions;
using Example.Container.Infrastructure.Abstractions.Repo;
using Example.Container.Infrastructure.Repos.Abstract;
using OLT.Core;

namespace Example.Container.Infrastructure.Repos.ProfileSchema
{
    public class ProfileRepo : BaseAppEntityIdRepo<Core.Domain.Entities.Profile.Profile>, IProfileRepo
    {
        public ProfileRepo(IRepoServiceManager serviceManager, IDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
