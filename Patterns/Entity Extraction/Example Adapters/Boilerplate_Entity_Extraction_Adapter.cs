using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

public static class EntityExtractionAPI
{
    [FunctionName("PostTestConfiguration")]
    public static IActionResult PostTestConfiguration(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "TestConfiguration")]
        HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Test Configuration Triggered");
        try
        {
            // Do the necessary logic to test the configuration            

            return new OkObjectResult(true);
        }
        catch (Exception ex)
        {            
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("PostEntityExtraction")]
    public static IActionResult PostEntityExtraction(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "EntityExtraction")]
        HttpRequest req, ILogger log)
    {
        try
        {
            // Parse the request body
            EmailDetailsForEntityExtraction input = req.ReadFromJson<EmailDetailsForEntityExtraction>();
            
            // Implementation of entity extraction logic
            // Sample result (replace with actual result)
            var exampleEntityResult = new Dictionary<string, object>()
            {
                {"example_key", new Dictionary<string, object>()
                    {
                        {"example_property", "example_value"}
                    }
                }
            };

            EntityExtractionResult result = new EntityExtractionResult()
            {
                PacketGuid = input.PacketGuid,
                ExtractionResult = exampleEntityResult
            };            

            return new OkObjectResult(result);
        }
        catch (Exception ex)
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

public class EmailDetailsForEntityExtraction
{
    [JsonProperty("AdapterId")]
    public string AdapterId { get; set; }
    [JsonProperty("ConfigurationId")]
    public string ConfigurationId { get; set; }
    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }
    [JsonProperty("Subject")]
    public string Subject { get; set; }
    [JsonProperty("Body")]
    public string Body { get; set; }
    [JsonProperty("Categories")]
    public string Categories { get; set; }
    [JsonProperty("DataFields")]
    public Dictionary<string, object> DataFields { get; set; }
    [JsonProperty("EnateInstance")]
    public EnateIdentityResult EnateInstance { get; set; }
}

public class EntityExtractionResult
{
    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }
    [JsonProperty("ExtractionResult")]
    public Dictionary<string, object> ExtractionResult { get; set; }
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