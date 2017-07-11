using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Playground.MediatrPipelines
{

    public class SomePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private ILogger<TRequest> _logger;
        private IValidator<TRequest> _validator;

        public SomePipelineBehavior(ILogger<TRequest> logger, IValidator<TRequest> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("IPipelineBehavior executes BEFORE IRequestHandler in the registered order.");

            await _validator.ValidateAndThrowAsync(request);

            var response = await next();

            _logger.LogInformation("IPipelineBehavior executes AFTER IRequestHandler in the registered order.");

            return response;

        }
    }
}
