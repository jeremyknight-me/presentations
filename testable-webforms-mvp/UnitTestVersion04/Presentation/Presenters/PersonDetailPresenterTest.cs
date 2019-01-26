using System.Collections.Generic;
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
    public class PersonDetailPresenterTest
    {
        private IPersonDetailView view;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockPersonDetailView();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.view = null;
        }

        [TestMethod]
        public void Delete_EnsureCalled()
        {
            bool methodCalled = false;
            IPersonRepository repository = new StubIPersonRepository
            {
                DeleteInt32 = x =>
                {
                    methodCalled = true;
                }
            };

            var presenter = new PersonDetailPresenter(this.view, repository, null);
            presenter.Delete();
            Assert.IsTrue(methodCalled);
        }

        [TestMethod]
        public void Load_NoId()
        {
            bool getAllCalled = false;
            bool getByIdCalled = false;

            ICompanyRepository companyRepository = new StubICompanyRepository
            {
                GetAll = () =>
                {
                    getAllCalled = true;
                    return new List<Company>();
                }
            };

            IPersonRepository personRepository = new StubIPersonRepository
            {
                GetByIdInt32 = x =>
                {
                    getByIdCalled = true;
                    return new Person
                    {
                        FirstName = "John",
                        LastName = "Doe"
                    };
                }
            };

            this.view.PersonIdField = 0;

            var presenter = new PersonDetailPresenter(this.view, personRepository, companyRepository);
            presenter.Load();

            Assert.IsTrue(getAllCalled);
            Assert.IsFalse(getByIdCalled);
            Assert.IsFalse((this.view as MockPersonDetailView).CanUserDelete);
        }

        [TestMethod]
        public void Load_Id()
        {
            bool getAllCalled = false;
            bool getByIdCalled = false;

            ICompanyRepository companyRepository = new StubICompanyRepository
            {
                GetAll = () =>
                {
                    getAllCalled = true;
                    return new List<Company>();
                }
            };

            IPersonRepository personRepository = new StubIPersonRepository
            {
                GetByIdInt32 = x =>
                {
                    getByIdCalled = true;
                    return new Person
                    {
                        FirstName = "John",
                        LastName = "Doe"
                    };
                }
            };

            this.view.PersonIdField = 3;

            var presenter = new PersonDetailPresenter(this.view, personRepository, companyRepository);
            presenter.Load();

            Assert.IsTrue(getAllCalled);
            Assert.IsTrue(getByIdCalled);
            Assert.IsTrue((this.view as MockPersonDetailView).CanUserDelete);
            Assert.AreEqual(3, this.view.PersonIdField);
            Assert.AreEqual("John", this.view.FirstNameField);
            Assert.AreEqual("Doe", this.view.LastNameField);
        }

        [TestMethod]
        public void Save_NoId()
        {
            bool addCalled = false;
            bool updateCalled = false;
            IPersonRepository repository = new StubIPersonRepository
            {
                AddPerson = x =>
                {
                    addCalled = true;
                },
                UpdatePerson = x =>
                {
                    updateCalled = true;
                }
            };

            this.view.PersonIdField = 0;

            var presenter = new PersonDetailPresenter(this.view, repository, null);
            presenter.Save();

            Assert.IsTrue(addCalled);
            Assert.IsFalse(updateCalled);
            Assert.IsTrue((this.view as MockPersonDetailView).CanUserDelete);
        }

        [TestMethod]
        public void Save_Id()
        {
            bool addCalled = false;
            bool updateCalled = false;
            IPersonRepository repository = new StubIPersonRepository
            {
                AddPerson = x =>
                {
                    addCalled = true;
                },
                UpdatePerson = x =>
                {
                    updateCalled = true;
                }
            };

            this.view.PersonIdField = 3;

            var presenter = new PersonDetailPresenter(this.view, repository, null);
            presenter.Save();

            Assert.IsFalse(addCalled);
            Assert.IsTrue(updateCalled);
            Assert.IsTrue((this.view as MockPersonDetailView).CanUserDelete);
        }
    }
}
