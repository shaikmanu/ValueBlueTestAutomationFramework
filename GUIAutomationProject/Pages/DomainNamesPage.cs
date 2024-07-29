using EaFramework.Driver;
using FluentAssertions;
using OpenQA.Selenium;
using System;

namespace GUIAutomationProject.Pages
{
    public class DomainNamesPage
    {
        private readonly IDriverWait _driver;

        public DomainNamesPage(IDriverWait driver)
        {
            _driver = driver;
        }

        public string VerifyLinkTextIsPresent(string linkText)
        {
            var link = _driver.FindElement(By.LinkText(linkText));
            return link.Text;
            
        }

        public string VerifyDomainNamesBoxContainsTextAtIndex(int index, string domainName, string expectedText)
        {
            string nonBreakingSpace = "\u00A0";
            string modifiedDomainName = domainName.Replace(" ", nonBreakingSpace);

            string xpath = $"//a[@href = '/domains' and contains(text(), '{modifiedDomainName}')]/following::td[1]/ul/li[{index}]/a[@href='/domains/int' and contains(text(), '.INT Registry')]";
            By dynamicXpath = By.XPath(xpath);
                                 
            var domainElement = _driver.FindElement((dynamicXpath));
            return domainElement.Text;
        }

    }
}
