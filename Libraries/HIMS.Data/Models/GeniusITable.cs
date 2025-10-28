using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusITable
    {
        public string? Ipopflag { get; set; }
        public string? Pinno { get; set; }
        public string RefVisitno { get; set; } = null!;
        public string? Admissionno { get; set; }
        public DateTime Reqdatetime { get; set; }
        public string TestprofCode { get; set; } = null!;
        public string? Processed { get; set; }
        public string? Patfname { get; set; }
        public string? Patmname { get; set; }
        public string? Patlname { get; set; }
        public DateTime? PatDob { get; set; }
        public string? Gender { get; set; }
        public decimal? Patage { get; set; }
        public string? Ageunit { get; set; }
        public string? DocName { get; set; }
        public string? Patienttypeclass { get; set; }
        public decimal? Seqno { get; set; }
        public DateTime? Adddate { get; set; }
        public decimal? Addtime { get; set; }
        public string? Title { get; set; }
        public decimal? Labno { get; set; }
        public DateTime? Datestamp { get; set; }
        public string Paramcode { get; set; } = null!;
        public string? Paramname { get; set; }
        public string? Mresult { get; set; }
        public string? BcPrinted { get; set; }
    }
}
