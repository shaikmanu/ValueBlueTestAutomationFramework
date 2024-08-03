using OpenQA.Selenium;

namespace GenericFramework.Driver;

public interface IDriverWait
{
    IWebElement FindElement(By elementLocator);
    IEnumerable<IWebElement> FindElements(By elementLocator);
}
