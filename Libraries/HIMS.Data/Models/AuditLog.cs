using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AuditLog
    {
        public int Id { get; set; }
        public int ActionId { get; set; }
        public int? ActionById { get; set; }
        public string? ActionByName { get; set; }
        public int? EntityId { get; set; }
        public string? EntityName { get; set; }
        public string? Description { get; set; }
        public string? AdditionalInfo { get; set; }
        public int LogTypeId { get; set; }
        public int LogSourceId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
