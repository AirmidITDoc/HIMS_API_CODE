using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class THomeCollectionPatientRegistartion
    {
        public THomeCollectionPatientRegistartion()
        {
            THomeCollectionPatientRegDetails = new HashSet<THomeCollectionPatientRegDetail>();
        }

        public long PatientRegId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public long? AgeY { get; set; }
        public long? AgeM { get; set; }
        public long? AgeD { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public long? PatRegId { get; set; }

        public virtual ICollection<THomeCollectionPatientRegDetail> THomeCollectionPatientRegDetails { get; set; }
    }
}
