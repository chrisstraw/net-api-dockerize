using Example.Container.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Container.Infrastructure.Db
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddAppInMemoryDatabase(this IServiceCollection services, bool showExceptionDetails)
        {

            return services
                .AddDbContext<DatabaseContext>(optionsBuilder =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName: "ProfileDb");

                    if (showExceptionDetails)
                    {
                        optionsBuilder
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors();
                    }
                })
                .AddScoped<IDatabaseContext>(opt => opt.GetRequiredService<SqlServerDatabaseContext>());
        }

        public static IServiceCollection AddAppPostgresDatabase(this IServiceCollection services, string connectionString, bool showExceptionDetails)
        {
            return services
                .AddDbContext<DatabaseContext>(optionsBuilder =>
                {
                    optionsBuilder
                        .UseNpgsql(connectionString)
                        .UseLowerCaseNamingConvention();  //Makes all table names lower case using (EFCore.NamingConventions)

                    if (showExceptionDetails)
                    {
                        optionsBuilder
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors();
                    }
                })
                .AddScoped<IDatabaseContext>(opt => opt.GetRequiredService<DatabaseContext>());

        }


        public static IServiceCollection AddAppSqlServerDatabase(this IServiceCollection services, string connectionString, bool showExceptionDetails)
        {

            return services
                .AddDbContext<SqlServerDatabaseContext>(optionsBuilder =>
                {
                    optionsBuilder.UseSqlServer(connectionString);

                    if (showExceptionDetails)
                    {
                        optionsBuilder
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors();
                    }
                })
                .AddScoped<IDatabaseContext>(opt => opt.GetRequiredService<SqlServerDatabaseContext>());
        }
    }
}