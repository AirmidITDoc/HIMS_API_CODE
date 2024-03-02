using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Obst
    {
        public long ObstId { get; set; }
        public long? OpdIpdId { get; set; }
        public DateTime? TranDate { get; set; }
        public string? Lmp { get; set; }
        public string? EddbyLmp { get; set; }
        public string? SingleTwinPregnancyWks { get; set; }
        public string? SingleTwinPregnancyDays { get; set; }
        public string? CardiacActivity { get; set; }
        public string? Fhs { get; set; }
        public string? FetalMovements { get; set; }
        public string? Placenta { get; set; }
        public string? Grade { get; set; }
        public string? Liquor { get; set; }
        public string? InternalOs { get; set; }
        public string? CervicalLength { get; set; }
        public string? PresentationLie { get; set; }
        public string? Bpdcms { get; set; }
        public string? Bpdwks { get; set; }
        public string? HeadCricms { get; set; }
        public string? HeadCriWks { get; set; }
        public string? AbdCircms { get; set; }
        public string? AbdCriWks { get; set; }
        public string? Femurcms { get; set; }
        public string? FemurWks { get; set; }
        public string? Crlcms { get; set; }
        public string? Clrwks { get; set; }
        public string? Msdcms { get; set; }
        public string? Msdwks { get; set; }
        public string? TheFetalMaturity { get; set; }
        public string? ApproxWeight { get; set; }
        public string? ApproxEddbyUsg { get; set; }
        public string? Anomalies { get; set; }
        public string? Comments { get; set; }
    }
}
