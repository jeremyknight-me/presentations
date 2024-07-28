using System.Collections.Generic;
using ClassLibrary.Objects;

namespace ClassLibrary.Data
{
    public interface ICompanyRepository
    {
        void Add(Company company);

        void Delete(int id);

        IEnumerable<Company> GetAll();

        Company GetById(int id);

        void Update(Company company);
    }
}
