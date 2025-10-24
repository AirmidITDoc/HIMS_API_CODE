using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DailyExpenceListtDto
    {
        public long ExpID { get; set; }
        public string? ExpDate { get; set; }
        public string RExpDate { get; set; }
        public string? ExpTime { get; set; }
        public string ExpType { get; set; }
        public decimal? ExpAmount { get; set; }
        public string PersonName { get; set; } 
        public string? Narration { get; set; }
        public string? UserName { get; set; }
        public string ExpensesType { get; set; }
        public string? VoucharNo { get; set; }
        public string HeadName { get; set; }
        public long? ExpHeadId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? Utrno { get; set; }
        public long? ExpCategoryId { get; set; }





    }
}
