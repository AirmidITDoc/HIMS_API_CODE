﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDrugMaster
    {
        public long DrugId { get; set; }
        public string? DrugName { get; set; }
        public long? GenericId { get; set; }
        public long? ClassId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
