using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class DependencyInjectionFunctionV2
{
    private readonly IFirstLayerService _firstLayerService;

    public DependencyInjectionFunctionV2(IFirstLayerService firstLayerService)
    {
        _firstLayerService = firstLayerService;
    }
    [Function("DependencyInjectionFunctionV2")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "dependencyinjection/v2")] HttpRequestData req,
                    FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        var guids = _firstLayerService.GetResponse();
        
        var jsonOptions = new JsonSerializerOptions(new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        });
        
        var payload = JsonSerializer.Serialize(guids, jsonOptions);
        response.WriteString(payload);

        return response;
    }
}