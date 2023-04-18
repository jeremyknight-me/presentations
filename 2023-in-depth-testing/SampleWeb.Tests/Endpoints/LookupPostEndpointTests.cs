using System.Linq.Expressions;
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
    private readonly Mock<ILookupRepository> mockRepo;
    private readonly Expression<Func<ILookupRepository, Task>> repoMethod;
    private readonly LookupPostEndpoint sut;

    public LookupPostEndpointTests()
    {
        var validator = new LookupPostRequestValidator();
        this.mockRepo = new Mock<ILookupRepository>();
        this.repoMethod = x => x.AddAsync(It.IsAny<Lookup>());
        this.sut = new LookupPostEndpoint(this.mockRepo.Object, validator);
    }

    [Fact]
    public async Task Execute_Invalid()
    {
        this.mockRepo.Setup(this.repoMethod);
        var request = new LookupPostRequest { Name = string.Empty };

        var result = await this.sut.Execute(request);
        this.mockRepo.Verify(this.repoMethod, Times.Never);
        Assert.IsType<ProblemHttpResult>(result);
    }

    [Fact]
    public async Task Execute_Valid()
    {
        this.mockRepo.Setup(this.repoMethod);
        var request = new LookupPostRequest { Name = "Valid Name" };

        var result = await this.sut.Execute(request);
        this.mockRepo.Verify(this.repoMethod, Times.Once);

        Assert.IsType<Created<LookupResponse>>(result);
        var created = result as Created<LookupResponse>;
        Assert.NotNull(created);
        Assert.NotNull(created.Value);
        Assert.Equal(created.Value.Name, request.Name);
    }
}
