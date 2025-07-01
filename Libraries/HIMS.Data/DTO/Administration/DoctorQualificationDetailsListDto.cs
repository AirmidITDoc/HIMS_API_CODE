using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class DoctorQualificationDetailsListDto
    {
        public long DocQualfiId {  get; set; }
        public long DoctorId { get; set; }
        public long QualificationId { get; set; }
        public string? PassingYear { get; set; }
        public long InstitutionNameId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CityId { get; set; }
        public long CountryId { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? InstituteName { get; set; }
        public long? ConstantId { get; set; }
        public string? QualificationName { get; set; }

    }
}
