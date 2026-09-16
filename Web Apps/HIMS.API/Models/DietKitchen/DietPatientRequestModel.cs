using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.DietKitchen
{
    public class DietPatientRequestModel
    {
        public long DietReqId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? UnitId { get; set; }
        public string? DietReqNo { get; set; }
        public long? DietMenuId { get; set; }
        public List<DietPatientRequestDetailModel> TDietPatReqDetails { get; set; }

    }
    public class DietPatientRequestDetailModel

    {
        public long DietReqDetId { get; set; }
        public long DietReqId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderTime { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public long DietMenuId { get; set; }
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public long? DietRestrictionId { get; set; }
        public long? AllergyId { get; set; }
        public long? NutritionistId { get; set; }
        public bool? IsPriority { get; set; }
        public string? Comments { get; set; }
        public long Status { get; set; }
        public bool? IsAccept { get; set; }
        public long? IsAcceptedBy { get; set; }
        public DateTime? IsAcceptedDateTime { get; set; }
        public bool? IsDelived { get; set; }
        public long? IsDelivedBy { get; set; }
        public DateTime? IsDelivedDateTime { get; set; }

    }
}

    

