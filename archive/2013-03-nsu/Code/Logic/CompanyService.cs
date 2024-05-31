using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication.Code.Data;
using WebApplication.Code.Objects;

namespace WebApplication.Code.Logic
{
    public class CompanyService
    {
        private readonly ContactContext context;

        public CompanyService()
        {
            Database.SetInitializer<ContactContext>(null);
            this.context = new ContactContext();
        }

        public CompanyService(ContactContext contextToUse)
        {
            this.context = contextToUse;
        }

        public void Add(Company company)
        {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Company company = context.Companies.Single(x => x.Id == id);
            context.Companies.Remove(company);
            context.SaveChanges();
        }

        public IEnumerable<Company> GetAll()
        {
            //return context.Companies.OrderBy(x => x.Name).ToList();

            return (from c in context.Companies 
                    orderby c.Name 
                    select c).ToList();
        }

        public Company GetById(int id)
        {
            return context.Companies.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Company company)
        {
            Company companyToUpdate = context.Companies.Single(x => x.Id == company.Id);
            companyToUpdate.Name = company.Name;
            context.SaveChanges();
        }

    }
}