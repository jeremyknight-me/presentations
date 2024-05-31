using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace WebApplication.Code.Data
{
    internal class AuditChangeProcessor
    {
        private readonly Dictionary<EntityState, string> strategies = new Dictionary<EntityState, string>();

        public AuditChangeProcessor()
        {
            this.DefineStrategies();
        }

        public IEnumerable<AuditLog> ProcessChanges(IEnumerable<DbEntityEntry> changedEntities, string userId)
        {
            if (changedEntities == null)
            {
                throw new ArgumentNullException("changedEntities");
            }

            var result = new List<AuditLog>();

            foreach (var entity in changedEntities)
            {
                var changes = this.GetAuditChangesForEntity(entity, userId);

                if (changes != null)
                {
                    result.AddRange(changes);
                }
            }

            return result;
        }

        private IEnumerable<AuditLog> GetAuditChangesForEntity(DbEntityEntry entry, string userId)
        {
            var result = new List<AuditLog>();

            var changeTime = DateTime.UtcNow;
            const string keyName = "Id";
            string tableName = this.GetTableName(entry);

            if (entry.State == EntityState.Added
                || entry.State == EntityState.Deleted)
            {
                // adjust RecordId if you have a multi-column key
                var log = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    EventDateStamp = changeTime,
                    EventType = this.strategies[entry.State],
                    TableName = tableName,
                    ColumnName = "ALL",
                    NewValue = null,
                    OriginalValue = null,
                    RecordId =
                        entry.State == EntityState.Added
                            ? entry.CurrentValues.GetValue<object>(keyName).ToString()
                            : entry.OriginalValues.GetValue<object>(keyName).ToString()
                };

                result.Add(log);
            }
            else if (entry.State == EntityState.Modified)
            {
                string eventType = this.strategies[entry.State];
                result.AddRange(
                    entry.OriginalValues.PropertyNames
                        .Where(propertyName => !object.Equals(entry.OriginalValues.GetValue<object>(propertyName), entry.CurrentValues.GetValue<object>(propertyName)))
                        .Select(propertyName =>
                            new AuditLog
                            {
                                Id = Guid.NewGuid(),
                                UserId = userId,
                                EventDateStamp = changeTime,
                                EventType = eventType,
                                TableName = tableName,
                                RecordId = entry.OriginalValues.GetValue<object>(keyName).ToString(),
                                ColumnName = propertyName,
                                OriginalValue = entry.OriginalValues.GetValue<object>(propertyName) == null
                                    ? null : entry.OriginalValues.GetValue<object>(propertyName).ToString(),
                                NewValue = entry.CurrentValues.GetValue<object>(propertyName) == null
                                    ? null : entry.CurrentValues.GetValue<object>(propertyName).ToString()
                            }));
            }

            return result;
        }

        private string GetTableName(DbEntityEntry entry)
        {
            var tableAttribute = entry.Entity.GetType()
                .GetCustomAttributes(typeof(TableAttribute), false)
                .SingleOrDefault() as TableAttribute;

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            Type entryType = entry.Entity.GetType();
            string name = entryType.Name;

            if (name.Contains("_"))
            {
                int position = name.IndexOf("_");
                return name.Substring(0, position);
            }

            return entryType.Name;
        }

        private void DefineStrategies()
        {
            this.strategies.Add(EntityState.Added, "C"); // created
            this.strategies.Add(EntityState.Deleted, "D"); // deleted
            this.strategies.Add(EntityState.Modified, "U"); // updated
        }
    }
}