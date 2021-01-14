namespace SystemUnderTest.Data
{
    public interface IPersonRepository
    {
        Person GetById(int id);
    }
}
