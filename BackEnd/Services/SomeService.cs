namespace Backend.Services;

public interface ISomeService
{
    public List<DiResponse> GetResponse();
}

public class SomeService : ISomeService
{
    private readonly ITransientService _transientService;
    private readonly IScopedService _scopedService;
    private readonly ISingletonService _singletonService;

    public SomeService(ITransientService transientService, IScopedService scopedService, ISingletonService singletonService)
    {
        _transientService = transientService;
        _scopedService = scopedService;
        _singletonService = singletonService;
    }

    public List<DiResponse> GetResponse()
    {
        return new List<DiResponse>()
        {
            new(InjectionType.Transient, _transientService.GetGuid().ToString()),
            new(InjectionType.Scoped, _scopedService.GetGuid().ToString()),
            new(InjectionType.Singleton, _singletonService.GetGuid().ToString()),
        };
    }
}