using Backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Options;

namespace Backend.Repositories;

public class MascotConfigRepository : IMascotConfigRepository
{
    private readonly IConfiguration _configuration;
    private readonly MascotOptions _mascotOptions;
    private readonly MascotOptions _mascotOptionsSnapshot;

    public MascotConfigRepository(IConfiguration configuration,
        IOptions<MascotOptions> mascotOptions,
        IOptionsSnapshot<MascotOptions> mascotOptionsSnapshot)
    {
        _configuration = configuration;
        _mascotOptions = mascotOptions.Value;
        _mascotOptionsSnapshot = mascotOptionsSnapshot.Value;
    }
    
    public Task<Mascot> GetMascot()
    {
        var mascot = new Mascot() { Species = _mascotOptions.Species, Name = _configuration["Mascot:Name"] };
        return Task.FromResult(mascot);
    }
}