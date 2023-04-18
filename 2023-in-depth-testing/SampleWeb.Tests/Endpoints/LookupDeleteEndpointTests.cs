using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints;
using SampleWeb.Repositories;

namespace SampleWeb.Tests.Endpoints;

public class LookupDeleteEndpointTests
{
    [Fact]
    public async Task Execute_ReturnsNoContent()
    {
        var mockRepo = new Mock<ILookupRepository>();
        Expression<Func<ILookupRepository, Task>> repoMethod = x => x.DeleteAsync(It.IsAny<int>());
        mockRepo.Setup(repoMethod);
        var sut = new LookupDeleteEndpoint(mockRepo.Object);

        var result = await sut.Execute(0);
        mockRepo.Verify(repoMethod, Times.Once);
        Assert.IsType<NoContent>(result);
    }
}
