using Backend.Services;

namespace Backend.Repositories;

public class MascotFeatureFlagRepository : IMascotRepository
{
    private readonly IFeatureFlagService _featureFlagService;

    public MascotFeatureFlagRepository(IFeatureFlagService featureFlagService)
    {
        _featureFlagService = featureFlagService;
    }
    
    public async Task<Mascot> GetMascot()
    {
        if (await _featureFlagService.ShouldUseNewMascot())
        {
            return GetNewMascot();
        }

        return GetOldMascot();
    }

    private Mascot GetOldMascot() => new() { Name = "GulleGris", Species = "Pig" };

    private Mascot GetNewMascot() => new() { Name = "Shinzo", Species = "Red panda" };
}