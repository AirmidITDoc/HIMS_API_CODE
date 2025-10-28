using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Otdetail
    {
        public long OtdetailId { get; set; }
        public DateTime? TranDate { get; set; }
        public long? OpdIpdId { get; set; }
        public long? OtheaderNo { get; set; }
        public string? TheaterName { get; set; }
        public long? SurgeonName { get; set; }
        public long? AnaName { get; set; }
        public DateTime? OpDate { get; set; }
        public DateTime? Optime { get; set; }
        public string? OperationNotes { get; set; }
        public long? AnaType { get; set; }
        public string? BirthRegNo { get; set; }
        public long? PaediatricionName { get; set; }
        public long? ProcedureType { get; set; }
        public long? Sex { get; set; }
        public DateTime? BirthTime { get; set; }
        public bool? IsBirthTimechk { get; set; }
        public double? SurgeonAmt { get; set; }
        public double? AnaAmt { get; set; }
        public double? PadAmt { get; set; }
        public double? ProcedueAmt { get; set; }
    }
}
