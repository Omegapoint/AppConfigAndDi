using Backend.Services;
using Microsoft.Extensions.Configuration;

namespace Backend.Repositories;

public class MascotConfigRepository : IMascotRepository
{
    private readonly IConfiguration _configuration;

    public MascotConfigRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task<Mascot> GetMascot()
    {
        throw new NotImplementedException();
    }
}