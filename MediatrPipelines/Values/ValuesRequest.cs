using MediatR;

namespace Playground.MediatrPipelines
{
    public class ValuesRequest : IRequest<string[]>
    {
        public string Search { get; set; }
    }
}
