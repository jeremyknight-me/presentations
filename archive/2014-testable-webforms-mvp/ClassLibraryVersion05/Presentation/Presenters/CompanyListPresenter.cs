using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Data;
using ClassLibrary.Data.SqlClient;
using ClassLibrary.Objects;

namespace ClassLibrary.Presentation.Presenters
{
    public class CompanyListPresenter
    {
        private readonly ICompanyRepository repository;

        public CompanyListPresenter()
        {
            this.repository = new CompanyRepository();
        }

        public CompanyListPresenter(ICompanyRepository repositoryToUse)
        {
            this.repository = repositoryToUse;
        }

        public int ListAllCount(int maximumRows, int startRowIndex)
        {
            IEnumerable<Company> companies = this.repository.GetAll();
            return companies.Count();
        }

        public IEnumerable<Company> ListAll(int maximumRows, int startRowIndex)
        {
            IEnumerable<Company> companies = this.repository.GetAll();
            return companies.Skip(startRowIndex).Take(maximumRows);
        }
    }
}
