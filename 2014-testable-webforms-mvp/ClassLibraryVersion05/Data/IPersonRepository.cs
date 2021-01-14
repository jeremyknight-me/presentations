using System.Collections.Generic;
using ClassLibrary.Objects;

namespace ClassLibrary.Data
{
    public interface IPersonRepository
    {
        void Add(Person person);

        void Delete(int id);

        IEnumerable<Person> GetAll();

        IEnumerable<Person> GetByCompanyId(int companyId);

        Person GetById(int id);

        void Update(Person person);
    }
}
