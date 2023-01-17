using Backend.Repositories;

namespace Backend.Services;

public interface IMascotService
{
    Task<string> GetCurrentMascot();
}

public class MascotService : IMascotService
{
    private readonly IMascotConfigRepository _mascotConfigRepository;

    public MascotService(IMascotConfigRepository mascotConfigRepository)
    {
        _mascotConfigRepository = mascotConfigRepository;
    }
    
    public async Task<string> GetCurrentMascot()
    {
        var mascot = await _mascotConfigRepository.GetMascot();
        return $"Our current mascot is a {mascot.Species} and is named {mascot.Name}";
    }
}

public class Mascot
{
    public string Name { get; set; }
    public string Species { get; set; }
}