using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingOrygenVentilator
    {
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public long AdmissionId { get; set; }
        public int? Mode { get; set; }
        public string? TidolV { get; set; }
        public string? SetRange { get; set; }
        public string? Ipap { get; set; }
        public string? MinuteV { get; set; }
        public string? RateTotal { get; set; }
        public string? Epap { get; set; }
        public string? Peep { get; set; }
        public string? Pc { get; set; }
        public string? Mvpercentage { get; set; }
        public string? PrSup { get; set; }
        public string? Fio2 { get; set; }
        public string? Ie { get; set; }
        public string? OxygenRate { get; set; }
        public string? SaturationWithO2 { get; set; }
        public string? FlowTrigger { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
