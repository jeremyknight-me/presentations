using System.Data.Entity;
using WebApplication.Code.Objects;

namespace WebApplication.Code.Data
{
    public class CompanyMapper : IMapper
    {
        private readonly DbModelBuilder builder;

        public CompanyMapper(DbModelBuilder builderToUse)
        {
            this.builder = builderToUse;
        }

        public void Map()
        {
            var mapping = this.builder.Entity<Company>();
            mapping.ToTable("Company");
            mapping.HasKey(x => x.Id);
            mapping.Property(x => x.Name).HasMaxLength(100);
        }
    }
}