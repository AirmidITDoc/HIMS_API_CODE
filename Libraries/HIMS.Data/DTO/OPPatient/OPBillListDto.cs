using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPBillListDto
    {
        public String PbillNo { get; set; }

        public DateTime BillTime { get; set; }
       // public long RegNo { get; set; }
       public string PatientName { get; set; }
       public string MobileNo { get; set; }
        public string DoctorName { get; set; }
        public DateTime VisitDate { get; set; }
        public string DepartmentName { get; set; }
        public decimal TotalAmt { get; set; }
        //public Double ConcessionAmt { get; set; }
        //public long PatientType { get; set; }
        public decimal NetPayableAmt { get; set; }

        // public int OPD_IPD_ID { get; set; }
        //public decimal PaidAmt { get; set; }

        //public decimal BalanceAmt { get; set; }

        //public decimal CashPay { get; set; }

        //public decimal ChequePay { get; set; }
        //public decimal CardPay { get; set; }

        //public decimal AdvUsedPay { get; set; }
        //public decimal OnlinePay { get; set; }


        // public Double PayCount { get; set; }
        public int IsCancelled { get; set; }


        
    }
}
