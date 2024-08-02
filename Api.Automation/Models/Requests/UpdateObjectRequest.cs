using Api.Automation.Models.Response;
using Newtonsoft.Json;

namespace Api.Automation.Models.Requests;

public class UpdateObjectRequest
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("data")] public Data? Data { get; set; }
}