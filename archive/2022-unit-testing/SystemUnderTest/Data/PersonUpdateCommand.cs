namespace SystemUnderTest.Data;

public class PersonUpdateCommand
{
    private readonly IGetPersonByIdQuery personRepo;

    public PersonUpdateCommand(IGetPersonByIdQuery personRepository)
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
