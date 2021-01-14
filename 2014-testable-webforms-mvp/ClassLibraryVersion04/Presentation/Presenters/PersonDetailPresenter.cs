using ClassLibrary.Data;
using ClassLibrary.Data.SqlClient;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.ViewContracts;

namespace ClassLibrary.Presentation.Presenters
{
    public class PersonDetailPresenter
    {
        private readonly IPersonDetailView view;

        private readonly IPersonRepository personRepository;

        private readonly ICompanyRepository companyRepository;

        public PersonDetailPresenter(IPersonDetailView viewToUse)
        {
            this.view = viewToUse;
            this.personRepository = new PersonRepository();
            this.companyRepository = new CompanyRepository();
        }

        public PersonDetailPresenter(IPersonDetailView viewToUse, IPersonRepository personRepositoryToUse, ICompanyRepository companyRepositoryToUse)
        {
            this.view = viewToUse;
            this.personRepository = personRepositoryToUse;
            this.companyRepository = companyRepositoryToUse;
        }

        public void Delete()
        {
            this.personRepository.Delete(this.view.PersonIdField);
        }

        public void Load()
        {
            this.view.CompanyFieldItems = this.companyRepository.GetAll();

            if (this.view.PersonIdField > 0)
            {
                Person person = this.personRepository.GetById(this.view.PersonIdField);

                this.view.FirstNameField = person.FirstName;
                this.view.LastNameField = person.LastName;
                this.view.EmailAddressField = person.EmailAddress;
                this.view.PhoneNumberField = person.PhoneNumber;
                this.view.NotesField = person.Notes;
                this.view.BirthdayField = person.Birthday;
                this.view.CompanyIdField = person.CompanyId;

                this.view.CanUserDelete = true;
            }
        }

        public void Save()
        {
            var person = new Person
            {
                Id = this.view.PersonIdField,
                FirstName = this.view.FirstNameField,
                LastName = this.view.LastNameField,
                EmailAddress = this.view.EmailAddressField,
                PhoneNumber = this.view.PhoneNumberField,
                Notes = this.view.NotesField,
                Birthday = this.view.BirthdayField,
                CompanyId = this.view.CompanyIdField
            };

            if (this.view.PersonIdField > 0)
            {
                this.personRepository.Update(person);
            }
            else
            {
                this.personRepository.Add(person);
                this.view.PersonIdField = person.Id;
            }

            this.view.CanUserDelete = true;
        }
    }
}
