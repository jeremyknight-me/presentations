using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication.Code.Data;
using WebApplication.Code.Objects;

namespace WebApplication.Code.Logic
{
    public class PersonService
    {
        public void Add(Person person)
        {
            using (var context = this.GetContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = this.GetContext())
            {
                Person person = context.Persons.Single(x => x.Id == id);
                context.Persons.Remove(person);
                context.SaveChanges();
            }
        }

        public IEnumerable<Person> GetAll()
        {
            using (var context = this.GetContext())
            {
                return context
                    .Persons
                    .Include("Company")
                    .OrderBy(x => x.LastName)
                    .ToList();
            }
        }

        public Person GetById(int id)
        {
            using (var context = this.GetContext())
            {
                return context.Persons.Single(x => x.Id == id);
            }
        }

        public void Update(Person person)
        {
            using (var context = this.GetContext())
            {
                Person personToUpdate = context.Persons.Single(x => x.Id == person.Id);
                personToUpdate.Birthday = person.Birthday;
                personToUpdate.City = person.City;
                personToUpdate.CompanyId = person.CompanyId;
                personToUpdate.EmailAddress = person.EmailAddress;
                personToUpdate.FirstName = person.FirstName;
                personToUpdate.LastName = person.LastName;
                personToUpdate.MiddleName = person.MiddleName;
                personToUpdate.Nickname = person.Nickname;
                personToUpdate.PhoneNumber = person.PhoneNumber;
                personToUpdate.State = person.State;
                personToUpdate.Street = person.Street;
                personToUpdate.ZipCode = person.ZipCode;
                context.SaveChanges();
            }
        }

        private ContactContext GetContext()
        {
            Database.SetInitializer<ContactContext>(null);
            return new ContactContext();
        }   
    }
}