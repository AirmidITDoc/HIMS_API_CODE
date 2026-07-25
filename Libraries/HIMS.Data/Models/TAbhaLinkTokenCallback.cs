using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAbhaLinkTokenCallback
    {
        public long CallbackId { get; set; }
        public string? RequestId { get; set; }
        public string? AbhaAddress { get; set; }
        public string? LinkToken { get; set; }
        public bool? IsSuccess { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime? CallbackDate { get; set; }
        public string? RawResponse { get; set; }
    }
}
