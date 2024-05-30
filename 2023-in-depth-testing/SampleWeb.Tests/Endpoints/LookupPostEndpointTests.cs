using DataPersistence;
using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.Create;

namespace SampleWeb.Tests.Endpoints;

public class LookupPostEndpointTests
{
    private readonly ILookupRepository repo = A.Fake<ILookupRepository>();
    private readonly FakeItEasy.Configuration.IReturnValueArgumentValidationConfiguration<Task> call;
    private readonly LookupCreateEndpoint sut;

    public LookupPostEndpointTests()
    {
        var validator = new LookupCreateRequestValidator();
        this.call = A.CallTo(() => this.repo.AddAsync(A<Lookup>._));
        this.sut = new LookupCreateEndpoint(this.repo, validator);
    }

    [Fact]
    public async Task Execute_Invalid()
    {
        var request = new LookupCreateRequest { Name = string.Empty };

        var result = await this.sut.Execute(request);

        this.call.MustNotHaveHappened();
        Assert.IsType<ProblemHttpResult>(result);
    }

    [Fact]
    public async Task Execute_Valid()
    {
        var request = new LookupCreateRequest { Name = "Valid Name" };

        var result = await this.sut.Execute(request);

        this.call.MustHaveHappenedOnceExactly();
        Assert.IsType<Created<LookupResponse>>(result);
        var created = result as Created<LookupResponse>;
        Assert.NotNull(created);
        Assert.NotNull(created.Value);
        Assert.Equal(created.Value.Name, request.Name);
    }
}
