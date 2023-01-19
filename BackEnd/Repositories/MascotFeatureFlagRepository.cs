using Backend.Services;

namespace Backend.Repositories;

public class MascotFeatureFlagRepository : IMascotFeatureFlagRepository
{
    private readonly IFeatureFlagService _featureFlagService;

    public MascotFeatureFlagRepository(IFeatureFlagService featureFlagService)
    {
        _featureFlagService = featureFlagService;
    }
    
    public async Task<Mascot> GetMascot()
    {
        var shouldUseNewMascot = await _featureFlagService.ShouldUseNewMascot();
        if (shouldUseNewMascot)
        {
            return GetNewMascot();
        }

        return GetOldMascot();
    }

    private Mascot GetOldMascot() => new() { Name = "Old Tom", Species = "Cat" };

    private Mascot GetNewMascot() => new() { Name = "New Peppa", Species = "Pig" };
}