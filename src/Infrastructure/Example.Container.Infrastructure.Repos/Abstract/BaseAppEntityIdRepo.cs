using Example.Container.Infrastructure.Abstractions;
using Example.Container.Infrastructure.Abstractions.Repo;
using OLT.Core;

namespace Example.Container.Infrastructure.Repos.Abstract
{

    public abstract class BaseAppEntityIdRepo<TEntity> : OltEntityIdService<IDatabaseContext, TEntity>
      where TEntity : class, IOltEntity, IOltEntityId, new()
    {
        protected BaseAppEntityIdRepo(IRepoServiceManager serviceManager, IDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
