using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAbhaErrorLog
    {
        public long Id { get; set; }
        public string? ErrorId { get; set; }
        public string? RequestId { get; set; }
        public string? Message { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? Path { get; set; }
        public string? FacilityId { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}
