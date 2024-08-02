using Newtonsoft.Json;
using RestSharp;

namespace Api.Automation.Tests.Utility;

public class HandleContent
{
    public static T GetContent<T> (RestResponse response)
    {
        var content = response.Content;

        return JsonConvert.DeserializeObject<T>(content);
    }

    public static T? ParseJson<T>(string file)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
    }

    public static string GetRequestFilePath(string name)
    {
        var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
        path = string.Format(path + "TestData\\RequestData\\{0}", name);
        return path;
    }
}