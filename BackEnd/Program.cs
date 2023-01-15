using Backend.Repositories;
using Backend.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddAzureAppConfiguration("");
        builder.AddAzureAppConfiguration(options =>
        {
            options.ConfigureRefresh(config => config.SetCacheExpiration(TimeSpan.FromSeconds(10)));
            options.UseFeatureFlags(opt => opt.CacheExpirationInterval = TimeSpan.FromSeconds(10));
        });
    })
    .ConfigureServices((builder, s) =>
    {
        s.AddTransient<ITransientService, TransientService>();
        s.AddScoped<IScopedService, ScopedService>();
        s.AddSingleton<ISingletonService, SingletonService>();

        s.AddTransient<ISomeService, SomeService>();
        s.AddTransient<IFirstLayerService, FirstLayerService>();

        s.AddTransient<IMascotRepository, MascotFeatureFlagRepository>();
        
        // var configBuilder = new ConfigurationBuilder();
        // configBuilder.AddAzureAppConfiguration(options =>
        // {
        //     options.Connect("azureAppConfigEndpoint.Value");
        //     options.UseFeatureFlags(featureFlagsOptions => { }); // default CacheExpirationInterval is 30 seconds
        //     s.AddSingleton(options.GetRefresher());
        // });
        // var builtConfig = configBuilder.Build();

        // s.AddFeatureManagement(builtConfig);
        s.AddScoped<IFeatureFlagService, FeatureFlagService>();
    })
    .Build();

host.Run();
