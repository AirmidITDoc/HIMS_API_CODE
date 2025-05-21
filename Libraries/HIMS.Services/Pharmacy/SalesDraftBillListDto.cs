using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class SalesDraftBillListDto
    {

        public long DsalesId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public long? RegID { get; set; }
        public string? AdmDoctorName { get; set; }
        public decimal? NetAmount { get; set; }
        public long? OP_IP_ID { get; set; }
        public long? OP_IP_Type { get; set; }
        public bool? IsPrint { get; set; }
        public long? IsPrescription { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public string? ExtMobileNo { get; set; }
        public string? ExtAddress { get; set; }
        public string? UserName { get; set; }

    }
}
