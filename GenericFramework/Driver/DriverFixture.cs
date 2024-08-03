using GenericFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace GenericFramework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
{
    private readonly TestSettings _testSettings;

    public IWebDriver Driver { get; }
    
    public DriverFixture(TestSettings testSettings)
    {
        _testSettings = testSettings;
        Driver = GetWebDriver();
        Driver.Manage().Window.Maximize();
        Driver.Navigate().GoToUrl(_testSettings.ApplicationUrl);
    }


    private IWebDriver GetWebDriver()
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            BrowserType.Safari => new SafariDriver(),
            BrowserType.EdgeChromium => new EdgeDriver(),
            _ => new ChromeDriver()
        };
    }

    public void Dispose()
    {
       Driver.Quit();
    }
}


public enum BrowserType
{
    Chrome,
    Firefox,
    Safari,
    EdgeChromium
}
