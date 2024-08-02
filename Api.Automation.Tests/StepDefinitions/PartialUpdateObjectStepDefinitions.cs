using System;
using Api.Automation.Models.Requests;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Api.Automation.Tests.Utility;
using NUnit.Framework;
using Api.Automation.Models.Response;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class PartialUpdateObjectStepDefinitions
    {
        private PartialUpdateObjectRequest _partialUpdateObjectRequest;
        private RestResponse _response;
        private ScenarioContext _scenarioContext;
        private IApiClient _apiClient;
        HttpStatusCode _statusCode;

        public PartialUpdateObjectStepDefinitions(PartialUpdateObjectRequest partialUpdateObjectRequest,
            ScenarioContext scenarioContext)
        {
            _partialUpdateObjectRequest = partialUpdateObjectRequest;
            _scenarioContext = scenarioContext;
            _apiClient = new ApiClient();
        }

        [Given(@"There is existing object Id (.*) with valid partial update payload '([^']*)'")]
        public void GivenThereIsExistingObjectIdWithValidPartialUpdatePayload(string id, string fileName)
        {
            string file = HandleContent.GetRequestFilePath(fileName);
            var payload = HandleContent.ParseJson<PartialUpdateObjectRequest>(file);
            _scenarioContext.Add("partialUpdateObject _payload", payload);
            _scenarioContext.Add("id _objectId", id);
        }

        [When(@"Send request to partial update object")]
        public async Task WhenSendRequestToPartialUpdateObject()
        {
            var partialUpdateObjectRequest =
                _scenarioContext.Get<PartialUpdateObjectRequest>("partialUpdateObject _payload");
            var objectId = _scenarioContext.Get<string>("id _objectId");
            try
            {
                _response =
                await _apiClient.PartialUpdateObjectAsync<PartialUpdateObjectRequest>(partialUpdateObjectRequest,
                    objectId);

                if (_response.StatusCode == HttpStatusCode.MethodNotAllowed)
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

        [Then(@"the response status of patch method should be (.*)")]
        public void ThenTheResponseStatusOfPatchMethodShouldBe(int statusCode)
        {
            ApiAssertions.AssertStatusCode(_response, statusCode);
        }

        [Then(@"The response should contain the updated name object")]
        public void ThenTheResponseShouldContainTheUpdatedNameObject()
        {
            var content = HandleContent.GetContent<UpdateObjectResponse>(_response);
            var id = _scenarioContext.Get<string>("id _objectId");
            var objectId = int.Parse(id);

            var updateObjectRequest = _scenarioContext.Get<UpdateObjectRequest>("partialUpdateObject _payload");

            Assert.AreEqual(content.Id, objectId);
            Assert.AreEqual(content.Name, updateObjectRequest.Name);
        }
    }
}