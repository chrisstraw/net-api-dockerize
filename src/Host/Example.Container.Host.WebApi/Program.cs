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
using Example.Container.Core.Domain.Entities.Profile;
using Microsoft.Extensions.Options;

LocalServiceCollectionExtenstions.ConfigureGlobalLogger(args);

var builder = WebApplication.CreateBuilder(args);

Log.Information("HostBuilder: {EnvironmentName} -> {ContentRootPath}", builder.Environment.EnvironmentName, builder.Environment.ContentRootPath);

//Add application Configuration
builder.Configuration.BuildAppConfig(Assembly.GetEntryAssembly(), Directory.GetCurrentDirectory(), builder.Environment.EnvironmentName, args);

//After BuildAppConfig, pull app settings for service config
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

//Add Enable Serilog
builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
{
    //Call app ext to stub in default Serilog config
    loggerConfiguration.AppSeriLog(hostingContext.Configuration, hostingContext.HostingEnvironment.EnvironmentName);
});






builder.Services.AddProblemDetails();

#region [ Configuration ]

builder.Services
    .Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"))
    .Configure<OltAspNetAppSettings>(options =>
    {
        options.Hosting = appSettings.Hosting;
    });

#endregion


// Add services to the container.
builder.Services
    .AddOltAspNetCore(AssemblyScan.GetAll(), mvcBuilder =>
    {
        mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
    })
    //.AddFluentValidationAutoValidation()
    //.AddOltInjectionAutoMapper(AssemblyScan.GetAll())
    .AddScoped<IAppIdentity, AppIdentity>()
    .AddOltSerilog(configOptions => configOptions.ShowExceptionDetails = appSettings.Hosting.ShowExceptionDetails)
    //.AddOltCacheMemory(TimeSpan.FromMinutes(30))
    ;

#region [ Swagger ]

//var enableSwagger = System.Diagnostics.Debugger.IsAttached || builder.Environment.IsDevelopment(); //My normal way of hiding Swagger in deployed apps
var enableSwagger = true;  //Force it show swagger for demo purpose
builder.Services.AddAppSwagger(enableSwagger);

#endregion

#region [ Database ]

//SQL Server, Postgres, InMemory
builder.Services.AddAppInMemoryDatabase(appSettings.Hosting.ShowExceptionDetails);
//builder.Services.AddAppPostgresDatabase(builder.Configuration.GetOltConnectionString(ConnectionStrings.DbConnectionName), appSettings.Hosting.ShowExceptionDetails);
//builder.Services.AddAppSqlServerDatabase(builder.Configuration.GetOltConnectionString(ConnectionStrings.DbConnectionName), appSettings.Hosting.ShowExceptionDetails);

#endregion


//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//Refresh App Config Hostings from app
var hostingSettings =
    app.Services.GetRequiredService<IOptions<AppSettings>>().Value.Hosting ??
    appSettings?.Hosting ??
    new OltAspNetHostingOptions();

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
