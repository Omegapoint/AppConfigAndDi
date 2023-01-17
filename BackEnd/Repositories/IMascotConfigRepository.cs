using Backend.Services;

namespace Backend.Repositories;

public interface IMascotConfigRepository
{
    Task<Mascot> GetMascot();
}