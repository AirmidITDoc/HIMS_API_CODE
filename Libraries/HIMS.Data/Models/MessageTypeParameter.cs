using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MessageTypeParameter
    {
        public long MessageTypeParameterId { get; set; }
        public long MessageConfigId { get; set; }
        public string? MessageType { get; set; }
        public string? ViewName { get; set; }
        public string? ReturnParameters { get; set; }
        public string? IdColumn { get; set; }
        public string? TemplateName { get; set; }
        public string? MessageUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
