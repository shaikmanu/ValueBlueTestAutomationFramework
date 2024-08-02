using Newtonsoft.Json;

namespace Api.Automation.Models.Response;

public class ListOfObjectsResponse
{
    public List<Objects>? objects { get; set; }
}

public class Objects
{
    [JsonProperty("id")] public string? Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("data")] public Data? Data { get; set; }
}

public class Data
{
    [JsonProperty("color")] public string? Color { get; set; }

    [JsonProperty("capacity")] public string? Capacity { get; set; }

    [JsonProperty("capacity GB")] public int? CapacityGB { get; set; }

    [JsonProperty("price")] public double? Price { get; set; }

    [JsonProperty("generation")] public string? Generation { get; set; }

    [JsonProperty("year")] public int? Year { get; set; }

    [JsonProperty("CPU model")] public string? CPUModel { get; set; }

    [JsonProperty("Hard disk size")] public string? HardDiskSize { get; set; }

    [JsonProperty("Strap Colour")] public string? StrapColour { get; set; }

    [JsonProperty("case Size")] public string? CaseSize { get; set; }

    [JsonProperty("description")] public string? Description { get; set; }

    [JsonProperty("Screen size")] public double? Screensize { get; set; }
}