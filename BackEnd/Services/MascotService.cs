using Backend.Repositories;

namespace Backend.Services;

public interface IMascotService
{
    Task<Mascot> GetCurrentMascot();
}

public class MascotService : IMascotService
{
    private readonly IMascotRepository _mascotRepository;

    public MascotService(IMascotRepository mascotRepository)
    {
        _mascotRepository = mascotRepository;
    }
    
    public async Task<Mascot> GetCurrentMascot()
    {
        return await _mascotRepository.GetMascot();
    }
}

public class Mascot
{
    public string Name { get; set; }
    public string Species { get; set; }
}