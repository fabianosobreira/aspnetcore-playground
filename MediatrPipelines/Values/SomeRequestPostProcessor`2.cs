using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Playground.MediatrPipelines
{

    public class SomeRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private ILogger<TRequest> _logger;

        public SomeRequestPostProcessor(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, TResponse response)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation("IRequestPostProcessor always executes AFTER all IPipelineBehavior.");
            });
        }
    }
}
