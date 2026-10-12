using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class ImmunizatoinInfoDto
    {
        public long ImmunizatoinId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? RegId { get; set; }
        public long? VaccineId { get; set; }
        public string? LotNumber { get; set; }
        public DateTime? LotExpDate { get; set; }
        public DateTime? OccuranceDate { get; set; }
        public string? PrimarySource { get; set; }
        public DateTime? RecommandationDate { get; set; }
        public string? ForecastStatus { get; set; }
        public string? Series { get; set; }
        public string? Description { get; set; }
        public long? DoseNumber { get; set; }
        public string? SeriesDoses { get; set; }
        public DateTime? RecommendedDate { get; set; }
        public string? Comment { get; set; }
        public string? SupportingImmunization { get; set; }
        public string? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? VaccineCode { get; set; }
        public string? VaccineName { get; set; }
        public string? UserName { get; set; }
    }
}
