using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMrdfileReceived
    {
        public long RmdrecordId { get; set; }
        public DateTime RecievedDate { get; set; }
        public DateTime? RecievedTime { get; set; }
        public long UnitId { get; set; }
        public long Opipid { get; set; }
        public string Mrdno { get; set; } = null!;
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public bool IsInOut { get; set; }
        public long? OutFileId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
