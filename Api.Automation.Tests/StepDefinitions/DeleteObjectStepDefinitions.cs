using System;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;
using Api.Automation.Models.Response;
using Api.Automation.Tests.Utility;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class DeleteObjectStepDefinitions
    {
        private RestResponse _response;
        private ScenarioContext _scenarioContext;
        private IApiClient _apiClient;
        HttpStatusCode _statusCode;

        public DeleteObjectStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _apiClient = new ApiClient();
        }


        [Given(@"Delete operation for existing Id is performed")]
        public async Task GivenDeleteOperationForExistingIdIsPerformed(Table table)
        {
            var objectId = table.Rows[0].Id();
            try
            {
                _response = await _apiClient.DeleteObjectAsync(objectId);

                if(_response.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    throw new InvalidOperationException($"Method not allowed: {_response.Content}");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException($"Error during delete operation: {ex.Message}");
            }

        }

        [Then(@"the delete response status should be (.*)")]
        public async Task ThenTheDeleteResponseStatusShouldBe(int statusCode)
        {

            ApiAssertions.AssertStatusCode(_response, statusCode);

        }

        [Then(@"the object should no longer exist")]
        public async Task ThenTheObjectShouldNoLongerExist()
        {
            var content = HandleContent.GetContent<DeleteObjectResponse>(_response);
            Assert.AreEqual(content.Message, "Object with id = 6, has been deleted.");
        }
    }
}