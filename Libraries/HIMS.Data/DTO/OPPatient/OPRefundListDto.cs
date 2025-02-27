﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPRefundListDto
    {
        public long RefundId { get; set; }

        public DateTime RefundDate { get; set; }
        public DateTime RefundTime { get; set; }
        public string RefundNo { get; set; }
        public string PBillNo { get; set; }
        public long OPD_IPD_Type { get; set; }
        public long OPD_IPD_ID { get; set; }
        public string PatientName { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public decimal RefundAmount { get; set; }
        public long PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        public string Remark { get; set; }
        public string PaymentRemark { get; set; }
        public long TransactionId { get; set; }
        public long TransactionType { get; set; }
        public string BillId { get; set; }
        public string RefunDate { get; set; }
        public DateTime VisitDate { get; set; }
        public string RegNo { get; set; }
        public decimal TotalAmt { get; set; }
        public double ConcessionAmt { get; set; }
        public decimal NetPayableAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public string AddedBy { get; set; }
        public string RefDoctorName { get; set; }
        public string HospitalName { get; set; }
        public string DoctorName { get; set; }
        public string PatientType { get; set; }
        public string TariffName { get; set; }





    }
}
