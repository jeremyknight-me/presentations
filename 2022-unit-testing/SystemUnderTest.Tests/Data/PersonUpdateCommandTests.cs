using Moq;
using SystemUnderTest.Data;
using Xunit;

namespace SystemUnderTest.Tests.Data;

public class PersonUpdateCommandTests
{
    [Fact]
    public void Execute_NoPerson_False()
    {
        var mockPersonRepo = new Mock<IGetPersonByIdQuery>();
        mockPersonRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns((Person)null);
        var command = new PersonUpdateCommand(mockPersonRepo.Object);
        var actual = command.Execute(new Person());
        mockPersonRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        Assert.False(actual);

    }

    [Fact]
    public void Execute_Person_True()
    {
        var mockPersonRepo = new Mock<IGetPersonByIdQuery>();
        mockPersonRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Person());
        var command = new PersonUpdateCommand(mockPersonRepo.Object);
        var actual = command.Execute(new Person());
        mockPersonRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        Assert.True(actual);
    }
}
