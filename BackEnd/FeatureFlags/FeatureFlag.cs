namespace Backend.FeatureFlags;

public class MascotFeatureFlag : FeatureFlag 
{
    public MascotFeatureFlag() : base("ShouldUseNewMascot")
    { }
}

public abstract class FeatureFlag
{
    public readonly string Name;

    protected FeatureFlag(string name)
    {
        Name = name;
    }
}