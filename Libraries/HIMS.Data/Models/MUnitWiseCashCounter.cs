using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MUnitWiseCashCounter
    {
        public long Id { get; set; }
        public long? UnitId { get; set; }
        public long? CashCounterId { get; set; }
        public string? CashCounterCode { get; set; }
    }
}
