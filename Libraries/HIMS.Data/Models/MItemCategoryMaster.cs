﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemCategoryMaster
    {
        public long ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ItemTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
