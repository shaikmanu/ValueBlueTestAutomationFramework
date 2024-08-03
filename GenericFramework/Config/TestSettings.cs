using GenericFramework.Driver;

namespace GenericFramework.Config;

public class TestSettings
{
    public BrowserType BrowserType { get; set; }
    public Uri ApplicationUrl { get; set; }
    public float? TimeoutInterval { get; set; }
}
