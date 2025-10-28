using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ViewDoctorshare
    {
        public long Doctorshareid { get; set; }
        public long? Doctorid { get; set; }
        public long? Serviceid { get; set; }
        public string? Doctorname { get; set; }
        public string? ServiceShortDesc { get; set; }
        public bool? Amounttype { get; set; }
        public double? DoctorAmount { get; set; }
    }
}
