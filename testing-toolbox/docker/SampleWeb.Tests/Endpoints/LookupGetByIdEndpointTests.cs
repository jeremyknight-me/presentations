using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.GetById;
using SampleWeb.Persistence;

namespace SampleWeb.Tests.Endpoints;

public class LookupGetByIdEndpointTests
{
    private readonly ILookupRepository repo = A.Fake<ILookupRepository>();
    private readonly FakeItEasy.Configuration.IReturnValueArgumentValidationConfiguration<Task<Lookup?>> call;
    private readonly LookupGetByIdEndpoint sut;

    public LookupGetByIdEndpointTests()
    {
        this.call = A.CallTo(() => this.repo.GetByIdAsync(A<int>._));
        this.sut = new LookupGetByIdEndpoint(this.repo);
    }

    [Fact]
    public async Task Execute_NotFound()
    {
        const Lookup? data = null;
        this.call.Returns(data);

        var result = await this.sut.Execute(0);

        this.call.MustHaveHappenedOnceExactly();
        Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task Execute_Found()
    {
        var data = LookupFakerFactory.Make(1337, 1)[0];
        this.call.Returns(data);

        var result = await this.sut.Execute(0);

        this.call.MustHaveHappenedOnceExactly();
        Assert.IsType<Ok<LookupResponse>>(result);
        var ok = result as Ok<LookupResponse>;
        Assert.NotNull(ok);
        Assert.NotNull(ok.Value);
        Assert.True(ok.Value.Id > 0);
    }
}
