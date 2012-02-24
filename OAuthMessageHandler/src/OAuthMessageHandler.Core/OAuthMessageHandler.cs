using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using OAuth;
using System.Threading.Tasks;

namespace OAuthMessageHandler.Core {

    public class OAuthMessageHandler : DelegatingHandler {

        private static string _consumerKey = System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"];
        private static string _consumerSecret = System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"];
        private static string _token = System.Configuration.ConfigurationManager.AppSettings["AccessToken"];
        private static string _tokenSecret = System.Configuration.ConfigurationManager.AppSettings["AccessTokenSecret"];

        private OAuthBase _oauthBase = new OAuthBase();

        public OAuthMessageHandler(HttpMessageHandler innerHandler) 
            : base(innerHandler) { }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {

            // Compute OAuth header
            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oauthBase.GenerateSignature(
                request.RequestUri,
                _consumerKey,
                _consumerSecret,
                _token,
                _tokenSecret,
                request.Method.Method,
                _oauthBase.GenerateTimeStamp(),
                _oauthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", authHeader);
            return base.SendAsync(request, cancellationToken);
        }
    }
}