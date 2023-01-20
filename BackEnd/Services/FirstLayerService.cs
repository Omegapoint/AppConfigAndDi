namespace Backend.Services;

public class FirstLayerService : IFirstLayerService
{
    private readonly ISomeService _someService;
    private readonly ITransientService _transientService;
    private readonly IScopedService _scopedService;
    private readonly ISingletonService _singletonService;

    public FirstLayerService(ISomeService someService, ITransientService transientService, 
                    IScopedService scopedService, ISingletonService singletonService)
    {
        _someService = someService;
        _transientService = transientService;
        _scopedService = scopedService;
        _singletonService = singletonService;
    }

    public List<DiResponse> GetResponse()
    {
        var response = new List<DiResponse>
        {
                        new(InjectionType.Transient, _transientService.GetGuid().ToString()),
                        new(InjectionType.Scoped, _scopedService.GetGuid().ToString()),
                        new(InjectionType.Singleton, _singletonService.GetGuid().ToString())
        };
        
        response.AddRange(_someService.GetResponse());
        response.AddRange(_someService.GetResponse());

        return response.OrderBy(x => x.InjectionType).ToList();
    }
}

public interface IFirstLayerService
{
    public List<DiResponse> GetResponse();
}