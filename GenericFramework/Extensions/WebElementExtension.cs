using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GenericFramework.Extensions;

public static class WebElementExtension
{
    //This is a class where some generic methods as shown below can be used accross the tests as part of framework creation
    public static void SelectDropDownByText(this IWebElement element, string text)
    {
        var select = new SelectElement(element);
        select.SelectByText(text);
    }
    
    public static void ClearAndEnterText(this IWebElement element, string value)
    {
        element.Clear();
        element.SendKeys(value);
    }
}
