using System;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Api.Automation.Models.Requests;
using Api.Automation.Models.Response;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;
using Api.Automation.Tests.Utility;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class GetSingleObjectStepDefinitions
    {
        private RestResponse _response;
        private ScenarioContext _scenarioContext;
        private IApiClient _apiClient;
        HttpStatusCode _statusCode;

        public GetSingleObjectStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _apiClient = new ApiClient();
        }


        [Given(@"Get operation for existing Id is performed")]
        public async Task GivenGetOperationForExistingIdIsPerformed(Table table)
        {
            var objectId = table.Rows[0].Id();
            _response = await  _apiClient.GetObjectAsync(objectId);
        }


        [Then(@"the response status should be (.*)")]
        public void ThenTheResponseStatusShouldBe(int statusCode)
        {
            ApiAssertions.AssertStatusCode(_response, statusCode);
        }


        [Then(@"the response should contain the object details")]
        public void ThenTheResponseShouldContainTheObjectDetails()
        {
            var content = HandleContent.GetContent<SingleObjectsResponse>(_response);
            content.Should().NotBeNull();
            content.Id.Should().Be("7");
            content.Name.Should().Be("Apple MacBook Pro 16");
            content.Data.Year.Should().Be(2019);
            content.Data.Price.Should().Be(1849.99);
            content.Data.CPUModel.Should().Be("Intel Core i9");
            content.Data.HardDiskSize.Should().Be("1 TB");
        }
    }
}