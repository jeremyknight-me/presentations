using ClassLibrary.Data;
using ClassLibrary.Data.SqlClient;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.ViewContracts;

namespace ClassLibrary.Presentation.Presenters
{
    public class CompanyDetailPresenter
    {
        private readonly ICompanyDetailView view;

        private readonly ICompanyRepository repository;

        public CompanyDetailPresenter(ICompanyDetailView viewToUse)
        {
            this.view = viewToUse;
            this.repository = new CompanyRepository();
        }

        public CompanyDetailPresenter(ICompanyDetailView viewToUse, ICompanyRepository repositoryToUse)
        {
            this.view = viewToUse;
            this.repository = repositoryToUse;
        }

        public void Delete()
        {
            this.repository.Delete(this.view.CompanyIdField);
        }

        public void Load()
        {
            if (this.view.CompanyIdField > 0)
            {
                Company company = this.repository.GetById(this.view.CompanyIdField);
                this.view.NameField = company.Name;

                this.view.CanUserDelete = true;
            }
        }

        public void Save()
        {
            var company = new Company
            {
                Id = this.view.CompanyIdField,
                Name = this.view.NameField
            };

            if (this.view.CompanyIdField > 0)
            {
                this.repository.Update(company);
            }
            else
            {
                this.repository.Add(company);
                this.view.CompanyIdField = company.Id;
            }

            this.view.CanUserDelete = true;
        }
    }
}
