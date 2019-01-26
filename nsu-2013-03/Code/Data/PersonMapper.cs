using System.Data.Entity;
using WebApplication.Code.Objects;

namespace WebApplication.Code.Data
{
    public class PersonMapper : IMapper
    {
        private readonly DbModelBuilder builder;

        public PersonMapper(DbModelBuilder builderToUse)
        {
            this.builder = builderToUse;
        }

        public void Map()
        {
            var mapping = this.builder.Entity<Person>();
            mapping.ToTable("Person");
            mapping.HasKey(x => x.Id);
            
            mapping.Ignore(x => x.BirthdayDisplay);
            mapping.Ignore(x => x.FullName);
            mapping.Ignore(x => x.SortableName);

            mapping.Property(x => x.FirstName).HasMaxLength(100);
            mapping.Property(x => x.MiddleName).HasMaxLength(100);
            mapping.Property(x => x.LastName).HasMaxLength(100);
            mapping.Property(x => x.Nickname).HasMaxLength(100);
            mapping.Property(x => x.EmailAddress).HasMaxLength(100);
            mapping.Property(x => x.PhoneNumber).HasMaxLength(100);
            mapping.Property(x => x.Street).HasMaxLength(100);
            mapping.Property(x => x.City).HasMaxLength(100);
            mapping.Property(x => x.State).HasMaxLength(2);
            mapping.Property(x => x.ZipCode).HasMaxLength(10);
        }
    }
}