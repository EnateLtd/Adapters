using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public static class SentimentAnalysisFunction
{
    [FunctionName("TestConfiguration")]
    public static async Task<IActionResult> TestAdapterConfiguration(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "TestConfiguration")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Test Configuration Triggered");
        try
        {
            // Parse request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var configurationEntry = Newtonsoft.Json.JsonConvert.DeserializeObject<AdapterConfigurationEntryResult>(requestBody);

            // Validate the configuration entry

            // Return success if the test was successful
            return new OkObjectResult(true);
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("SubmitForSentimentAnalysis")]
    public static async Task<IActionResult> SubmitForSentimentAnalysis(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "SentimentAnalysis")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Submit For Sentiment Analysis Triggered");
        try
        {
            // Parse request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var communication = Newtonsoft.Json.JsonConvert.DeserializeObject<PacketCommunicationForTriage>(requestBody);

            // Process the communication for sentiment analysis

            // Return success if the processing was successful
            return new NoContentResult();
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}

public class AdapterConfigurationEntryResult
{
    [JsonProperty("Name")]
    public string Name { get; set; }
    [JsonProperty("Value")]
    public object Value { get; set; }
}

public class PacketCommunicationForTriage
{
    [JsonProperty("GUID")]
    public string GUID { get; set; }
    [JsonProperty("PacketGUID")]
    public string PacketGUID { get; set; }
    [JsonProperty("From")]
    public string From { get; set; }
    [JsonProperty("To")]
    public string To { get; set; }
    [JsonProperty("Subject")]
    public string Subject { get; set; }
    [JsonProperty("PlainBody")]
    public string PlainBody { get; set; }
    [JsonProperty("SentOn")]
    public string SentOn { get; set; }    
}
