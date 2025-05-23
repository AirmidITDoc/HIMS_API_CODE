﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class OpeningBalModel
    {
        public long OpeningHId { get; set; }
        public long StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public string OpeningTime { get; set; }
        public long Addedby { get; set; }

    }


    public class OpeningTransactionModel
    {
        public long StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public string OpeningTime { get; set; }
        public long OpeningDocNo { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public double PerUnitPurRate { get; set; }
        public double PerUnitMrp { get; set; }
        public double VatPer { get; set; }
        public long BalQty { get; set; }
        public long Addedby { get; set; }
        public long updatedby { get; set; }

    }

    public class OpeningBalanceModel
    {
        public OpeningBalModel OpeningBal { get; set; }
        public List<OpeningTransactionModel> OpeningTransaction { get; set; }
    }
}
