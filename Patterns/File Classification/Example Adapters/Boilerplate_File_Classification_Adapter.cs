using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

public static class FileClassificationFunction
{
    [FunctionName("GetFileClassification")]
    public static IActionResult GetFileClassification(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "FileClassification")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Get File Classification Triggered");
        try
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            FileDetailsForClassification fileDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<FileDetailsForClassification>(requestBody);
            
            // Perform file classification logic here
            
            // Create and return the file classification result
            FileClassificationResult result = new FileClassificationResult
            {
                PacketGuid = fileDetails.PacketGuid,
                FileGuid = fileDetails.FileGuid,
                Tags = new Tags
                {
                    Name = "Pay Slip",
                    Confidence = 0.87
                }
            };
            
            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
    
    [FunctionName("GetModelList")]
    public static IActionResult GetModelList(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ModelList")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Get Model List Triggered");
        try
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            ModelListRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelListRequest>(requestBody);
            
            // Perform logic to get list of models based on the request
            
            // Create and return the list of models
            ModelListResult result = new ModelListResult
            {
                Models = new []
                {
                    new Model
                    {
                        ModelName = "exampleModel1",
                        ModelId = "92958610-0D15-487D-A2AA-7293171BD95E"
                    },
                    new Model
                    {
                        ModelName = "exampleModel2",
                        ModelId = "C75E7F62-9E5C-4F52-BD48-184E3A5A5F88"
                    }
                }
            };
            
            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
    
    [FunctionName("PostTestAdapterConfiguration")]
    public static IActionResult PostTestAdapterConfiguration(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "TestConfiguration")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Test Adapter Configuration Triggered");
        try
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            AdapterConfigurationEntryResult configuration = Newtonsoft.Json.JsonConvert.DeserializeObject<AdapterConfigurationEntryResult>(requestBody);
            
            // Perform logic to test the adapter configuration
            
            // Create and return the test configuration results
            TestConfigurationResults result = new TestConfigurationResults
            {
                Success = true,
                Messages = new []
                {
                    new TestConfigurationResultMessage
                    {
                        Parameters = new [] { "setting-name" },
                        Type = "MissingOrIncorrectInformation"
                    },
                    new TestConfigurationResultMessage
                    {
                        Parameters = new string[0],
                        Type = "ConfigurationEmpty"
                    }
                }
            };
            
            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}

public class FileDetailsForClassification
{
    [JsonProperty("AdapterId")]
    public string AdapterId { get; set; }
    [JsonProperty("ConfigurationId")]
    public string ConfigurationId { get; set; }
    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }
    [JsonProperty("FileGuid")]
    public string FileGuid { get; set; }
    [JsonProperty("ModelId")]
    public string ModelId { get; set; }
    [JsonProperty("Source")]
    public int Source { get; set; }
    [JsonProperty("EnateInstance")]
    public EnateIdentityResult EnateInstance { get; set; }
}

public class FileClassificationResult
{
    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }
    [JsonProperty("FileGuid")]
    public string FileGuid { get; set; }
    [JsonProperty("Tags")]
    public Tags Tags { get; set; }
}

public class Tags
{
    [JsonProperty("Name")]
    public string Name { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
}

public class ModelListRequest
{
    [JsonProperty("AdapterId")]
    public string AdapterId { get; set; }
    [JsonProperty("ConfigurationId")]
    public string ConfigurationId { get; set; }
}

public class ModelListResult
{
    [JsonProperty("Models")]
    public Model[] Models { get; set; }
}

public class Model
{
    [JsonProperty("ModelName")]
    public string ModelName { get; set;
    [JsonProperty("ModelId")] }
    public string ModelId { get; set; }
}

public class AdapterConfigurationEntryResult
{
    [JsonProperty("Name")]
    public string Name { get; set; }
    [JsonProperty("Value")]
    public object Value { get; set; }
}

public class TestConfigurationResults
{
    [JsonProperty("Success")]
    public bool Success { get; set; }
    [JsonProperty("Messages")]
    public TestConfigurationResultMessage[] Messages { get; set; }
}

public class TestConfigurationResultMessage
{
    [JsonProperty("Parameters")]
    public string[] Parameters { get; set; }
    [JsonProperty("Type")]
    public string Type { get; set; }
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