using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class RadioPcpndtListDto
    {
        public string? AdmissionDate { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
        public string MobileNo { get; set; }
        public long PCPNDTProcessId { get; set; }
        public string? ProcessDate { get; set; }
        public long? OPIPID { get; set; }
        public byte OPIPType { get; set; }
        public string RelativeName { get; set; }
        public string? ChildrenCount { get; set; }
        public string DaughtersDetails { get; set; }
        public string SonsDetails { get; set; }
        public string MPeriod { get; set; }
        public long? RefDocId { get; set; }
        public string NonInvasive { get; set; }
        public string Indication { get; set; }
        public string Prenatal { get; set; }
        public string? ResultDate { get; set; }
        public bool? Ultrasound { get; set; }
        public bool? Obs { get; set; }
        public bool? Pelvic { get; set; }
        public long? InvasiveDoctorId { get; set; }
        public long? ComplicationsId { get; set; }
        public bool? Clinical { get; set; }
        public bool? BioChemical { get; set; }
        public bool? Cytogenetic { get; set; }
        public bool? OtherRadiological { get; set; }
        public bool? Chromosomaldisorder { get; set; }
        public bool? Metabolicdisorder { get; set; }
        public bool? Congenitalanomaly { get; set; }
        public bool? MentalDisability { get; set; }
        public bool? Haemoglobinopathy { get; set; }
        public bool? SexLinkeddisorder { get; set; }
        public bool? Singlegenedisorder { get; set; }
        public string AnyOther1 { get; set; }
        public bool? Mage { get; set; }
        public bool? GeneticDisease { get; set; }
        public string AnyOtherIndication { get; set; }
        public bool? Chromosomal { get; set; }
        public bool? Molecular { get; set; }
        public bool? PreImplantation { get; set; }
        public string AnyOtherTest { get; set; }
        public string TestResult { get; set; }
        public string ResultConveyedto { get; set; }
        public string IndicationofMTP { get; set; }
        public bool? Amniocentesis { get; set; }
        public bool? ChorionicVilliaspiration { get; set; }
        public bool? FetalBiopsy { get; set; }
        public bool? Cordocentesis { get; set; }
        public long? AnyOther2 { get; set; }
        public string? ConsentDate { get; set; }
        public long? DeclarationDoctorid { get; set; }
        public long? CreatedBy { get; set; }
        public string? AbhaAddress { get; set; }
        public string? ABHANumber { get; set; }
        public string Refrancedoctor { get; set; }
        public string CondDoctor { get; set; }
        public string? ProcedureDate { get; set; }
        public string? DeclarationDoctor { get; set; }
        public long? ConsultantDocId { get; set; }
        public string? IndicationDesc { get; set; }
        public bool? IndicationValues { get; set; }
        public string? PatientAddress { get; set; }
        public string? PatientMobileNo { get; set; }


    }
    public class IndicationListDto
    {
        public string? IndicationDesc { get; set; }
        public bool? IndicationValues { get; set; }
    }
    public class RadioLogistListDto
    {
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public long? DoctorId { get; set; }

    }
    public class GynecologistDoctorListDto
    {
        public string? DoctorName { get; set; }
        public long? DoctorId { get; set; }

    }

}
