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
    public class CompanyDetailPresenterTest
    {
        private ICompanyDetailView view;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockCompanyDetailView();
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
            ICompanyRepository repository = new StubICompanyRepository
            {
                DeleteInt32 = x =>
                {
                    methodCalled = true;
                }
            };

            var presenter = new CompanyDetailPresenter(this.view, repository);
            presenter.Delete();
            Assert.IsTrue(methodCalled);
        }

        [TestMethod]
        public void Load_NoId()
        {
            bool methodCalled = false;
            ICompanyRepository repository = new StubICompanyRepository
            {
                GetByIdInt32 = x =>
                {
                    methodCalled = true;
                    return new Company { Id = x, Name = "Test Company"};
                }
            };

            this.view.CompanyIdField = 0;

            var presenter = new CompanyDetailPresenter(this.view, repository);
            presenter.Load();

            Assert.IsFalse(methodCalled);
            Assert.IsFalse((this.view as MockCompanyDetailView).CanUserDelete);
        }

        [TestMethod]
        public void Load_Id()
        {
            bool methodCalled = false;
            ICompanyRepository repository = new StubICompanyRepository
            {
                GetByIdInt32 = x =>
                {
                    methodCalled = true;
                    return new Company { Id = x, Name = "Test Company" };
                }
            };

            this.view.CompanyIdField = 3;

            var presenter = new CompanyDetailPresenter(this.view, repository);
            presenter.Load();
            Assert.IsTrue(methodCalled);

            Assert.IsTrue((this.view as MockCompanyDetailView).CanUserDelete);
            Assert.AreEqual(3, this.view.CompanyIdField);
            Assert.AreEqual("Test Company", this.view.NameField);
        }

        [TestMethod]
        public void Save_NoId()
        {
            bool addCalled = false;
            bool updateCalled = false;
            ICompanyRepository repository = new StubICompanyRepository
            {
                AddCompany = x =>
                {
                    addCalled = true;
                },
                UpdateCompany = x =>
                {
                    updateCalled = true;
                }
            };

            this.view.CompanyIdField = 0;

            var presenter = new CompanyDetailPresenter(this.view, repository);
            presenter.Save();

            Assert.IsTrue(addCalled);
            Assert.IsFalse(updateCalled);
            Assert.IsTrue((this.view as MockCompanyDetailView).CanUserDelete);
        }

        [TestMethod]
        public void Save_Id()
        {
            bool addCalled = false;
            bool updateCalled = false;
            ICompanyRepository repository = new StubICompanyRepository
            {
                AddCompany = x =>
                {
                    addCalled = true;
                },
                UpdateCompany = x =>
                {
                    updateCalled = true;
                }
            };

            this.view.CompanyIdField = 3;

            var presenter = new CompanyDetailPresenter(this.view, repository);
            presenter.Save();

            Assert.IsFalse(addCalled);
            Assert.IsTrue(updateCalled);
            Assert.IsTrue((this.view as MockCompanyDetailView).CanUserDelete);
        }
    }
}
