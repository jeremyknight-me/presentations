using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.GetAll;
using SampleWeb.Persistence;

namespace SampleWeb.Tests.Endpoints;

public class LookupGetAllEndpointTests
{
    [Fact]
    public async Task Execute_NoResults()
    {
        var result = await this.RunTest(Enumerable.Empty<Lookup>().ToList());
        Assert.IsType<Ok<IEnumerable<LookupResponse>>>(result);
        var ok = result as Ok<IEnumerable<LookupResponse>>;
        Assert.NotNull(ok);
        Assert.NotNull(ok.Value);
        Assert.Empty(ok.Value);
    }

    [Fact]
    public async Task Execute_Results()
    {
        var data = LookupFakerFactory.Make(1337, 10);
        var result = await this.RunTest(data);
        Assert.IsType<Ok<IEnumerable<LookupResponse>>>(result);
        var ok = result as Ok<IEnumerable<LookupResponse>>;
        Assert.NotNull(ok);
        Assert.NotNull(ok.Value);
        Assert.NotEmpty(ok.Value);
        Assert.Equal(10, ok.Value.Count());
    }

    private async Task<IResult> RunTest(IReadOnlyList<Lookup> data)
    {
        var repo = A.Fake<ILookupRepository>();
        var call = A.CallTo(() => repo.GetAllAsync());
        call.Returns(data);

        var sut = new LookupGetAllEndpoint(repo);
        var result = await sut.Execute();

        call.MustHaveHappenedOnceExactly();
        return result;
    }
}
