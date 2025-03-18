using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorPerGroupWiseMaster
    {
        public long DocShareId { get; set; }
        public long? DoctorId { get; set; }
        public long? GroupId { get; set; }
        public decimal? ServicePercentage { get; set; }
        public long? ClassId { get; set; }
    }
}
