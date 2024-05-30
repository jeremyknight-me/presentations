using FluentValidation;

namespace SampleWeb.Endpoints.Lookups.Create;

public sealed class LookupCreateRequestValidator : AbstractValidator<LookupCreateRequest>
{
    public LookupCreateRequestValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
