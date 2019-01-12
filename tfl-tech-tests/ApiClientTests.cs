using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Net;
using tfl_tech.Models;

namespace Tests
{
    public class ApiClientTests
    {
        private Mock<IHttpClient> mockHttpClient;

        [SetUp]
        public void Setup()
        {
            mockHttpClient = new Mock<IHttpClient>();
            mockHttpClient.SetupGet(m => m.BaseAddress).Returns(new System.Uri("https://example.com"));

            // Set up our road responses - A2 returns a valid response
            mockHttpClient
                .Setup(m => m.Get("https://example.com/Road/A2?app_id=APP_ID&app_key=DEVELOPER_ID"))
                .Returns(
                    new HttpResponseMessage() {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(@"
                            [
                                {
                                    ""$type"": ""Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities"",
                                    ""id"": ""a2"",
                                    ""displayName"": ""A2"",
                                    ""statusSeverity"": ""Good"",
                                    ""statusSeverityDescription"": ""No Exceptional Delays"",
                                    ""bounds"": ""[[-0.0857,51.44091],[0.17118,51.49438]]"",
                                    ""envelope"": ""[[-0.0857,51.44091],[-0.0857, 51.49438],[0.17118, 51.49438],[0.17118, 51.44091],[-0.0857, 51.44091]]"",
                                    ""url"": ""/Road/a2""
                                }
                            ]
                        ")
                    }
                )
            ;

            // Set up our road responses - A233 returns a 'Not Found'
            mockHttpClient
                .Setup(m => m.Get("https://example.com/Road/A233?app_id=APP_ID&app_key=DEVELOPER_ID"))
                .Returns(
                    new HttpResponseMessage() {
                        StatusCode = HttpStatusCode.NotFound,
                        Content = new StringContent(@"
                            {
                                ""$type"": ""Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities"",
                                ""timestampUtc"": ""2017-11-21T14:37:39.7206118Z"",
                                ""exceptionType"": ""EntityNotFoundException"",
                                ""httpStatusCode"": 404,
                                ""httpStatus"": ""NotFound"",
                                ""relativeUri"": ""/Road/A233"",
                                ""message"": ""The following road id is not recognised: A233""
                            }
                        ")
                    }
                )
            ;

            // Set up our road responses - A234 returns a server error
            mockHttpClient
                .Setup(m => m.Get("https://example.com/Road/A234?app_id=APP_ID&app_key=DEVELOPER_ID"))
                .Returns(
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                )
            ;
        }

        [Test]
        public void TestConstructor()
        {
            HttpClientWrapper httpClient = new HttpClientWrapper(new HttpClient());

            // Test with no params
            Assert.Catch<System.ArgumentNullException>(() => new ApiClient(null, "id", "key"));

            // Test with empty base address
            Assert.Catch<System.ArgumentException>(() => new ApiClient(httpClient, "id", "key"));

            httpClient.BaseAddress = new System.Uri("https://example.com");

            // Test with null App ID
            Assert.Catch<System.ArgumentNullException>(() => new ApiClient(httpClient, null, "key"));

            // Test with empty App ID
            Assert.Catch<System.ArgumentNullException>(() => new ApiClient(httpClient, "", "key"));

            // Test with null Dev Key
            Assert.Catch<System.ArgumentNullException>(() => new ApiClient(httpClient, "id", null));

            // Test with empty Dev Key
            Assert.Catch<System.ArgumentNullException>(() => new ApiClient(httpClient, "id", ""));
        }

        [Test]
        public void TestValidResponse()
        {
            ApiClient client = new ApiClient(mockHttpClient.Object, "APP_ID", "DEVELOPER_ID");

            // Get the result for the A2 and test for validity
            RoadStatus a2Result = client.GetRoadStatus("A2");

            Assert.AreEqual("A2", a2Result.displayName);
            Assert.AreEqual("Good", a2Result.statusSeverity);
            Assert.AreEqual("No Exceptional Delays", a2Result.statusSeverityDescription);
        }

        [Test]
        public void TestInvalidRoad()
        {
            ApiClient client = new ApiClient(mockHttpClient.Object, "APP_ID", "DEVELOPER_ID");

            // Get the result for the A233 - should throw an exception
            Assert.Catch<System.ArgumentException>(() => client.GetRoadStatus("A233"));
        }

        [Test]
        public void TestServerError()
        {
            ApiClient client = new ApiClient(mockHttpClient.Object, "APP_ID", "DEVELOPER_ID");

            // Get the result for the A233 - should throw an exception
            Assert.Catch<System.Exception>(() => client.GetRoadStatus("A234"));
        }
    }
}