using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

namespace Backend.Services;

public interface IFeatureToggle
{
    
}

public class FeatureToggle : IFeatureToggle
{
    private readonly IFeatureManager _featureManager;
    private readonly IConfigurationRefresher _configurationRefresher;

    public FeatureToggle(IFeatureManager featureManager,
        IConfigurationRefresher configurationRefresher)
    {
        _featureManager = featureManager;
        _configurationRefresher = configurationRefresher;
    }

    private async Task<bool> IsFlagEnabled(FeatureFlag featureFlag)
    {
        await RefreshFeatureFlags();
        return await _featureManager.IsEnabledAsync(featureFlag.Name);
    }

    private async Task RefreshFeatureFlags() => await _configurationRefresher.TryRefreshAsync();
}

public abstract class FeatureFlag
{
    public readonly string Name;

    protected FeatureFlag(string name)
    {
        Name = name;
    }
}