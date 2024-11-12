using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class IPBillListDto
    {
        public long BillNo { get; set; }
        public long? OpdIpdId { get; set; }

        public string? PatientName { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? IsCancelled { get; set; }
        public string? PbillNo { get; set; }
        public DateTime? BillTime { get; set; }
    }
}
