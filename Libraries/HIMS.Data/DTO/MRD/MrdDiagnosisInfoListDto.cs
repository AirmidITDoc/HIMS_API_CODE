using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.MRD
{
    public  class MrdDiagnosisInfoListDto
    {
        public long IpdiagId { get; set; }
        public long? AdmId { get; set; }
        public bool? IsSync { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string Icdcode { get; set; } = null!;
        public string Diagnosisinformation { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public long RegId { get; set; }
        public string? RegNo { get; set; } 
        public string? RegPrefix { get; set; } 
        public string? RegDate { get; set; }
        public string RegTime { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string AgeGender { get; set; } = null!;

        public string MobileNo { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PinNo { get; set; } = null!;

        public string? DateofBirth { get; set; }
        public string? Age { get; set; }
        public string GenderName { get; set; } = null!;
        public string AadharCardNo { get; set; } = null!;
        public string EmailId { get; set; } = null!;

        public string? CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
