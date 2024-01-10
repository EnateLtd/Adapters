using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

public static class DataExtractionFunction
{
    [FunctionName("GetDataExtracted")]
    public static IActionResult GetDataExtracted(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "DataExtraction")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Data Extraction Triggered");
        try
        {
            // Parse the request body
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            FileDetailsForExtraction fileDetails = JsonConvert.DeserializeObject<FileDetailsForExtraction>(requestBody);

            // TODO: Perform data extraction logic using the provided file details
            // 1) Get Configuration from Marketplace using the Marketplace API https://digitalworker-dev.azure-api.net/api/v3/Adapters/{fileDetails.AdapterId}/ExternalConfigurations/{fileDetails.ConfigurationId} 
            // passing AdapterID and ConfigurationID
            // 2) Get File from IDP Hub to process using hub API endpoint https://digitalworker-dev.azure-api.net/DataExtraction/api/v1/GetFileForExtraction
            // passing DataExtractionCallBackRequest dto object
            // 3) Send file to 3rd party system for extraction - returns JSON
            // This is where you will need to implement some custom logic
            // 4) Store details of Enate File ID and 3rd Party id, json data and details 
            // of any UI url required in db/storage.  this will be required in a callback by 
            // 3rd party and Enate later
            
            // Create the data extraction result (Example)
            DataExtractionResult result = new DataExtractionResult
            {
                PacketGuid = fileDetails.PacketGuid,
                FileGuid = fileDetails.FileGuid,
                Source = fileDetails.Source,
                Confidence = 0.87, // Replace with actual confidence value
                JsonResult = "{ \"key\": \"value\" }", // Replace with actual JSON result
                FileResult = new FileDownloadResponse
                {
                    FileData = "base64-encoded-data", // Replace with actual file data
                    FileName = "example.txt" // Replace with actual file name
                }
            };

            // Return the data extraction result
            return new OkObjectResult(result);
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("PostDocumentValidationDetails")]
    public static IActionResult PostDocumentValidationDetails(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "DocumentValidationDetails")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Document Validation Details Triggered");
        try
        {
            // Parse the request body
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            DocumentValidationDetailsRequest documentDetails = JsonConvert.DeserializeObject<DocumentValidationDetailsRequest>(requestBody);

            // Perform document validation details logic here

            // Create the document validation details result
            DocumentValidationDetailsResult result = new DocumentValidationDetailsResult
            {
                FileGuid = documentDetails.FileGuid,
                PacketGuid = documentDetails.PacketGuid,
                FrameHTML = string.Empty, // Replace with actual frame HTML
                AccessToken = string.Empty, // Replace with actual access token
                FunctionName = string.Empty // Replace with actual function name
            };

            // Return the document validation details result
            return new OkObjectResult(result);
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("PostModelList")]
    public static IActionResult PostModelList(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "ModelList")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Get Model List Triggered");
        try
        {
            // Parse the request body
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            ModelListRequest modelRequest = JsonConvert.DeserializeObject<ModelListRequest>(requestBody);

            // Perform getModelList logic here

            // Create a list of models
            List<Model> models = new List<Model>
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
            };

            // Create the model list result
            ModelListResult result = new ModelListResult
            {
                Models = models
            };

