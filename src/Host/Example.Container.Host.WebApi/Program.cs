using Serilog;
using System.Reflection;
using Example.Container.Core;
using Example.Container.Host.WebApi.Extenstions;
using Example.Container.Host.WebApi.Models;
using Newtonsoft.Json.Converters;
using OLT.Core;
using Example.Container.Infrastructure.Abstractions.Repo;
using Example.Container.Core.Common.Abstract;
using Example.Container.Infrastructure.Repos.ProfileSchema;
using Example.Container.Host.WebApi.LocalServices;
using OLT.Extensions.SwaggerGen;
using Example.Container.Infrastructure.Db;
using static Example.Container.Core.AppConstants;
using System.Text.Json;
using OLT.Logging.Serilog;

LocalServiceCollectionExtenstions.ConfigureGlobalLogger(args);

var builder = WebApplication.CreateBuilder(args);

Log.Information("HostBuilder: {EnvironmentName} -> {ContentRootPath}", builder.Environment.EnvironmentName, builder.Environment.ContentRootPath);

//Add application Configuration
builder.Configuration.BuildAppConfig(Assembly.GetEntryAssembly(), Directory.GetCurrentDirectory(), builder.Environment.EnvironmentName, args);


//Add Enable Serilog
builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
{
    //Call app ext to stub in default Serilog config
    loggerConfiguration.AppSeriLog(hostingContext.Configuration, hostingContext.HostingEnvironment.EnvironmentName);
});


var scanAssemblies = new List<Assembly>
{
    Assembly.GetExecutingAssembly(),
    typeof(IAppIdentity).Assembly,
    typeof(IRepoServiceManager).Assembly,
    typeof(ProfileRepo).Assembly,
};

//var enableSwagger = System.Diagnostics.Debugger.IsAttached || builder.Environment.IsDevelopment();
var enableSwagger = true;

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

builder.Services.AddProblemDetails();

// Add services to the container.

// Add services to the container.
builder.Services
    .Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"))
    .Configure<OltAspNetAppSettings>(options =>
    {
        options.Hosting = appSettings.Hosting;
    })
    .AddOltAspNetCore(scanAssemblies, mvcBuilder =>
    {        
        mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
    })
    //.AddFluentValidationAutoValidation()
    //.AddOltInjectionAutoMapper(scanAssemblies)
    .AddScoped<IAppIdentity, AppIdentity>()
    //.AddScoped<IMentorMessageBus, AzureQueueMessageBus>()
    .AddOltSerilog(configOptions => configOptions.ShowExceptionDetails = appSettings.Hosting.ShowExceptionDetails)
    //.AddOltCacheMemory(TimeSpan.FromMinutes(30))
    .AddSwaggerWithVersioning(
            new OltSwaggerArgs(new OltOptionsApiVersion())
                .WithTitle("Example Container WebApi")
                .WithDescription("RESTful API for example container")
                .WithSecurityScheme(new OltSwaggerJwtBearerToken())
                .WithOperationFilter(new OltDefaultValueFilter())
                .WithOperationFilter(new OltCamelCasingOperationFilter())
                .Enable(enableSwagger))
    .AddSwaggerGenNewtonsoftSupport();

//SQL Server, Postgres, InMemory
builder.Services.AddAppInMemoryDatabase(appSettings.Hosting.ShowExceptionDetails);
//builder.Services.AddAppPostgresDatabase(builder.Configuration.GetOltConnectionString(ConnectionStrings.DbConnectionName), appSettings.Hosting.ShowExceptionDetails);
//builder.Services.AddAppSqlServerDatabase(builder.Configuration.GetOltConnectionString(ConnectionStrings.DbConnectionName), appSettings.Hosting.ShowExceptionDetails);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var hostingSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>()?.Hosting ?? new AppSettings().Hosting;
hostingSettings = hostingSettings ?? new OltAspNetHostingOptions();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Enabling for example purposes
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(hostingSettings);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
