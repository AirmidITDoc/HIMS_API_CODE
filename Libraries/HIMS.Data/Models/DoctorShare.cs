using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DoctorShare
    {
        public long Doctorshareid { get; set; }
        public long? Doctorid { get; set; }
        public long? Serviceid { get; set; }
        public int? Doctorpersentage { get; set; }
        public bool? Amounttype { get; set; }
        public double? DoctorAmount { get; set; }
    }
}
