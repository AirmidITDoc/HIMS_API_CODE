using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.AbhaIntegration
{
    public class CareContextModel
    {
        public string abhaId { get; set; }
        public string abhaNumber { get; set; }
        public string patientReferenceNumber { get; set; }
        public string yearOfBirth { get; set; }
        public List<CareContext2> careContexts { get; set; }
        public string hipId { get; set; }
    }

    public class CareContext2
    {
        public string referenceNumber { get; set; }
        public string comment { get; set; }
    }

    public class PatientVisitRequest
    {
        public string AbhaNumber { get; set; }
        public string AbhaAddress { get; set; }
        public string HipId { get; set; }
        public string OpIpId { get; set; }
        public string OpIpType { get; set; }
    }

    public class PatientVisitResponse
    {
        public Patient Patient { get; set; }
        public List<Visit> Visits { get; set; }
        public string HipId { get; set; }
    }

    public class Patient
    {
        public string PatientRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string HealthId { get; set; }
        public string HealthIdNumber { get; set; }
        public string DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }
        public string YearOfBirth { get; set; }
        public string HipId { get; set; }
    }

    public class Visit
    {
        public string VisitNumber { get; set; }
        public string VisitReason { get; set; }
        public Doctor Doctor { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string VisitType { get; set; }
        public EncounterCode EncounterCode { get; set; }
        public List<Diagnosis> Diagnosis { get; set; }
        public List<ChiefComplaint> ChiefComplaints { get; set; }
        public List<Prescription> Prescriptions { get; set; }

        public List<DiagnosticReport> DiagnosticReports { get; set; }
        public List<DischargeSummaryItem> DischargeSummaries { get; set; }
        public List<ObservationResult> ObservationResult { get; set; }
        public List<AllergyData> AllergiesData { get; set; }
        public List<PhysicalExam> PhysicalExams { get; set; }
        public List<InvoiceRecord> InvoiceRecord { get; set; }
        public List<MedicalHistory> MedicalHistory { get; set; }
        public List<Referral> Referrals { get; set; }
        public List<InvestigationAdvice> InvestigationAdvice { get; set; }
        public List<Procedure> Procedures { get; set; }
        public List<FamilyMedicalHistory> FamilyMedicalHistory { get; set; }
        public List<CarePlan> CarePlan { get; set; }

        // keep reports at end
        public List<Reports> Reports { get; set; }
        public FollowUp FollowUp { get; set; }
    }

    public class Reports
    {
        public string visitNumber { get; set; }
        public string patientRegistrationNumber { get; set; }
        public string fileName { get; set; }
        public string documentType { get; set; }
    }

    public class Doctor
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public string Speciality { get; set; }
    }

    public class EncounterCode
    {
        public string Text { get; set; }
        public EncounterCodeDetails Code { get; set; }
    }

    public class EncounterCodeDetails
    {
        public string HospitalId { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
    }
    public class Diagnosis
    {
        public string Summary { get; set; }
        public ConditionCode ConditionCode { get; set; }
        public string RecordedDate { get; set; }
    }
    public class ChiefComplaint
    {
        public string Summary { get; set; }
        public ConditionCode ConditionCode { get; set; }
        public string RecordedDate { get; set; }
    }

    public class ConditionCode
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class Prescription
    {
        public string Status { get; set; }
        public string Intent { get; set; }
        public string AuthoredOn { get; set; }
        public Drug Drug { get; set; }
        //public string Manufacturer { get; set; }
        //public bool Brand { get; set; }
        public Reason Reason { get; set; }
        public Dosage Dosage { get; set; }
    }

    public class Drug
    {
        public DrugCode DrugCode { get; set; }
        public string Manufacturer { get; set; }
        public bool Brand { get; set; }
    }

    public class DrugCode
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class Reason
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class Dosage
    {
        public string Text { get; set; }
        public AdditionalInstruction AdditionalInstruction { get; set; }
        public string Frequency { get; set; }
        public string Period { get; set; }
        public string PeriodUnit { get; set; }
        public Route Route { get; set; }
        public Method Method { get; set; }
    }

    public class AdditionalInstruction
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class Route
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class Method
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class CodeDetails
    {
        public string HospitalId { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
    }


    public class DiagnosticReport
    {
        public string Status { get; set; }
        public DiagnosticCodeableConcept DiagnosticCode { get; set; }
        public string Conclusion { get; set; }
        public List<ObservationResult> Results { get; set; } = new();
        public string EffectiveDate { get; set; }
        public string IssuedAt { get; set; }
    }

    public class DiagnosticCodeableConcept
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class ObservationResult
    {
        public string Status { get; set; }
        public ResultCodeableConcept ResultCode { get; set; }
        public ValueQuantity Value { get; set; }
        public CategoryCodeableConcept Category { get; set; }
        public ReferenceRange ReferenceRange { get; set; }
        public string EffectiveOn { get; set; }
        public InterpretationCodeableConcept Interpretation { get; set; }
    }

    public class ResultCodeableConcept
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class CategoryCodeableConcept
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class InterpretationCodeableConcept
    {
        public string Text { get; set; }
        public CodeDetails Code { get; set; }
    }

    public class ValueQuantity
    {
        public object Value { get; set; }
        public QuantityCode Code { get; set; }
    }

    public class QuantityCode
    {
        public string Id { get; set; }
        public string Unit { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
    }

    public class ReferenceRange
    {
        public ValueQuantity High { get; set; }
        public ValueQuantity Low { get; set; }
    }


    public class DischargeSummaryItem
    {
        public string DischargeSummary { get; set; }
        public string DischargeStatus { get; set; }
    }
    public class AllergyData
    {
        public CodeableConcept clinicalStatus { get; set; }
        public CodeableConcept verificationStatus { get; set; }
        public CodeableConcept allergy { get; set; }
        public string recordedDate { get; set; }
        public string note { get; set; }
    }

    public class CodeableConcept
    {
        public string text { get; set; }
        public CodeDetails code { get; set; }
    }

    public class PhysicalExam
    {
        public string physicalExamSummary { get; set; }
    }

    public class InvoiceRecord
    {
        public string recepient { get; set; }
        public string issuer { get; set; }
        public string issuedDate { get; set; }
        public BillIdentifier billIdentifier { get; set; }
        public List<InvoiceLineItem> lineItems { get; set; }
        public string invoiceRecordType { get; set; }
    }

    public class BillIdentifier
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class InvoiceLineItem
    {
        public ChargeItem chargeItem { get; set; }
        public List<PriceComponent> priceComponents { get; set; }
    }

    public class ChargeItem
    {
        public CodeableConcept chargeItemCodeableConcept { get; set; }
    }

    public class PriceComponent
    {
        public string type { get; set; }
        public PriceAmount amount { get; set; }
    }

    public class PriceAmount
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }

    public class MedicalHistory
    {
        public string summary { get; set; }
        public ConditionCode conditionCode { get; set; }
        public string recordedDate { get; set; }
    }

    public class Referral
    {
        public CodeableConcept referral { get; set; }
        public string status { get; set; }
        public string intent { get; set; }
    }

    public class InvestigationAdvice
    {
        public CodeableConcept investigation { get; set; }
        public string status { get; set; }
        public string intent { get; set; }
    }
    public class Procedure
    {
        public string status { get; set; }
        public CodeableConcept procedureCode { get; set; }
        public string performedAt { get; set; }
        public List<CodeableConcept> complications { get; set; }
    }

    public class FamilyMedicalHistory
    {
        public string status { get; set; }
        public string subjectId { get; set; }
        public CodeableConcept relationship { get; set; }
        public string name { get; set; }
        public bool contributedToDeath { get; set; }
        public int age { get; set; }
        public List<CodeableConcept> reasonCode { get; set; }
        public string date { get; set; }
        public string gender { get; set; }
        public List<MedicalHistory> condition { get; set; }
    }

    public class CarePlan
    {
        public string status { get; set; }
        public string intent { get; set; }
        public List<CodeableConcept> category { get; set; }
        public string codeDescription { get; set; }
        public string title { get; set; }
    }
    public class FollowUp
    {
        public string status { get; set; }
        public CodeableConcept serviceCategory { get; set; }
        public List<CodeableConcept> serviceType { get; set; }
        public List<CodeableConcept> specialty { get; set; }
        public List<CodeableConcept> reasonCode { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string description { get; set; }
    }
}
