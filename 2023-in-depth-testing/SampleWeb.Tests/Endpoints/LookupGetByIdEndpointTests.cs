using System.Linq.Expressions;
using DataPersistence;
using Microsoft.AspNetCore.Http.HttpResults;
using SampleWeb.Endpoints;
using SampleWeb.Repositories;
using SampleWeb.Responses;

namespace SampleWeb.Tests.Endpoints;

public class LookupGetByIdEndpointTests
{
    private readonly Mock<ILookupRepository> mockRepo;
    private readonly Expression<Func<ILookupRepository, Lookup>> repoMethod;
    private readonly LookupGetByIdEndpoint sut;

    public LookupGetByIdEndpointTests()
    {
        this.mockRepo = new Mock<ILookupRepository>();
        this.repoMethod = x => x.GetByIdAsync(It.IsAny<int>()).Result;
        this.sut = new LookupGetByIdEndpoint(this.mockRepo.Object);
    }

    [Fact]
    public async Task Execute_NotFound()
    {
        const Lookup? data = null;
        _ = this.mockRepo.Setup(this.repoMethod).Returns(data);

        var result = await this.sut.Execute(0);
        this.mockRepo.Verify(this.repoMethod, Times.Once);
        Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task Execute_Found()
    {
        var data = LookupFakerFactory.Make(1337, 1)[0];
        this.mockRepo.Setup(this.repoMethod).Returns(data);

        var result = await this.sut.Execute(0);
        this.mockRepo.Verify(this.repoMethod, Times.Once);
        Assert.IsType<Ok<LookupResponse>>(result);
        var ok = result as Ok<LookupResponse>;
        Assert.NotNull(ok);
        Assert.NotNull(ok.Value);
        Assert.True(ok.Value.Id > 0);
    }
}
