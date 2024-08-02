using Api.Automation.Models.Requests;
using Api.Automation.Models.Response;
using Api.Automation.Tests.Utility;
using RestSharp;
using System.Net;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class CreateObjectStepDefinitions
    {
        private AddObjectRequest _addObjectRequest;
        private RestResponse _response;
        private ScenarioContext _scenarioContext;
        private IApiClient _apiClient;
        HttpStatusCode _statusCode;

        public CreateObjectStepDefinitions(AddObjectRequest addObjectRequest, ScenarioContext scenarioContext)
        {
            _addObjectRequest = addObjectRequest;
            _scenarioContext  = scenarioContext;
            _apiClient = new ApiClient();
        }

        [Given(@"Object is created using AddObject api with '([^']*)'")]
        public void GivenObjectIsCreatedUsingAddObjectApiWith(string fileName)
        {
            string file = HandleContent.GetRequestFilePath(fileName);
            var payload = HandleContent.ParseJson<AddObjectRequest>(file);
            _scenarioContext.Add("addObject _payload", payload);
        }

        [When(@"Send request to add object")]
        public async Task WhenSendRequestToAddObject()
        {
            var addObjectRequest = _scenarioContext.Get<AddObjectRequest>("addObject _payload");
            _response = await _apiClient.AddObjectAsync<AddObjectRequest>(addObjectRequest);
        }

        [Then(@"the response of AddObject status should be (.*)")]
        public void ThenTheResponseOfAddObjectStatusShouldBe(int statusCode)
        {
            ApiAssertions.AssertStatusCode(_response, statusCode);
        }

        [Then(@"the object should be created with valid data")]
        public void ThenTheObjectShouldBeCreatedWithValidData()
        {
            var content = HandleContent.GetContent<AddObjectResponse>(_response);

            DateTime createdAt = DateTime.Parse(content.CreatedAt);
            DateTime createdAtUTC = createdAt.ToUniversalTime();
            DateTime currentUtcTime = DateTime.UtcNow;
            TimeSpan tolerance = TimeSpan.FromSeconds(3);
            DateTimeUtility.AssertDateTimeCloseTo(createdAtUTC, currentUtcTime, tolerance);
            content.Name.Should().Be("Apple MacBook Pro 16");
            content.Data.Year.Should().Be(2019);
            content.Data.Price.Should().Be(1849.99);
            content.Data.CPUModel.Should().Be("Intel Core i9");
            content.Data.HardDiskSize.Should().Be("1 TB");

        }

    }
}