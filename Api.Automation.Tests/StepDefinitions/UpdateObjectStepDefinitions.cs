using System.Net;
using Api.Automation.Models.Requests;
using Api.Automation.Models.Response;
using Api.Automation.Tests.Utility;
using NUnit.Framework;
using RestSharp;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class UpdateObjectStepDefinitions
    {
        private UpdateObjectRequest _updateObjectRequest;
        private RestResponse _response;
        private ScenarioContext _scenarioContext;
        private IApiClient _apiClient;
        HttpStatusCode _statusCode;

        public UpdateObjectStepDefinitions(UpdateObjectRequest updateObjectRequest, ScenarioContext scenarioContext)
        {
            _updateObjectRequest = updateObjectRequest;
            _scenarioContext = scenarioContext;
            _apiClient = new ApiClient();
        }

        [Given(@"There is existing object Id (.*) with valid update '([^']*)'")]
        public void GivenThereIsExistingObjectIdWithValidUpdate(string id, string fileName)
        {
            string file = HandleContent.GetRequestFilePath(fileName);
            var payload = HandleContent.ParseJson<UpdateObjectRequest>(file);
            _scenarioContext.Add("updateObject _payload", payload);
            _scenarioContext.Add("id _objectId", id);
        }

        [When(@"Send request to update object")]
        public async Task WhenSendRequestToUpdateObject()
        {
            var updateObjectRequest = _scenarioContext.Get<UpdateObjectRequest>("updateObject _payload");
            var objectId = _scenarioContext.Get<string>("id _objectId");
            try
            {
                _response = await _apiClient.UpdateObjectAsync<UpdateObjectRequest>(updateObjectRequest, objectId);

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

        [Then(@"the response status of put method should be (.*)")]
        public void ThenTheResponseStatusOfPutMethodShouldBe(int statusCode)
        {
            ApiAssertions.AssertStatusCode(_response, statusCode);
        }

        [Then(@"The response should contain the updated object details")]
        public void ThenTheResponseShouldContainTheUpdatedObjectDetails()
        {
            var content = HandleContent.GetContent<UpdateObjectResponse>(_response);
            var id = _scenarioContext.Get<string>("id _objectId");
            var objectId = int.Parse(id);

            var updateObjectRequest = _scenarioContext.Get<UpdateObjectRequest>("updateObject _payload");

            Assert.AreEqual(content.Id, objectId);
            Assert.AreEqual(content.Data!.Color, updateObjectRequest.Data!.Color);
        }
    }
}