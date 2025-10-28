using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorQualificationDetail
    {
        public long DocQualfiId { get; set; }
        public long? DoctorId { get; set; }
        public long? QualificationId { get; set; }
        public string? PassingYear { get; set; }
        public long? InstitutionNameId { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DoctorMaster? Doctor { get; set; }
    }
}
