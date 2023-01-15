using Backend.Services;

namespace Backend.Repositories;

public interface IMascotRepository
{
    Task<Mascot> GetMascot();
}