using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPathologyReportDetail
    {
        public long PathReportDetId { get; set; }
        public long? PathReportId { get; set; }
        public long? CategoryId { get; set; }
        public long? TestId { get; set; }
        public long? SubTestId { get; set; }
        public long? ParameterId { get; set; }
        public string? ResultValue { get; set; }
        public long? UnitId { get; set; }
        public string? NormalRange { get; set; }
        public long? PrintOrder { get; set; }
        public long? PisNumeric { get; set; }
        public string? CategoryName { get; set; }
        public string? TestName { get; set; }
        public string? SubTestName { get; set; }
        public string? ParameterName { get; set; }
        public string? UnitName { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? SampleId { get; set; }
    }
}
