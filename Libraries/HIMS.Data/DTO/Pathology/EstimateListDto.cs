using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class EstimateListDto
    {
        public long EstimateId { get; set; }
        public long? UnitId { get; set; }
        public string? EstimateNo { get; set; }
        public long? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public long? EmailId { get; set; }
        public long? AgeYear { get; set; }
        public long? CityId { get; set; }
        public long? DoctorId { get; set; }
        public long? CompanyId { get; set; }
        public string? Comments { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class EstimateDetailsListDto
    {
    public long EstimateDetId { get; set; }
    public long? EstimateId { get; set; }
    public long? ServiceId { get; set; }
    public decimal? Price { get; set; }
    public long? Qty { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? DiscAmount { get; set; }
    public decimal? NetAmount { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    }
}
