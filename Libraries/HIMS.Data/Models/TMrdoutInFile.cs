using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMrdoutInFile
    {
        public long OutFileId { get; set; }
        public long Opipid { get; set; }
        public string? OutNo { get; set; }
        public long GivenUserId { get; set; }
        public string? PersonName { get; set; }
        public DateTime OutDate { get; set; }
        public DateTime OutTime { get; set; }
        public string? OutReason { get; set; }
        public string? InNo { get; set; }
        public DateTime? InDate { get; set; }
        public DateTime? InTime { get; set; }
        public long? ReturnUserId { get; set; }
        public string? ReturnPersonName { get; set; }
        public string? InReason { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
