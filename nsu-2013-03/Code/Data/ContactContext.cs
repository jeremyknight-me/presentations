using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using WebApplication.Code.Objects;

namespace WebApplication.Code.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext()
            : base("Contact")
        {
        }

        public IDbSet<Company> Companies { get; set; }

        public IDbSet<Person> Persons { get; set; }

        public IDbSet<AuditLog> AuditLogs { get; set; }

        public override int SaveChanges()
        {
            return this.SaveChanges("system");
        }

        public int SaveChanges(string userId)
        {
            this.AuditChanges(userId);
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mappers = new Queue<IMapper>();
            mappers.Enqueue(new AuditLogMapper(modelBuilder));
            mappers.Enqueue(new CompanyMapper(modelBuilder));
            mappers.Enqueue(new PersonMapper(modelBuilder));

            while (mappers.Count > 0)
            {
                IMapper mapper = mappers.Dequeue();
                mapper.Map();
            }
        }

        private void AuditChanges(string userId)
        {
            var changedEntities =
                this.ChangeTracker.Entries()
                    .Where(p => p.State == EntityState.Added
                        || p.State == EntityState.Deleted
                        || p.State == EntityState.Modified);

            var changeProcessor = new AuditChangeProcessor();
            foreach (var auditLog in changeProcessor.ProcessChanges(changedEntities, userId))
            {
                this.AuditLogs.Add(auditLog);
            }
        }
    }
}