using EaFramework.Driver;
using OpenQA.Selenium;

namespace GUIAutomationProject.Pages;

public class HomePage 
{
    private readonly IDriverWait _driver;
    
    public HomePage(IDriverWait driver)
    {
        _driver = driver;
    }


    public void ClickMoreInformationLink(string infoLink)
    {
        var moreInformationLink = _driver.FindElement(By.LinkText(infoLink));
        moreInformationLink.Click();
    }


}