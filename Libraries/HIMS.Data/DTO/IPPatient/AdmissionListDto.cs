using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class AdmissionListDto
    {
        public long AdmissionId { get; set; }
        public long? RegId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? AdmissionTime { get; set; }

        public string? PatientName { get; set; }

        public long? GenderId { get; set; }

        public string? GenderName { get; set; }

        
        public long? MaritalStatusId { get; set; }

        public string? AadharCardNo { get; set; }

        public string? DateofBirth { get; set; }

        public long? PatientTypeID { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public long? DocNameId { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public string? Ipdno { get; set; }

        public string? Doctorname { get; set; }
        public string? RefDocName { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }



        public string? DischargeTime { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public string? TariffName { get; set; }
        public string? ClassName { get; set; }

        public string? RegNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? ReligionId { get; set; }
        public long? AreaId { get; set; }
        public string? City { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? RelativeName { get; set; }

        public string? RelatvieMobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }

        public long? AdmittedDoctor1ID { get; set; }
        public string? AdmittedDoctor1 { get; set; }
        public long? AdmittedDoctor2ID { get; set; }
        public string? AdmittedDoctor2 { get; set; }

        public bool? IsMLC { get; set; }
        public string? SubTpaComId { get; set; }
        public string? PolicyNo { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? HosApreAmt { get; set; }
        public decimal? PathApreAmt { get; set; }
        public decimal? PharApreAmt { get; set; }

        public decimal? RadiApreAmt { get; set; }


        public byte? AdmissionType { get; set; }
    }
}
