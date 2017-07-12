using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Playground.MediatrPipelines
{
    public class ValuesRequestHandler : IRequestHandler<ValuesRequest, string[]>
    {
        private ILogger<ValuesRequestHandler> _logger;

        public ValuesRequestHandler(ILogger<ValuesRequestHandler> logger)
        {
            _logger = logger;
        }

        public string[] Handle(ValuesRequest message)
        {
            _logger.LogInformation("IRequestHandler handles the request.");

            // Allways a failure.
            //var failures = new ValidationFailure[] { new ValidationFailure(nameof(ValuesRequestHandler), "ooooppps...") };
            //throw new ValidationException(failures);

            return new string[] { "value-1", "value-2", };
        }
    }
}
