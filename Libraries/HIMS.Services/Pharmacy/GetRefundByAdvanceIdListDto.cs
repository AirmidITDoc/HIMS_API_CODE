﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class GetRefundByAdvanceIdListDto
    {

        public DateTime? RefundDate { get; set; }
        public decimal? RefundAmount { get; set; }
    }
}
