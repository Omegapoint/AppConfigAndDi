using Backend.Services;
using Microsoft.Extensions.Configuration;

namespace Backend.Repositories;

public interface IMascotSingletonConfigRepository : IMascotConfigRepository
{ }

public class MascotSingletonConfigRepository : IMascotSingletonConfigRepository
{
    private readonly IConfiguration _configuration;

    public MascotSingletonConfigRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<Mascot> GetMascot()
    {
        var mascot = new Mascot() { Species = _configuration["Mascot:Species"], Name = _configuration["Mascot:Name"] };
        return Task.FromResult(mascot);
    }
}