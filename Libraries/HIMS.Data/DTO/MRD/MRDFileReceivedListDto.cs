using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.MRD
{
    public  class MRDFileReceivedListDto
    {
        public DateTime RecievedDate { get; set; }
        public DateTime? RecievedTime { get; set; }
        public long UnitId { get; set; }
        public long Opipid { get; set; }
        public string Mrdno { get; set; } = null!;
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public bool IsInOut { get; set; }
        public long? OutFileId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long GivenUserId { get; set; }
        public string? PersonName { get; set; }
        public DateTime OutDate { get; set; }
        public DateTime OutTime { get; set; }
        public string? OutReason { get; set; }
        public string? InNo { get; set; }
        public DateTime? InDate { get; set; }
        public DateTime? InTime { get; set; }
        public long? ReturnUserId { get; set; }
        public string? ReturnPersonName { get; set; }
        public string? InReason { get; set; }


    }
}
