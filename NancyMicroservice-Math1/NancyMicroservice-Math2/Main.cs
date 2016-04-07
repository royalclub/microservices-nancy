using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace NancyMicroservice_Math2
{
    public class Main : NancyModule
    {
        private const String URL_SQUARE = "http://localhost:56496/square";
        private const String URL_SQUARE_ROOT = "http://localhost:57205/squareroots";

        public Main()
        {

            Get["/"] = _ => "Calculate Pythagoras";

            //formule: a^2 + b^2 = c^2
            //with this function you can only get 'c'.
            Get["/pythagoras"] = _ =>
            {
/*                int value = doCall(URL_SQUARE, new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("value", "5")
                }));

                return value;*/
                GetPOSTResponse();
                return 1;

            };

        }

/*
        private int doCall(String url, FormUrlEncodedContent content)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            var result = client.PostAsync("", content).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;
            var resultItem = JsonConvert.DeserializeObject<KeyValuePair<string, int>>(resultContent);
            return resultItem.Value;
        }
*/

        private async void GetPOSTResponse()
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                    {
                       { "value", "5" }
                    };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(URL_SQUARE, content);

                var responseString = await response.Content.ReadAsStringAsync();
                Output responseObject = await response.Content.ReadAsAsync<Output>();
                int i = 1;
            }
        }

        public class Input
        {
            public int a { get; set; }
            public int b { get; set; }
        }

        public class Output
        {
            public int value { get; set; }
        }


    }
}