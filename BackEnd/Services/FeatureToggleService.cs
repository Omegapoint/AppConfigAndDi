using Backend.FeatureFlags;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

namespace Backend.Services;

public interface IFeatureFlagService
{
    Task<bool> ShouldUseNewMascot();
}

public class FeatureFlagService : IFeatureFlagService
{
    private readonly IFeatureManager _featureManager;
    private readonly IConfigurationRefresher _configurationRefresher;

    public FeatureFlagService(IFeatureManager featureManager,
        IConfigurationRefresherProvider refresherProvider
        )
    {
        _featureManager = featureManager;
        _configurationRefresher = refresherProvider.Refreshers.First();
    }

    public async Task<bool> ShouldUseNewMascot()
    {
        return await IsFlagEnabled(new MascotFeatureFlag());
    }
    
    private async Task<bool> IsFlagEnabled(FeatureFlag featureFlag)
    {
        await RefreshFeatureFlags();
        return await _featureManager.IsEnabledAsync(featureFlag.Name);
    }

    private async Task RefreshFeatureFlags() => await _configurationRefresher.TryRefreshAsync();
}