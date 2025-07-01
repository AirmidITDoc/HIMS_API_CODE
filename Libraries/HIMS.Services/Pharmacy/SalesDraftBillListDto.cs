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
        public long? OPIPID { get; set; }
        public long? OPIPType { get; set; }
        public bool? IsPrint { get; set; }
        public long? IsPrescription { get; set; }
        public string? ExtMobileNo { get; set; }
        public string? ExtAddress { get; set; }
        public string? UserName { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public string? OP_IP_No { get; set; }

    }
}
