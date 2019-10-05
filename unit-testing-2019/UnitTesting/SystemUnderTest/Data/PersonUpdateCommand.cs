namespace SystemUnderTest.Data
{
    public class PersonUpdateCommand
    {
        private readonly IPersonRepository personRepo;

        public PersonUpdateCommand() : this(new PersonRepository())
        {
        }

        public PersonUpdateCommand(IPersonRepository personRepository)
        {
            this.personRepo = personRepository;
        }

        public bool Execute(Person person)
        {
            // run validation logic

            var dbPerson = this.personRepo.GetById(person.Id);
            if (dbPerson == null)
            {
                return false;
            }

            // run update logic 

            return true;
        }
    }
}
