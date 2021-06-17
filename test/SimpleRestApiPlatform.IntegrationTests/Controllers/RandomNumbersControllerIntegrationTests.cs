using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SimpleRestApiPlatform.IntegrationTests.Controllers
{
    public class RandomNumbersControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<SimpleRestApiPlatform.Startup>>
    {
        readonly WebApplicationFactory<SimpleRestApiPlatform.Startup> _simpleRestApiPlatformFactory;
        const string urlGetRandomInteger = "/api/RandomNumbers/GetRandomInteger";
        const string urlGetRandomIntegerBetweenTwoStringIntegerValues = "/api/RandomNumbers/GetRandomIntegerBetweenTwoStringIntegerValues";
        const string urlGetRandomIntegerBetweenTwoNumericIntegerValues = "/api/RandomNumbers/GetRandomIntegerBetweenTwoNumericIntegerValues";

        readonly List<HttpMethod> nonGetMethods = new List<HttpMethod> {
                HttpMethod.Post,
                HttpMethod.Delete,
                HttpMethod.Head,
                HttpMethod.Options,
                HttpMethod.Put,
                HttpMethod.Patch,
                HttpMethod.Trace
            };

        public RandomNumbersControllerIntegrationTests(WebApplicationFactory<SimpleRestApiPlatform.Startup> simpleRestApiPlatformFactory)
        {
            _simpleRestApiPlatformFactory = simpleRestApiPlatformFactory;
        }

        [Fact]
        public async Task When_making_a_GET_request_to_GetRandomInteger()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            //Act
            var response = await client.GetAsync(urlGetRandomInteger);
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(int.TryParse(responseContent, out int theOutputInteger));
        }

        [Fact]
        public async Task When_making_anything_other_than_a_GET_request_to_GetRandomInteger()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            var theUri = new UriBuilder("https://localhost" + urlGetRandomInteger);

            for (int i = 0; i < nonGetMethods.Count; i++)
            {
                var theHttpRequestMessage = new HttpRequestMessage(nonGetMethods[i], theUri.Uri);

                //Act
                var response = await client.SendAsync(theHttpRequestMessage);

                //Assert
                var allowedVerbs = response.Content.Headers.Allow;

                Assert.Null(response.Content.Headers.ContentType);
                Assert.Equal(1, allowedVerbs.Count);
                Assert.True(allowedVerbs.Contains("GET"));
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
        }

        [Fact]
        public async Task When_making_a_GET_request_to_GetRandomIntegerBetweenTwoStringIntegerValues()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            var theUri = new UriBuilder("https://localhost" + urlGetRandomIntegerBetweenTwoStringIntegerValues);
            theUri.Query = "minimumvalue=-1&maximumvalue=1";

            var theHttpRequestMessage = new HttpRequestMessage(HttpMethod.Get, theUri.Uri);

            //Act
            var response = await client.SendAsync(theHttpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(int.TryParse(responseContent, out int theOutputInteger));
        }

        [Fact]
        public async Task When_making_anything_other_than_a_GET_request_to_GetRandomIntegerBetweenTwoStringIntegerValues()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            var theUri = new UriBuilder("https://localhost" + urlGetRandomIntegerBetweenTwoStringIntegerValues);

            for (int i = 0; i < nonGetMethods.Count; i++)
            {
                var theHttpRequestMessage = new HttpRequestMessage(nonGetMethods[i], theUri.Uri);

                //Act
                var response = await client.SendAsync(theHttpRequestMessage);

                //Assert
                var allowedVerbs = response.Content.Headers.Allow;

                Assert.Null(response.Content.Headers.ContentType);
                Assert.Equal(1, allowedVerbs.Count);
                Assert.True(allowedVerbs.Contains("GET"));
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
        }

        [Fact]
        public async Task When_making_a_GET_request_to_GetRandomIntegerBetweenTwoNumericIntegerValues()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            var theUri = new UriBuilder("https://localhost" + urlGetRandomIntegerBetweenTwoNumericIntegerValues);
            theUri.Query = "minimumvalue=-1&maximumvalue=1";

            var theHttpRequestMessage = new HttpRequestMessage(HttpMethod.Get, theUri.Uri);

            //Act
            var response = await client.SendAsync(theHttpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(int.TryParse(responseContent, out int theOutputInteger));
        }

        [Fact]
        public async Task When_making_anything_other_than_a_GET_request_to_GetRandomIntegerBetweenTwoNumericIntegerValues()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            var theUri = new UriBuilder("https://localhost" + urlGetRandomIntegerBetweenTwoNumericIntegerValues);

            for (int i = 0; i < nonGetMethods.Count; i++)
            {
                var theHttpRequestMessage = new HttpRequestMessage(nonGetMethods[i], theUri.Uri);

                //Act
                var response = await client.SendAsync(theHttpRequestMessage);

                //Assert
                var allowedVerbs = response.Content.Headers.Allow;

                Assert.Null(response.Content.Headers.ContentType);
                Assert.Equal(1, allowedVerbs.Count);
                Assert.True(allowedVerbs.Contains("GET"));
                Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
            }
        }
    }
}
