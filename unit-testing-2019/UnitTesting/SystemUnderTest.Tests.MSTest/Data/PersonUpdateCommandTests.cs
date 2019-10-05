using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SystemUnderTest.Data;

namespace SystemUnderTest.Tests.MSTest.Data
{
    [TestClass]
    public class PersonUpdateCommandTests
    {
        [TestMethod]
        public void Execute_NoPerson_False()
        {
            var mockPersonRepo = new Mock<IPersonRepository>();
            mockPersonRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns((Person)null);
            var command = new PersonUpdateCommand(mockPersonRepo.Object);
            var actual = command.Execute(new Person());
            mockPersonRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_Person_True()
        {
            var mockPersonRepo = new Mock<IPersonRepository>();
            mockPersonRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Person());
            var command = new PersonUpdateCommand(mockPersonRepo.Object);
            var actual = command.Execute(new Person());
            mockPersonRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.IsTrue(actual);
        }
    }
}
