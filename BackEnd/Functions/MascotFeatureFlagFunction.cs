using System.Collections.Generic;
using System.Net;
using Backend.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class MascotFeatureFlagFunction
{
    private readonly IMascotFeatureFlagRepository _mascotFeatureFlagRepository;

    public MascotFeatureFlagFunction(IMascotFeatureFlagRepository mascotFeatureFlagRepository)
    {
        _mascotFeatureFlagRepository = mascotFeatureFlagRepository;
    }
    
    [Function("MascotFeatureFlagFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        var mascot = await _mascotFeatureFlagRepository.GetMascot();
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        await response.WriteStringAsync($"Our current featureflag mascot is a {mascot.Species} and is named {mascot.Name}");

        return response;
        
    }
}