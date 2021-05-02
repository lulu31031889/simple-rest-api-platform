using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleRestApiPlatform.IntegrationTests.Controllers
{
    public class HelloWorldIntegrationTests
        : IClassFixture<WebApplicationFactory<SimpleRestApiPlatform.Startup>>
    {
        readonly WebApplicationFactory<SimpleRestApiPlatform.Startup> _simpleRestApiPlatformFactory;
        const string urlHelloWorld = "/api/helloworld";

        public HelloWorldIntegrationTests(WebApplicationFactory<SimpleRestApiPlatform.Startup> simpleRestApiPlatformFactory)
        {
            _simpleRestApiPlatformFactory = simpleRestApiPlatformFactory;
        }

        [Fact]
        public async Task When_making_a_GET_request_to_HelloWorld()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();

            //Act
            var response = await client.GetAsync(urlHelloWorld);
            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("\"Hello World!\"", responseContent);
        }

        [Fact]
        public async Task When_making_anything_other_than_a_GET_request_to_HelloWorld()
        {
            //Arrange
            var client = _simpleRestApiPlatformFactory.CreateClient();
            var nonGetMethods = new List<HttpMethod> {
                HttpMethod.Post,
                HttpMethod.Delete,
                HttpMethod.Head,
                HttpMethod.Options,
                HttpMethod.Put,
                HttpMethod.Patch,
                HttpMethod.Trace
            };

            for (int i = 0; i < nonGetMethods.Count; i++)
            {
                var theHttpRequestMessage = new HttpRequestMessage(nonGetMethods[i], urlHelloWorld);

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
