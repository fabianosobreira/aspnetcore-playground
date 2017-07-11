using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Playground.MediatrPipelines.Validation
{
    public class ValidationExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var validationResult = new ValidationResult(validationException.Errors);
                validationResult.AddToModelState(context.ModelState, string.Empty);
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
