using System.Collections.Generic;
using System.Net;
using Backend.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class BabyFunction
{
    private readonly IFeatureFlagService _featureFlagService;

    public BabyFunction(IFeatureFlagService featureFlagService)
    {
        _featureFlagService = featureFlagService;
    }
    
    [Function("BabyFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        var gettingABaby = await  _featureFlagService.GetABaby();
        var babyMessage = gettingABaby ? "Yes, you are getting a baby" : "No baby for you!";
        
        await response.WriteStringAsync($@"Are you getting a baby?
{babyMessage}");

        return response;
    }
}