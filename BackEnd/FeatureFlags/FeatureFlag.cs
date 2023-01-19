namespace Backend.FeatureFlags;

public class MascotFeatureFlag : FeatureFlag 
{
    public MascotFeatureFlag() : base("ShouldUseNewMascot")
    { }
}

public class GetABabyFeatureFlag : FeatureFlag
{
    public GetABabyFeatureFlag() : base("MaybeBaby") 
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