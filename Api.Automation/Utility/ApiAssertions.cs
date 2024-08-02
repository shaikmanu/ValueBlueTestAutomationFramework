using FluentAssertions;
using RestSharp;
using System.Net;

namespace Api.Automation.Tests.Utility
{
    public static class ApiAssertions
    {
        public static void AssertStatusCode(RestResponse response, int expectedStatusCode)
        {
            response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }
    }
}
