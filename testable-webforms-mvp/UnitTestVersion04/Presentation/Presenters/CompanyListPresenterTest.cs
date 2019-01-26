using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Data;
using ClassLibrary.Data.Fakes;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.Presenters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Presentation.Presenters
{
    [TestClass]
    public class CompanyListPresenterTest
    {
        [TestMethod]
        public void ListAllCount_NoRecords_ZeroReturn()
        {
            bool methodCalled = false;
            ICompanyRepository repository = new StubICompanyRepository
            {
                GetAll = () =>
                {
                    methodCalled = true;
                    return new List<Company>();
                }
            };

            var presenter = new CompanyListPresenter(repository);
            int actual = presenter.ListAllCount(10, 0);
            Assert.IsTrue(methodCalled);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ListAll_NoRecords_NoReturn()
        {
            bool methodCalled = false;
            ICompanyRepository repository = new StubICompanyRepository
            {
                GetAll = () =>
                {
                    methodCalled = true;
                    return new List<Company>();
                }
            };

            var presenter = new CompanyListPresenter(repository);
            IEnumerable<Company> actual = presenter.ListAll(10, 0);
            Assert.IsTrue(methodCalled);
            Assert.AreEqual(0, actual.Count());
        }
    }
}
