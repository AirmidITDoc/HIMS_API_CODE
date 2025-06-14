﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPBillservicedetailListDto
    {
        public long ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public double TotalAmt { get; set; }
        public decimal NetAmount { get; set; }
        public long DoctorId { get; set; }
        public decimal BalanceAmount { get; set; }
        public string? ChargesDocName { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public long? IsCancelled { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal BalAmt { get; set; }
        public long BillNo { get; set; }
        public object? ServiceWiseDisc { get; set; }

    }
}
