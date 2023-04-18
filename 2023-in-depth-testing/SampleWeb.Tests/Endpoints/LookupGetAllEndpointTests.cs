using System.Linq.Expressions;
using DataPersistence;
using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints;
using SampleWeb.Repositories;
using SampleWeb.Responses;

namespace SampleWeb.Tests.Endpoints;

public class LookupGetAllEndpointTests
{
    private readonly Mock<ILookupRepository> mockRepo;
    private readonly Expression<Func<ILookupRepository, IReadOnlyList<DataPersistence.Lookup>>> repoMethod;

    public LookupGetAllEndpointTests()
    {
        this.mockRepo = new Mock<ILookupRepository>();
        this.repoMethod = x => x.GetAllAsync().Result;
    }

    [Fact]
    public async Task Execute_NoResults()
    {
        this.mockRepo.Setup(this.repoMethod)
            .Returns(Enumerable.Empty<DataPersistence.Lookup>().ToList());
        var sut = new LookupGetAllEndpoint(this.mockRepo.Object);

        var result = await sut.Execute();
        this.mockRepo.Verify(this.repoMethod, Times.Once);
        Assert.IsType<Ok<IEnumerable<LookupGetResponse>>>(result);
        var ok = result as Ok<IEnumerable<LookupGetResponse>>;
        Assert.NotNull(ok);
        Assert.NotNull(ok.Value);
        Assert.Empty(ok.Value);
    }

    [Fact]
    public async Task Execute_Results()
    {
        var data = LookupFakerFactory.Make(1337, 10);
        this.mockRepo.Setup(this.repoMethod).Returns(data);
        var sut = new LookupGetAllEndpoint(this.mockRepo.Object);

        var result = await sut.Execute();
        this.mockRepo.Verify(this.repoMethod, Times.Once);
        Assert.IsType<Ok<IEnumerable<LookupGetResponse>>>(result);
        var ok = result as Ok<IEnumerable<LookupGetResponse>>;
        Assert.NotNull(ok);
        Assert.NotNull(ok.Value);
        Assert.NotEmpty(ok.Value);
        Assert.Equal(10, ok.Value.Count());
    }
}
