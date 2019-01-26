using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Data;
using ClassLibrary.Data.SqlClient;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.ViewContracts;

namespace ClassLibrary.Presentation.Presenters
{
    public class PersonListPresenter
    {
        private readonly IPersonListView view;

        private readonly IPersonRepository personRepository;

        private readonly ICompanyRepository companyRepository;

        public PersonListPresenter(IPersonListView viewToUse)
        {
            this.view = viewToUse;
            this.personRepository = new PersonRepository();
            this.companyRepository = new CompanyRepository();
        }

        public PersonListPresenter(IPersonListView viewToUse, IPersonRepository personRepositoryToUse, ICompanyRepository companyRepositoryToUse)
        {
            this.view = viewToUse;
            this.personRepository = personRepositoryToUse;
            this.companyRepository = companyRepositoryToUse;
        }

        public void Clear()
        {
            this.view.SelectedCompanyIdField = null;
        }

        public int ListAllCount(int maximumRows, int startRowIndex)
        {
            IEnumerable<Person> people = this.view.SelectedCompanyIdField.HasValue 
                ? this.personRepository.GetByCompanyId(this.view.SelectedCompanyIdField.Value) 
                : this.personRepository.GetAll();
            return people.Count();
        }

        public IEnumerable<Person> ListAll(int maximumRows, int startRowIndex)
        {
            IEnumerable<Person> people = this.view.SelectedCompanyIdField.HasValue
                ? this.personRepository.GetByCompanyId(this.view.SelectedCompanyIdField.Value)
                : this.personRepository.GetAll();
            return people.Skip(startRowIndex).Take(maximumRows);
        }

        public void Load()
        {
            this.view.CompanyFilterFieldItems = this.companyRepository.GetAll();
        }
    }
}
