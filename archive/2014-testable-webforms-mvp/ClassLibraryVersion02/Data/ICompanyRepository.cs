using ClassLibrary.Objects;

namespace ClassLibrary.Data
{
    public interface ICompanyRepository
    {
        void Add(Company company);

        void Delete(int id);

        Company GetById(int id);

        void Update(Company company);
    }
}
