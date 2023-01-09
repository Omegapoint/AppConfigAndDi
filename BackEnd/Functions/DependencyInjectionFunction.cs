using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class DependencyInjectionFunction
{
    private readonly ISomeService _someService;

    public DependencyInjectionFunction(ISomeService someService)
    {
        _someService = someService;
    }
    
    [Function("DependencyInjectionFunction")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "dependencyinjection")] HttpRequestData req,
                    FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        var guids = _someService.GetResponse();
        
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