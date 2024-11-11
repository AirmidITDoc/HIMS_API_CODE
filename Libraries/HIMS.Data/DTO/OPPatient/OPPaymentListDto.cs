using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPPaymentListDto
    {
       // public long RegNo { get; set; }
      //  public long RegId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string MobileNo { get; set; }
        public string OPDNo { get; set; }
        public string PBillNo { get; set; }

        public decimal BillAmount { get; set; }
        public decimal BalanceAmt { get; set; }

        public decimal CashPay { get; set; }
        public decimal ChequePay { get; set; }

        public decimal CardPay { get; set; }
        public decimal AdvUsedPay { get; set; }

        public decimal OnlinePay { get; set; }
      //  public decimal AdvUsedPay { get; set; }


    }
}