            // Return the model list result
            return new OkObjectResult(result);
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("PostTestAdapterConfiguration")]
    public static IActionResult PostTestAdapterConfiguration(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "TestConfiguration")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. Test Adapter Configuration Triggered");
        try
        {
            // Parse the request body
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            AdapterConfigurationEntryResult configuration = JsonConvert.DeserializeObject<AdapterConfigurationEntryResult>(requestBody);

            // Perform test adapter configuration logic here

            // Create test configuration result
            TestConfigurationResults result = new TestConfigurationResults
            {
                Success = true,
                Messages = new List<TestConfigurationResultMessage>
                {
                    new TestConfigurationResultMessage
                    {
                        Parameters = new List<string> { "setting-name" },
                        Type = "MissingOrIncorrectInformation"
                    },
                    new TestConfigurationResultMessage
                    {
                        Parameters = new List<string>(),
                        Type = "ConfigurationEmpty"
                    }
                }
            };

            // Return the test configuration result
            return new OkObjectResult(result);
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    [FunctionName("PostExternalCallBack")]
    public static IActionResult PostExternalCallBack(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ExternalCallBack/{itemId}")] 
        HttpRequest req, 
        string itemId, 
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. External Callback Triggered");
        try
        {
            // Get the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ConvertedJsonResult>(requestBody);

            // Process the request and generate the data extraction result
            DataExtractionResult result = ProcessDataExtraction(data);

            return new OkObjectResult(result)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        catch (Exception)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
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
    public List<Model> Models { get; set; }
}

public class Model
{
    [JsonProperty("ModelName")]
    public string ModelName { get; set; }
    [JsonProperty("ModelId")]
    public string ModelId { get; set; }
}

public class AdapterConfigurationEntryResult
{
    // Add properties for the AdapterConfigurationEntryResult schema
}

public class TestConfigurationResults
{
    [JsonProperty("Success")]
    public bool Success { get; set; }
    [JsonProperty("Messages")]
    public List<TestConfigurationResultMessage> Messages { get; set; }
}

public class TestConfigurationResultMessage
{
    [JsonProperty("Parameters")]
    public List<string> Parameters { get; set; }
    [JsonProperty("Type")]
    public string Type { get; set; }
    [JsonProperty("Message")]
    public string Message { get; set; }
}

public class DocumentValidationDetailsRequest
{
    [JsonProperty("AdapterId")]
    public string AdapterId { get; set; }

    [JsonProperty("FileGuid")]
    public string FileGuid { get; set; }

    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }
}

public class DocumentValidationDetailsResult
{
    [JsonProperty("FileGuid")]
    public string FileGuid { get; set; }

    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }

    [JsonProperty("FrameHTML")]
    public string FrameHTML { get; set; }

    [JsonProperty("AccessToken")]
    public string AccessToken { get; set; }

    [JsonProperty("FunctionName")]
    public string FunctionName { get; set; }
}

public class ConvertedJsonResult
{
    [JsonProperty("UserLanguage")]
    public string UserLanguage { get; set; }
    [JsonProperty("Access")]
    public bool Access { get; set; }
    [JsonProperty("Assigned")]
    public bool Assigned { get; set; }
    [JsonProperty("Pages")]
    public List<Page> Pages { get; set; }
    [JsonProperty("Validations")]
    public List<Validation> Validations { get; set; }
}

public class Page
{
    [JsonProperty("PageNumber")]
    public int PageNumber { get; set; }
    [JsonProperty("Image")]
    public Image Image { get; set; }
    [JsonProperty("RawEntities")]
    public List<RawEntity> RawEntities { get; set; }
    [JsonProperty("KeyValuePairs")]
    public List<KeyValuePair> KeyValuePairs { get; set; }
    [JsonProperty("Tables")]
    public List<Table> Tables { get; set; }
}

public class Validation
{
    [JsonProperty("Status")]
    public string Status { get; set; }
    [JsonProperty("Pages")]
    public List<Page> Pages { get; set; }
}

public class Image
{
    [JsonProperty("Url")]
    public string Url { get; set; }
    [JsonProperty("Width")]
    public double Width { get; set; }
    [JsonProperty("Height")]
    public double Height { get; set; }
}

public class KeyValuePair
{
    [JsonProperty("Id")]
    public int Id { get; set; }
    [JsonProperty("Key")]
    public Key Key { get; set; }
    [JsonProperty("Value")]
    public Value Value { get; set; }
    [JsonProperty("Edited")]
    public bool Edited { get; set; }
}

