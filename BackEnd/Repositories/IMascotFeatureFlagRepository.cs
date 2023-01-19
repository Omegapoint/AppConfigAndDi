using Backend.Services;

namespace Backend.Repositories;

public interface IMascotFeatureFlagRepository
{
    Task<Mascot> GetMascot();
}