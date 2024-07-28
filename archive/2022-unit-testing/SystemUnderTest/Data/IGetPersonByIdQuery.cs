namespace SystemUnderTest.Data;

public interface IGetPersonByIdQuery
{
    Person GetById(int id);
}
