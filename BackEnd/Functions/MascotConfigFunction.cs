using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Backend.Helpers;
using Backend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class MascotConfigFunction
{
    private readonly IMascotService _mascotService;

    public MascotConfigFunction(IMascotService mascotService)
    {
        _mascotService = mascotService;
    }
    [Function("MascotFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req
        )
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        await response.WriteStringAsync(await _mascotService.GetCurrentMascot());

        return response;
    }
}