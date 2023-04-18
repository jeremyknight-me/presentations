using DataPersistence;
using FluentValidation;
using SampleWeb.Repositories;
using SampleWeb.Requests;

namespace SampleWeb.Endpoints;

internal sealed class LookupPostEndpoint
{
    private readonly ILookupRepository repository;
    private readonly IValidator<LookupPostRequest> validator;

    public LookupPostEndpoint(ILookupRepository lookupRepository, IValidator<LookupPostRequest> requestValidator)
    {
        this.repository = lookupRepository;
        this.validator = requestValidator;
    }

    public async Task<IResult> Execute(LookupPostRequest request)
    {
        var validationResult = this.validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var lookup = Lookup.Create(request.Name);
        await this.repository.AddAsync(lookup);
        return Results.Created($"/lookups/{lookup.Id}", lookup);
    }
}
