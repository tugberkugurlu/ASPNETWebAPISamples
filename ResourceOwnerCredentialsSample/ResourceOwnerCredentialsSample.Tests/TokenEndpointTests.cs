using Microsoft.Owin.Testing;
using System;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using Xunit;

namespace ResourceOwnerCredentialsSample.Tests
{
    public class TokenEndpointTests
    {
        [Fact]
        public async Task Should_Validate_And_Issue_Access_Token_When_Resource_Owner_Credentials_Are_Correct()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                OAuth2Client client = new OAuth2Client(new Uri("http://whatever:5000/token"), server.Handler);
                TokenResponse tokenResponse = await client.RequestResourceOwnerPasswordAsync("bob", "bob");

                Assert.NotNull(tokenResponse);
                Assert.NotNull(tokenResponse.AccessToken);
            }
        }
    }
}