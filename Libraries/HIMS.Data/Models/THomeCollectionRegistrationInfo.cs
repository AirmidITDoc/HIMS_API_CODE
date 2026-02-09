using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class THomeCollectionRegistrationInfo
    {
        public long HomeCollectionId { get; set; }
        public long? UnitId { get; set; }
        public string? HomeSeqNo { get; set; }
        public long PrefixId { get; set; }
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
        public string? Remark { get; set; }
        public bool? Priority { get; set; }
        public DateTime? CollectionDate { get; set; }
        public DateTime? CollectionTime { get; set; }
        public long? Phlebotomist { get; set; }
        public string? Location { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancel { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? Status { get; set; }
    }
}
