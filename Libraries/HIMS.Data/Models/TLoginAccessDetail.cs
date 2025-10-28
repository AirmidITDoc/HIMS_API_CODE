using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLoginAccessDetail
    {
        public long LoginAccessId { get; set; }
        public long? LoginId { get; set; }
        public long? AccessValueId { get; set; }
        public bool? AccessValue { get; set; }
        public string? AccessInputValue { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual LoginManager? Login { get; set; }
    }
}
