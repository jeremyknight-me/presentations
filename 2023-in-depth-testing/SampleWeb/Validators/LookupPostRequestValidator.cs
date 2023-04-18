using FluentValidation;
using SampleWeb.Requests;

namespace SampleWeb.Validators;

public sealed class LookupPostRequestValidator : AbstractValidator<LookupPostRequest>
{
    public LookupPostRequestValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
