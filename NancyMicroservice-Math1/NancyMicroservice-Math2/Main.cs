using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            Post["/pythagoras"] = _ =>
            {
                var input = this.Bind<Input>();
                
                var valueA = new Dictionary<string, string>
                {
                    { "value", input.a.ToString() }
                };
                var valueB = new Dictionary<string, string>
                {
                    { "value", input.b.ToString() }
                };

                var squareA = AsyncHelpers.RunSync<OutputSquare>(() => GetSquareResult(valueA));
                var squareB = AsyncHelpers.RunSync<OutputSquare>(() => GetSquareResult(valueB));

                var c = squareA.value + squareB.value;

                var valueC = new Dictionary<string, string>
                {
                    { "value", c.ToString() }
                };
                var squareRootC = AsyncHelpers.RunSync<OutputSquareRoot>(() => GetSquareRootResult(valueC));

                return Response.AsJson(new OutputPythagoras { value = squareRootC.value });

            };

        }

        private async Task<OutputSquare> GetSquareResult(Dictionary<string, string> pairs)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(pairs);
                var response = await client.PostAsync(URL_SQUARE, content);
                return await response.Content.ReadAsAsync<OutputSquare>();
            }
        }

        private async Task<OutputSquareRoot> GetSquareRootResult(Dictionary<string, string> pairs)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(pairs);
                var response = await client.PostAsync(URL_SQUARE_ROOT, content);
                return await response.Content.ReadAsAsync<OutputSquareRoot>();
            }
        }

        public class Input
        {
            public int a { get; set; }
            public int b { get; set; }
        }

        public class OutputPythagoras
        {
            public double value { get; set; }
        }

        public class OutputSquare
        {
            public int value { get; set; }
        }

        public class OutputSquareRoot
        {
            public double value { get; set; }
        }

    }
}