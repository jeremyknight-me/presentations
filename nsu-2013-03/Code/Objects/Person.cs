using System;

namespace WebApplication.Code.Objects
{
    public class Person
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public DateTime? Birthday { get; set; }

        public string BirthdayDisplay
        {
            get { return this.Birthday.HasValue ? this.Birthday.Value.ToShortDateString() : string.Empty; }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName)
                    && string.IsNullOrEmpty(this.LastName))
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(this.FirstName))
                {
                    return this.LastName;
                }

                return string.IsNullOrEmpty(this.LastName) 
                    ? this.FirstName 
                    : string.Concat(this.FirstName, " ", this.LastName);
            }
        }

        public string SortableName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName)
                    && string.IsNullOrEmpty(this.LastName))
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(this.FirstName))
                {
                    return this.LastName;
                }

                if (string.IsNullOrEmpty(this.MiddleName))
                {
                    return string.IsNullOrEmpty(this.LastName)
                        ? this.FirstName
                        : string.Concat(this.LastName, ", ", this.FirstName);
                }

                return string.IsNullOrEmpty(this.LastName)
                    ? this.FirstName
                    : string.Format("{0}, {1} {2}", this.LastName, this.FirstName, this.MiddleName);
            }
        }
    }
}