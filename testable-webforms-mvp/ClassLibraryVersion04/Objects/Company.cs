namespace ClassLibrary.Objects
{
    public class Company
    {
        public Company()
        {
            this.IsDeleted = false;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
