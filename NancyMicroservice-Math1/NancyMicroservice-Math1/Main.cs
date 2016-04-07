using Nancy;
using Nancy.ModelBinding;

namespace NancyMicroservice_Math1
{
    public class Main : NancyModule
    {
        public Main()
        {

            Get["/"] = _ => "Calculate Square.";

            Post["square"] = parameter =>
            {
                var data = this.Bind<Input>();
                return Response.AsJson(new Output { value = (data.value * data.value) });
            };

        }

    }

    public class Input
    {
        public int value { get; set; }
    }

    public class Output
    {
        public int value { get; set; }
    }

}