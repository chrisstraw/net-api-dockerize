using Example.Container.Core.Common.Abstract;
using Example.Container.Core.Domain.Entities.Profile;
using Example.Container.Infrastructure.Abstractions.Repo;
using Example.Container.Infrastructure.Repos.ProfileSchema;
using System.Reflection;

namespace Example.Container.Host.WebApi.Extenstions
{
    public static class AssemblyScan
    {
        public static List<Assembly> GetAll()
        {
            // I try to get one "type" from each referenced library
            //Build out Injection Scan Assemblies
            return new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
                typeof(IAppIdentity).Assembly,
                typeof(IRepoServiceManager).Assembly,
                typeof(Profile).Assembly,
                typeof(ProfileRepo).Assembly,
            };
        }
    }
}
