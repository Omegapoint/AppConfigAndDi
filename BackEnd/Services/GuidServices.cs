namespace Backend.Services;

public interface IGuidService
{
    public Guid GetGuid();
}

public interface ITransientService : IGuidService
{ }

public class TransientService : GuidGetter, ITransientService
{
    public Guid GetGuid() => GetValue;
}

public interface IScopedService : IGuidService
{ }

public class ScopedService : GuidGetter, IScopedService
{
    public Guid GetGuid() => GetValue;
}

public interface ISingletonService : IGuidService
{ }

public class SingletonService : GuidGetter, ISingletonService
{
    public Guid GetGuid() => GetValue;

}

public abstract class GuidGetter
{
    private readonly Guid _guid;

    protected GuidGetter()
    {
        _guid = Guid.NewGuid();
    }

    public Guid GetValue => _guid;
} 