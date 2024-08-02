using System.Net;
using Api.Automation.Models.Response;
using Api.Automation.Tests.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Api.Automation.Tests.StepDefinitions
{
    [Binding]
    public class GetListOfObjectsByIdsStepDefinitions
    {
        private RestResponse _response;

        private IApiClient _apiClient;
        HttpStatusCode _statusCode;
        private readonly List<int> objectIds = new List<int>();

        public GetListOfObjectsByIdsStepDefinitions()
        {
            _apiClient = new ApiClient();
        }

        [Given(@"Get list of objects by multiple Ids is performed")]
        public void GivenGetListOfObjectsByMultipleIdsIsPerformed(Table table)
        {
            foreach (var row in table.Rows)
            {
                objectIds.Add(int.Parse(row["Id"]));
            }
        }

        [When(@"Request is send to get the objects by these Ids")]
        public async Task WhenRequestIsSendToGetTheObjectsByTheseIds()
        {
            _response = await _apiClient.GetListOfObjectByIdsAsync(objectIds);
        }

        [Then(@"the response status of objects should be (.*)")]
        public void ThenTheResponseStatusOfObjectsShouldBe(int statusCode)
        {
            ApiAssertions.AssertStatusCode(_response, statusCode);
        }


        [Then(@"the response should contain the valid objects details")]
        public void ThenTheResponseShouldContainTheValidObjectsDetails(Table table)
        {
            var expectedObjects = table.Rows.Select(row => new
            {
                id = row["id"],
                name = row["name"],
                data = row["data"] == "null" ? null : JsonConvert.DeserializeObject<Data>(row["data"])
            }).ToList();



            var actualObjects = HandleContent.GetContent<List<SingleObjectsResponse>>(_response);
            actualObjects.Should().NotBeNull("The actual objects list should not be null.");

            foreach (var expected in expectedObjects)
            {
                var match = actualObjects.FirstOrDefault(obj =>
                    obj.Id == expected.id &&
                    obj.Name == expected.name &&
                    ApiResponseValidator.AreObjectsEqual(obj.Data, expected.data));

                match.Should().NotBeNull($"Expected object with id {expected.id} and name {expected.name} not found");
            }
        }

        private bool AreDataEqual(Data actualData, Data expectedData)
        {
            if (expectedData == null)
                return actualData == null;

            if (actualData == null)
                return false;

            // Perform a deep comparison of Data objects
            return JToken.DeepEquals(JToken.FromObject(actualData), JToken.FromObject(expectedData));
        }



        /*var expectedObjects = table.Rows.Select(row => new
        {
            id   = row["id"],
            name = row["name"]
        });

        foreach (var expected in expectedObjects)
        {
            var match = actualObjects.FirstOrDefault(x => x.Id == expected.id && x.Name == expected.name);
            match.Should().NotBeNull($"Expected object with id {expected.id} and name {expected.name} not found");
        }*/
    }

}
