using Microsoft.Extensions.Configuration;
using OLT.Core;
using Serilog;

namespace Example.Container.Core
{
    public static class SerilogConfigurationExtensions
    {
        public static LoggerConfiguration AppSeriLog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration, string environmentName)
        {

            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", System.Diagnostics.Debugger.IsAttached);

            return loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithOltEventType()
                .Enrich.WithOltEnvironment(environmentName);

        }
    }
}