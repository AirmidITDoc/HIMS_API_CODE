using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class NursingOxygenVentilatorListDto
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
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
