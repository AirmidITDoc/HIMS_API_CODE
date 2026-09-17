using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.DietKitchen
{
    public  class DietPatientRequestHeaderListDto
    {
        public long DietReqId { get; set; }
        public DateTime Date { get; set; }
        public string? DietReqNo { get; set; }
        public long? DietMenuId { get; set; }
        public string DietMenuCode { get; set; }
        public string DietMenuName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
    }
    public class DietPatientRequestDetailsListDto
    {
        public long DietReqId { get; set; }
        public DateTime Date { get; set; }
        public string? DietReqNo { get; set; }
        public long? DietMenuId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? OrderTime { get; set; }
        public long? OPIPID { get; set; }
        public string? DietMenuCode { get; set; }
        public string? DietMenuName { get; set; }
        public string? MealTypeCode { get; set; }
        public string? MealName { get; set; }
        public string? DietCode { get; set; }
        public string? DietName { get; set; }
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public string? RestrictionName { get; set; }
        public string? AllergyCode { get; set; }
        public string? AllergyName { get; set; }
        public string? Reaction { get; set; }
        public bool IsPriority { get; set; }
        public string? Comments { get; set; }
        public string? Status { get; set; }
        public bool? IsAccept { get; set; }
        public long? IsAcceptedBy { get; set; }
        public DateTime? IsAcceptedDateTime { get; set; }
        public bool? IsDelived { get; set; }
        public long? IsDelivedBy { get; set; }
        public DateTime? IsDelivedDateTime { get; set; }
        public bool? DetIsCancelled { get; set; }
        public long? DetIsCancelledBy { get; set; }
        public DateTime? DetIsCancelledDate { get; set; }
        public string? CancelledReason { get; set; }
        public long? RestrictionId { get; set; }
        public long? MealId { get; set; }
        public long? RegID { get; set; }
        public long? GenderId { get; set; }
        public string? PatientName { get; set; }
        public string? RoomName { get; set; }
    }
}
