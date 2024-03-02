using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDoctorShareDetail
    {
        public long DocDetId { get; set; }
        public long? DocShareId { get; set; }
        public long? GroupId { get; set; }
        public long? ChargesId { get; set; }
    }
}
