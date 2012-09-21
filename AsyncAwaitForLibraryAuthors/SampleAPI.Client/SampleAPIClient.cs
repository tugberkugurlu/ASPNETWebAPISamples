using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Client {

    public class SampleAPIClient {

        private const string ApiUri = "http://localhost:17257/api/cars";

        public async Task<IEnumerable<Car>> GetCarsAsync() {

            using (HttpClient client = new HttpClient()) {

                var response = await client.GetAsync(ApiUri).ConfigureAwait(continueOnCapturedContext: false);

                // Note the best way to handle it but will do the work for demo purposes
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<Car>>().ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public async Task<IEnumerable<Car>> GetCarsInAWrongWayAsync() {

            using (HttpClient client = new HttpClient()) {

                var response = await client.GetAsync(ApiUri);

                // Note the best way to handle it but will do the work for demo purposes
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<Car>>();
            }
        }
    }
}