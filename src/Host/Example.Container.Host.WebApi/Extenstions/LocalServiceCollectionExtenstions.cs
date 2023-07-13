using Example.Container.Core;
using Serilog;
using System.Reflection;

namespace Example.Container.Host.WebApi.Extenstions
{
    public static class LocalServiceCollectionExtenstions
    {
        private static string RunEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Test";

        public static void ConfigureGlobalLogger(string[] args)
        {

            try
            {
                var configuration = new ConfigurationBuilder()
                    .BuildAppConfig(Assembly.GetExecutingAssembly(), Directory.GetCurrentDirectory(), RunEnvironment, args)
                    .Build();

                //var allConfigValues = configuration.AsEnumerable().ToList();
                //var filtered = allConfigValues.Where(p => p.Key.StartsWith("AppSettings")).ToList();

                Log.Logger = new LoggerConfiguration().AppSeriLog(configuration, RunEnvironment).CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"RunEnvironment -> {RunEnvironment}");
                Console.WriteLine();

                foreach (var innerEx in ex.GetInnerExceptions())
                {
                    Console.WriteLine();
                    Console.WriteLine("********************************");
                    Console.WriteLine($"{innerEx}");
                    Console.WriteLine("********************************");
                    Console.WriteLine();
                }
                throw;
            }
        }
    }
}
