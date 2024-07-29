using FluentAssertions;
using GUIAutomationProject.Pages;
using TechTalk.SpecFlow;
using Xunit.Sdk;

namespace GUIAutomationProject.StepDefinitions
{
    [Binding]
    public class ExampleStepDefinitions 
    {
        private readonly HomePage _homePage;
        private readonly DomainNamesPage _domainNamesPage;

        public ExampleStepDefinitions(DomainNamesPage domainNamesPage, HomePage homePage)
        {
            _domainNamesPage = domainNamesPage;
            _homePage = homePage;
        }

        [Given(@"I am on the example home page and click on the link '([^']*)'")]
        public void GivenIAmOnTheExampleHomePageAndClickOnTheLink(string infoLink)
        {
            _homePage.ClickMoreInformationLink(infoLink);
        }


        [Then(@"a link with text '([^']*)' must be present")]
        public void ThenALinkWithTextMustBePresent(string subLinks)
        {
            string link = _domainNamesPage.VerifyLinkTextIsPresent(subLinks);
            link.Should().NotBeNull($"Link with text '{link}' not found.");
        }

        [Then(@"the '([^']*)' box must contain '([^']*)' at index '([^']*)'")]
        public void ThenTheBoxMustContainAtIndex(string domainName, string expectedText, string index)
        {
            string actualDomainNameText = string.Empty;
            try 
            { 
                actualDomainNameText = _domainNamesPage.VerifyDomainNamesBoxContainsTextAtIndex(int.Parse(index), domainName, expectedText);
                actualDomainNameText.Should().Be(expectedText,
                because: $"Did not find the expected text '{expectedText}' in the given index.");
        }
            catch (XunitException)
            {
                throw;
            }
}
    }
}
