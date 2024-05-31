using System.Data.Entity;

namespace WebApplication.Code.Data
{
    public class AuditLogMapper : IMapper
    {
        private readonly DbModelBuilder builder;

        public AuditLogMapper(DbModelBuilder builderToUse)
        {
            this.builder = builderToUse;
        }

        public void Map()
        {
            var mapping = this.builder.Entity<AuditLog>();
            mapping.ToTable("AuditLog");
            mapping.HasKey(x => x.Id);
            mapping.Property(x => x.EventType).HasMaxLength(1);
            mapping.Property(x => x.UserId).HasMaxLength(50);
            mapping.Property(x => x.TableName).HasMaxLength(100);
            mapping.Property(x => x.RecordId).HasMaxLength(50);
            mapping.Property(x => x.ColumnName).HasMaxLength(100);
        }
    }
}