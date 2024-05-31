using System.Collections.Generic;
using ClassLibrary.Objects;

namespace ClassLibrary.Data
{
    public interface IPersonRepository
    {
        void Add(Person person);

        void Delete(int id);

        IEnumerable<Person> GetAll();

        Person GetById(int id);

        void Update(Person person);
    }
}
