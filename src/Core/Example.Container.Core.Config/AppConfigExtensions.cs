using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Example.Container.Core
{
    public static class AppConfigExtensions
    {
        public static IConfigurationBuilder BuildAppConfig(this IConfigurationBuilder builder, Assembly startupAssembly, string basePath, string environmentName, string[] args)
        {
            builder
              .SetBasePath(basePath)
              .AddJsonFile("appsettings.json", true)
              .AddJsonFile($"appsettings.{environmentName}.json", true)
              .AddEnvironmentVariables()
              .AddCommandLine(args)
              .AddUserSecrets(startupAssembly);

            return builder;
        }       
    }
}