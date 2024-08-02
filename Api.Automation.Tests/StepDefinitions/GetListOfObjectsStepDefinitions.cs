using System;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Api.Automation.Models.Response;
using Api.Automation.Tests.Utility;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class GetListOfObjectsStepDefinitions
    {
        private RestResponse _response;

        private IApiClient _apiClient;
        HttpStatusCode _statusCode;
        private readonly List<int> objectIds = new List<int>();

        public GetListOfObjectsStepDefinitions()
        {
            _apiClient = new ApiClient();
        }

        [Given(@"Get list of all objects is performed")]
        public async Task GivenGetListOfAllObjectsIsPerformed()
        {
            _response = await _apiClient.GetListOfObjectAsync();
        }

        [Then(@"the response status of all objects should be (.*)")]
        public void ThenTheResponseStatusOfAllObjectsShouldBe(int statusCode)
        {
            ApiAssertions.AssertStatusCode(_response, statusCode);
        }


        [Then(@"the response should contain list of objects")]
        public void ThenTheResponseShouldContainListOfObjects(Table table)
        {
            var expectedObjects = table.Rows.Select(row => new
            {
                id = row["id"],
                name = row["name"],
                data = row["data"] == "null" ? null : JsonConvert.DeserializeObject<Data>(row["data"])
            }).ToList();

            var actualObjects = HandleContent.GetContent<List<SingleObjectsResponse>>(_response);
            Assert.IsNotNull(actualObjects);
            Assert.AreEqual(13, actualObjects.Count);

            foreach (var expected in expectedObjects)
            {
                var match = actualObjects.FirstOrDefault(obj =>
                    obj.Id == expected.id &&
                    obj.Name == expected.name &&
                    ApiResponseValidator.AreObjectsEqual(obj.Data, expected.data));

                match.Should().NotBeNull($"Expected object with id {expected.id} and name {expected.name} not found");
            }
        }
    }
}