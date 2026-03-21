using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class CanteenBillListDo
    {
        public string CustomerName { get; set; }
        public string BillNo { get; set; }
        public DateTime BDate { get; set; }

        public string PBillNo { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal BalanceAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal NetAmount { get; set; }
        //public string OP_IP_ID { get; set; }
        //public long ReqId { get; set; }
    }
}
