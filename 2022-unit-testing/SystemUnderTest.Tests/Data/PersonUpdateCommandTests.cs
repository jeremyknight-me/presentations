using FakeItEasy;
using SystemUnderTest.Data;
using Xunit;

namespace SystemUnderTest.Tests.Data;

public class PersonUpdateCommandTests
{
    [Fact]
    public void Execute_NoPerson_False()
    {
        var repository = A.Fake<IGetPersonByIdQuery>();
        var call = A.CallTo(() => repository.GetById(A<int>.Ignored));
        call.Returns(null);

        var command = new PersonUpdateCommand(repository);
        var actual = command.Execute(new Person());
        
        call.MustHaveHappenedOnceExactly();
        Assert.False(actual);
    }

    [Fact]
    public void Execute_Person_True()
    {
        var repository = A.Fake<IGetPersonByIdQuery>();
        var call = A.CallTo(() => repository.GetById(A<int>._));
        call.Returns(new Person());
        
        var command = new PersonUpdateCommand(repository);
        var actual = command.Execute(new Person());
        
        call.MustHaveHappenedOnceExactly();
        Assert.True(actual);
    }
}
