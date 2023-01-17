using Backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Backend.Repositories;

public class MascotConfigRepository : IMascotConfigRepository
{
    private readonly IConfiguration _configuration;

    public MascotConfigRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task<Mascot> GetMascot()
    {
        var mascot = new Mascot() { Species = _configuration["Mascot:Species"], Name = _configuration["Mascot:Name"] };
        return Task.FromResult(mascot);
    }
}