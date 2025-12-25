using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TConsentMaster
    {
        public long ConsentId { get; set; }
        public DateTime? ConsentDate { get; set; }
        public DateTime? ConsentTime { get; set; }
        public long? RefId { get; set; }
        public long? RefType { get; set; }
        public long? Opipid { get; set; }
        public long? Opiptype { get; set; }
        public long? ConsentTempId { get; set; }
        public string? ConsentName { get; set; }
        public long? ConsentDepartment { get; set; }
        public string? ConsentDescription { get; set; }
        public string? TransactionLabel { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
