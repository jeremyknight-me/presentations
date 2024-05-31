using FluentValidation;
using SampleWeb.Persistence;

namespace SampleWeb.Endpoints.Lookups.Create;

internal sealed class LookupCreateEndpoint
{
    private readonly ILookupRepository repository;
    private readonly IValidator<LookupCreateRequest> validator;

    public LookupCreateEndpoint(ILookupRepository lookupRepository, IValidator<LookupCreateRequest> requestValidator)
    {
        this.repository = lookupRepository;
        this.validator = requestValidator;
    }

    public async Task<IResult> Execute(LookupCreateRequest request)
    {
        var validationResult = this.validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var lookup = Lookup.Create(request.Name);
        await this.repository.AddAsync(lookup);
        var response = LookupResponse.Create(lookup);
        return Results.Created($"/lookups/{lookup.Id}", response);
    }
}
