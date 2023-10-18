using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;

public static class EmailTriageFunction
{
    [FunctionName("PostGetCategoryPredictions")]
    public static IActionResult PostGetCategoryPredictions(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "CategoryPrediction")]
        HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. GetCategoryPredictions HTTP trigger function processed a request.");
        try
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var packet = JsonConvert.DeserializeObject<PacketCommunicationForEmailTriage>(requestBody);

            // Category prediction logic goes here
            // This is where the logic for classification/cateogorisation is performed.
            // Typically the packet.Subject and/or the packet.Body will be sent/passed to
            // A pretrained NLU solution like Azure LUIS or a large lanaguage model like OpenAI
            // This will return a proprietary data model or JSON object which will need to be
            // reshaped to conform to the response below.

            var predictionResult = new PredictionResult
            {
                PacketGUID = packet.PacketGUID,
                CategoryLevel1 = "Technology",
                CategoryLevel2 = "Hardware",
                CategoryLevel3 = "Computer Components",
                PredictionScore = 0.84
            };

            return new OkObjectResult(predictionResult);
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("PostTestAdapterConfiguration")]
    public static IActionResult PostTestAdapterConfiguration(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "TestConfiguration")]
        HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. TestAdapterConfiguration HTTP trigger function processed a request.");
        try
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var configuration = JsonConvert.DeserializeObject<AdapterConfigurationEntryResult>(requestBody);

            // Configuration test logic goes here

            return new OkObjectResult(true);
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

public class PacketCommunicationForEmailTriage
{
    [JsonProperty("AdapterId")]
    public string AdapterId { get; set; }
    [JsonProperty("ConfigurationId")]
    public string ConfigurationId { get; set; }
    [JsonProperty("Guid")]
    public string Guid { get; set; }
    [JsonProperty("PacketGUID")]
    public string PacketGUID { get; set; }
    [JsonProperty("From")]
    public string From { get; set; }
    [JsonProperty("To")]
    public string To { get; set; }
    [JsonProperty("Subject")]
    public string Subject { get; set; }
    [JsonProperty("Body")]
    public string Body { get; set; }
    [JsonProperty("SentOn")]
    public string SentOn { get; set; }
    [JsonProperty("AllowedCategories")]
    public string[] AllowedCategories { get; set; }
    [JsonProperty("EnateInstance")]
    public EnateIdentityResult EnateInstance { get; set; }
}

public class PredictionResult
{
    [JsonProperty("PacketGUID")]
    public string PacketGUID { get; set; }
    [JsonProperty("CategoryLevel1")]
    public string CategoryLevel1 { get; set; }
    [JsonProperty("CategoryLevel2")]
    public string CategoryLevel2 { get; set; }
    [JsonProperty("CategoryLevel3")]
    public string CategoryLevel3 { get; set; }
    [JsonProperty("PredictionScore")]
    public double PredictionScore { get; set; }
}

public class EnateIdentityResult
{
    [JsonProperty("EnateID")]
    public string EnateID { get; set; }
    [JsonProperty("Name")]
    public string Name { get; set; }
    [JsonProperty("Subject")]
    public string Subject { get; set; }
    [JsonProperty("URL")]
    public string URL { get; set; }
    [JsonProperty("EnateIdentityCertificateName")]
    public string EnateIdentityCertificateName { get; set; }
}