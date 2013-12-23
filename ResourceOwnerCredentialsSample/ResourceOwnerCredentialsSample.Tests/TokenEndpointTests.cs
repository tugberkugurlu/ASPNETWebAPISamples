using Microsoft.Owin.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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
                OAuth2Client client = new OAuth2Client(new Uri("http://localhost.fiddler:5000/token"));
                TokenResponse tokenResponse = await client.RequestResourceOwnerPasswordAsync("bob", "bob");
                
                Assert.NotNull(tokenResponse);
                Assert.NotNull(tokenResponse.AccessToken);
            }
        }
    }

    public class MyOAuth2Client : OAuth2Client
    {
        public MyOAuth2Client(Uri address, HttpClient client) : base(address)
        {
            _client = client;
        }
    }
}