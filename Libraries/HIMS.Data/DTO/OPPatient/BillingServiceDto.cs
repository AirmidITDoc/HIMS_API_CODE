﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class BillingServiceDto
    {
        public long ServiceId {  get; set; }
        public long? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? ServiceShortDesc { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public bool? IsEditable { get; set; }
        public bool? CreditedtoDoctor { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public bool? IsActive { get; set; }
        public long? PrintOrder { get; set; }
        public long? TariffId { get; set; }
        public string? TariffName { get; set; }
        public bool? IsEmergency { get; set; }
        public decimal? EmgAmt { get; set; }
   }
}