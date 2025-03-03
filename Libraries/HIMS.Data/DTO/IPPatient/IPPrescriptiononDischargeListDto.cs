using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class IPPrescriptiononDischargeListDto
    {
        public long? OPD_IPD_ID { get; set; }
        //public byte? OpdIpdType { get; set; }
        //public DateTime? Date { get; set; }
        //public string? Ptime { get; set; }
        //public long? ClassId { get; set; }
        //public long? GenericId { get; set; }
        public long? ItemID { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        //public long? InstructionId { get; set; }
        //public double? QtyPerDay { get; set; }
        //public double? TotalQty { get; set; }
        public string? Instruction { get; set; }
        //public string? Remark { get; set; }
        //public bool? IsEnglishOrIsMarathi { get; set; }
        //public long? StoreId { get; set; }
        //public int? CreatedBy { get; set; }

        public string? ItemName { get; set; }
        public string? DoseNameInEnglish { get; set; }

        public string? DoseName { get; set; }
        public string? ItemGenericName { get; set; }





    }
}
