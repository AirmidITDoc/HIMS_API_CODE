using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DoctorShareprocessListDto
    {
       
        public long DoctorPayoutId { get; set; }
        public string? DoctorName { get; set; }

        public string? Mobile { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public DateTime? ProcessDate { get; set; }


        public decimal? NetAmount { get; set; }


        public decimal? DoctorAmount { get; set; }
        public decimal? HospitalAmount { get; set; }


        public decimal? TDSAmount { get; set; }


        //public bool? OPD_IPD_Type { get; set; }
        //public long? OPD_IPD_Id { get; set; }
        public string? ServiceName { get; set; }

        public double? Addcharges { get; set; }

       

    }
}
