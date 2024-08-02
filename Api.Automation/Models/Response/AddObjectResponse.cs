using Newtonsoft.Json;

namespace Api.Automation.Models.Response;

public class AddObjectResponse
{
    [JsonProperty("id")] public string? Id { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }

    [JsonProperty("data")] public Data? Data { get; set; }

    [JsonProperty("createdAt")] public String? CreatedAt { get; set; }
}