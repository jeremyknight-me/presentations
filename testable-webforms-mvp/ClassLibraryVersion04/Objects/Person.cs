using System;

namespace ClassLibrary.Objects
{
    public class Person
    {
        public Person()
        {
            this.Birthday = null;
            this.CompanyId = null;
            this.IsDeleted = false;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string Notes { get; set; }

        public int? CompanyId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
