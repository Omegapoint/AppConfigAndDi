using Azure.Security.KeyVault.Secrets;
using Backend;
using Backend.Repositories;
using Backend.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.Azure.AppConfiguration.Functions.Worker;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, builder) =>
    {
        builder.AddJsonFile("local.settings.json");
        builder.AddAzureAppConfiguration(options =>
        {
            options.Connect(Environment.GetEnvironmentVariable("AppConfigConnectionString"));
            options.Select("Mascot:*")
                .ConfigureRefresh(config => 
                config.Register("Mascot:Settings:Sentinel", refreshAll: true)
                    .SetCacheExpiration(TimeSpan.FromSeconds(10))
                );
            options.UseFeatureFlags(opt => opt.CacheExpirationInterval = TimeSpan.FromSeconds(10));
        });
    })
    .ConfigureServices((builder, services) =>
    {
        services.AddTransient<ITransientService, TransientService>();
        services.AddScoped<IScopedService, ScopedService>();
        services.AddSingleton<ISingletonService, SingletonService>();

        services.AddTransient<ISomeService, SomeService>();
        services.AddTransient<IFirstLayerService, FirstLayerService>();

        services.AddTransient<IMascotConfigRepository, MascotConfigRepository>();
        services.AddTransient<IMascotFeatureFlagRepository, MascotFeatureFlagRepository>();
        services.AddSingleton<IMascotSingletonConfigRepository, MascotSingletonConfigRepository>();

        services.AddTransient<IMascotService, MascotService>();
        services.AddFeatureManagement();
        services.AddAzureAppConfiguration();
        services.AddScoped<IFeatureFlagService, FeatureFlagService>();
        
        services.Configure<MascotOptions>(
            builder.Configuration.GetSection(MascotOptions.Mascot));
    })
    .ConfigureFunctionsWorkerDefaults((context, app) =>
    {
        app.UseAzureAppConfiguration();
    })
    .Build();

host.Run();
