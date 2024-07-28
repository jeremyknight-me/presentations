using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Data;
using ClassLibrary.Data.Fakes;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.Presenters;
using ClassLibrary.Presentation.ViewContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Presentation.MockViews;

namespace UnitTest.Presentation.Presenters
{
    [TestClass]
    public class PersonListPresenterTest
    {
        private IPersonListView view;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockPersonListView();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.view = null;
        }

        [TestMethod]
        public void ListAllCount_NoFilter()
        {
            bool getAllCalled = false;
            bool getByCompanyIdCalled = false;
            IPersonRepository personRepository = new StubIPersonRepository
            {
                GetAll = () =>
                {
                    getAllCalled = true;
                    return new List<Person>();
                },
                GetByCompanyIdInt32 = x =>
                {
                    getByCompanyIdCalled = true;
                    return new List<Person>();
                }
            };

            var presenter = new PersonListPresenter(this.view, personRepository, null);
            int actual = presenter.ListAllCount(10, 0);
            Assert.IsTrue(getAllCalled);
            Assert.IsFalse(getByCompanyIdCalled);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ListAll_NoFilter()
        {
            bool getAllCalled = false;
            bool getByCompanyIdCalled = false;
            IPersonRepository personRepository = new StubIPersonRepository
            {
                GetAll = () =>
                {
                    getAllCalled = true;
                    return new List<Person>();
                },
                GetByCompanyIdInt32 = x =>
                {
                    getByCompanyIdCalled = true;
                    return new List<Person>();
                }
            };

            var presenter = new PersonListPresenter(this.view, personRepository, null);
            IEnumerable<Person> actual = presenter.ListAll(10, 0);
            Assert.IsTrue(getAllCalled);
            Assert.IsFalse(getByCompanyIdCalled);
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void ListAllCount_Filter()
        {
            bool getAllCalled = false;
            bool getByCompanyIdCalled = false;
            IPersonRepository personRepository = new StubIPersonRepository
            {
                GetAll = () =>
                {
                    getAllCalled = true;
                    return new List<Person>();
                },
                GetByCompanyIdInt32 = x =>
                {
                    getByCompanyIdCalled = true;
                    return new List<Person>();
                }
            };

            (this.view as MockPersonListView).SelectedCompanyIdField = 2;

            var presenter = new PersonListPresenter(this.view, personRepository, null);
            int actual = presenter.ListAllCount(10, 0);
            Assert.IsFalse(getAllCalled);
            Assert.IsTrue(getByCompanyIdCalled);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ListAll_Filter()
        {
            bool getAllCalled = false;
            bool getByCompanyIdCalled = false;
            IPersonRepository personRepository = new StubIPersonRepository
            {
                GetAll = () =>
                {
                    getAllCalled = true;
                    return new List<Person>();
                },
                GetByCompanyIdInt32 = x =>
                {
                    getByCompanyIdCalled = true;
                    return new List<Person>();
                }
            };

            (this.view as MockPersonListView).SelectedCompanyIdField = 2;

            var presenter = new PersonListPresenter(this.view, personRepository, null);
            IEnumerable<Person> actual = presenter.ListAll(10, 0);
            Assert.IsFalse(getAllCalled);
            Assert.IsTrue(getByCompanyIdCalled);
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void Load_Ensure()
        {
            bool methodCalled = false;

            IPersonRepository personRepository = new StubIPersonRepository();
            ICompanyRepository companyRepository = new StubICompanyRepository
            {
                GetAll = () =>
                {
                    methodCalled = true;
                    return new List<Company> { new Company { Id = 1, Name = "Test Company"}};
                }
            };

            var presenter = new PersonListPresenter(this.view, personRepository, companyRepository);
            presenter.Load();
            Assert.IsTrue(methodCalled);
            Assert.AreEqual(1, (this.view as MockPersonListView).CompanyFilterFieldItems.Count());
        }
    }
}
