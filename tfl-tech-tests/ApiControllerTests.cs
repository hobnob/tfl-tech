using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using tfl_tech.Controllers;
using tfl_tech.Models;
using tfl_tech.Views;

namespace Tests
{
    class ApiControllerTests
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

            // Test with no URI
            Assert.Catch<ArgumentNullException>(() => new ApiController(null, "id", "key", mockHttpClient.Object));

            // Test with null ID
            Assert.Catch<ArgumentNullException>(() => new ApiController(new Uri("https://example.com"), null, "key", mockHttpClient.Object));

            // Test with empty ID
            Assert.Catch<ArgumentNullException>(() => new ApiController(new Uri("https://example.com"), "", "key", mockHttpClient.Object));

            // Test with null dev key
            Assert.Catch<ArgumentNullException>(() => new ApiController(new Uri("https://example.com"), "id", null, mockHttpClient.Object));

            // Test with empty dev key
            Assert.Catch<ArgumentNullException>(() => new ApiController(new Uri("https://example.com"), "id", "", mockHttpClient.Object));

            // Test with null HTTP Client
            Assert.Catch<ArgumentNullException>(() => new ApiController(new Uri("https://example.com"), "id", "key", null));
        }

        [Test]
        public void TestValidRoad()
        {
            ApiController controller = new ApiController(
                new Uri("https://example.com/"),
                "APP_ID",
                "DEVELOPER_ID",
                mockHttpClient.Object
            );

            // Get the result for the A2 and test for validity
            IView result = controller.GetRoad("A2");

            Assert.AreEqual(
                "The status of the A2 is as follows\r\n\tRoad Status is Good\r\n\tRoad Status Description is No Exceptional Delays",
                result.Output
            );
            Assert.AreEqual(0, result.StatusCode);
        }

        [Test]
        public void TestInvalidRoad()
        {
            ApiController controller = new ApiController(
                new Uri("https://example.com/"),
                "APP_ID",
                "DEVELOPER_ID",
                mockHttpClient.Object
            );

            // Get the result for the A2 and test for validity
            IView result = controller.GetRoad("A233");

            Assert.AreEqual("A233 is not a valid road", result.Output);
            Assert.AreEqual(1, result.StatusCode);
        }

        [Test]
        public void TestServerError()
        {
            ApiController controller = new ApiController(
                new Uri("https://example.com/"),
                "APP_ID",
                "DEVELOPER_ID",
                mockHttpClient.Object
            );

            // Get the result for the A2 and test for validity
            IView result = controller.GetRoad("A234");

            Assert.AreEqual("An unknown status was returned by the API (500)", result.Output);
            Assert.AreEqual(1, result.StatusCode);
        }
    }
}
