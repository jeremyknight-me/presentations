using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Data;
using ClassLibrary.Data.SqlClient;
using ClassLibrary.Objects;

namespace ClassLibrary.Presentation.Presenters
{
    public class PersonListPresenter
    {
        private readonly IPersonRepository repository;

        public PersonListPresenter()
        {
            this.repository = new PersonRepository();
        }

        public PersonListPresenter(IPersonRepository repositoryToUse)
        {
            this.repository = repositoryToUse;
        }

        public int ListAllCount(int maximumRows, int startRowIndex)
        {
            IEnumerable<Person> people = this.repository.GetAll();
            return people.Count();
        }

        public IEnumerable<Person> ListAll(int maximumRows, int startRowIndex)
        {
            IEnumerable<Person> people = this.repository.GetAll();
            return people.Skip(startRowIndex).Take(maximumRows);
        }
    }
}
