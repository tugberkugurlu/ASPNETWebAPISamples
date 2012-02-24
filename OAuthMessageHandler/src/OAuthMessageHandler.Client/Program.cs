using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Json;
using System.Threading.Tasks;

namespace OAuthMessageHandler.Client {

    class Program {

        static string _address =
            "http://api.twitter.com/1/statuses/user_timeline.json?include_entities=true&include_rts=true&screen_name=tourismgeek&count=5";

        static void Main(string[] args) {

            // Create client and insert an OAuth message handler in the message path that
            // inserts an OAuth authentication header in the request
            HttpClient _client = new HttpClient(
                new OAuthMessageHandler.Core.OAuthMessageHandler(
                    new HttpClientHandler()
                )
            );

            #region _w/o await

            //_client.GetAsync(_address).ContinueWith(requestTask => {

            //    // Get HTTP response from completed task.
            //    HttpResponseMessage response = requestTask.Result;

            //    // Check that response was successful or throw exception
            //    response.EnsureSuccessStatusCode();

            //    // Read response asynchronously as JsonValue and write out tweet texts
            //    response.Content.ReadAsAsync<JsonArray>().ContinueWith(readTask => {

            //        JsonArray statuses = readTask.Result;
            //        Console.WriteLine("\nLast 5 statuses from ScottGu's twitter account:\n");
            //        foreach (var status in statuses) {

            //            Console.WriteLine(status["text"] + "\n");
            //        }

            //    });

            //});

            #endregion

            #region _with await

            readResponseAsync(_client);

            #endregion

            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        private static async Task<HttpResponseMessage> getResponseAsync(HttpClient httpClient) {

            var response = await httpClient.GetAsync(_address);

            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();

            return response;
        }

        private static async void readResponseAsync(HttpClient httpClient) {

            var response = await getResponseAsync(httpClient);
            var statuses = await response.Content.ReadAsAsync<JsonArray>();

            Console.WriteLine("\nLast 5 statuses from ScottGu's twitter account:\n");
            foreach (var status in statuses) {

                Console.WriteLine(status["text"] + "\n");
            }
        }
    }
}