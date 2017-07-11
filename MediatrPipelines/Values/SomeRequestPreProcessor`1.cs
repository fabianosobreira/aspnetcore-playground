using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Playground.MediatrPipelines
{
    public class SomeRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private ILogger<TRequest> _logger;

        public SomeRequestPreProcessor(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation("IRequestPreProcessor always executes BEFORE all IPipelineBehavior.");
            });
        }
    }
}
