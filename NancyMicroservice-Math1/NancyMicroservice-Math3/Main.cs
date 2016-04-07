using Nancy;
using Nancy.ModelBinding;
using System;

namespace NancyMicroservice_Math3
{
    public class Main : NancyModule
    {
        public Main()
        {
            Get["/"] = _ => "Calculate Square Roots.";

            Post["squareroots"] = parameter =>
            {
                var data = this.Bind<Input>();
                return Response.AsJson(new Output { value = Math.Sqrt(data.value) });
            };
        }
    }

    public class Input
    {
        public int value { get; set; }
    }

    public class Output
    {
        public double value { get; set; }
    }

}