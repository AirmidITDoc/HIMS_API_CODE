namespace HIMS.Data.DTO.OPPatient
{
    public class RegistrationListDto
    {

        public long? RegId { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public long? PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Age { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PatientName { get; set; }
        public string? RDate { get; set; }
        public string? RegTimeDate { get; set; }
        public string? RegNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public long? MaritalStatusId { get; set; }
        public bool? IsCharity { get; set; }
        public string? RegNoWithPrefix { get; set; }
        public long? ReligionId { get; set; }
        public long? AreaId { get; set; }
        public string? AadharCardNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? UserName { get; set; }
        public decimal? AnnualIncome { get; set; }
        public string? RationCardNo { get; set; }
        public string? PanCardNo { get; set; }
        public string? EmgContactPersonName { get; set; }
        public long? EmgRelationshipId { get; set; }
        public string? EmgMobileNo { get; set; }
        public string? EmgLandlineNo { get; set; }
        public string? EngAddress { get; set; }
        public string? EmgAadharCardNo { get; set; }
        public string? EmgDrivingLicenceNo { get; set; }
        public string? MedTourismPassportNo { get; set; }
        public DateTime? MedTourismVisaIssueDate { get; set; }
        public DateTime? MedTourismVisaValidityDate { get; set; }
        public string? MedTourismNationalityId { get; set; }
        public long? MedTourismCitizenship { get; set; }
        public string? MedTourismPortOfEntry { get; set; }
        public DateTime? MedTourismDateOfEntry { get; set; }
        public string? MedTourismResidentialAddress { get; set; }
        public string? MedTourismOfficeWorkAddress { get; set; }
        public string? Photo { get; set; }
        public long AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public string? EmailId { get; set; }





    }




    public class RegistrationAutoCompleteDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string RegNo { get; set; }
        public string Mobile { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public long RegId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? AadharCardNo { get; set; }

    }
}
