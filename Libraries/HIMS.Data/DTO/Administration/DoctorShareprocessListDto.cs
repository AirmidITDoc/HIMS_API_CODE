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
        public byte? OPdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Addcharges { get; set; }
        public long? BillNo { get; set; }
        public long? PaymentId { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? balanceAmt { get; set; }
        public DateTime? PaymentDate { get; set; }

    }
}
