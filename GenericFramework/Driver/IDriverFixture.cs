using OpenQA.Selenium;

namespace GenericFramework.Driver;

public interface IDriverFixture
{
    IWebDriver Driver { get; }
}
