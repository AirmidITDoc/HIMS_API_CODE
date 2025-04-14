using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MLvwRtrvPathologyResultOpwithAge
    {
        public long TestDetId { get; set; }
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public long? SubTestId { get; set; }
        public string? SubTestName { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public string? ParameterShortName { get; set; }
        public long? ServiceId { get; set; }
        public long PathparaRangeId { get; set; }
        public string MinValue { get; set; } = null!;
        public string MaxValue { get; set; } = null!;
        public string UnitName { get; set; } = null!;
        public long? UnitId { get; set; }
        public string? SuggestionNote { get; set; }
        public string ResultValue { get; set; } = null!;
        public string NormalRange { get; set; } = null!;
        public long? PathTestId { get; set; }
        public long PathReportId { get; set; }
        public long? OpdIpdId { get; set; }
        public long? OpdIpdType { get; set; }
        public bool? IsCompleted { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public long? SexId { get; set; }
        public string? AgeType { get; set; }
        public string Formula { get; set; } = null!;
        public long? ParaIsNumeric { get; set; }
        public string ParaBoldFlag { get; set; } = null!;
        public long? CategoryId { get; set; }
    }
}
