using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class CertificateInformationListDto
    {
        public long? CertificateId {  get; set; }
        public long? CertificateTemplateId { get; set; }
        public DateTime? CertificateDate { get; set; }
        public string? CertificateTime { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateText { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
      

    }

}
