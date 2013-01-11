using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTTPSWebAPISample.Client {
    
    class Program {

        static void Main(string[] args) {

            Console.WriteLine(GetStringAsync().Result);
            Console.ReadLine();
        }

        public static async Task<string> GetStringAsync() {

            using (HttpClient client = new HttpClient()) {

                return await client.GetStringAsync("https://localhost:44304/api/cars");
            }
        }
    }
}