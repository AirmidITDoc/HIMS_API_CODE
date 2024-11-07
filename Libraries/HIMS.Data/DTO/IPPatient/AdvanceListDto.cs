using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class AdvanceListDto
    {
        public string PatientName { get; set; }
        public string RegNo { get; set; }
        public string IPDNo { get; set; }
        public string DoctorName { get; set; }
        public string RefDoctorName { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public string WardName { get; set; }
        public string AdvanceNo { get; set; }
        public string AdvanceAmount { get; set; }
        public string CashPayAmount { get; set; }
        public string ChequePayAmount { get; set; }
        public string CardPayAmount { get; set; }
        public string PayTMAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string RefundAmount { get; set; }
        public string UserName { get; set; }
    }

}
