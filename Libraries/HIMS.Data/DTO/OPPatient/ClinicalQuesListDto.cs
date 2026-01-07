using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class ClinicalQuesListDto
    {
        public long ClinicalQuesHeaderId { get; set; }
        public DateTime? ClinicalQuesDate { get; set; }
        public DateTime? ClinicalQuesTime { get; set; }
        public long? OPIPID { get; set; }
        public long? QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public byte? OPIPType { get; set; }
        public int? CreatedBy { get; set; }

        public long ClinicalQuesDetId { get; set; }
        public long? SubQuesId { get; set; }
        public string? SubQuesName { get; set; }
        public string? ResultEntry { get; set; }
        public long? SeqNo { get; set; }
    }
}
