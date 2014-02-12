using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // ref: http://stackoverflow.com/questions/12533533/async-reading-chunked-content-with-httpclient-from-asp-net-webapi
            // ref: http://stackoverflow.com/questions/16998/reading-chunked-response-with-httpwebresponse

            // HttpGetForLargeFileInWrongWay();
            // HttpGetForLargeFileInRightWay();
        }

        static void HttpGetForLargeFileInWrongWay()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:22381");
                HttpResponseMessage response = client.GetAsync("/api/file").Result;
                using (Stream streamToReadFrom = response.Content.ReadAsStreamAsync().Result)
                {
                    string fileToWriteTo = Path.GetTempFileName();
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        streamToReadFrom.CopyTo(streamToWriteTo);
                    }
                }
            }
        }

        static void HttpGetForLargeFileInRightWay()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:22381");
                HttpResponseMessage response = client.GetAsync("/api/file", HttpCompletionOption.ResponseHeadersRead).Result;
                using (Stream streamToReadFrom = response.Content.ReadAsStreamAsync().Result)
                {
                    string fileToWriteTo = Path.GetTempFileName();
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        streamToReadFrom.CopyTo(streamToWriteTo);
                    }
                }
            }
        }
    }
}
