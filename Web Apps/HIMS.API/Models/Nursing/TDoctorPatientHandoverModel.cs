using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Nursing
{
    public partial class TDoctorPatientHandoverModel
    {
        public long DocHandId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Tdate { get; set; }    
        public DateTime? Ttime { get; set; }
        public string? ShiftInfo { get; set; }
        public string? PatHandI { get; set; }
        public string? PatHandS { get; set; }
        public string? PatHandB { get; set; }
        public string? PatHandA { get; set; }
        public string? PatHandR { get; set; }
        public long? IsAddedBy { get; set; }
        public int? CreatedBy { get; set; }
    }

}
