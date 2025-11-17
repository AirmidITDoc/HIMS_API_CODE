using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class PartsOfSurgeryMaster
    {
        public long PartsOfSurgeryId { get; set; }
        public long? RefId { get; set; }
        public long? RefType { get; set; }
        public string? NameOfSurgeryPart { get; set; }
        public string? ImgOfSurgeryPart { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
