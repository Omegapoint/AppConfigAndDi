using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Helpers;
using Backend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class DependencyInjectionFunctionV2
{
    private readonly IFirstLayerService _firstLayerService;
    private readonly IConfiguration _configuration;

    public DependencyInjectionFunctionV2(IFirstLayerService firstLayerService,
        IConfiguration configuration)
    {
        _firstLayerService = firstLayerService;
        _configuration = configuration;
    }
    
    [Function("DependencyInjectionFunctionV2")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "dependencyinjection/v2")] HttpRequestData req,
                    FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        var guids = _firstLayerService.GetResponse();
        
        var payload = JsonSerializer.Serialize(guids, JsonHelper.GetJsonSerializerOptions());
        response.WriteString(payload);

        return response;
    }
}