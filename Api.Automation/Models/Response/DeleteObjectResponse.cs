using Newtonsoft.Json;
namespace Api.Automation.Models.Response;

public class DeleteObjectResponse
{
    [JsonProperty("message")] public string? Message { get; set; }
}