using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MBankMaster
    {
        public long BankId { get; set; }
        public string? BankName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
