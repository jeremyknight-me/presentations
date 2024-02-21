using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints;
using SampleWeb.Repositories;

namespace SampleWeb.Tests.Endpoints;

public class LookupDeleteEndpointTests
{
    [Fact]
    public async Task Execute_ReturnsNoContent()
    {
        var repo = A.Fake<ILookupRepository>();
        var call = A.CallTo(() => repo.DeleteAsync(A<int>._));

        var sut = new LookupDeleteEndpoint(repo);
        var result = await sut.Execute(0);

        call.MustHaveHappenedOnceExactly();
        Assert.IsType<NoContent>(result);
    }
}
