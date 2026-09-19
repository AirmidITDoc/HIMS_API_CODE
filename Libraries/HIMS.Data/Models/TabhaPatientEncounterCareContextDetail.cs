using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TabhaPatientEncounterCareContextDetail
    {
        public long Peccid { get; set; }
        public string? AbhaNumber { get; set; }
        public string? AbhaAddress { get; set; }
        public long? RegId { get; set; }
        public long? OpIpId { get; set; }
        public int? OpIpType { get; set; }
        public string? PeMessage { get; set; }
        public string? PeErrMessage { get; set; }
        public string? PeHipId { get; set; }
        public string? PePatientReferenceNumber { get; set; }
        public string? PeCareContext { get; set; }
        public string? CcWorkflowId { get; set; }
        public string? CcMessage { get; set; }
        public string? CcHipId { get; set; }
        public string? CcErrMessage { get; set; }
    }
}
