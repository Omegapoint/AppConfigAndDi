using Backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Options;

namespace Backend.Repositories;

public class MascotConfigRepository : IMascotConfigRepository
{
    private readonly IConfiguration _configuration;
    private readonly MascotOptions _mascotOptions;

    public MascotConfigRepository(IConfiguration configuration,
        IOptions<MascotOptions> mascotOptions)
    {
        _configuration = configuration;
        _mascotOptions = configuration.GetSection(MascotOptions.Mascot).Get<MascotOptions>();
    }
    
    public Task<Mascot> GetMascot()
    {
        
        
        var mascot = new Mascot() { Species = _mascotOptions.Species, Name = _configuration["Mascot:Name"] };
        return Task.FromResult(mascot);
    }
}