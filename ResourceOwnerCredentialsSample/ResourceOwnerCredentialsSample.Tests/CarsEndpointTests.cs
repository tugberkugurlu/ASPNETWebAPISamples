using Microsoft.Owin.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using Xunit;

namespace ResourceOwnerCredentialsSample.Tests
{
    public class CarsEndpointTests
    {
        [Fact]
        public async Task Should_Call_The_Cars_Endpoint_With_A_Valid_Access_Token()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                OAuth2Client client = new OAuth2Client(new Uri("http://whatever:18008/token"), server.Handler);
                TokenResponse tokenResponse = await client.RequestResourceOwnerPasswordAsync("bob", "bob");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://whatever:18008/api/cars");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                HttpResponseMessage response = await server.HttpClient.SendAsync(request);

                Assert.True(response.IsSuccessStatusCode);
            }
        }
    }
}