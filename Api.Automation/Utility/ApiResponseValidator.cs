using Newtonsoft.Json.Linq;

namespace Api.Automation.Tests.Utility
{
    public static class ApiResponseValidator
    {
        // Generic method to compare two objects deeply
        public static bool AreObjectsEqual<T>(T actual, T expected)
        {
            if (expected == null)
                return actual == null;

            if (actual == null)
                return false;

            return JToken.DeepEquals(JToken.FromObject(actual), JToken.FromObject(expected));
        }
    }
}
