using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class CertificateInformationDto
    {
        public long CertificateId {  get; set; }
        public DateTime? CertificateDate { get; set; }
        public string? CertificateTime { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateText { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
      

    }

}
