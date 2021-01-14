using System;

namespace WebApplication.Code.Data
{
    public class AuditLog
    {
        public AuditLog()
        {
            this.Id = Guid.NewGuid();
            this.EventDateStamp = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        public string UserId { get; set; }

        public DateTime EventDateStamp { get; set; }

        public string EventType { get; set; }

        public string TableName { get; set; }

        public string RecordId { get; set; }

        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }
    }
}