using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PureTPLHttpClientNet40 {

    public class Car {

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }
    }

    class Program {

        static void Main(string[] args) {

            GetCarsAsync().Then(cars => {

                foreach (var c in cars)
                    Console.WriteLine(c.Make);

            }).Catch(info => {

                Console.WriteLine("Exception occured. Details:");
                Console.WriteLine("Exception : {0}", info.Exception.GetType());
                Console.WriteLine("Message : {0}", info.Exception.Message);

                return info.Handled();
            });

            Console.ReadLine();
        }

        public static Task<IEnumerable<Car>> GetCarsAsync() { 

            var uri = "http://localhost:12941/api/cars";
            var tcs = new TaskCompletionSource<IEnumerable<Car>>();

            HttpClient client = new HttpClient();

            return client.GetAsync(uri).Then<HttpResponseMessage, IEnumerable<Car>>(response => {

                // Note the best way to handle this but will do the work
                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsAsync<IEnumerable<Car>>().Then<IEnumerable<Car>, IEnumerable<Car>>(cars => {

                    try {

                        tcs.SetResult(cars);
                    }
                    catch (Exception ex) {

                        tcs.SetException(ex);
                    }

                    return tcs.Task;

                }, runSynchronously: true).Catch<IEnumerable<Car>>(info => {

                    tcs.SetException(info.Exception);
                    return new CatchInfoBase<Task<IEnumerable<Car>>>.CatchResult { Task = tcs.Task };

                }).Finally(() => client.Dispose(), runSynchronously: true);

            }, runSynchronously: true).Catch<IEnumerable<Car>>(info => {

                client.Dispose();
                tcs.SetException(info.Exception);
                return new CatchInfoBase<Task<IEnumerable<Car>>>.CatchResult { Task = tcs.Task };
            });
        }
    }
}