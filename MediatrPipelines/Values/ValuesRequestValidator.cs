using FluentValidation;

namespace Playground.MediatrPipelines
{
    public class ValuesRequestValidator : AbstractValidator<ValuesRequest>
    {
        public ValuesRequestValidator()
        {
            RuleFor(request => request.Search).MinimumLength(3);
        }
    }
}
