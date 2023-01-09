using Backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddTransient<ITransientService, TransientService>();
        s.AddScoped<IScopedService, ScopedService>();
        s.AddSingleton<ISingletonService, SingletonService>();

        s.AddTransient<ISomeService, SomeService>();
        s.AddTransient<IFirstLayerService, FirstLayerService>();
    })
    .Build();

host.Run();
