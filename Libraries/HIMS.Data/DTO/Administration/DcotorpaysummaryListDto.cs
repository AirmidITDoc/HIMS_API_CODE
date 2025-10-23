using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DcotorpaysummaryListDto
    {
        public string? AddChargeDrName { get; set; }
        public decimal? NetAmount { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
       
    }
}
