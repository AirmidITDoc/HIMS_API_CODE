using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MUploadCategoryMaster
    {
        public long UploadCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
    }
}
