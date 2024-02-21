using DataPersistence;
using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints;
using SampleWeb.Repositories;
using SampleWeb.Requests;
using SampleWeb.Responses;
using SampleWeb.Validators;

namespace SampleWeb.Tests.Endpoints;

public class LookupPostEndpointTests
{
    private readonly ILookupRepository repo = A.Fake<ILookupRepository>();
    private readonly FakeItEasy.Configuration.IReturnValueArgumentValidationConfiguration<Task> call;
    private readonly LookupPostEndpoint sut;

    public LookupPostEndpointTests()
    {
        var validator = new LookupPostRequestValidator();
        this.call = A.CallTo(() => this.repo.AddAsync(A<Lookup>._));
        this.sut = new LookupPostEndpoint(this.repo, validator);
    }

    [Fact]
    public async Task Execute_Invalid()
    {
        var request = new LookupPostRequest { Name = string.Empty };

        var result = await this.sut.Execute(request);

        this.call.MustNotHaveHappened();
        Assert.IsType<ProblemHttpResult>(result);
    }

    [Fact]
    public async Task Execute_Valid()
    {
        var request = new LookupPostRequest { Name = "Valid Name" };

        var result = await this.sut.Execute(request);

        this.call.MustHaveHappenedOnceExactly();
        Assert.IsType<Created<LookupResponse>>(result);
        var created = result as Created<LookupResponse>;
        Assert.NotNull(created);
        Assert.NotNull(created.Value);
        Assert.Equal(created.Value.Name, request.Name);
    }
}
