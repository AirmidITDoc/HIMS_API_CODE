using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class HimsLoginConfirmation
    {
        public string ConfirmationId { get; set; } = null!;
        public long UserId { get; set; }
        public int LoginType { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