public class Key
{
    [JsonProperty("StartX")]
    public double StartX { get; set; }
    [JsonProperty("StartY")]
    public double StartY { get; set; }
    [JsonProperty("EndX")]
    public double EndX { get; set; }
    [JsonProperty("EndY")]
    public double EndY { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
    [JsonProperty("Text")]
    public string Text { get; set; }
    [JsonProperty("ConfidenceColor")]
    public string ConfidenceColor { get; set; }
    [JsonProperty("OutlineColor")]
    public string OutlineColor { get; set; }
    [JsonProperty("FillColor")]
    public string FillColor { get; set; }
    [JsonProperty("DataType")]
    public int DataType { get; set; }
}

public class Value
{   
    [JsonProperty("StartX")]
    public double StartX { get; set; }
    [JsonProperty("StartY")]
    public double StartY { get; set; }
    [JsonProperty("EndX")]
    public double EndX { get; set; }
    [JsonProperty("EndY")]
    public double EndY { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
    [JsonProperty("Text")]
    public string Text { get; set; }
    [JsonProperty("ConfidenceColor")]
    public string ConfidenceColor { get; set; }
    [JsonProperty("OutlineColor")]
    public string OutlineColor { get; set; }
    [JsonProperty("FillColor")]
    public string FillColor { get; set; }
    [JsonProperty("DataType")]
    public int DataType { get; set; }
}

public class RawEntity
{
    [JsonProperty("StartX")]
    public double StartX { get; set; }
    [JsonProperty("StartY")]
    public double StartY { get; set; }
    [JsonProperty("EndX")]
    public double EndX { get; set; }
    [JsonProperty("EndY")]
    public double EndY { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
    [JsonProperty("Text")]
    public string Text { get; set; }
    [JsonProperty("ConfidenceColor")]
    public string ConfidenceColor { get; set; }
    [JsonProperty("OutlineColor")]
    public string OutlineColor { get; set; }
    [JsonProperty("FillColor")]
    public string FillColor { get; set; }
    [JsonProperty("DataType")]
    public int DataType { get; set; }
}

public class Table
{
    [JsonProperty("StartX")]
    public double StartX { get; set; }
    [JsonProperty("StartY")]
    public double StartY { get; set; }
    [JsonProperty("EndX")]
    public double EndX { get; set; }
    [JsonProperty("EndY")]
    public double EndY { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
    [JsonProperty("Rows")]
    public List<List<TableCell>> Rows { get; set; }
}

public class TableCell
{        
    [JsonProperty("StartX")]
    public double StartX { get; set; }
    [JsonProperty("StartY")]
    public double StartY { get; set; }
    [JsonProperty("EndX")]
    public double EndX { get; set; }
    [JsonProperty("EndY")]
    public double EndY { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
    [JsonProperty("Text")]
    public string Text { get; set; }
    [JsonProperty("ConfidenceColor")]
    public string ConfidenceColor { get; set; }
    [JsonProperty("OutlineColor")]
    public string OutlineColor { get; set; }
    [JsonProperty("FillColor")]
    public string FillColor { get; set; }
    [JsonProperty("DataType")]
    public int DataType { get; set; }
}

public class DataExtractionResult
{
    [JsonProperty("PacketGuid")]
    public string PacketGuid { get; set; }
    [JsonProperty("FileGuid")]
    public string FileGuid { get; set; }
    [JsonProperty("Source")]
    public int Source { get; set; }
    [JsonProperty("Confidence")]
    public double Confidence { get; set; }
    [JsonProperty("JsonResult")]
    public string JsonResult { get; set; }
    [JsonProperty("FileResult")]
    public FileDownloadResponse FileResult { get; set; }
}

public class FileDetailsForExtraction
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

public class FileDownloadResponse
{
    [JsonProperty("FileData")]
    public string FileData { get; set; }
    [JsonProperty("FileName")]
    public string FileName { get; set; }
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