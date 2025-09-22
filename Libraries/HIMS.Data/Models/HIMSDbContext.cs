using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HIMS.Data.Models
{
    public partial class HIMSDbContext : DbContext
    {
        //public HIMSDbContext()
        //{
        //}

        //public HIMSDbContext(DbContextOptions<HIMSDbContext> options)
        //    : base(options)
        //{
        //}

        public virtual DbSet<AddCharge> AddCharges { get; set; } = null!;
        public virtual DbSet<Admission> Admissions { get; set; } = null!;
        public virtual DbSet<AdmittedPatientBalanceAmount> AdmittedPatientBalanceAmounts { get; set; } = null!;
        public virtual DbSet<AdvRefundDetail> AdvRefundDetails { get; set; } = null!;
        public virtual DbSet<AdvanceDetail> AdvanceDetails { get; set; } = null!;
        public virtual DbSet<AdvanceHeader> AdvanceHeaders { get; set; } = null!;
        public virtual DbSet<AllOutStanding> AllOutStandings { get; set; } = null!;
        public virtual DbSet<AllOutStandingPatientWise> AllOutStandingPatientWises { get; set; } = null!;
        public virtual DbSet<AllOutStandingPatientWiseLedger> AllOutStandingPatientWiseLedgers { get; set; } = null!;
        public virtual DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public virtual DbSet<Bedmaster> Bedmasters { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<BillDetail> BillDetails { get; set; } = null!;
        public virtual DbSet<CasePaper> CasePapers { get; set; } = null!;
        public virtual DbSet<CashCounter> CashCounters { get; set; } = null!;
        public virtual DbSet<ClassMaster> ClassMasters { get; set; } = null!;
        public virtual DbSet<CompanyMaster> CompanyMasters { get; set; } = null!;
        public virtual DbSet<CompanyTypeMaster> CompanyTypeMasters { get; set; } = null!;
        public virtual DbSet<ConfigSetting> ConfigSettings { get; set; } = null!;
        public virtual DbSet<DbGenderMaster> DbGenderMasters { get; set; } = null!;
        public virtual DbSet<DbPrefixMaster> DbPrefixMasters { get; set; } = null!;
        public virtual DbSet<DbPurposeMaster> DbPurposeMasters { get; set; } = null!;
        public virtual DbSet<Discharge> Discharges { get; set; } = null!;
        public virtual DbSet<DischargeSummary> DischargeSummaries { get; set; } = null!;
        public virtual DbSet<DischargeTypeMaster> DischargeTypeMasters { get; set; } = null!;
        public virtual DbSet<DoctorMaster> DoctorMasters { get; set; } = null!;
        public virtual DbSet<DoctorName> DoctorNames { get; set; } = null!;
        public virtual DbSet<DoctorShare> DoctorShares { get; set; } = null!;
        public virtual DbSet<DoctorTypeMaster> DoctorTypeMasters { get; set; } = null!;
        public virtual DbSet<DynamicExecuteSchedule> DynamicExecuteSchedules { get; set; } = null!;
        public virtual DbSet<DynamicExecuteScheduleLog> DynamicExecuteScheduleLogs { get; set; } = null!;
        public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; } = null!;
        public virtual DbSet<EmailNotificationDatum> EmailNotificationData { get; set; } = null!;
        public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; } = null!;
        public virtual DbSet<EmployeeMasterDetail> EmployeeMasterDetails { get; set; } = null!;
        public virtual DbSet<EmployeeUnitMapping> EmployeeUnitMappings { get; set; } = null!;
        public virtual DbSet<FileMaster> FileMasters { get; set; } = null!;
        public virtual DbSet<GeTIpPrescriptionItemDet> GeTIpPrescriptionItemDets { get; set; } = null!;
        public virtual DbSet<GeTTPrescriptionItemDet> GeTTPrescriptionItemDets { get; set; } = null!;
        public virtual DbSet<GeniusBufferresult> GeniusBufferresults { get; set; } = null!;
        public virtual DbSet<GeniusControl> GeniusControls { get; set; } = null!;
        public virtual DbSet<GeniusControlparam> GeniusControlparams { get; set; } = null!;
        public virtual DbSet<GeniusControltype> GeniusControltypes { get; set; } = null!;
        public virtual DbSet<GeniusITable> GeniusITables { get; set; } = null!;
        public virtual DbSet<GeniusMachinemaster> GeniusMachinemasters { get; set; } = null!;
        public virtual DbSet<GeniusMachineparam> GeniusMachineparams { get; set; } = null!;
        public virtual DbSet<GeniusMacid> GeniusMacids { get; set; } = null!;
        public virtual DbSet<GeniusParam> GeniusParams { get; set; } = null!;
        public virtual DbSet<GeniusQcresult> GeniusQcresults { get; set; } = null!;
        public virtual DbSet<GeniusSampledatum> GeniusSampledata { get; set; } = null!;
        public virtual DbSet<GeniusUser> GeniusUsers { get; set; } = null!;
        public virtual DbSet<GetPrescriptionItemDet> GetPrescriptionItemDets { get; set; } = null!;
        public virtual DbSet<GetSalesDraftBillItemDet> GetSalesDraftBillItemDets { get; set; } = null!;
        public virtual DbSet<GroupMaster> GroupMasters { get; set; } = null!;
        public virtual DbSet<HmsLog> HmsLogs { get; set; } = null!;
        public virtual DbSet<HospitalMaster> HospitalMasters { get; set; } = null!;
        public virtual DbSet<IndentDetial> IndentDetials { get; set; } = null!;
        public virtual DbSet<InitiateDischarge> InitiateDischarges { get; set; } = null!;
        public virtual DbSet<IpadmBedtransferSmsquery> IpadmBedtransferSmsqueries { get; set; } = null!;
        public virtual DbSet<IpadmRefDoctorSmsquery> IpadmRefDoctorSmsqueries { get; set; } = null!;
        public virtual DbSet<IpadmRefInfo> IpadmRefInfos { get; set; } = null!;
        public virtual DbSet<IpadmSmsquery> IpadmSmsqueries { get; set; } = null!;
        public virtual DbSet<IpadvDet> IpadvDets { get; set; } = null!;
        public virtual DbSet<IpadvRefund> IpadvRefunds { get; set; } = null!;
        public virtual DbSet<IpbedTranferSmsqueryTempleteSm> IpbedTranferSmsqueryTempleteSms { get; set; } = null!;
        public virtual DbSet<IpbedTransferSm> IpbedTransferSms { get; set; } = null!;
        public virtual DbSet<IpcompanyPayment> IpcompanyPayments { get; set; } = null!;
        public virtual DbSet<IpddiscRefDoctorSmsquery> IpddiscRefDoctorSmsqueries { get; set; } = null!;
        public virtual DbSet<IpddischargeSmsquery> IpddischargeSmsqueries { get; set; } = null!;
        public virtual DbSet<IpotbookingSmsquery> IpotbookingSmsqueries { get; set; } = null!;
        public virtual DbSet<IpsmsqueryTempleteSm> IpsmsqueryTempleteSms { get; set; } = null!;
        public virtual DbSet<LCurstkItemWiseBalQty> LCurstkItemWiseBalQties { get; set; } = null!;
        public virtual DbSet<LMaxSupplierNameForItem> LMaxSupplierNameForItems { get; set; } = null!;
        public virtual DbSet<LoadingLog> LoadingLogs { get; set; } = null!;
        public virtual DbSet<LocationMaster> LocationMasters { get; set; } = null!;
        public virtual DbSet<LoginManager> LoginManagers { get; set; } = null!;
        public virtual DbSet<LvwAddCharge> LvwAddCharges { get; set; } = null!;
        public virtual DbSet<LvwAdmPatListForSearch> LvwAdmPatListForSearches { get; set; } = null!;
        public virtual DbSet<LvwAdmPatListForSearchAll> LvwAdmPatListForSearchAlls { get; set; } = null!;
        public virtual DbSet<LvwAdmPatListForSearchRefPhaAdv> LvwAdmPatListForSearchRefPhaAdvs { get; set; } = null!;
        public virtual DbSet<LvwAdmissionList> LvwAdmissionLists { get; set; } = null!;
        public virtual DbSet<LvwAdmissionListWithRegNoForPhar> LvwAdmissionListWithRegNoForPhars { get; set; } = null!;
        public virtual DbSet<LvwBill> LvwBills { get; set; } = null!;
        public virtual DbSet<LvwBillDateWise> LvwBillDateWises { get; set; } = null!;
        public virtual DbSet<LvwBillDiffQuery> LvwBillDiffQueries { get; set; } = null!;
        public virtual DbSet<LvwBillIpd> LvwBillIpds { get; set; } = null!;
        public virtual DbSet<LvwBillPrint> LvwBillPrints { get; set; } = null!;
        public virtual DbSet<LvwBillWiseMediniceTol> LvwBillWiseMediniceTols { get; set; } = null!;
        public virtual DbSet<LvwBrowseBill> LvwBrowseBills { get; set; } = null!;
        public virtual DbSet<LvwBrowseBillsIpd> LvwBrowseBillsIpds { get; set; } = null!;
        public virtual DbSet<LvwBrowsePayment> LvwBrowsePayments { get; set; } = null!;
        public virtual DbSet<LvwBrowsePaymentsIpd> LvwBrowsePaymentsIpds { get; set; } = null!;
        public virtual DbSet<LvwCanteenRequstItemDet> LvwCanteenRequstItemDets { get; set; } = null!;
        public virtual DbSet<LvwChargesAmtAddDoctorWise> LvwChargesAmtAddDoctorWises { get; set; } = null!;
        public virtual DbSet<LvwChargesCompany> LvwChargesCompanies { get; set; } = null!;
        public virtual DbSet<LvwCompanyPatientApprovalInf0> LvwCompanyPatientApprovalInf0s { get; set; } = null!;
        public virtual DbSet<LvwCompanyPayment> LvwCompanyPayments { get; set; } = null!;
        public virtual DbSet<LvwConfigSetting> LvwConfigSettings { get; set; } = null!;
        public virtual DbSet<LvwCurrentAdmBed> LvwCurrentAdmBeds { get; set; } = null!;
        public virtual DbSet<LvwCurrentAdmissionList> LvwCurrentAdmissionLists { get; set; } = null!;
        public virtual DbSet<LvwCurrentAdmittedList> LvwCurrentAdmittedLists { get; set; } = null!;
        public virtual DbSet<LvwCurrentBalQtyCheck> LvwCurrentBalQtyChecks { get; set; } = null!;
        public virtual DbSet<LvwDischargedList> LvwDischargedLists { get; set; } = null!;
        public virtual DbSet<LvwDoctorMasterList> LvwDoctorMasterLists { get; set; } = null!;
        public virtual DbSet<LvwEndoscopyNote> LvwEndoscopyNotes { get; set; } = null!;
        public virtual DbSet<LvwGeneralOtdetail> LvwGeneralOtdetails { get; set; } = null!;
        public virtual DbSet<LvwGetIppatientCashAmt> LvwGetIppatientCashAmts { get; set; } = null!;
        public virtual DbSet<LvwGetIpphCashAmt> LvwGetIpphCashAmts { get; set; } = null!;
        public virtual DbSet<LvwIpconcession> LvwIpconcessions { get; set; } = null!;
        public virtual DbSet<LvwIpdrefundAgainstBillList> LvwIpdrefundAgainstBillLists { get; set; } = null!;
        public virtual DbSet<LvwIpmedicalRecordVisitde> LvwIpmedicalRecordVisitdes { get; set; } = null!;
        public virtual DbSet<LvwItemExpList> LvwItemExpLists { get; set; } = null!;
        public virtual DbSet<LvwItemWithoutBatchno> LvwItemWithoutBatchnos { get; set; } = null!;
        public virtual DbSet<LvwListofAdmforAdvanceRefund> LvwListofAdmforAdvanceRefunds { get; set; } = null!;
        public virtual DbSet<LvwMedicalCertificate> LvwMedicalCertificates { get; set; } = null!;
        public virtual DbSet<LvwObst> LvwObsts { get; set; } = null!;
        public virtual DbSet<LvwOpdrefundAgainstBillList> LvwOpdrefundAgainstBillLists { get; set; } = null!;
        public virtual DbSet<LvwOtbookedSmsforDrQuery> LvwOtbookedSmsforDrQueries { get; set; } = null!;
        public virtual DbSet<LvwOtcharge> LvwOtcharges { get; set; } = null!;
        public virtual DbSet<LvwOtdetail> LvwOtdetails { get; set; } = null!;
        public virtual DbSet<LvwOtdetailsDoctorWise> LvwOtdetailsDoctorWises { get; set; } = null!;
        public virtual DbSet<LvwOttableMasterList> LvwOttableMasterLists { get; set; } = null!;
        public virtual DbSet<LvwPath> LvwPaths { get; set; } = null!;
        public virtual DbSet<LvwPathParaFill> LvwPathParaFills { get; set; } = null!;
        public virtual DbSet<LvwPathSubTestFill> LvwPathSubTestFills { get; set; } = null!;
        public virtual DbSet<LvwPatientListAdmission> LvwPatientListAdmissions { get; set; } = null!;
        public virtual DbSet<LvwPayment> LvwPayments { get; set; } = null!;
        public virtual DbSet<LvwPaymentCharity> LvwPaymentCharities { get; set; } = null!;
        public virtual DbSet<LvwPaymentPharmacy> LvwPaymentPharmacies { get; set; } = null!;
        public virtual DbSet<LvwPaymentPharmacyRefund> LvwPaymentPharmacyRefunds { get; set; } = null!;
        public virtual DbSet<LvwPharmacyBill> LvwPharmacyBills { get; set; } = null!;
        public virtual DbSet<LvwPrePostOperativeNote> LvwPrePostOperativeNotes { get; set; } = null!;
        public virtual DbSet<LvwRefundOfBillIpdlist> LvwRefundOfBillIpdlists { get; set; } = null!;
        public virtual DbSet<LvwRefundOfBillOpdlist> LvwRefundOfBillOpdlists { get; set; } = null!;
        public virtual DbSet<LvwRegNoWithPrefix> LvwRegNoWithPrefixes { get; set; } = null!;
        public virtual DbSet<LvwRegistrationList> LvwRegistrationLists { get; set; } = null!;
        public virtual DbSet<LvwRetrieveDischargeSummary> LvwRetrieveDischargeSummaries { get; set; } = null!;
        public virtual DbSet<LvwRetrievePathologyResult> LvwRetrievePathologyResults { get; set; } = null!;
        public virtual DbSet<LvwRetrievePathologyResultIppatientUpdate> LvwRetrievePathologyResultIppatientUpdates { get; set; } = null!;
        public virtual DbSet<LvwRetrievePathologyResultMachineUpload> LvwRetrievePathologyResultMachineUploads { get; set; } = null!;
        public virtual DbSet<LvwRetrievePathologyResultUpdate> LvwRetrievePathologyResultUpdates { get; set; } = null!;
        public virtual DbSet<LvwRtrvPathologyResultIpwithAge> LvwRtrvPathologyResultIpwithAges { get; set; } = null!;
        public virtual DbSet<LvwRtrvPathologyResultIpwithAgeMachineUpload> LvwRtrvPathologyResultIpwithAgeMachineUploads { get; set; } = null!;
        public virtual DbSet<LvwRtrvPathologyResultOpwithAge> LvwRtrvPathologyResultOpwithAges { get; set; } = null!;
        public virtual DbSet<LvwRtrvPathologyResultOpwithAgeMachineUpload> LvwRtrvPathologyResultOpwithAgeMachineUploads { get; set; } = null!;
        public virtual DbSet<LvwSalesBatchNo> LvwSalesBatchNos { get; set; } = null!;
        public virtual DbSet<LvwSalesGstissue> LvwSalesGstissues { get; set; } = null!;
        public virtual DbSet<LvwServiceMasterList> LvwServiceMasterLists { get; set; } = null!;
        public virtual DbSet<LvwServicePriceList> LvwServicePriceLists { get; set; } = null!;
        public virtual DbSet<LvwSonography> LvwSonographies { get; set; } = null!;
        public virtual DbSet<LvwStaffRefSmsquery> LvwStaffRefSmsqueries { get; set; } = null!;
        public virtual DbSet<LvwSubTest> LvwSubTests { get; set; } = null!;
        public virtual DbSet<LvwVisitAppPatientForSmsquery> LvwVisitAppPatientForSmsqueries { get; set; } = null!;
        public virtual DbSet<LvwVisitDetailsForCasePaper> LvwVisitDetailsForCasePapers { get; set; } = null!;
        public virtual DbSet<LvwVisitDetailsList> LvwVisitDetailsLists { get; set; } = null!;
        public virtual DbSet<LvwVisitRefDocForSmsquery> LvwVisitRefDocForSmsqueries { get; set; } = null!;
        public virtual DbSet<LvwWardDetail> LvwWardDetails { get; set; } = null!;
        public virtual DbSet<MAnaesthesiaTypeMaster> MAnaesthesiaTypeMasters { get; set; } = null!;
        public virtual DbSet<MAppWhatsAppDly> MAppWhatsAppDlies { get; set; } = null!;
        public virtual DbSet<MAreaMaster> MAreaMasters { get; set; } = null!;
        public virtual DbSet<MAssignItemToStore> MAssignItemToStores { get; set; } = null!;
        public virtual DbSet<MAssignSupplierToStore> MAssignSupplierToStores { get; set; } = null!;
        public virtual DbSet<MAutoServiceList> MAutoServiceLists { get; set; } = null!;
        public virtual DbSet<MBankMaster> MBankMasters { get; set; } = null!;
        public virtual DbSet<MCampMaster> MCampMasters { get; set; } = null!;
        public virtual DbSet<MCanItemMaster> MCanItemMasters { get; set; } = null!;
        public virtual DbSet<MCertificateMaster> MCertificateMasters { get; set; } = null!;
        public virtual DbSet<MCertificateTemplateMaster> MCertificateTemplateMasters { get; set; } = null!;
        public virtual DbSet<MCityMaster> MCityMasters { get; set; } = null!;
        public virtual DbSet<MClassMaster> MClassMasters { get; set; } = null!;
        public virtual DbSet<MCompanyServiceAssignMaster> MCompanyServiceAssignMasters { get; set; } = null!;
        public virtual DbSet<MCompanyWiseServiceDiscount> MCompanyWiseServiceDiscounts { get; set; } = null!;
        public virtual DbSet<MComplaintMaster> MComplaintMasters { get; set; } = null!;
        public virtual DbSet<MConcessionReasonMaster> MConcessionReasonMasters { get; set; } = null!;
        public virtual DbSet<MConsentMaster> MConsentMasters { get; set; } = null!;
        public virtual DbSet<MConstant> MConstants { get; set; } = null!;
        public virtual DbSet<MCountryMaster> MCountryMasters { get; set; } = null!;
        public virtual DbSet<MCreditReasonMaster> MCreditReasonMasters { get; set; } = null!;
        public virtual DbSet<MCurrencyMaster> MCurrencyMasters { get; set; } = null!;
        public virtual DbSet<MDepartmentMaster> MDepartmentMasters { get; set; } = null!;
        public virtual DbSet<MDiagnosisMaster> MDiagnosisMasters { get; set; } = null!;
        public virtual DbSet<MDietChartDetail> MDietChartDetails { get; set; } = null!;
        public virtual DbSet<MDietChartMaster> MDietChartMasters { get; set; } = null!;
        public virtual DbSet<MDoctorChargesDetail> MDoctorChargesDetails { get; set; } = null!;
        public virtual DbSet<MDoctorDepartmentDet> MDoctorDepartmentDets { get; set; } = null!;
        public virtual DbSet<MDoctorExperienceDetail> MDoctorExperienceDetails { get; set; } = null!;
        public virtual DbSet<MDoctorHouseManMaster> MDoctorHouseManMasters { get; set; } = null!;
        public virtual DbSet<MDoctorLeaveDetail> MDoctorLeaveDetails { get; set; } = null!;
        public virtual DbSet<MDoctorNotesTemplateMaster> MDoctorNotesTemplateMasters { get; set; } = null!;
        public virtual DbSet<MDoctorPerGroupWiseMaster> MDoctorPerGroupWiseMasters { get; set; } = null!;
        public virtual DbSet<MDoctorPerMaster> MDoctorPerMasters { get; set; } = null!;
        public virtual DbSet<MDoctorQualificationDetail> MDoctorQualificationDetails { get; set; } = null!;
        public virtual DbSet<MDoctorScheduleDetail> MDoctorScheduleDetails { get; set; } = null!;
        public virtual DbSet<MDoctorSignPageDetail> MDoctorSignPageDetails { get; set; } = null!;
        public virtual DbSet<MDoseMaster> MDoseMasters { get; set; } = null!;
        public virtual DbSet<MDrugMaster> MDrugMasters { get; set; } = null!;
        public virtual DbSet<MExaminationMaster> MExaminationMasters { get; set; } = null!;
        public virtual DbSet<MExpensesHeadMaster> MExpensesHeadMasters { get; set; } = null!;
        public virtual DbSet<MGenericMaster> MGenericMasters { get; set; } = null!;
        public virtual DbSet<MIcdcdeMainMaster> MIcdcdeMainMasters { get; set; } = null!;
        public virtual DbSet<MIcdcodingMaster> MIcdcodingMasters { get; set; } = null!;
        public virtual DbSet<MInstructionMaster> MInstructionMasters { get; set; } = null!;
        public virtual DbSet<MItemCategoryMaster> MItemCategoryMasters { get; set; } = null!;
        public virtual DbSet<MItemClassMaster> MItemClassMasters { get; set; } = null!;
        public virtual DbSet<MItemCompanyMaster> MItemCompanyMasters { get; set; } = null!;
        public virtual DbSet<MItemDrugTypeMaster> MItemDrugTypeMasters { get; set; } = null!;
        public virtual DbSet<MItemGenericNameMaster> MItemGenericNameMasters { get; set; } = null!;
        public virtual DbSet<MItemManufactureMaster> MItemManufactureMasters { get; set; } = null!;
        public virtual DbSet<MItemMaster> MItemMasters { get; set; } = null!;
        public virtual DbSet<MItemTypeMaster> MItemTypeMasters { get; set; } = null!;
        public virtual DbSet<MItemWiseSupplierRate> MItemWiseSupplierRates { get; set; } = null!;
        public virtual DbSet<MLoginAccessConfig> MLoginAccessConfigs { get; set; } = null!;
        public virtual DbSet<MLvwRetrievePathologyResultUpdateIpageWise> MLvwRetrievePathologyResultUpdateIpageWises { get; set; } = null!;
        public virtual DbSet<MLvwRetrievePathologyResultUpdateOpageWise> MLvwRetrievePathologyResultUpdateOpageWises { get; set; } = null!;
        public virtual DbSet<MLvwRtrvPathologyResultIpwithAge> MLvwRtrvPathologyResultIpwithAges { get; set; } = null!;
        public virtual DbSet<MLvwRtrvPathologyResultIpwithAgeMachineUpload> MLvwRtrvPathologyResultIpwithAgeMachineUploads { get; set; } = null!;
        public virtual DbSet<MLvwRtrvPathologyResultOpwithAge> MLvwRtrvPathologyResultOpwithAges { get; set; } = null!;
        public virtual DbSet<MLvwRtrvPathologyResultOpwithAgeMachineUpload> MLvwRtrvPathologyResultOpwithAgeMachineUploads { get; set; } = null!;
        public virtual DbSet<MManufactureMaster> MManufactureMasters { get; set; } = null!;
        public virtual DbSet<MMaritalStatusMaster> MMaritalStatusMasters { get; set; } = null!;
        public virtual DbSet<MMemberCategoryMaster> MMemberCategoryMasters { get; set; } = null!;
        public virtual DbSet<MMessageTemplate> MMessageTemplates { get; set; } = null!;
        public virtual DbSet<MModeOfDeliveryMaster> MModeOfDeliveryMasters { get; set; } = null!;
        public virtual DbSet<MModeOfDischarge> MModeOfDischarges { get; set; } = null!;
        public virtual DbSet<MModeOfPayment> MModeOfPayments { get; set; } = null!;
        public virtual DbSet<MNursingTemplateMaster> MNursingTemplateMasters { get; set; } = null!;
        public virtual DbSet<MOctroiMaster> MOctroiMasters { get; set; } = null!;
        public virtual DbSet<MOpcasepaperDignosisMaster> MOpcasepaperDignosisMasters { get; set; } = null!;
        public virtual DbSet<MOtSiteDescriptionMaster> MOtSiteDescriptionMasters { get; set; } = null!;
        public virtual DbSet<MOtSurgeryCategoryMaster> MOtSurgeryCategoryMasters { get; set; } = null!;
        public virtual DbSet<MOtSurgeryMaster> MOtSurgeryMasters { get; set; } = null!;
        public virtual DbSet<MOtcomplicationsMaster> MOtcomplicationsMasters { get; set; } = null!;
        public virtual DbSet<MOthistopathologyMaster> MOthistopathologyMasters { get; set; } = null!;
        public virtual DbSet<MOtnotesTemplateMaster> MOtnotesTemplateMasters { get; set; } = null!;
        public virtual DbSet<MOttableMaster> MOttableMasters { get; set; } = null!;
        public virtual DbSet<MOttypeMaster> MOttypeMasters { get; set; } = null!;
        public virtual DbSet<MPackageDetail> MPackageDetails { get; set; } = null!;
        public virtual DbSet<MParameterDescriptiveMaster> MParameterDescriptiveMasters { get; set; } = null!;
        public virtual DbSet<MPastHistoryMaster> MPastHistoryMasters { get; set; } = null!;
        public virtual DbSet<MPathCategoryMaster> MPathCategoryMasters { get; set; } = null!;
        public virtual DbSet<MPathParaRangeMaster> MPathParaRangeMasters { get; set; } = null!;
        public virtual DbSet<MPathParaRangeWithAgeMaster> MPathParaRangeWithAgeMasters { get; set; } = null!;
        public virtual DbSet<MPathParameterMaster> MPathParameterMasters { get; set; } = null!;
        public virtual DbSet<MPathTemplateDetail> MPathTemplateDetails { get; set; } = null!;
        public virtual DbSet<MPathTemplateDetail1> MPathTemplateDetails1 { get; set; } = null!;
        public virtual DbSet<MPathTestDetailMaster> MPathTestDetailMasters { get; set; } = null!;
        public virtual DbSet<MPathTestFormula> MPathTestFormulas { get; set; } = null!;
        public virtual DbSet<MPathTestMaster> MPathTestMasters { get; set; } = null!;
        public virtual DbSet<MPathUnitMaster> MPathUnitMasters { get; set; } = null!;
        public virtual DbSet<MPresTemplateD> MPresTemplateDs { get; set; } = null!;
        public virtual DbSet<MPresTemplateH> MPresTemplateHs { get; set; } = null!;
        public virtual DbSet<MPrescriptionInstructionMaster> MPrescriptionInstructionMasters { get; set; } = null!;
        public virtual DbSet<MPrescriptionTemplateMaster> MPrescriptionTemplateMasters { get; set; } = null!;
        public virtual DbSet<MRadiologyCategoryMaster> MRadiologyCategoryMasters { get; set; } = null!;
        public virtual DbSet<MRadiologyTemplateDetail> MRadiologyTemplateDetails { get; set; } = null!;
        public virtual DbSet<MRadiologyTemplateMaster> MRadiologyTemplateMasters { get; set; } = null!;
        public virtual DbSet<MRadiologyTestMaster> MRadiologyTestMasters { get; set; } = null!;
        public virtual DbSet<MRefByPatientMaster> MRefByPatientMasters { get; set; } = null!;
        public virtual DbSet<MRefBySubCategoryMaster> MRefBySubCategoryMasters { get; set; } = null!;
        public virtual DbSet<MRelationshipMaster> MRelationshipMasters { get; set; } = null!;
        public virtual DbSet<MReligionMaster> MReligionMasters { get; set; } = null!;
        public virtual DbSet<MReportConfig> MReportConfigs { get; set; } = null!;
        public virtual DbSet<MReportConfigDetail> MReportConfigDetails { get; set; } = null!;
        public virtual DbSet<MReportConfiguration> MReportConfigurations { get; set; } = null!;
        public virtual DbSet<MReportTemplateConfig> MReportTemplateConfigs { get; set; } = null!;
        public virtual DbSet<MReportconfigBackup> MReportconfigBackups { get; set; } = null!;
        public virtual DbSet<MSalesTypeMaster> MSalesTypeMasters { get; set; } = null!;
        public virtual DbSet<MSmsmappingTemplate> MSmsmappingTemplates { get; set; } = null!;
        public virtual DbSet<MStateMaster> MStateMasters { get; set; } = null!;
        public virtual DbSet<MStoreMaster> MStoreMasters { get; set; } = null!;
        public virtual DbSet<MSubGroupMaster> MSubGroupMasters { get; set; } = null!;
        public virtual DbSet<MSubTpacompanyMaster> MSubTpacompanyMasters { get; set; } = null!;
        public virtual DbSet<MSupplierMaster> MSupplierMasters { get; set; } = null!;
        public virtual DbSet<MSystemConfig> MSystemConfigs { get; set; } = null!;
        public virtual DbSet<MTalukaMaster> MTalukaMasters { get; set; } = null!;
        public virtual DbSet<MTaxNatureMaster> MTaxNatureMasters { get; set; } = null!;
        public virtual DbSet<MTemplateMaster> MTemplateMasters { get; set; } = null!;
        public virtual DbSet<MTermsOfPaymentMaster> MTermsOfPaymentMasters { get; set; } = null!;
        public virtual DbSet<MUnitofMeasurementMaster> MUnitofMeasurementMasters { get; set; } = null!;
        public virtual DbSet<MUploadCategoryMaster> MUploadCategoryMasters { get; set; } = null!;
        public virtual DbSet<MVillage> MVillages { get; set; } = null!;
        public virtual DbSet<Mdimenu> Mdimenus { get; set; } = null!;
        public virtual DbSet<MenuMaster> MenuMasters { get; set; } = null!;
        public virtual DbSet<MessageConfiguration> MessageConfigurations { get; set; } = null!;
        public virtual DbSet<MessageTypeParameter> MessageTypeParameters { get; set; } = null!;
        public virtual DbSet<MisIpgroWiseTot> MisIpgroWiseTots { get; set; } = null!;
        public virtual DbSet<MisOpdocPatCnt> MisOpdocPatCnts { get; set; } = null!;
        public virtual DbSet<MisOpdocRevTot> MisOpdocRevTots { get; set; } = null!;
        public virtual DbSet<MisOpgroWiseTot> MisOpgroWiseTots { get; set; } = null!;
        public virtual DbSet<NeroOtdetail> NeroOtdetails { get; set; } = null!;
        public virtual DbSet<NewPriceList> NewPriceLists { get; set; } = null!;
        public virtual DbSet<NotificationMaster> NotificationMasters { get; set; } = null!;
        public virtual DbSet<Obst> Obsts { get; set; } = null!;
        public virtual DbSet<OpIpColCashRefund> OpIpColCashRefunds { get; set; } = null!;
        public virtual DbSet<OpIpCollection> OpIpCollections { get; set; } = null!;
        public virtual DbSet<OpIpCollectionRefund> OpIpCollectionRefunds { get; set; } = null!;
        public virtual DbSet<OpphAppSmsqueryTempleteSm> OpphAppSmsqueryTempleteSms { get; set; } = null!;
        public virtual DbSet<OpsmsqueryTempleteSm> OpsmsqueryTempleteSms { get; set; } = null!;
        public virtual DbSet<Otcharge> Otcharges { get; set; } = null!;
        public virtual DbSet<Otdetail> Otdetails { get; set; } = null!;
        public virtual DbSet<PatientTypeMaster> PatientTypeMasters { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentPharmacy> PaymentPharmacies { get; set; } = null!;
        public virtual DbSet<PermissionMaster> PermissionMasters { get; set; } = null!;
        public virtual DbSet<PharTotalSalesV> PharTotalSalesVs { get; set; } = null!;
        public virtual DbSet<ProcedureMaster> ProcedureMasters { get; set; } = null!;
        public virtual DbSet<PsLvwRtrvPathologyResultIpwithAge> PsLvwRtrvPathologyResultIpwithAges { get; set; } = null!;
        public virtual DbSet<PsLvwRtrvPathologyResultOpwithAge> PsLvwRtrvPathologyResultOpwithAges { get; set; } = null!;
        public virtual DbSet<Refund> Refunds { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<RegistrationSmsquery> RegistrationSmsqueries { get; set; } = null!;
        public virtual DbSet<ReportBatchLog> ReportBatchLogs { get; set; } = null!;
        public virtual DbSet<ReportConfig> ReportConfigs { get; set; } = null!;
        public virtual DbSet<ReportDatum> ReportData { get; set; } = null!;
        public virtual DbSet<ReportDemo> ReportDemos { get; set; } = null!;
        public virtual DbSet<ReportExecutionLog> ReportExecutionLogs { get; set; } = null!;
        public virtual DbSet<RoleMaster> RoleMasters { get; set; } = null!;
        public virtual DbSet<RoleTemplateDetail> RoleTemplateDetails { get; set; } = null!;
        public virtual DbSet<RoleTemplateMaster> RoleTemplateMasters { get; set; } = null!;
        public virtual DbSet<RoomMaster> RoomMasters { get; set; } = null!;
        public virtual DbSet<RtrvAdvDetForPay> RtrvAdvDetForPays { get; set; } = null!;
        public virtual DbSet<SalesGststoreWiseOpip> SalesGststoreWiseOpips { get; set; } = null!;
        public virtual DbSet<SalesPaymentDateWise> SalesPaymentDateWises { get; set; } = null!;
        public virtual DbSet<SalesRefundDateWise> SalesRefundDateWises { get; set; } = null!;
        public virtual DbSet<SalesReturnAmt> SalesReturnAmts { get; set; } = null!;
        public virtual DbSet<SalesReturnGststoreWise> SalesReturnGststoreWises { get; set; } = null!;
        public virtual DbSet<SalesReturnGststoreWiseOpip> SalesReturnGststoreWiseOpips { get; set; } = null!;
        public virtual DbSet<ScheduleLog> ScheduleLogs { get; set; } = null!;
        public virtual DbSet<ScheduleMaster> ScheduleMasters { get; set; } = null!;
        public virtual DbSet<SerPer> SerPers { get; set; } = null!;
        public virtual DbSet<ServiceDetail> ServiceDetails { get; set; } = null!;
        public virtual DbSet<ServiceMaster> ServiceMasters { get; set; } = null!;
        public virtual DbSet<ServiceWiseCompanyCode> ServiceWiseCompanyCodes { get; set; } = null!;
        public virtual DbSet<SmsoutGoing> SmsoutGoings { get; set; } = null!;
        public virtual DbSet<Sonography> Sonographies { get; set; } = null!;
        public virtual DbSet<SsConfigInitiateDischarge> SsConfigInitiateDischarges { get; set; } = null!;
        public virtual DbSet<SsGstperConfig> SsGstperConfigs { get; set; } = null!;
        public virtual DbSet<SsMenuMaster> SsMenuMasters { get; set; } = null!;
        public virtual DbSet<SsMenuMasterDetail> SsMenuMasterDetails { get; set; } = null!;
        public virtual DbSet<SsMenuMasterDetailDetail> SsMenuMasterDetailDetails { get; set; } = null!;
        public virtual DbSet<SsMobileAppLogin> SsMobileAppLogins { get; set; } = null!;
        public virtual DbSet<SsRoleTemplateDetail> SsRoleTemplateDetails { get; set; } = null!;
        public virtual DbSet<SsRoleTemplateMaster> SsRoleTemplateMasters { get; set; } = null!;
        public virtual DbSet<SsSmsConfig> SsSmsConfigs { get; set; } = null!;
        public virtual DbSet<StockLog> StockLogs { get; set; } = null!;
        public virtual DbSet<TAbill> TAbills { get; set; } = null!;
        public virtual DbSet<TAbillDetail> TAbillDetails { get; set; } = null!;
        public virtual DbSet<TAddCharge> TAddCharges { get; set; } = null!;
        public virtual DbSet<TAdditionalDocPay> TAdditionalDocPays { get; set; } = null!;
        public virtual DbSet<TAdmRefInfo> TAdmRefInfos { get; set; } = null!;
        public virtual DbSet<TAdvDetail> TAdvDetails { get; set; } = null!;
        public virtual DbSet<TAdvHeader> TAdvHeaders { get; set; } = null!;
        public virtual DbSet<TApayment> TApayments { get; set; } = null!;
        public virtual DbSet<TBatchAdjustment> TBatchAdjustments { get; set; } = null!;
        public virtual DbSet<TBedOccupancy> TBedOccupancies { get; set; } = null!;
        public virtual DbSet<TBedTransferDetail> TBedTransferDetails { get; set; } = null!;
        public virtual DbSet<TCanteenBillDetail> TCanteenBillDetails { get; set; } = null!;
        public virtual DbSet<TCanteenBillHeader> TCanteenBillHeaders { get; set; } = null!;
        public virtual DbSet<TCanteenRequestDetail> TCanteenRequestDetails { get; set; } = null!;
        public virtual DbSet<TCanteenRequestHeader> TCanteenRequestHeaders { get; set; } = null!;
        public virtual DbSet<TCardMemberDet> TCardMemberDets { get; set; } = null!;
        public virtual DbSet<TCardMemberDetGrp> TCardMemberDetGrps { get; set; } = null!;
        public virtual DbSet<TCardMemberHeader> TCardMemberHeaders { get; set; } = null!;
        public virtual DbSet<TCertificateInformation> TCertificateInformations { get; set; } = null!;
        public virtual DbSet<TChrgComp> TChrgComps { get; set; } = null!;
        public virtual DbSet<TCompBl> TCompBls { get; set; } = null!;
        public virtual DbSet<TCompBlDt> TCompBlDts { get; set; } = null!;
        public virtual DbSet<TCompanyDetail> TCompanyDetails { get; set; } = null!;
        public virtual DbSet<TCompanyHeader> TCompanyHeaders { get; set; } = null!;
        public virtual DbSet<TConsentInformation> TConsentInformations { get; set; } = null!;
        public virtual DbSet<TCurrentStk> TCurrentStks { get; set; } = null!;
        public virtual DbSet<TCurrentStkWithDaily> TCurrentStkWithDailies { get; set; } = null!;
        public virtual DbSet<TCurrentStock> TCurrentStocks { get; set; } = null!;
        public virtual DbSet<TCurrentStock2322> TCurrentStock2322s { get; set; } = null!;
        public virtual DbSet<TCurrentStockLog> TCurrentStockLogs { get; set; } = null!;
        public virtual DbSet<TCustomerInformation> TCustomerInformations { get; set; } = null!;
        public virtual DbSet<TCustomerInvoiceRaise> TCustomerInvoiceRaises { get; set; } = null!;
        public virtual DbSet<TCustomerPayment> TCustomerPayments { get; set; } = null!;
        public virtual DbSet<TDeathCertificate> TDeathCertificates { get; set; } = null!;
        public virtual DbSet<TDialysi> TDialyses { get; set; } = null!;
        public virtual DbSet<TDiscCaseSheet> TDiscCaseSheets { get; set; } = null!;
        public virtual DbSet<TDlabRequest> TDlabRequests { get; set; } = null!;
        public virtual DbSet<TDoctorPatientHandover> TDoctorPatientHandovers { get; set; } = null!;
        public virtual DbSet<TDoctorShareDetail> TDoctorShareDetails { get; set; } = null!;
        public virtual DbSet<TDoctorShareGenerationLog> TDoctorShareGenerationLogs { get; set; } = null!;
        public virtual DbSet<TDoctorShareHeader> TDoctorShareHeaders { get; set; } = null!;
        public virtual DbSet<TDoctorShareHeaderBack> TDoctorShareHeaderBacks { get; set; } = null!;
        public virtual DbSet<TDoctorsNote> TDoctorsNotes { get; set; } = null!;
        public virtual DbSet<TDrbill> TDrbills { get; set; } = null!;
        public virtual DbSet<TDrbillDet> TDrbillDets { get; set; } = null!;
        public virtual DbSet<TEmergencyAdm> TEmergencyAdms { get; set; } = null!;
        public virtual DbSet<TEmergencyMedicalHistory> TEmergencyMedicalHistories { get; set; } = null!;
        public virtual DbSet<TEndoscopyBooking> TEndoscopyBookings { get; set; } = null!;
        public virtual DbSet<TEndoscopyNote> TEndoscopyNotes { get; set; } = null!;
        public virtual DbSet<TExpense> TExpenses { get; set; } = null!;
        public virtual DbSet<TFavouriteUserList> TFavouriteUserLists { get; set; } = null!;
        public virtual DbSet<TFileLocation> TFileLocations { get; set; } = null!;
        public virtual DbSet<TGeneralSurgeryOtnote> TGeneralSurgeryOtnotes { get; set; } = null!;
        public virtual DbSet<TGrndetail> TGrndetails { get; set; } = null!;
        public virtual DbSet<TGrnheader> TGrnheaders { get; set; } = null!;
        public virtual DbSet<TGrnreturnDetail> TGrnreturnDetails { get; set; } = null!;
        public virtual DbSet<TGrnreturnHeader> TGrnreturnHeaders { get; set; } = null!;
        public virtual DbSet<TGrnsupPayment> TGrnsupPayments { get; set; } = null!;
        public virtual DbSet<TGstadjustment> TGstadjustments { get; set; } = null!;
        public virtual DbSet<THlabRequest> THlabRequests { get; set; } = null!;
        public virtual DbSet<THomeDeliveryOrder> THomeDeliveryOrders { get; set; } = null!;
        public virtual DbSet<TIndentDetail> TIndentDetails { get; set; } = null!;
        public virtual DbSet<TIndentHeader> TIndentHeaders { get; set; } = null!;
        public virtual DbSet<TIpPrescription> TIpPrescriptions { get; set; } = null!;
        public virtual DbSet<TIpPrescriptionDischarge> TIpPrescriptionDischarges { get; set; } = null!;
        public virtual DbSet<TIpmedicalRecord> TIpmedicalRecords { get; set; } = null!;
        public virtual DbSet<TIpprescriptionReturnD> TIpprescriptionReturnDs { get; set; } = null!;
        public virtual DbSet<TIpprescriptionReturnH> TIpprescriptionReturnHs { get; set; } = null!;
        public virtual DbSet<TIssueToDepartmentDetail> TIssueToDepartmentDetails { get; set; } = null!;
        public virtual DbSet<TIssueToDepartmentHeader> TIssueToDepartmentHeaders { get; set; } = null!;
        public virtual DbSet<TItemMovementReport> TItemMovementReports { get; set; } = null!;
        public virtual DbSet<TLoginAccessDetail> TLoginAccessDetails { get; set; } = null!;
        public virtual DbSet<TLoginStoreDetail> TLoginStoreDetails { get; set; } = null!;
        public virtual DbSet<TLoginUnitDetail> TLoginUnitDetails { get; set; } = null!;
        public virtual DbSet<TMailOutGoing1> TMailOutGoings1 { get; set; } = null!;
        public virtual DbSet<TMailOutgoing> TMailOutgoings { get; set; } = null!;
        public virtual DbSet<TMaterialConsumptionDetail> TMaterialConsumptionDetails { get; set; } = null!;
        public virtual DbSet<TMaterialConsumptionHeader> TMaterialConsumptionHeaders { get; set; } = null!;
        public virtual DbSet<TMedicolegalCertificate> TMedicolegalCertificates { get; set; } = null!;
        public virtual DbSet<TMlcinformation> TMlcinformations { get; set; } = null!;
        public virtual DbSet<TMrdAdmFile> TMrdAdmFiles { get; set; } = null!;
        public virtual DbSet<TMrdcasePaperIssueReturn> TMrdcasePaperIssueReturns { get; set; } = null!;
        public virtual DbSet<TMrpAdjustment> TMrpAdjustments { get; set; } = null!;
        public virtual DbSet<TMrpstockAdjestment> TMrpstockAdjestments { get; set; } = null!;
        public virtual DbSet<TNeroSurgeryOtnote> TNeroSurgeryOtnotes { get; set; } = null!;
        public virtual DbSet<TNurNote> TNurNotes { get; set; } = null!;
        public virtual DbSet<TNurPatientHandover> TNurPatientHandovers { get; set; } = null!;
        public virtual DbSet<TNursingCaseSheet> TNursingCaseSheets { get; set; } = null!;
        public virtual DbSet<TNursingChart> TNursingCharts { get; set; } = null!;
        public virtual DbSet<TNursingFluidChart> TNursingFluidCharts { get; set; } = null!;
        public virtual DbSet<TNursingMedicationChart> TNursingMedicationCharts { get; set; } = null!;
        public virtual DbSet<TNursingMedicationChart1> TNursingMedicationCharts1 { get; set; } = null!;
        public virtual DbSet<TNursingNote> TNursingNotes { get; set; } = null!;
        public virtual DbSet<TNursingOrygenVentilator> TNursingOrygenVentilators { get; set; } = null!;
        public virtual DbSet<TNursingPainAssessment> TNursingPainAssessments { get; set; } = null!;
        public virtual DbSet<TNursingPatientHandover> TNursingPatientHandovers { get; set; } = null!;
        public virtual DbSet<TNursingSugarLevel> TNursingSugarLevels { get; set; } = null!;
        public virtual DbSet<TNursingVital> TNursingVitals { get; set; } = null!;
        public virtual DbSet<TNursingWeight> TNursingWeights { get; set; } = null!;
        public virtual DbSet<TOpeningTransactionDetail> TOpeningTransactionDetails { get; set; } = null!;
        public virtual DbSet<TOpeningTransactionHeader> TOpeningTransactionHeaders { get; set; } = null!;
        public virtual DbSet<TOpinvAdviceList> TOpinvAdviceLists { get; set; } = null!;
        public virtual DbSet<TOprequestList> TOprequestLists { get; set; } = null!;
        public virtual DbSet<TOtReservation> TOtReservations { get; set; } = null!;
        public virtual DbSet<TOtbookingRequest> TOtbookingRequests { get; set; } = null!;
        public virtual DbSet<TOtcathLabBooking> TOtcathLabBookings { get; set; } = null!;
        public virtual DbSet<TPatIcdcdeD> TPatIcdcdeDs { get; set; } = null!;
        public virtual DbSet<TPatIcdcdeH> TPatIcdcdeHs { get; set; } = null!;
        public virtual DbSet<TPathologyReportDetail> TPathologyReportDetails { get; set; } = null!;
        public virtual DbSet<TPathologyReportDetailsArch> TPathologyReportDetailsArches { get; set; } = null!;
        public virtual DbSet<TPathologyReportHeader> TPathologyReportHeaders { get; set; } = null!;
        public virtual DbSet<TPathologyReportTemplateDetail> TPathologyReportTemplateDetails { get; set; } = null!;
        public virtual DbSet<TPatientDetail> TPatientDetails { get; set; } = null!;
        public virtual DbSet<TPatientFeedback> TPatientFeedbacks { get; set; } = null!;
        public virtual DbSet<TPaymentCanteen> TPaymentCanteens { get; set; } = null!;
        public virtual DbSet<TPhColHadOvToAcc> TPhColHadOvToAccs { get; set; } = null!;
        public virtual DbSet<TPhRefund> TPhRefunds { get; set; } = null!;
        public virtual DbSet<TPhSm> TPhSms { get; set; } = null!;
        public virtual DbSet<TPhadvRefundDetail> TPhadvRefundDetails { get; set; } = null!;
        public virtual DbSet<TPhadvanceDetail> TPhadvanceDetails { get; set; } = null!;
        public virtual DbSet<TPhadvanceHeader> TPhadvanceHeaders { get; set; } = null!;
        public virtual DbSet<TPharPatientInformation> TPharPatientInformations { get; set; } = null!;
        public virtual DbSet<TPhoneAppointment> TPhoneAppointments { get; set; } = null!;
        public virtual DbSet<TPrePostOperativeNote> TPrePostOperativeNotes { get; set; } = null!;
        public virtual DbSet<TPrescription> TPrescriptions { get; set; } = null!;
        public virtual DbSet<TPurchaseDetail> TPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TPurchaseHeader> TPurchaseHeaders { get; set; } = null!;
        public virtual DbSet<TRadiologyReportHeader> TRadiologyReportHeaders { get; set; } = null!;
        public virtual DbSet<TRefundDetail> TRefundDetails { get; set; } = null!;
        public virtual DbSet<TReturnFromDepartmentDetail> TReturnFromDepartmentDetails { get; set; } = null!;
        public virtual DbSet<TReturnFromDepartmentHeader> TReturnFromDepartmentHeaders { get; set; } = null!;
        public virtual DbSet<TSalesDetail> TSalesDetails { get; set; } = null!;
        public virtual DbSet<TSalesDraftDet> TSalesDraftDets { get; set; } = null!;
        public virtual DbSet<TSalesDraftHeader> TSalesDraftHeaders { get; set; } = null!;
        public virtual DbSet<TSalesHeader> TSalesHeaders { get; set; } = null!;
        public virtual DbSet<TSalesReturnDetail> TSalesReturnDetails { get; set; } = null!;
        public virtual DbSet<TSalesReturnHeader> TSalesReturnHeaders { get; set; } = null!;
        public virtual DbSet<TSmsOutgoing> TSmsOutgoings { get; set; } = null!;
        public virtual DbSet<TSmsoutGoing1> TSmsoutGoings1 { get; set; } = null!;
        public virtual DbSet<TStockAdjustment> TStockAdjustments { get; set; } = null!;
        public virtual DbSet<TStockLedger> TStockLedgers { get; set; } = null!;
        public virtual DbSet<TStockUpdate> TStockUpdates { get; set; } = null!;
        public virtual DbSet<TSupPayDet> TSupPayDets { get; set; } = null!;
        public virtual DbSet<TTokenNoDoctorWiseMannual> TTokenNoDoctorWiseMannuals { get; set; } = null!;
        public virtual DbSet<TTokenNumberGroupWise> TTokenNumberGroupWises { get; set; } = null!;
        public virtual DbSet<TTokenNumberWithDoctorWise> TTokenNumberWithDoctorWises { get; set; } = null!;
        public virtual DbSet<TWhatsAppSmsOutgoing> TWhatsAppSmsOutgoings { get; set; } = null!;
        public virtual DbSet<TWorkOrderDetail> TWorkOrderDetails { get; set; } = null!;
        public virtual DbSet<TWorkOrderHeader> TWorkOrderHeaders { get; set; } = null!;
        public virtual DbSet<TableSubhash> TableSubhashes { get; set; } = null!;
        public virtual DbSet<TariffMaster> TariffMasters { get; set; } = null!;
        public virtual DbSet<TempPathReportId> TempPathReportIds { get; set; } = null!;
        public virtual DbSet<TempPkCurrStkIssueToDept> TempPkCurrStkIssueToDepts { get; set; } = null!;
        public virtual DbSet<TempPkCurrStkNewBatchNo> TempPkCurrStkNewBatchNos { get; set; } = null!;
        public virtual DbSet<TempPkCurrStkNewRecord> TempPkCurrStkNewRecords { get; set; } = null!;
        public virtual DbSet<TempPkCurrStkReceivedQty> TempPkCurrStkReceivedQties { get; set; } = null!;
        public virtual DbSet<TempPkCurrStkSale> TempPkCurrStkSales { get; set; } = null!;
        public virtual DbSet<TempPkCurrStkSales1> TempPkCurrStkSales1s { get; set; } = null!;
        public virtual DbSet<TempStrId> TempStrIds { get; set; } = null!;
        public virtual DbSet<TempSulpId> TempSulpIds { get; set; } = null!;
        public virtual DbSet<TemppkCurrstkNegativeBalQty> TemppkCurrstkNegativeBalQties { get; set; } = null!;
        public virtual DbSet<TgHtlTmp> TgHtlTmps { get; set; } = null!;
        public virtual DbSet<Tpr> Tprs { get; set; } = null!;
        public virtual DbSet<UserChatMailSystem> UserChatMailSystems { get; set; } = null!;
        public virtual DbSet<UserMailSystemBlog> UserMailSystemBlogs { get; set; } = null!;
        public virtual DbSet<VAdmissionMsg> VAdmissionMsgs { get; set; } = null!;
        public virtual DbSet<VCheckingBalQty> VCheckingBalQties { get; set; } = null!;
        public virtual DbSet<VPaymentBillwisesumAmount> VPaymentBillwisesumAmounts { get; set; } = null!;
        public virtual DbSet<VVisitMsg> VVisitMsgs { get; set; } = null!;
        public virtual DbSet<View1> View1s { get; set; } = null!;
        public virtual DbSet<ViewDoctorshare> ViewDoctorshares { get; set; } = null!;
        public virtual DbSet<ViewTallyPharSalesReceiptNewOld> ViewTallyPharSalesReceiptNewOlds { get; set; } = null!;
        public virtual DbSet<VisitDetail> VisitDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.2.200;Initial Catalog=SSWeb_AIRMID_API;Persist Security Info=True;User ID=DEV001;Password=DEV001;MultipleActiveResultSets=True;Max Pool Size=5000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddCharge>(entity =>
            {
                entity.HasKey(e => e.ChargesId);

                entity.HasIndex(e => new { e.OpdIpdType, e.OpdIpdId, e.IsGenerated, e.IsCancelled, e.IsPackage }, "OP_IPTY");

                entity.HasIndex(e => e.PackageMainChargeId, "_dta_index_AddCharges_11_1597248745__K29_1")
                    .HasFillFactor(80);

                entity.Property(e => e.CPrice)
                    .HasColumnType("money")
                    .HasColumnName("C_Price");

                entity.Property(e => e.CQty).HasColumnName("C_Qty");

                entity.Property(e => e.CTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("C_TotalAmount");

                entity.Property(e => e.ChPrice)
                    .HasColumnType("money")
                    .HasColumnName("Ch_Price");

                entity.Property(e => e.ChQty).HasColumnName("Ch_Qty");

                entity.Property(e => e.ChTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_TotalAmount");

                entity.Property(e => e.ChargesDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesTime).HasColumnType("datetime");

                entity.Property(e => e.CompanyServiceName).HasMaxLength(255);

                entity.Property(e => e.ConcessionAmount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorName).HasMaxLength(500);

                entity.Property(e => e.IsBillableCharity).HasColumnName("IsBillable_Charity");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsDoctorShareGenerated).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsInterimBillFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PackageMainChargeId).HasColumnName("PackageMainChargeID");

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.ServiceCode).HasMaxLength(50);

                entity.Property(e => e.ServiceName).HasMaxLength(255);

                entity.HasOne(d => d.BillNoNavigation)
                    .WithMany(p => p.AddCharges)
                    .HasForeignKey(d => d.BillNo)
                    .HasConstraintName("FK_AddCharges_Bill");
            });

            modelBuilder.Entity<Admission>(entity =>
            {
                entity.ToTable("Admission");

                entity.HasIndex(e => new { e.DocNameId, e.IsDischarged, e.IsCancelled }, "Admis_Cond");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionTime).HasColumnType("datetime");

                entity.Property(e => e.ApprovedAmount).HasColumnType("money");

                entity.Property(e => e.AprovAmount).HasColumnType("money");

                entity.Property(e => e.CBillNo).HasColumnName("C_BillNo");

                entity.Property(e => e.CDisallowedAmt)
                    .HasColumnType("money")
                    .HasColumnName("C_DisallowedAmt");

                entity.Property(e => e.CFinalBillAmt)
                    .HasColumnType("money")
                    .HasColumnName("C_FinalBillAmt");

                entity.Property(e => e.COutsideInvestAmt)
                    .HasColumnType("money")
                    .HasColumnName("C_OutsideInvestAmt");

                entity.Property(e => e.ClaimNo).HasMaxLength(100);

                entity.Property(e => e.CompBillDate).HasColumnType("datetime");

                entity.Property(e => e.CompDisDate).HasColumnType("date");

                entity.Property(e => e.CompDiscount).HasColumnType("money");

                entity.Property(e => e.CompDod)
                    .HasColumnType("datetime")
                    .HasColumnName("CompDOD");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeTime).HasColumnType("datetime");

                entity.Property(e => e.DocNameId).HasColumnName("DocNameID");

                entity.Property(e => e.EstimatedAmount).HasColumnType("money");

                entity.Property(e => e.HAdvAmt)
                    .HasColumnType("money")
                    .HasColumnName("H_AdvAmt");

                entity.Property(e => e.HBalAmt)
                    .HasColumnType("money")
                    .HasColumnName("H_BalAmt");

                entity.Property(e => e.HBillDate)
                    .HasColumnType("datetime")
                    .HasColumnName("H_BillDate");

                entity.Property(e => e.HBillId).HasColumnName("H_BillId");

                entity.Property(e => e.HBillNo)
                    .HasMaxLength(20)
                    .HasColumnName("H_BillNo");

                entity.Property(e => e.HChargeAmt)
                    .HasColumnType("money")
                    .HasColumnName("H_ChargeAmt");

                entity.Property(e => e.HDiscAmt1)
                    .HasColumnType("money")
                    .HasColumnName("H_DiscAmt");

                entity.Property(e => e.HNetAmt)
                    .HasColumnType("money")
                    .HasColumnName("H_NetAmt");

                entity.Property(e => e.HPaidAmt)
                    .HasColumnType("money")
                    .HasColumnName("H_PaidAmt");

                entity.Property(e => e.HTotalAmt)
                    .HasColumnType("money")
                    .HasColumnName("H_TotalAmt");

                entity.Property(e => e.HdiscAmt)
                    .HasColumnType("money")
                    .HasColumnName("HDiscAmt");

                entity.Property(e => e.HosApreAmt).HasColumnType("money");

                entity.Property(e => e.HospitalId).HasColumnName("HospitalID");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.Ipnumber).HasColumnName("IPNumber");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCovidUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.IsMarkForDisNur).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMarkForDisNurDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsMlc).HasColumnName("IsMLC");

                entity.Property(e => e.IsOpToIpconv).HasColumnName("IsOpToIPConv");

                entity.Property(e => e.IsProcessing)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))")
                    .IsFixedLength();

                entity.Property(e => e.IsUpdatedBy).HasColumnName("isUpdatedBy");

                entity.Property(e => e.Ischarity).HasDefaultValueSql("((0))");

                entity.Property(e => e.MedicalApreAmt).HasColumnType("money");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MotherName).HasMaxLength(50);

                entity.Property(e => e.PathApreAmt).HasColumnType("money");

                entity.Property(e => e.PatientTypeId).HasColumnName("PatientTypeID");

                entity.Property(e => e.PharApreAmt).HasColumnType("money");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.PolicyNo).HasMaxLength(100);

                entity.Property(e => e.RadiApreAmt).HasColumnType("money");

                entity.Property(e => e.RecoveredByPatient).HasColumnType("money");

                entity.Property(e => e.RefByName).HasDefaultValueSql("((0))");

                entity.Property(e => e.RefByTypeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.RefDocNameId).HasColumnName("RefDocNameID");

                entity.Property(e => e.RefDoctorDept).HasMaxLength(200);

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RelativeAddress).HasMaxLength(200);

                entity.Property(e => e.RelativeName).HasMaxLength(100);

                entity.Property(e => e.SubTpacomp).HasColumnName("SubTPAComp");
            });

            modelBuilder.Entity<AdmittedPatientBalanceAmount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AdmittedPatientBalanceAmount");

                entity.Property(e => e.ChargesAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");
            });

            modelBuilder.Entity<AdvRefundDetail>(entity =>
            {
                entity.HasKey(e => e.AdvRefId);

                entity.ToTable("AdvRefundDetail");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AdvanceDetail>(entity =>
            {
                entity.ToTable("AdvanceDetail");

                entity.Property(e => e.AdvanceDetailId).HasColumnName("AdvanceDetailID");

                entity.Property(e => e.AdvanceAmount).HasColumnType("money");

                entity.Property(e => e.AdvanceNo).HasMaxLength(50);

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.Reason).HasMaxLength(200);

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.UsedAmount).HasColumnType("money");
            });

            modelBuilder.Entity<AdvanceHeader>(entity =>
            {
                entity.HasKey(e => e.AdvanceId)
                    .HasName("PK_Advance");

                entity.ToTable("AdvanceHeader");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");
            });

            modelBuilder.Entity<AllOutStanding>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("All_OutStanding");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Lbl)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("lbl");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<AllOutStandingPatientWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("All_OutStanding_PatientWise");

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.Lbl)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("lbl");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PatientName).HasMaxLength(500);

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<AllOutStandingPatientWiseLedger>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("All_OutStanding_PatientWise_Ledger");

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.Lbl)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("lbl");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PatientName).HasMaxLength(500);

                entity.Property(e => e.Ptype)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("PType");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.ActionByName).HasMaxLength(250);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityName).HasMaxLength(250);
            });

            modelBuilder.Entity<Bedmaster>(entity =>
            {
                entity.HasKey(e => e.BedId);

                entity.ToTable("Bedmaster");

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.BillNo);

                entity.ToTable("Bill");

                entity.HasIndex(e => e.BillDate, "ind_BillNo")
                    .HasFillFactor(80);

                entity.HasIndex(e => e.OpdIpdId, "ind_OP_IP_ID")
                    .HasFillFactor(90);

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillMonth).HasMaxLength(10);

                entity.Property(e => e.BillPrefix).HasMaxLength(10);

                entity.Property(e => e.BillTime).HasColumnType("datetime");

                entity.Property(e => e.BillYear).HasMaxLength(10);

                entity.Property(e => e.ChConcessionAmt)
                    .HasColumnType("money")
                    .HasColumnName("Ch_ConcessionAmt");

                entity.Property(e => e.ChNetPayAmt)
                    .HasColumnType("money")
                    .HasColumnName("Ch_NetPayAmt");

                entity.Property(e => e.ChTotalAmt)
                    .HasColumnType("money")
                    .HasColumnName("Ch_TotalAmt");

                entity.Property(e => e.CompDiscAmt).HasColumnType("money");

                entity.Property(e => e.CompanyAmt).HasColumnType("money");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CompanyRefNo).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscComments).HasMaxLength(200);

                entity.Property(e => e.DoctorName).HasMaxLength(50);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.IsBillCheck).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsBillShrHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmt).HasColumnType("money");

                entity.Property(e => e.PatientAmt).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(50);

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.PrintBillNo).HasMaxLength(100);

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.SpeTaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpeTaxPer).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalAdvanceAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.HasKey(e => e.BillDetailId)
                    .IsClustered(false);

                entity.HasIndex(e => new { e.BillNo, e.ChargesId }, "Ind_BillNo")
                    .HasFillFactor(80);

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");

                entity.HasOne(d => d.BillNoNavigation)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.BillNo)
                    .HasConstraintName("FK_BillDetails_Bill");
            });

            modelBuilder.Entity<CasePaper>(entity =>
            {
                entity.ToTable("CasePaper");

                entity.Property(e => e.CasePaperId).HasColumnName("CasePaperID");

                entity.Property(e => e.Bmi)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("BMI")
                    .IsFixedLength();

                entity.Property(e => e.Bp)
                    .HasMaxLength(50)
                    .HasColumnName("BP");

                entity.Property(e => e.Bsa)
                    .HasMaxLength(10)
                    .HasColumnName("BSA")
                    .IsFixedLength();

                entity.Property(e => e.Bsl)
                    .HasMaxLength(50)
                    .HasColumnName("BSL");

                entity.Property(e => e.Complaint).HasColumnType("text");

                entity.Property(e => e.Diagnosis).HasColumnType("text");

                entity.Property(e => e.Finding).HasColumnType("text");

                entity.Property(e => e.Hc)
                    .HasMaxLength(10)
                    .HasColumnName("HC")
                    .IsFixedLength();

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.Investigations).HasColumnType("text");

                entity.Property(e => e.PastHistory).HasColumnType("text");

                entity.Property(e => e.PersonalDetails).HasMaxLength(100);

                entity.Property(e => e.Pluse).HasMaxLength(50);

                entity.Property(e => e.PresentHistory).HasColumnType("text");

                entity.Property(e => e.Rr)
                    .HasMaxLength(10)
                    .HasColumnName("RR")
                    .IsFixedLength();

                entity.Property(e => e.SpO2).HasMaxLength(50);

                entity.Property(e => e.Temp)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Weight).HasMaxLength(50);
            });

            modelBuilder.Entity<CashCounter>(entity =>
            {
                entity.ToTable("CashCounter");

                entity.Property(e => e.BillNo).HasMaxLength(100);

                entity.Property(e => e.CashCounterName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Prefix).HasMaxLength(50);
            });

            modelBuilder.Entity<ClassMaster>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("ClassMaster");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CompanyMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("CompanyMaster");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.CompanyShortName).HasMaxLength(50);

                entity.Property(e => e.ContactNumber).HasMaxLength(10);

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(200);

                entity.Property(e => e.FaxNo).HasMaxLength(10);

                entity.Property(e => e.Gstin)
                    .HasMaxLength(50)
                    .HasColumnName("GSTIN");

                entity.Property(e => e.LoginWebsitePassword).HasMaxLength(50);

                entity.Property(e => e.LoginWebsiteUser).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PanNo).HasMaxLength(20);

                entity.Property(e => e.PhoneNo).HasMaxLength(10);

                entity.Property(e => e.PinNo).HasMaxLength(10);

                entity.Property(e => e.Tanno)
                    .HasMaxLength(50)
                    .HasColumnName("TANNo");

                entity.Property(e => e.Website).HasMaxLength(200);
            });

            modelBuilder.Entity<CompanyTypeMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyTypeId);

                entity.ToTable("CompanyTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<ConfigSetting>(entity =>
            {
                entity.HasKey(e => e.ConfigId)
                    .HasName("PK_ConfigSetting_1");

                entity.ToTable("ConfigSetting");

                entity.Property(e => e.AnesthetishId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChkPharmacyDue).HasColumnName("chkPharmacyDue");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FilePathLocation).HasMaxLength(100);

                entity.Property(e => e.GIppaperName)
                    .HasMaxLength(100)
                    .HasColumnName("G_IPPaperName");

                entity.Property(e => e.GIpprintName)
                    .HasMaxLength(100)
                    .HasColumnName("G_IPPrintName");

                entity.Property(e => e.GIsIppaperSetting).HasColumnName("G_IsIPPaperSetting");

                entity.Property(e => e.GIsOppaperSetting)
                    .HasMaxLength(100)
                    .HasColumnName("G_IsOPPaperSetting");

                entity.Property(e => e.GIsPharmacyPaperSetting).HasColumnName("G_IsPharmacyPaperSetting");

                entity.Property(e => e.GOppaperName)
                    .HasMaxLength(100)
                    .HasColumnName("G_OPPaperName");

                entity.Property(e => e.GOpprintName)
                    .HasMaxLength(100)
                    .HasColumnName("G_OPPrintName");

                entity.Property(e => e.GPharmacyPaperName)
                    .HasMaxLength(100)
                    .HasColumnName("G_PharmacyPaperName");

                entity.Property(e => e.GPharmacyPrintName)
                    .HasMaxLength(100)
                    .HasColumnName("G_PharmacyPrintName");

                entity.Property(e => e.GeneralSurgeonId).HasDefaultValueSql("((0))");

                entity.Property(e => e.GenerateOpbillInCashOption).HasColumnName("GenerateOPBillInCashOption");

                entity.Property(e => e.GrnpartyCounterId).HasColumnName("GRNPartyCounterId");

                entity.Property(e => e.IpdAdvanceCounterId).HasColumnName("IPD_Advance_CounterId");

                entity.Property(e => e.IpdBillingCounterId).HasColumnName("IPD_Billing_CounterId");

                entity.Property(e => e.IpdReceiptCounterId).HasColumnName("IPD_Receipt_CounterId");

                entity.Property(e => e.IpdRefundOfAdvanceCounterId).HasColumnName("IPD_Refund_of_Advance_CounterId");

                entity.Property(e => e.IpdRefundOfBillCounterId).HasColumnName("IPD_Refund_of_Bill_CounterId");

                entity.Property(e => e.IpdayCareNo).HasColumnName("IPDayCareNo");

                entity.Property(e => e.Ipdprefix)
                    .HasMaxLength(5)
                    .HasColumnName("IPDPrefix");

                entity.Property(e => e.Ipno)
                    .HasMaxLength(20)
                    .HasColumnName("IPNo");

                entity.Property(e => e.IpnoEmg).HasColumnName("IPNoEmg");

                entity.Property(e => e.Ipprefix)
                    .HasMaxLength(20)
                    .HasColumnName("IPPrefix");

                entity.Property(e => e.IsPathologistDr).HasDefaultValueSql("((0))");

                entity.Property(e => e.LabSampleNo)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NeroSurgeonId).HasDefaultValueSql("((0))");

                entity.Property(e => e.OpdAdvanceCounterId).HasColumnName("OPD_Advance_CounterId");

                entity.Property(e => e.OpdBillingCounterId).HasColumnName("OPD_Billing_CounterId");

                entity.Property(e => e.OpdReceiptCounterId).HasColumnName("OPD_Receipt_CounterId");

                entity.Property(e => e.OpdRefundAdvanceCounterId).HasColumnName("OPD_Refund_Advance_CounterId");

                entity.Property(e => e.OpdRefundBillCounterId).HasColumnName("OPD_Refund_Bill_CounterId");

                entity.Property(e => e.Opno)
                    .HasMaxLength(20)
                    .HasColumnName("OPNo");

                entity.Property(e => e.Opprefix)
                    .HasMaxLength(20)
                    .HasColumnName("OPPrefix");

                entity.Property(e => e.Otcharges).HasColumnName("OTCharges");

                entity.Property(e => e.PathDepartment).HasDefaultValueSql("((0))");

                entity.Property(e => e.PharIpadvCounterId).HasColumnName("PharIPAdvCounterId");

                entity.Property(e => e.PharmacyReceiptCounterId).HasColumnName("PharmacyReceipt_CounterId");

                entity.Property(e => e.PharmacySalesCounterId).HasColumnName("PharmacySales_CounterId");

                entity.Property(e => e.PharmacySalesReturnCounterId).HasColumnName("PharmacySalesReturn_CounterId");

                entity.Property(e => e.PopOpbillAfterVisit).HasColumnName("PopOPBillAfterVisit");

                entity.Property(e => e.PopPayAfterOpbill).HasColumnName("PopPayAfterOPBill");

                entity.Property(e => e.PrintIpdafterAdm).HasColumnName("PrintIPDAfterAdm");

                entity.Property(e => e.PrintOpdcaseAfterVisit).HasColumnName("PrintOPDCaseAfterVisit");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RegPrefix).HasMaxLength(20);
            });

            modelBuilder.Entity<DbGenderMaster>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PK_GenderMaster");

                entity.ToTable("DB_GenderMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DbPrefixMaster>(entity =>
            {
                entity.HasKey(e => e.PrefixId)
                    .HasName("PK_PrefixMaster");

                entity.ToTable("DB_PrefixMaster");

                entity.Property(e => e.PrefixId).HasColumnName("PrefixID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PrefixName).HasMaxLength(100);

                entity.Property(e => e.SexId).HasColumnName("SexID");
            });

            modelBuilder.Entity<DbPurposeMaster>(entity =>
            {
                entity.HasKey(e => e.PurposeId)
                    .HasName("PK_PurposeMaster");

                entity.ToTable("DB_PurposeMaster");

                entity.Property(e => e.PurposeName).HasMaxLength(100);
            });

            modelBuilder.Entity<Discharge>(entity =>
            {
                entity.ToTable("Discharge");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeTime).HasColumnType("datetime");

                entity.Property(e => e.DischargedRmoid).HasColumnName("DischargedRMOID");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledby).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMrdreceived)
                    .HasColumnName("IsMRDReceived")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MrdreceivedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("MRDReceivedDate");

                entity.Property(e => e.MrdreceivedName)
                    .HasMaxLength(300)
                    .HasColumnName("MRDReceivedName");

                entity.Property(e => e.MrdreceivedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("MRDReceivedTime");

                entity.Property(e => e.MrdreceivedUserId).HasColumnName("MRDReceivedUserID");

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<DischargeSummary>(entity =>
            {
                entity.ToTable("DischargeSummary");

                entity.Property(e => e.AddedByDate).HasColumnType("datetime");

                entity.Property(e => e.ClaimNumber)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.ClinicalConditionOnAdmisssion).HasColumnType("text");

                entity.Property(e => e.ClinicalFinding).HasColumnType("text");

                entity.Property(e => e.ConditionAtTheTimeOfDischarge).HasColumnType("text");

                entity.Property(e => e.Diagnosis).HasColumnType("text");

                entity.Property(e => e.DischargeId).HasColumnName("DischargeID");

                entity.Property(e => e.DischargeSummaryDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeSummaryTime).HasColumnType("datetime");

                entity.Property(e => e.DoctorAssistantName).HasMaxLength(100);

                entity.Property(e => e.Followupdate).HasColumnType("datetime");

                entity.Property(e => e.History).HasColumnType("text");

                entity.Property(e => e.Icd10code)
                    .HasColumnType("text")
                    .HasColumnName("ICD10CODE");

                entity.Property(e => e.Investigation).HasColumnType("text");

                entity.Property(e => e.LifeStyle).HasColumnType("text");

                entity.Property(e => e.OpDate).HasColumnType("datetime");

                entity.Property(e => e.OpertiveNotes).HasColumnType("text");

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.OtherConDrOpinions).HasColumnType("text");

                entity.Property(e => e.PainManagementTechnique).HasColumnType("text");

                entity.Property(e => e.PreOthNumber)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Radiology).HasColumnType("text");

                entity.Property(e => e.Remark).HasColumnType("text");

                entity.Property(e => e.SurgeryProcDone).HasColumnType("text");

                entity.Property(e => e.TreatmentAdvisedAfterDischarge).HasColumnType("text");

                entity.Property(e => e.TreatmentGiven).HasColumnType("text");

                entity.Property(e => e.UpdatedByDate).HasColumnType("datetime");

                entity.Property(e => e.WarningSymptoms).HasColumnType("text");
            });

            modelBuilder.Entity<DischargeTypeMaster>(entity =>
            {
                entity.HasKey(e => e.DischargeTypeId);

                entity.ToTable("DischargeTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeTypeName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DoctorMaster>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("DoctorMaster");

                entity.Property(e => e.AadharCardNo).HasMaxLength(20);

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.AgeDay)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AgeMonth)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.Education).HasMaxLength(200);

                entity.Property(e => e.Esino)
                    .HasMaxLength(50)
                    .HasColumnName("ESINO");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MahRegDate).HasColumnType("datetime");

                entity.Property(e => e.MahRegNo).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PanCardNo).HasMaxLength(20);

                entity.Property(e => e.PassportNo).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Pin).HasMaxLength(10);

                entity.Property(e => e.PrefixId).HasColumnName("PrefixID");

                entity.Property(e => e.RefDocHospitalName).HasMaxLength(50);

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.Property(e => e.RegNo).HasMaxLength(50);

                entity.Property(e => e.Signature).HasMaxLength(500);
            });

            modelBuilder.Entity<DoctorName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DoctorName");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.Doctorname1)
                    .HasMaxLength(106)
                    .HasColumnName("Doctorname");
            });

            modelBuilder.Entity<DoctorShare>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DoctorShare");

                entity.Property(e => e.Doctorshareid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DoctorTypeMaster>(entity =>
            {
                entity.ToTable("DoctorTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorType).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DynamicExecuteSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK_DynamicEmailSchedule");

                entity.ToTable("DynamicExecuteSchedule");

                entity.Property(e => e.ExecuteTime).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Query).HasMaxLength(4000);

                entity.Property(e => e.ScheduleExecuteType).HasMaxLength(50);

                entity.Property(e => e.WeekDayName).HasMaxLength(50);
            });

            modelBuilder.Entity<DynamicExecuteScheduleLog>(entity =>
            {
                entity.HasKey(e => e.SrNo);

                entity.ToTable("DynamicExecuteScheduleLog");

                entity.Property(e => e.ExecuteDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailConfiguration>(entity =>
            {
                entity.ToTable("Email_Configuration");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_Address");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MailServerSmtp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MailServer_SMTP");

                entity.Property(e => e.Password).HasMaxLength(300);

                entity.Property(e => e.RequiredSquiredPasswordAuthentication)
                    .HasColumnName("Required_Squired_Password_Authentication")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ServerTimeout)
                    .HasColumnName("Server_Timeout")
                    .HasDefaultValueSql("((120))");

                entity.Property(e => e.SmtpPort)
                    .HasColumnName("SMTP_Port")
                    .HasDefaultValueSql("((25))");

                entity.Property(e => e.SmtpRequiredAuthentication)
                    .HasColumnName("SMTP_Required_Authentication")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<EmailNotificationDatum>(entity =>
            {
                entity.ToTable("Email_Notification_Data");

                entity.Property(e => e.AttachmentPath).HasMaxLength(1000);

                entity.Property(e => e.EmailCc)
                    .HasMaxLength(100)
                    .HasColumnName("EmailCC");

                entity.Property(e => e.SendDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Subject).HasMaxLength(100);

                entity.Property(e => e.ToAddress).HasMaxLength(100);
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.ToTable("EmployeeMaster");

                entity.Property(e => e.AcType)
                    .HasMaxLength(2)
                    .HasComment("0=Saving,1=Current");

                entity.Property(e => e.AcessCardNo).HasMaxLength(50);

                entity.Property(e => e.ApplicableFrom).HasColumnType("datetime");

                entity.Property(e => e.BankAcNo).HasMaxLength(15);

                entity.Property(e => e.BankBranchName).HasMaxLength(20);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.ConfirmationDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.DateofAnniversary).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(20);

                entity.Property(e => e.EmpPhoto).HasColumnType("image");

                entity.Property(e => e.EmpPhotoPath).HasMaxLength(1000);

                entity.Property(e => e.EmpSign).HasColumnType("image");

                entity.Property(e => e.EmpSinPath).HasMaxLength(1000);

                entity.Property(e => e.Esino)
                    .HasMaxLength(10)
                    .HasColumnName("ESINo");

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.IsOtapplicable).HasColumnName("IsOTApplicable");

                entity.Property(e => e.Issupp)
                    .HasColumnName("issupp")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Laddress)
                    .HasMaxLength(200)
                    .HasColumnName("LAddress")
                    .HasComment("L=Local");

                entity.Property(e => e.LareaId).HasColumnName("LAreaId");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.LastWorkingDate).HasColumnType("datetime");

                entity.Property(e => e.LcityId).HasColumnName("LCityId");

                entity.Property(e => e.LcontactNo)
                    .HasMaxLength(15)
                    .HasColumnName("LContactNo");

                entity.Property(e => e.LcountryId).HasColumnName("LCountryId");

                entity.Property(e => e.LdistrictId).HasColumnName("LDistrictId");

                entity.Property(e => e.Lpin)
                    .HasMaxLength(10)
                    .HasColumnName("LPIN");

                entity.Property(e => e.LstateId).HasColumnName("LStateId");

                entity.Property(e => e.Micrno)
                    .HasMaxLength(15)
                    .HasColumnName("MICRNo");

                entity.Property(e => e.MiddleName).HasMaxLength(20);

                entity.Property(e => e.MobileNo).HasMaxLength(15);

                entity.Property(e => e.Paddress)
                    .HasMaxLength(200)
                    .HasColumnName("PAddress")
                    .HasComment("P=Permanent");

                entity.Property(e => e.Panno)
                    .HasMaxLength(15)
                    .HasColumnName("PANNo");

                entity.Property(e => e.PareaId).HasColumnName("PAreaId");

                entity.Property(e => e.PassportExpirydate).HasColumnType("datetime");

                entity.Property(e => e.PassportIssuePlace).HasMaxLength(50);

                entity.Property(e => e.PassportIssuedate).HasColumnType("datetime");

                entity.Property(e => e.Passpotno)
                    .HasMaxLength(10)
                    .HasColumnName("PASSPOTNo");

                entity.Property(e => e.PatientId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PcityId).HasColumnName("PCityId");

                entity.Property(e => e.PcontactNo)
                    .HasMaxLength(15)
                    .HasColumnName("PContactNo");

                entity.Property(e => e.PcountryId).HasColumnName("PCountryId");

                entity.Property(e => e.PdistrictId).HasColumnName("PDistrictId");

                entity.Property(e => e.Pfno)
                    .HasMaxLength(15)
                    .HasColumnName("PFNo");

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.Property(e => e.Ppin)
                    .HasMaxLength(10)
                    .HasColumnName("PPIN")
                    .HasComment("");

                entity.Property(e => e.PstateId).HasColumnName("PStateId");

                entity.Property(e => e.ReportingTo).HasColumnName("ReportingTO");

                entity.Property(e => e.ResignReason).HasMaxLength(150);

                entity.Property(e => e.ResignationDt).HasColumnType("datetime");

                entity.Property(e => e.TelNo).HasMaxLength(15);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.WardId).HasColumnName("WardID");
            });

            modelBuilder.Entity<EmployeeMasterDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeMasterDetId);

                entity.ToTable("EmployeeMasterDetail");

                entity.Property(e => e.AcType).HasMaxLength(2);

                entity.Property(e => e.ApplicableFrom).HasColumnType("datetime");

                entity.Property(e => e.BankAcNo).HasMaxLength(15);

                entity.Property(e => e.BankBranchName).HasMaxLength(20);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateOfJoining).HasColumnType("datetime");

                entity.Property(e => e.DateofAnniversary).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(20);

                entity.Property(e => e.EmpPhoto).HasColumnType("image");

                entity.Property(e => e.EmpPhotoPath).HasMaxLength(1000);

                entity.Property(e => e.EmpSign).HasColumnType("image");

                entity.Property(e => e.EmpSinPath).HasMaxLength(1000);

                entity.Property(e => e.Esino)
                    .HasMaxLength(10)
                    .HasColumnName("ESINo");

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.Laddress)
                    .HasMaxLength(200)
                    .HasColumnName("LAddress");

                entity.Property(e => e.LareaId).HasColumnName("LAreaId");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.LcityId).HasColumnName("LCityId");

                entity.Property(e => e.LcontactNo)
                    .HasMaxLength(15)
                    .HasColumnName("LContactNo");

                entity.Property(e => e.LcountryId).HasColumnName("LCountryId");

                entity.Property(e => e.LdistrictId).HasColumnName("LDistrictId");

                entity.Property(e => e.Lpin)
                    .HasMaxLength(10)
                    .HasColumnName("LPIN");

                entity.Property(e => e.LstateId).HasColumnName("LStateId");

                entity.Property(e => e.Micrno)
                    .HasMaxLength(15)
                    .HasColumnName("MICRNo");

                entity.Property(e => e.MiddleName).HasMaxLength(20);

                entity.Property(e => e.MobileNo).HasMaxLength(15);

                entity.Property(e => e.Paddress)
                    .HasMaxLength(200)
                    .HasColumnName("PAddress");

                entity.Property(e => e.Panno)
                    .HasMaxLength(15)
                    .HasColumnName("PANNo");

                entity.Property(e => e.PareaId).HasColumnName("PAreaId");

                entity.Property(e => e.Passpotno)
                    .HasMaxLength(10)
                    .HasColumnName("PASSPOTNo");

                entity.Property(e => e.PcityId).HasColumnName("PCityId");

                entity.Property(e => e.PcontactNo)
                    .HasMaxLength(15)
                    .HasColumnName("PContactNo");

                entity.Property(e => e.PcountryId).HasColumnName("PCountryId");

                entity.Property(e => e.PdistrictId).HasColumnName("PDistrictId");

                entity.Property(e => e.Pfno)
                    .HasMaxLength(15)
                    .HasColumnName("PFNo");

                entity.Property(e => e.Ppin)
                    .HasMaxLength(10)
                    .HasColumnName("PPIN");

                entity.Property(e => e.PstateId).HasColumnName("PStateId");

                entity.Property(e => e.ResignationDt).HasColumnType("datetime");

                entity.Property(e => e.TelNo).HasMaxLength(15);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.WardId).HasColumnName("WardID");
            });

            modelBuilder.Entity<EmployeeUnitMapping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EmployeeUnitMapping");
            });

            modelBuilder.Entity<FileMaster>(entity =>
            {
                entity.ToTable("FileMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocName).HasMaxLength(500);

                entity.Property(e => e.DocSavedName).HasMaxLength(500);
            });

            modelBuilder.Entity<GeTIpPrescriptionItemDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GeT_IP_PrescriptionItemDet");

                entity.Property(e => e.IpmedId).HasColumnName("IPMedID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");
            });

            modelBuilder.Entity<GeTTPrescriptionItemDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GeT_t_PrescriptionItemDet");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.OpdIpdIp).HasColumnName("OPD_IPD_IP");
            });

            modelBuilder.Entity<GeniusBufferresult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GENIUS_BUFFERRESULT");

                entity.Property(e => e.Dttime)
                    .HasColumnType("datetime")
                    .HasColumnName("DTTIME");

                entity.Property(e => e.Labno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LABNO");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Machineparamid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEPARAMID");

                entity.Property(e => e.Qcflag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("QCFLAG");

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RESULT");

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");
            });

            modelBuilder.Entity<GeniusControl>(entity =>
            {
                entity.HasKey(e => new { e.Controlid, e.Machineid })
                    .HasName("PK_GENIUS_CONTROLS_CM");

                entity.ToTable("GENIUS_CONTROLS");

                entity.Property(e => e.Controlid)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("CONTROLID");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.Controlalias)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLALIAS");

                entity.Property(e => e.Controlbc)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLBC");

                entity.Property(e => e.Controlname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLNAME");

                entity.Property(e => e.Controltype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLTYPE");

                entity.Property(e => e.Enddate)
                    .HasColumnType("datetime")
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Lotno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LOTNO");

                entity.Property(e => e.Mfgname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MFGNAME");

                entity.Property(e => e.Sampletypeid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SAMPLETYPEID");

                entity.Property(e => e.Seqno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("SEQNO");

                entity.Property(e => e.Startdate)
                    .HasColumnType("datetime")
                    .HasColumnName("STARTDATE");

                entity.Property(e => e.Valid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALID");
            });

            modelBuilder.Entity<GeniusControlparam>(entity =>
            {
                entity.HasKey(e => new { e.Controlid, e.Controlparamid, e.Machineid })
                    .HasName("PK_GENIUS_CONTROLPARAM_CCM");

                entity.ToTable("GENIUS_CONTROLPARAM");

                entity.Property(e => e.Controlid)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("CONTROLID");

                entity.Property(e => e.Controlparamid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLPARAMID");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Calccv).HasColumnName("CALCCV");

                entity.Property(e => e.Calcmean).HasColumnName("CALCMEAN");

                entity.Property(e => e.Calcsd).HasColumnName("CALCSD");

                entity.Property(e => e.Controlparamname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLPARAMNAME");

                entity.Property(e => e.Cv).HasColumnName("CV");

                entity.Property(e => e.Highlimit).HasColumnName("HIGHLIMIT");

                entity.Property(e => e.Lotcv).HasColumnName("LOTCV");

                entity.Property(e => e.Lotmean).HasColumnName("LOTMEAN");

                entity.Property(e => e.Lotsd).HasColumnName("LOTSD");

                entity.Property(e => e.Lowlimit).HasColumnName("LOWLIMIT");

                entity.Property(e => e.Mean).HasColumnName("MEAN");

                entity.Property(e => e.Prece).HasColumnName("PRECE");

                entity.Property(e => e.Runcount)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RUNCOUNT");

                entity.Property(e => e.Sampletypeid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SAMPLETYPEID");

                entity.Property(e => e.Sd).HasColumnName("SD");

                entity.Property(e => e.Seq)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("SEQ");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UNIT");

                entity.Property(e => e.Valid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALID");
            });

            modelBuilder.Entity<GeniusControltype>(entity =>
            {
                entity.HasKey(e => e.Controltype)
                    .HasName("PK_GENIUS_CONTROLTYPES_CT");

                entity.ToTable("GENIUS_CONTROLTYPES");

                entity.Property(e => e.Controltype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLTYPE");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Seq)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("SEQ");
            });

            modelBuilder.Entity<GeniusITable>(entity =>
            {
                entity.HasKey(e => new { e.RefVisitno, e.TestprofCode });

                entity.ToTable("GENIUS_I_TABLE");

                entity.Property(e => e.RefVisitno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REF_VISITNO");

                entity.Property(e => e.TestprofCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TESTPROF_CODE");

                entity.Property(e => e.Adddate)
                    .HasColumnType("datetime")
                    .HasColumnName("ADDDATE");

                entity.Property(e => e.Addtime)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("ADDTIME");

                entity.Property(e => e.Admissionno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ADMISSIONNO");

                entity.Property(e => e.Ageunit)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("AGEUNIT");

                entity.Property(e => e.BcPrinted)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BC_PRINTED");

                entity.Property(e => e.Datestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("DATESTAMP");

                entity.Property(e => e.DocName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DOC_NAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Ipopflag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IPOPFLAG");

                entity.Property(e => e.Labno)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("LABNO");

                entity.Property(e => e.Mresult)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MRESULT");

                entity.Property(e => e.Paramcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARAMCODE");

                entity.Property(e => e.Paramname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARAMNAME");

                entity.Property(e => e.PatDob)
                    .HasColumnType("datetime")
                    .HasColumnName("PAT_DOB");

                entity.Property(e => e.Patage)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PATAGE");

                entity.Property(e => e.Patfname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PATFNAME");

                entity.Property(e => e.Patienttypeclass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PATIENTTYPECLASS");

                entity.Property(e => e.Patlname)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PATLNAME");

                entity.Property(e => e.Patmname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PATMNAME");

                entity.Property(e => e.Pinno)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PINNO");

                entity.Property(e => e.Processed)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PROCESSED");

                entity.Property(e => e.Reqdatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("REQDATETIME");

                entity.Property(e => e.Seqno)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("SEQNO");

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<GeniusMachinemaster>(entity =>
            {
                entity.HasKey(e => e.Machineid)
                    .HasName("PK_GENIUS_MACHINEMASTER_M");

                entity.ToTable("GENIUS_MACHINEMASTER");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Accessionno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCESSIONNO");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Commmode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("COMMMODE");

                entity.Property(e => e.Commport)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("COMMPORT");

                entity.Property(e => e.Compname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMPNAME");

                entity.Property(e => e.Export)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EXPORT");

                entity.Property(e => e.Graphpresent)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GRAPHPRESENT");

                entity.Property(e => e.Graphstartchar)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GRAPHSTARTCHAR");

                entity.Property(e => e.Issampleid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ISSAMPLEID");

                entity.Property(e => e.Machinename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINENAME");

                entity.Property(e => e.Nooftests)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NOOFTESTS");

                entity.Property(e => e.Settings)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SETTINGS");

                entity.Property(e => e.Testid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TESTID");
            });

            modelBuilder.Entity<GeniusMachineparam>(entity =>
            {
                entity.HasKey(e => new { e.Machineid, e.Machineparamid, e.Suffix, e.Paramcode })
                    .HasName("PK_GENIUS_MACHINEPARAM_MMSP");

                entity.ToTable("GENIUS_MACHINEPARAM");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Machineparamid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEPARAMID");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("SUFFIX");

                entity.Property(e => e.Paramcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARAMCODE");

                entity.Property(e => e.SampleType)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeniusMacid>(entity =>
            {
                entity.HasKey(e => new { e.Machineid, e.Machineparamid, e.Suffix })
                    .HasName("PK_GENIUS_MACID_MMS");

                entity.ToTable("GENIUS_MACID");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Machineparamid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEPARAMID");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("SUFFIX");

                entity.Property(e => e.Assayno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ASSAYNO");

                entity.Property(e => e.Conversion).HasColumnName("CONVERSION");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Program)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PROGRAM");

                entity.Property(e => e.Rounding)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ROUNDING");

                entity.Property(e => e.SampleType)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeniusParam>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GENIUS_PARAM");

                entity.Property(e => e.Paramcode)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PARAMCODE");

                entity.Property(e => e.Paramname)
                    .HasMaxLength(100)
                    .HasColumnName("PARAMNAME");
            });

            modelBuilder.Entity<GeniusQcresult>(entity =>
            {
                entity.HasKey(e => new { e.Controlid, e.Controlparamid, e.Machineid, e.Rundate, e.Runtime })
                    .HasName("PK_GENIUS_QCRESULT_CCMRR");

                entity.ToTable("GENIUS_QCRESULT");

                entity.Property(e => e.Controlid)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("CONTROLID");

                entity.Property(e => e.Controlparamid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLPARAMID");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.Runtime)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNTIME");

                entity.Property(e => e.Accept)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ACCEPT");

                entity.Property(e => e.Comments)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.Controlname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLNAME");

                entity.Property(e => e.Controltype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CONTROLTYPE");

                entity.Property(e => e.Enteredby)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ENTEREDBY");

                entity.Property(e => e.Entrydttime)
                    .HasColumnType("datetime")
                    .HasColumnName("ENTRYDTTIME");

                entity.Property(e => e.Lotno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LOTNO");

                entity.Property(e => e.Rackpos)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RACKPOS");

                entity.Property(e => e.Rejectcomment)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("REJECTCOMMENT");

                entity.Property(e => e.Result).HasColumnName("RESULT");

                entity.Property(e => e.Resultflag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RESULTFLAG");

                entity.Property(e => e.Resulttext)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("RESULTTEXT");

                entity.Property(e => e.Ruleflag)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RULEFLAG");

                entity.Property(e => e.Runno)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("RUNNO");

                entity.Property(e => e.Sampletypeid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SAMPLETYPEID");

                entity.Property(e => e.Valid)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALID");

                entity.Property(e => e.Verified)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VERIFIED");

                entity.Property(e => e.Verifiedby)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("VERIFIEDBY");

                entity.Property(e => e.Verifieddttime)
                    .HasColumnType("datetime")
                    .HasColumnName("VERIFIEDDTTIME");
            });

            modelBuilder.Entity<GeniusSampledatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GENIUS_SAMPLEDATA");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Finalflag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FINALFLAG");

                entity.Property(e => e.Machineid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MACHINEID");

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RESULT");

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.Sampleid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SAMPLEID");

                entity.Property(e => e.Sdate)
                    .HasColumnType("datetime")
                    .HasColumnName("SDATE");

                entity.Property(e => e.Srno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SRNO");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("SUFFIX");

                entity.Property(e => e.Testid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TESTID");

                entity.Property(e => e.Tmpvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMPVALUE");

                entity.Property(e => e.Transferflag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRANSFERFLAG");
            });

            modelBuilder.Entity<GeniusUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GENIUS_USER");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<GetPrescriptionItemDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Get_PrescriptionItemDet");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.OpdIpdIp).HasColumnName("OPD_IPD_IP");
            });

            modelBuilder.Entity<GetSalesDraftBillItemDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Get_SalesDraftBillItemDet");

                entity.Property(e => e.DsalesId).HasColumnName("DSalesId");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");
            });

            modelBuilder.Entity<GroupMaster>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("GroupMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.IsConsolidatedDr)
                    .HasColumnName("IsConsolidatedDR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<HmsLog>(entity =>
            {
                entity.HasKey(e => e.LogNumber);

                entity.ToTable("HMS_Logs");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<HospitalMaster>(entity =>
            {
                entity.HasKey(e => e.HospitalId);

                entity.ToTable("HospitalMaster");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(500)
                    .HasColumnName("EmailID");

                entity.Property(e => e.HospitalAddress).HasMaxLength(500);

                entity.Property(e => e.HospitalHeaderLine).HasMaxLength(50);

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.IpdAdvanceCounterId).HasColumnName("IPD_Advance_CounterId");

                entity.Property(e => e.IpdAdvanceReceiptCounterId).HasColumnName("IPD_Advance_Receipt_CounterId");

                entity.Property(e => e.IpdBillingCounterId).HasColumnName("IPD_Billing_CounterId");

                entity.Property(e => e.IpdReceiptCounterId).HasColumnName("IPD_Receipt_CounterId");

                entity.Property(e => e.IpdRefundOfAdvanceCounterId).HasColumnName("IPD_Refund_of_Advance_CounterId");

                entity.Property(e => e.IpdRefundOfAdvanceReceiptCounterId).HasColumnName("IPD_Refund_of_Advance_Receipt_CounterId");

                entity.Property(e => e.IpdRefundOfBillCounterId).HasColumnName("IPD_Refund_of_Bill_CounterId");

                entity.Property(e => e.IpdRefundOfBillReceiptCounterId).HasColumnName("IPD_Refund_of_Bill_Receipt_CounterId");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpdAdvanceCounterId).HasColumnName("OPD_Advance_CounterId");

                entity.Property(e => e.OpdBillingCounterId).HasColumnName("OPD_Billing_CounterId");

                entity.Property(e => e.OpdReceiptCounterId).HasColumnName("OPD_Receipt_CounterId");

                entity.Property(e => e.OpdRefundAdvanceCounterId).HasColumnName("OPD_Refund_Advance_CounterId");

                entity.Property(e => e.OpdRefundBillCounterId).HasColumnName("OPD_Refund_Bill_CounterId");

                entity.Property(e => e.OpdRefundBillReceiptCounterId).HasColumnName("OPD_Refund_Bill_Receipt_CounterId");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Pin).HasMaxLength(10);

                entity.Property(e => e.WebSiteInfo).HasMaxLength(500);
            });

            modelBuilder.Entity<IndentDetial>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IndentDetials");

                entity.Property(e => e.ItemName).HasMaxLength(200);
            });

            modelBuilder.Entity<InitiateDischarge>(entity =>
            {
                entity.HasKey(e => e.InitateDiscId);

                entity.ToTable("initiateDischarge");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.ApprovedDatetime).HasColumnType("datetime");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<IpadmBedtransferSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPAdm_Bedtransfer_SMSQuery");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(699)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpadmRefDoctorSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPAdmRefDoctorSMSQuery");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(533)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpadmRefInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPAdmRefInfo");

                entity.Property(e => e.AdmVisitId).HasColumnName("AdmVisitID");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(678)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpadmSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPAdmSMSQuery");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(695)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpadvDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPAdvDet");

                entity.Property(e => e.AdvAmt).HasColumnType("money");

                entity.Property(e => e.AdvBalAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");
            });

            modelBuilder.Entity<IpadvRefund>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPAdvRefund");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(50)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.RefundAmount).HasColumnType("money");
            });

            modelBuilder.Entity<IpbedTranferSmsqueryTempleteSm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPBedTranferSMSQueryTemplete_SMS");

                entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.DoctorName).HasMaxLength(152);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.FromWardBedNo).HasMaxLength(201);

                entity.Property(e => e.FromWardId).HasColumnName("FromWardID");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.ToWardBedNo).HasMaxLength(201);

                entity.Property(e => e.ToWardId).HasColumnName("ToWardID");

                entity.Property(e => e.TodaysDate).HasMaxLength(10);

                entity.Property(e => e.TranDatetime)
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IpbedTransferSm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPBedTransferSMS");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(683)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpcompanyPayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPCompanyPayment");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardDate).HasColumnType("datetime");

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(50)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PayAmount).HasColumnType("money");

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");

                entity.Property(e => e.SettlementTime).HasColumnType("datetime");

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.WrfAmount).HasColumnType("money");
            });

            modelBuilder.Entity<IpddiscRefDoctorSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPDDiscRefDoctorSMSQuery");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(683)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpddischargeSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPDDischargeSMSQuery");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(683)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpotbookingSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPOTBookingSMSQuery");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(506)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<IpsmsqueryTempleteSm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IPSMSQueryTemplete_SMS");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AgeYear).HasMaxLength(10);

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.Doadatetime)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DOADatetime");

                entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.DoctorName).HasMaxLength(105);

                entity.Property(e => e.Doddatetime)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DODDatetime");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.RefDoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.RefDoctorName).HasMaxLength(105);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.TodaysDate).HasMaxLength(10);

                entity.Property(e => e.WardName).HasMaxLength(100);
            });

            modelBuilder.Entity<LCurstkItemWiseBalQty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("l_Curstk_ItemWise_BalQty");
            });

            modelBuilder.Entity<LMaxSupplierNameForItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("l_Max_SupplierNameForItem");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.Mrp)
                    .HasColumnType("money")
                    .HasColumnName("MRP");

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.SupplierName).HasMaxLength(100);
            });

            modelBuilder.Entity<LoadingLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("loading_log");

                entity.Property(e => e.LedgerDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LocationMaster>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("LocationMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LocationName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LoginManager>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("LoginManager");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IsDiscApply).HasColumnName("isDiscApply");

                entity.Property(e => e.IsGrnverify)
                    .HasColumnName("IsGRNVerify")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPoinchargeVerify).HasColumnName("IsPOInchargeVerify");

                entity.Property(e => e.IsPoverify)
                    .HasColumnName("IsPOVerify")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRefDocEditOpt).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LoginStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.MailDomain).HasMaxLength(50);

                entity.Property(e => e.MailId).HasMaxLength(50);

                entity.Property(e => e.MobileToken).HasMaxLength(250);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PharIpopt).HasColumnName("PharIPOpt");

                entity.Property(e => e.PharOpopt).HasColumnName("PharOPOpt");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.UserToken).HasMaxLength(250);
            });

            modelBuilder.Entity<LvwAddCharge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwAddCharges");

                entity.Property(e => e.CPrice)
                    .HasColumnType("money")
                    .HasColumnName("C_Price");

                entity.Property(e => e.CQty).HasColumnName("C_qty");

                entity.Property(e => e.CTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("C_TotalAmount");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("CDate");

                entity.Property(e => e.ChargesAddedName).HasMaxLength(100);

                entity.Property(e => e.ChargesCancelledByName).HasMaxLength(100);

                entity.Property(e => e.ChargesDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.CompanyServiceName).HasMaxLength(500);

                entity.Property(e => e.ConcessionAmount).HasColumnType("money");

                entity.Property(e => e.DoctorName).HasMaxLength(156);

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ServiceName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwAdmPatListForSearch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwAdmPatListForSearch");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmittedDr).HasMaxLength(101);

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.Doa)
                    .HasMaxLength(21)
                    .HasColumnName("DOA");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.PatientName).HasMaxLength(302);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwAdmPatListForSearchAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwAdmPatListForSearch_All");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmittedDr).HasMaxLength(101);

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.Doa)
                    .HasMaxLength(21)
                    .HasColumnName("DOA");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.PatientName).HasMaxLength(302);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwAdmPatListForSearchRefPhaAdv>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwAdmPatListForSearch_RefPhaAdv");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmittedDr).HasMaxLength(101);

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.Doa)
                    .HasMaxLength(21)
                    .HasColumnName("DOA");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.PatientName).HasMaxLength(302);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwAdmissionList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwAdmissionList");

                entity.Property(e => e.AdmDateTime)
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmissionTime).HasColumnType("datetime");

                entity.Property(e => e.AgeDay)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AgeMonth)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeTime).HasColumnType("datetime");

                entity.Property(e => e.Doa)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DOA");

                entity.Property(e => e.DocNameId).HasColumnName("DocNameID");

                entity.Property(e => e.Doctorname).HasMaxLength(156);

                entity.Property(e => e.Dot)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DOT");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.HospitalId).HasColumnName("HospitalID");

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PatientType).HasMaxLength(100);

                entity.Property(e => e.PatientTypeId).HasColumnName("PatientTypeID");

                entity.Property(e => e.PrefixName).HasMaxLength(100);

                entity.Property(e => e.RefDocName).HasMaxLength(152);

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RoomName).HasMaxLength(100);

                entity.Property(e => e.TariffName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwAdmissionListWithRegNoForPhar>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwAdmissionListWithRegNoForPhar");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmittedDr).HasMaxLength(101);

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.Doa)
                    .HasMaxLength(21)
                    .HasColumnName("DOA");

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.PatientName).HasMaxLength(302);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBill");

                entity.Property(e => e.AdvUsdPay).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CardPay).HasColumnType("money");

                entity.Property(e => e.CashPay).HasColumnType("money");

                entity.Property(e => e.ChequePay).HasColumnType("money");

                entity.Property(e => e.NeftPay).HasColumnType("money");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PayTmpay)
                    .HasColumnType("money")
                    .HasColumnName("PayTMPay");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwBillDateWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBillDateWise");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwBillDiffQuery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LvwBillDiffQuery");

                entity.Property(e => e.BillAmount).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NetChargesAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");
            });

            modelBuilder.Entity<LvwBillIpd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBillIPD");

                entity.Property(e => e.AdvUsdPay).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillTime).HasColumnType("datetime");

                entity.Property(e => e.CardPay).HasColumnType("money");

                entity.Property(e => e.CashPay).HasColumnType("money");

                entity.Property(e => e.ChequePay).HasColumnType("money");

                entity.Property(e => e.NeftPay).HasColumnType("money");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PayTmpay)
                    .HasColumnType("money")
                    .HasColumnName("PayTMPay");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwBillPrint>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBillPrint");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BalAmt).HasColumnType("money");

                entity.Property(e => e.BillAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.DocName)
                    .HasMaxLength(152)
                    .HasColumnName("DocNAme");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.PinNo).HasMaxLength(10);

                entity.Property(e => e.ServiceName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwBillWiseMediniceTol>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBillWiseMediniceTol");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");
            });

            modelBuilder.Entity<LvwBrowseBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBrowseBills");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwBrowseBillsIpd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBrowseBillsIPD");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwBrowsePayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBrowsePayments");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.TotalAmt).HasColumnType("money");

                entity.Property(e => e.UserName).HasMaxLength(201);
            });

            modelBuilder.Entity<LvwBrowsePaymentsIpd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwBrowsePaymentsIPD");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.AgeDay)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AgeMonth)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.HospitalAddress).HasMaxLength(500);

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(406);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Pin).HasMaxLength(10);

                entity.Property(e => e.PrefixName).HasMaxLength(100);

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.TotalAmt).HasColumnType("money");

                entity.Property(e => e.UserName).HasMaxLength(201);
            });

            modelBuilder.Entity<LvwCanteenRequstItemDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwCanteenRequstItemDet");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<LvwChargesAmtAddDoctorWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwChargesAmtAddDoctorWise");

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwChargesCompany>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwChargesCompany");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("CDate");

                entity.Property(e => e.ChargesAddedName).HasMaxLength(100);

                entity.Property(e => e.ChargesCancelledByName).HasMaxLength(100);

                entity.Property(e => e.ChargesDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.ConcessionAmount).HasColumnType("money");

                entity.Property(e => e.DoctorName).HasMaxLength(156);

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ServiceName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwCompanyPatientApprovalInf0>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwCompanyPatientApprovalInf0");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdvBalAmt).HasColumnType("money");

                entity.Property(e => e.ApprovedAmount).HasColumnType("money");

                entity.Property(e => e.COutsideInvestAmt)
                    .HasColumnType("money")
                    .HasColumnName("C_OutsideInvestAmt");

                entity.Property(e => e.ClaimNo).HasMaxLength(100);

                entity.Property(e => e.CompDiscount).HasColumnType("money");

                entity.Property(e => e.DisallowedAmt).HasColumnType("money");

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedAmount).HasColumnType("money");

                entity.Property(e => e.FinalBillAmt).HasColumnType("money");

                entity.Property(e => e.HdiscAmt)
                    .HasColumnType("money")
                    .HasColumnName("HDiscAmt");

                entity.Property(e => e.HosApreAmt).HasColumnType("money");

                entity.Property(e => e.MedicalApreAmt).HasColumnType("money");

                entity.Property(e => e.PathApreAmt).HasColumnType("money");

                entity.Property(e => e.PharApreAmt).HasColumnType("money");

                entity.Property(e => e.PolicyNo).HasMaxLength(100);

                entity.Property(e => e.RadiApreAmt).HasColumnType("money");

                entity.Property(e => e.RecoveredByPatient).HasColumnType("money");

                entity.Property(e => e.RefundAmount).HasColumnType("money");
            });

            modelBuilder.Entity<LvwCompanyPayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwCompanyPayment");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PayAmount).HasColumnType("money");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");

                entity.Property(e => e.SettlementTime).HasColumnType("datetime");

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.WrfAmount).HasColumnType("money");
            });

            modelBuilder.Entity<LvwConfigSetting>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwConfigSetting");

                entity.Property(e => e.ChkPharmacyDue).HasColumnName("chkPharmacyDue");

                entity.Property(e => e.FilePathLocation).HasMaxLength(100);

                entity.Property(e => e.GIppaperName)
                    .HasMaxLength(100)
                    .HasColumnName("G_IPPaperName");

                entity.Property(e => e.GIpprintName)
                    .HasMaxLength(100)
                    .HasColumnName("G_IPPrintName");

                entity.Property(e => e.GIsIppaperSetting).HasColumnName("G_IsIPPaperSetting");

                entity.Property(e => e.GIsOppaperSetting)
                    .HasMaxLength(100)
                    .HasColumnName("G_IsOPPaperSetting");

                entity.Property(e => e.GIsPharmacyPaperSetting).HasColumnName("G_IsPharmacyPaperSetting");

                entity.Property(e => e.GOppaperName)
                    .HasMaxLength(100)
                    .HasColumnName("G_OPPaperName");

                entity.Property(e => e.GOpprintName)
                    .HasMaxLength(100)
                    .HasColumnName("G_OPPrintName");

                entity.Property(e => e.GPharmacyPaperName)
                    .HasMaxLength(100)
                    .HasColumnName("G_PharmacyPaperName");

                entity.Property(e => e.GPharmacyPrintName)
                    .HasMaxLength(100)
                    .HasColumnName("G_PharmacyPrintName");

                entity.Property(e => e.GenerateOpbillInCashOption).HasColumnName("GenerateOPBillInCashOption");

                entity.Property(e => e.IpadvanceCounter)
                    .HasMaxLength(100)
                    .HasColumnName("IPAdvanceCounter");

                entity.Property(e => e.IpbillCounter)
                    .HasMaxLength(100)
                    .HasColumnName("IPBillCounter");

                entity.Property(e => e.IpdAdvanceCounterId).HasColumnName("IPD_Advance_CounterId");

                entity.Property(e => e.IpdBillingCounterId).HasColumnName("IPD_Billing_CounterId");

                entity.Property(e => e.IpdReceiptCounterId).HasColumnName("IPD_Receipt_CounterId");

                entity.Property(e => e.IpdRefundOfAdvanceCounterId).HasColumnName("IPD_Refund_of_Advance_CounterId");

                entity.Property(e => e.IpdRefundOfBillCounterId).HasColumnName("IPD_Refund_of_Bill_CounterId");

                entity.Property(e => e.Ipdprefix)
                    .HasMaxLength(5)
                    .HasColumnName("IPDPrefix");

                entity.Property(e => e.Ipno)
                    .HasMaxLength(20)
                    .HasColumnName("IPNo");

                entity.Property(e => e.Ipprefix)
                    .HasMaxLength(20)
                    .HasColumnName("IPPrefix");

                entity.Property(e => e.IpreceiptCounter)
                    .HasMaxLength(100)
                    .HasColumnName("IPReceiptCounter");

                entity.Property(e => e.IprefofAdvCounter)
                    .HasMaxLength(100)
                    .HasColumnName("IPRefofAdvCounter");

                entity.Property(e => e.IprefundBillCounter)
                    .HasMaxLength(100)
                    .HasColumnName("IPRefundBillCounter");

                entity.Property(e => e.OpbillCounter)
                    .HasMaxLength(100)
                    .HasColumnName("OPBillCounter");

                entity.Property(e => e.OpdAdvanceCounterId).HasColumnName("OPD_Advance_CounterId");

                entity.Property(e => e.OpdBillingCounterId).HasColumnName("OPD_Billing_CounterId");

                entity.Property(e => e.OpdReceiptCounterId).HasColumnName("OPD_Receipt_CounterId");

                entity.Property(e => e.OpdRefundAdvanceCounterId).HasColumnName("OPD_Refund_Advance_CounterId");

                entity.Property(e => e.OpdRefundBillCounterId).HasColumnName("OPD_Refund_Bill_CounterId");

                entity.Property(e => e.Opno)
                    .HasMaxLength(20)
                    .HasColumnName("OPNo");

                entity.Property(e => e.Opprefix)
                    .HasMaxLength(20)
                    .HasColumnName("OPPrefix");

                entity.Property(e => e.OpreceiptCounter)
                    .HasMaxLength(100)
                    .HasColumnName("OPReceiptCounter");

                entity.Property(e => e.OprefundOfBillCounter)
                    .HasMaxLength(100)
                    .HasColumnName("OPRefundOfBillCounter");

                entity.Property(e => e.Otcharges).HasColumnName("OTCharges");

                entity.Property(e => e.PharmacyReceiptCounterId).HasColumnName("PharmacyReceipt_CounterId");

                entity.Property(e => e.PharmacySalesCounterId).HasColumnName("PharmacySales_CounterId");

                entity.Property(e => e.PharmacySalesReturnCounterId).HasColumnName("PharmacySalesReturn_CounterId");

                entity.Property(e => e.PopOpbillAfterVisit).HasColumnName("PopOPBillAfterVisit");

                entity.Property(e => e.PopPayAfterOpbill).HasColumnName("PopPayAfterOPBill");

                entity.Property(e => e.PrintIpdafterAdm).HasColumnName("PrintIPDAfterAdm");

                entity.Property(e => e.PrintOpdcaseAfterVisit).HasColumnName("PrintOPDCaseAfterVisit");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RegPrefix).HasMaxLength(20);
            });

            modelBuilder.Entity<LvwCurrentAdmBed>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwCurrentAdmBed");

                entity.Property(e => e.DocNameId).HasColumnName("DocNameID");

                entity.Property(e => e.DoctorName).HasMaxLength(105);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.PatientName).HasMaxLength(404);

                entity.Property(e => e.RegNo).HasMaxLength(20);
            });

            modelBuilder.Entity<LvwCurrentAdmissionList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LvwCurrentAdmissionList");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.PatientName).HasMaxLength(406);

                entity.Property(e => e.RegNo).HasMaxLength(20);
            });

            modelBuilder.Entity<LvwCurrentAdmittedList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwCurrentAdmittedList");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmissionTime).HasColumnType("datetime");

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeTime).HasColumnType("datetime");

                entity.Property(e => e.Doa)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DOA");

                entity.Property(e => e.DocNameId).HasColumnName("DocNameID");

                entity.Property(e => e.Doctorname).HasMaxLength(152);

                entity.Property(e => e.Dot)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DOT");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.HospitalId).HasColumnName("HospitalID");

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PatientType).HasMaxLength(100);

                entity.Property(e => e.PatientTypeId).HasColumnName("PatientTypeID");

                entity.Property(e => e.PrefixName).HasMaxLength(100);

                entity.Property(e => e.RefDocName).HasMaxLength(152);

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwCurrentBalQtyCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwCurrentBalQtyCheck");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");
            });

            modelBuilder.Entity<LvwDischargedList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwDischargedList");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdmissionTime).HasColumnType("datetime");

                entity.Property(e => e.BedName).HasMaxLength(100);

                entity.Property(e => e.Doa)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DOA");

                entity.Property(e => e.DocNameId).HasColumnName("DocNameID");

                entity.Property(e => e.Doctorname).HasMaxLength(152);

                entity.Property(e => e.Dot)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DOT");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.HospitalId).HasColumnName("HospitalID");

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PatientType).HasMaxLength(100);

                entity.Property(e => e.PatientTypeId).HasColumnName("PatientTypeID");

                entity.Property(e => e.PrefixName).HasMaxLength(100);

                entity.Property(e => e.RefDocName).HasMaxLength(152);

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwDoctorMasterList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwDoctorMasterList");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.DoctorType).HasMaxLength(100);

                entity.Property(e => e.Education).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Pin).HasMaxLength(10);

                entity.Property(e => e.PrefixId).HasColumnName("PrefixID");

                entity.Property(e => e.PrefixName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwEndoscopyNote>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwEndoscopyNotes");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.Otid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OTId");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryName)
                    .HasMaxLength(300)
                    .HasColumnName("surgeryName");

                entity.Property(e => e.ToTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwGeneralOtdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwGeneralOTDetail");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.OtgenSurId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OTGenSurId");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryName)
                    .HasMaxLength(300)
                    .HasColumnName("surgeryName");

                entity.Property(e => e.ToTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwGetIppatientCashAmt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwGetIPPatientCashAmt");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.CashPay).HasColumnType("money");
            });

            modelBuilder.Entity<LvwGetIpphCashAmt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwGetIPPhCashAmt");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.CashPay).HasColumnType("money");
            });

            modelBuilder.Entity<LvwIpconcession>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwIPConcession");

                entity.Property(e => e.BillNetPayableAmount).HasColumnType("money");

                entity.Property(e => e.BillTotalAmount).HasColumnType("money");

                entity.Property(e => e.ChargesDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ConcessionAmount).HasColumnType("money");

                entity.Property(e => e.DoctorName).HasMaxLength(152);

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.ServiceName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwIpdrefundAgainstBillList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwIPDRefundAgainstBillList");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentRemark).HasMaxLength(500);

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RefunDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundNo).HasMaxLength(20);

                entity.Property(e => e.RefundTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwIpmedicalRecordVisitde>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwIPMedicalRecordVisitde");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.Pdate)
                    .HasMaxLength(10)
                    .HasColumnName("PDate");

                entity.Property(e => e.Ptime)
                    .HasMaxLength(10)
                    .HasColumnName("PTime");
            });

            modelBuilder.Entity<LvwItemExpList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwItemExpList");

                entity.Property(e => e.ItemName).HasMaxLength(284);
            });

            modelBuilder.Entity<LvwItemWithoutBatchno>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwItemWithoutBatchno");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<LvwListofAdmforAdvanceRefund>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwListofAdmforAdvanceRefund");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.DoctorName).HasMaxLength(101);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");
            });

            modelBuilder.Entity<LvwMedicalCertificate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LvwMedicalCertificate");

                entity.Property(e => e.AccidentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accident_Date");

                entity.Property(e => e.AccidentTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Accident_Time");

                entity.Property(e => e.AgeofInjuries).HasMaxLength(1000);

                entity.Property(e => e.AuthorityName).HasMaxLength(100);

                entity.Property(e => e.BuckleNo).HasMaxLength(50);

                entity.Property(e => e.CauseofInjuries).HasMaxLength(1000);

                entity.Property(e => e.CertificateNo).HasMaxLength(50);

                entity.Property(e => e.DetailsInjuries)
                    .HasColumnType("text")
                    .HasColumnName("Details_Injuries");

                entity.Property(e => e.Mlcdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MLCDate");

                entity.Property(e => e.Mlcid).HasColumnName("MLCId");

                entity.Property(e => e.Mlcno)
                    .HasMaxLength(50)
                    .HasColumnName("MLCNo");

                entity.Property(e => e.Mlctime)
                    .HasColumnType("datetime")
                    .HasColumnName("MLCTime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_Id");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PoliceStation).HasMaxLength(100);

                entity.Property(e => e.ReportingDate).HasColumnType("datetime");

                entity.Property(e => e.ReportingTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwObst>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwObst");

                entity.Property(e => e.AbdCircms).HasMaxLength(50);

                entity.Property(e => e.AbdCriWks).HasMaxLength(50);

                entity.Property(e => e.Anomalies).HasMaxLength(200);

                entity.Property(e => e.ApproxEddbyUsg)
                    .HasMaxLength(50)
                    .HasColumnName("ApproxEDDByUSG");

                entity.Property(e => e.ApproxWeight).HasMaxLength(50);

                entity.Property(e => e.Bpdcms)
                    .HasMaxLength(50)
                    .HasColumnName("BPDcms");

                entity.Property(e => e.Bpdwks)
                    .HasMaxLength(50)
                    .HasColumnName("BPDWks");

                entity.Property(e => e.CardiacActivity).HasMaxLength(50);

                entity.Property(e => e.CervicalLength).HasMaxLength(50);

                entity.Property(e => e.Clrwks)
                    .HasMaxLength(50)
                    .HasColumnName("CLRWks");

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.Crlcms)
                    .HasMaxLength(50)
                    .HasColumnName("CRLcms");

                entity.Property(e => e.EddbyLmp)
                    .HasMaxLength(50)
                    .HasColumnName("EDDbyLMP");

                entity.Property(e => e.FemurWks).HasMaxLength(50);

                entity.Property(e => e.Femurcms).HasMaxLength(50);

                entity.Property(e => e.FetalMovements).HasMaxLength(50);

                entity.Property(e => e.Fhs)
                    .HasMaxLength(50)
                    .HasColumnName("FHS");

                entity.Property(e => e.Grade).HasMaxLength(50);

                entity.Property(e => e.HeadCriWks).HasMaxLength(50);

                entity.Property(e => e.HeadCricms).HasMaxLength(50);

                entity.Property(e => e.InternalOs)
                    .HasMaxLength(50)
                    .HasColumnName("InternalOS");

                entity.Property(e => e.Liquor).HasMaxLength(50);

                entity.Property(e => e.Lmp)
                    .HasMaxLength(50)
                    .HasColumnName("LMP");

                entity.Property(e => e.Msdcms)
                    .HasMaxLength(50)
                    .HasColumnName("MSDcms");

                entity.Property(e => e.Msdwks)
                    .HasMaxLength(50)
                    .HasColumnName("MSDWks");

                entity.Property(e => e.ObstId).HasColumnName("ObstID");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.Placenta).HasMaxLength(50);

                entity.Property(e => e.PresentationLie).HasMaxLength(50);

                entity.Property(e => e.SingleTwinPregnancyDays).HasMaxLength(50);

                entity.Property(e => e.SingleTwinPregnancyWks).HasMaxLength(50);

                entity.Property(e => e.TheFetalMaturity).HasMaxLength(50);

                entity.Property(e => e.TranDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwOpdrefundAgainstBillList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwOPDRefundAgainstBillList");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentRemark).HasMaxLength(500);

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RefunDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundNo).HasMaxLength(20);

                entity.Property(e => e.RefundTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwOtbookedSmsforDrQuery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwOTBookedSMSForDrQuery");

                entity.Property(e => e.DrMobileNo).HasMaxLength(20);

                entity.Property(e => e.OtbookingId).HasColumnName("OTBookingID");

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(730)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<LvwOtcharge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwOTCharges");

                entity.Property(e => e.ChargesDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.OtchargesId).HasColumnName("OTChargesId");

                entity.Property(e => e.ServiceName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwOtdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwOTDetails");

                entity.Property(e => e.AnaDoctor).HasMaxLength(152);

                entity.Property(e => e.BirthRegNo).HasMaxLength(50);

                entity.Property(e => e.BirthTime).HasColumnType("datetime");

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.OpDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OperationNotes).HasMaxLength(500);

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.OtdetailId).HasColumnName("OTDetailID");

                entity.Property(e => e.OtheaderNo).HasColumnName("OTHeaderNo");

                entity.Property(e => e.PaediatricionDocName).HasMaxLength(152);

                entity.Property(e => e.ProcedureName).HasMaxLength(500);

                entity.Property(e => e.SurgeonDoc).HasMaxLength(152);

                entity.Property(e => e.TheaterName).HasMaxLength(50);

                entity.Property(e => e.TranDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwOtdetailsDoctorWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LvwOTdetailsDoctorWise");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorName).HasMaxLength(152);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OpDate).HasColumnType("datetime");

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.ProcedureName).HasMaxLength(500);
            });

            modelBuilder.Entity<LvwOttableMasterList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwOTTableMasterList");

                entity.Property(e => e.LocationName).HasMaxLength(100);

                entity.Property(e => e.OttableId).HasColumnName("OTTableId");

                entity.Property(e => e.OttableName)
                    .HasMaxLength(100)
                    .HasColumnName("OTTableName");
            });

            modelBuilder.Entity<LvwPath>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPath");

                entity.Property(e => e.ConsultantDoctorName).HasMaxLength(107);

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.PathDate).HasColumnType("datetime");

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.PatientName).HasMaxLength(404);

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.ServiceName).HasMaxLength(200);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwPathParaFill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPathParaFill");

                entity.Property(e => e.NormalRange).HasMaxLength(101);

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwPathSubTestFill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPathSubTestFill");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.TestName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwPatientListAdmission>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPatientListAdmission");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwPayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPayment");

                entity.Property(e => e.AdvanceUseAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PayTmpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMPayAmount");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwPaymentCharity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPayment_Charity");

                entity.Property(e => e.AdvanceUseAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PayTmpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMPayAmount");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwPaymentPharmacy>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPaymentPharmacy");

                entity.Property(e => e.AdvanceUseAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAMount");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwPaymentPharmacyRefund>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPaymentPharmacy_Refund");

                entity.Property(e => e.AdvanceUseAmount).HasColumnType("money");

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAMount");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwPharmacyBill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPharmacyBills");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PaidAmountPayment).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(201);

                entity.Property(e => e.RefundAmt).HasColumnType("money");

                entity.Property(e => e.SalesNo).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("money");
            });

            modelBuilder.Entity<LvwPrePostOperativeNote>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwPrePostOperativeNotes");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.Otid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OTId");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryName)
                    .HasMaxLength(300)
                    .HasColumnName("surgeryName");

                entity.Property(e => e.ToTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwRefundOfBillIpdlist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRefundOfBillIPDList");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BilDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwRefundOfBillOpdlist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRefundOfBillOPDList");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BilDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<LvwRegNoWithPrefix>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRegNoWithPrefix");

                entity.Property(e => e.RegId).ValueGeneratedOnAdd();

                entity.Property(e => e.RegNoWithPre).HasMaxLength(30);
            });

            modelBuilder.Entity<LvwRegistrationList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRegistrationList");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Age).HasMaxLength(10);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.PatientName).HasMaxLength(404);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.PinNo).HasMaxLength(10);

                entity.Property(e => e.PrefixId).HasColumnName("PrefixID");

                entity.Property(e => e.PrefixName).HasMaxLength(100);

                entity.Property(e => e.Rdate)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("RDate");

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.Property(e => e.RegTime).HasColumnType("datetime");

                entity.Property(e => e.RegTimeDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LvwRetrieveDischargeSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LvwRetrieve_DischargeSummary");

                entity.Property(e => e.ClinicalFinding).HasColumnType("text");

                entity.Property(e => e.Diagnosis).HasColumnType("text");

                entity.Property(e => e.DischargeDate).HasColumnType("datetime");

                entity.Property(e => e.DischargeDoctorName1).HasMaxLength(101);

                entity.Property(e => e.DischargeDoctorName2).HasMaxLength(101);

                entity.Property(e => e.DischargeDoctorName2Education).HasMaxLength(200);

                entity.Property(e => e.DischargeDoctorName3).HasMaxLength(101);

                entity.Property(e => e.DischargeDoctorName3Education).HasMaxLength(200);

                entity.Property(e => e.DischargeSummaryTime).HasColumnType("datetime");

                entity.Property(e => e.DischargeTime).HasColumnType("datetime");

                entity.Property(e => e.DischargeTypeName).HasMaxLength(100);

                entity.Property(e => e.DoctorAssistantName).HasMaxLength(100);

                entity.Property(e => e.Dod)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DOD");

                entity.Property(e => e.Dot)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DOT");

                entity.Property(e => e.Education).HasMaxLength(200);

                entity.Property(e => e.Followupdate).HasColumnType("datetime");

                entity.Property(e => e.History).HasColumnType("text");

                entity.Property(e => e.Investigation).HasColumnType("text");

                entity.Property(e => e.OpDate).HasColumnType("datetime");

                entity.Property(e => e.OpertiveNotes).HasColumnType("text");

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.Remark).HasColumnType("text");

                entity.Property(e => e.TreatmentAdvisedAfterDischarge).HasColumnType("text");

                entity.Property(e => e.TreatmentGiven).HasColumnType("text");
            });

            modelBuilder.Entity<LvwRetrievePathologyResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvw_Retrieve_PathologyResult");

                entity.Property(e => e.DefaultValue).HasMaxLength(50);

                entity.Property(e => e.Maxvalue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalResult).HasMaxLength(152);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwRetrievePathologyResultIppatientUpdate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvw_Retrieve_PathologyResultIPPatientUpdate");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionTime).HasColumnType("datetime");

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.ConsultantDocName).HasMaxLength(101);

                entity.Property(e => e.FootNote).HasMaxLength(400);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.NormalRange).HasMaxLength(50);

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathResultDr1).HasMaxLength(101);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PisNumeric).HasColumnName("PIsNumeric");

                entity.Property(e => e.PrintParameterName).HasMaxLength(100);

                entity.Property(e => e.PrintTestName).HasMaxLength(200);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SubTestNamePrint).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(500);

                entity.Property(e => e.TechniqueName).HasMaxLength(200);

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwRetrievePathologyResultMachineUpload>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvw_Retrieve_PathologyResult_MachineUpload");

                entity.Property(e => e.DefaultValue).HasMaxLength(500);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Maxvalue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalResult).HasMaxLength(152);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.Result).HasMaxLength(500);

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.Sampleid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SAMPLEID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.TestRunDate).HasMaxLength(10);

                entity.Property(e => e.Tmpvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMPVALUE");

                entity.Property(e => e.Transferflag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRANSFERFLAG");

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwRetrievePathologyResultUpdate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvw_Retrieve_PathologyResultUpdate");

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.ConsultantDocName).HasMaxLength(101);

                entity.Property(e => e.FootNote).HasMaxLength(400);

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.NormalRange).HasMaxLength(50);

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathResultDr1).HasMaxLength(101);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PisNumeric).HasColumnName("PIsNumeric");

                entity.Property(e => e.PrintParameterName).HasMaxLength(100);

                entity.Property(e => e.PrintTestName).HasMaxLength(200);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SubTestNamePrint).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TechniqueName).HasMaxLength(200);

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LvwRtrvPathologyResultIpwithAge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRtrv_PathologyResultIPWithAge");

                entity.Property(e => e.AgeType).HasMaxLength(11);

                entity.Property(e => e.DefaultValue).HasMaxLength(500);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalResult).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwRtrvPathologyResultIpwithAgeMachineUpload>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRtrv_PathologyResultIPWithAge_MachineUpload");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalResult).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.RegNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.Tmpvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMPVALUE");

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwRtrvPathologyResultOpwithAge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRtrv_PathologyResultOPWithAge");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.DefaultValue).HasMaxLength(500);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalResult).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwRtrvPathologyResultOpwithAgeMachineUpload>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwRtrv_PathologyResultOPWithAge_MachineUpload");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.RegNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResultValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.Tmpvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMPVALUE");

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwSalesBatchNo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwSalesBatchNo");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");
            });

            modelBuilder.Entity<LvwSalesGstissue>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwSalesGSTISSUE");

                entity.Property(e => e.Acgstamt).HasColumnName("ACGSTAmt");

                entity.Property(e => e.AcgstamtDiff).HasColumnName("ACGSTAmtDiff");

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Mrpamount)
                    .HasColumnType("money")
                    .HasColumnName("MRPAmount");

                entity.Property(e => e.SalesNo).HasMaxLength(50);

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");
            });

            modelBuilder.Entity<LvwServiceMasterList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwServiceMasterList");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.ServiceName).HasMaxLength(200);

                entity.Property(e => e.ServiceShortDesc).HasMaxLength(200);

                entity.Property(e => e.TariffName).HasMaxLength(100);
            });

            modelBuilder.Entity<LvwServicePriceList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwServicePriceList");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.ClassRate).HasColumnType("money");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.ServiceName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwSonography>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwSonography");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Liver).HasMaxLength(500);

                entity.Property(e => e.LtKideny).HasMaxLength(500);

                entity.Property(e => e.LtOvary).HasMaxLength(500);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.Pancrea).HasMaxLength(500);

                entity.Property(e => e.RtKidney).HasMaxLength(500);

                entity.Property(e => e.RtOvary).HasMaxLength(500);

                entity.Property(e => e.Spleen).HasMaxLength(500);

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.Uterus).HasMaxLength(500);
            });

            modelBuilder.Entity<LvwStaffRefSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwStaffRefSMSQuery");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(646)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<LvwSubTest>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwSubTest");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.TestName).HasMaxLength(200);
            });

            modelBuilder.Entity<LvwVisitAppPatientForSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwVisitAppPatientForSMSQuery");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(575)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<LvwVisitDetailsForCasePaper>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwVisitDetailsForCasePaper");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Age).HasMaxLength(36);

                entity.Property(e => e.Bp)
                    .HasMaxLength(50)
                    .HasColumnName("BP");

                entity.Property(e => e.Bsa)
                    .HasMaxLength(10)
                    .HasColumnName("BSA")
                    .IsFixedLength();

                entity.Property(e => e.Bsl)
                    .HasMaxLength(50)
                    .HasColumnName("BSL");

                entity.Property(e => e.CasePaperId).HasColumnName("CasePaperID");

                entity.Property(e => e.Complaint).HasColumnType("text");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.Diagnosis).HasColumnType("text");

                entity.Property(e => e.DocName).HasMaxLength(156);

                entity.Property(e => e.Education).HasMaxLength(200);

                entity.Property(e => e.Finding).HasColumnType("text");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.Hc)
                    .HasMaxLength(10)
                    .HasColumnName("HC")
                    .IsFixedLength();

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.Investigations).HasColumnType("text");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MahRegNo).HasMaxLength(50);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.PastHistory).HasColumnType("text");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PersonalDetails).HasMaxLength(100);

                entity.Property(e => e.Pluse).HasMaxLength(50);

                entity.Property(e => e.PresentHistory).HasColumnType("text");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.Rr)
                    .HasMaxLength(10)
                    .HasColumnName("RR")
                    .IsFixedLength();

                entity.Property(e => e.SexId).HasColumnName("SexID");

                entity.Property(e => e.SpO2).HasMaxLength(50);

                entity.Property(e => e.Temp)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.VisitDate)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.VisitTime).HasColumnType("datetime");

                entity.Property(e => e.Weight).HasMaxLength(50);
            });

            modelBuilder.Entity<LvwVisitDetailsList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwVisitDetailsList");

                entity.Property(e => e.Doctorname).HasMaxLength(152);

                entity.Property(e => e.DvisitDate)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DVisitDate");

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PatientName).HasMaxLength(302);

                entity.Property(e => e.PatientType).HasMaxLength(100);

                entity.Property(e => e.RefDocId).HasColumnName("RefDocID");

                entity.Property(e => e.RefDocName).HasMaxLength(152);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.VistDateTime)
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LvwVisitRefDocForSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwVisitRefDocForSMSQuery");

                entity.Property(e => e.RefDocMobileNo).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(601)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<LvwWardDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lvwWardDetails");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.LocationName).HasMaxLength(100);

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<MAnaesthesiaTypeMaster>(entity =>
            {
                entity.HasKey(e => e.AnestTypeId);

                entity.ToTable("M_AnaesthesiaTypeMaster");

                entity.Property(e => e.AnaesthesiaType).HasMaxLength(50);
            });

            modelBuilder.Entity<MAppWhatsAppDly>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_APP_WhatsApp_Dly");

                entity.Property(e => e.IpadvCollection)
                    .HasColumnType("money")
                    .HasColumnName("IPAdvCollection");

                entity.Property(e => e.IpadvRefund)
                    .HasColumnType("money")
                    .HasColumnName("IPAdvRefund");

                entity.Property(e => e.Ipcollection)
                    .HasColumnType("money")
                    .HasColumnName("IPCollection");

                entity.Property(e => e.Iprefund)
                    .HasColumnType("money")
                    .HasColumnName("IPRefund");

                entity.Property(e => e.Opcollection)
                    .HasColumnType("money")
                    .HasColumnName("OPCollection");

                entity.Property(e => e.Oprefund)
                    .HasColumnType("money")
                    .HasColumnName("OPRefund");

                entity.Property(e => e.PharAdvCollection).HasColumnType("money");

                entity.Property(e => e.PharAdvRefund).HasColumnType("money");

                entity.Property(e => e.PharCollection).HasColumnType("money");

                entity.Property(e => e.PharPhRefund).HasColumnType("money");

                entity.Property(e => e.VitCnt).HasColumnName("VitCNT");
            });

            modelBuilder.Entity<MAreaMaster>(entity =>
            {
                entity.HasKey(e => e.AreaId)
                    .HasName("PK_M_AreaMaster_1");

                entity.ToTable("M_AreaMaster");

                entity.Property(e => e.AreaName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MAssignItemToStore>(entity =>
            {
                entity.HasKey(e => e.AssignId);

                entity.ToTable("M_AssignItemToStore");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.MAssignItemToStores)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_AssignItemToStore_M_ItemMaster");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.MAssignItemToStores)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_AssignItemToStore_M_StoreMaster");
            });

            modelBuilder.Entity<MAssignSupplierToStore>(entity =>
            {
                entity.HasKey(e => e.AssignId);

                entity.ToTable("M_AssignSupplierToStore");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.MAssignSupplierToStores)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_AssignSupplierToStore_M_StoreMaster");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.MAssignSupplierToStores)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_AssignSupplierToStore_M_SupplierMaster");
            });

            modelBuilder.Entity<MAutoServiceList>(entity =>
            {
                entity.HasKey(e => e.SysId);

                entity.ToTable("M_AutoServiceList");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MBankMaster>(entity =>
            {
                entity.HasKey(e => e.BankId);

                entity.ToTable("M_BankMaster");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCampMaster>(entity =>
            {
                entity.HasKey(e => e.CampId);

                entity.ToTable("M_CampMaster");

                entity.Property(e => e.CampLocation).HasMaxLength(100);

                entity.Property(e => e.CampName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCanItemMaster>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("M_CanItemMaster");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Cgst).HasColumnName("CGST");

                entity.Property(e => e.ConversionFactor).HasMaxLength(50);

                entity.Property(e => e.EmpPrice).HasColumnType("money");

                entity.Property(e => e.Igst).HasColumnName("IGST");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.ItemShortName).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProdLocation).HasMaxLength(100);

                entity.Property(e => e.PurchaseUomid).HasColumnName("PurchaseUOMId");

                entity.Property(e => e.Sgst).HasColumnName("SGST");
            });

            modelBuilder.Entity<MCertificateMaster>(entity =>
            {
                entity.HasKey(e => e.CertificateId);

                entity.ToTable("M_CertificateMaster");

                entity.Property(e => e.CertificateName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCertificateTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("M_CertificateTemplateMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCityMaster>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("M_CityMaster");

                entity.Property(e => e.CityName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MClassMaster>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("M_ClassMaster");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateDescName).HasColumnType("text");
            });

            modelBuilder.Entity<MCompanyServiceAssignMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyAssignId);

                entity.ToTable("M_CompanyServiceAssignMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCompanyWiseServiceDiscount>(entity =>
            {
                entity.HasKey(e => e.CompServiceDetailId);

                entity.ToTable("M_CompanyWiseServiceDiscount");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountAmount).HasColumnType("money");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MComplaintMaster>(entity =>
            {
                entity.HasKey(e => e.ComplaintId);

                entity.ToTable("M_ComplaintMaster");

                entity.Property(e => e.ComplaintDescr).HasMaxLength(50);
            });

            modelBuilder.Entity<MConcessionReasonMaster>(entity =>
            {
                entity.HasKey(e => e.ConcessionId);

                entity.ToTable("M_ConcessionReasonMaster");

                entity.Property(e => e.ConcessionReason).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MConsentMaster>(entity =>
            {
                entity.HasKey(e => e.ConsentId);

                entity.ToTable("M_ConsentMaster");

                entity.Property(e => e.ConsentName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MConstant>(entity =>
            {
                entity.HasKey(e => e.ConstantId);

                entity.ToTable("M_Constants");

                entity.Property(e => e.ConstantType).HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Value).HasMaxLength(250);
            });

            modelBuilder.Entity<MCountryMaster>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("M_CountryMaster");

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCreditReasonMaster>(entity =>
            {
                entity.HasKey(e => e.CreditId);

                entity.ToTable("M_CreditReasonMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreditReason).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MCurrencyMaster>(entity =>
            {
                entity.HasKey(e => e.CurrencyId);

                entity.ToTable("M_CurrencyMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDepartmentMaster>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("M_DepartmentMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDiagnosisMaster>(entity =>
            {
                entity.HasKey(e => e.DiagnosisId);

                entity.ToTable("M_DiagnosisMaster");

                entity.Property(e => e.DiagnosisDescr).HasMaxLength(50);
            });

            modelBuilder.Entity<MDietChartDetail>(entity =>
            {
                entity.HasKey(e => e.DietChartDetId);

                entity.ToTable("M_DietChartDetails");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDietChartMaster>(entity =>
            {
                entity.HasKey(e => e.DietChartId);

                entity.ToTable("M_DietChartMaster");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DietChartDescription).HasMaxLength(250);

                entity.Property(e => e.DietChartName).HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDoctorChargesDetail>(entity =>
            {
                entity.HasKey(e => e.DocChargeId);

                entity.ToTable("M_DoctorChargesDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorChargesDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_M_DoctorChargesDetails_DoctorMaster");
            });

            modelBuilder.Entity<MDoctorDepartmentDet>(entity =>
            {
                entity.HasKey(e => e.DocDeptId);

                entity.ToTable("M_DoctorDepartmentDet");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MDoctorDepartmentDets)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_DoctorDepartmentDet_M_DepartmentMaster");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorDepartmentDets)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_DoctorDepartmentDet_DoctorMaster");
            });

            modelBuilder.Entity<MDoctorExperienceDetail>(entity =>
            {
                entity.HasKey(e => e.DocExpId);

                entity.ToTable("M_DoctorExperienceDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasMaxLength(100);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.HospitalName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorExperienceDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_M_DoctorExperienceDetails_DoctorMaster");
            });

            modelBuilder.Entity<MDoctorHouseManMaster>(entity =>
            {
                entity.HasKey(e => e.DocHousmanId)
                    .HasName("PK_DoctorHouseManMaster");

                entity.ToTable("M_DoctorHouseManMaster");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.Education).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Pin).HasMaxLength(10);

                entity.Property(e => e.PrefixId).HasColumnName("PrefixID");
            });

            modelBuilder.Entity<MDoctorLeaveDetail>(entity =>
            {
                entity.HasKey(e => e.DocLeaveId);

                entity.ToTable("M_DoctorLeaveDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .HasMaxLength(250)
                    .HasColumnName("reason");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorLeaveDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_M_DoctorLeaveDetails_DoctorMaster");
            });

            modelBuilder.Entity<MDoctorNotesTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.DocNoteTempId);

                entity.ToTable("M_DoctorNotesTemplateMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocsTempName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDoctorPerGroupWiseMaster>(entity =>
            {
                entity.HasKey(e => e.DocShareId)
                    .HasName("PK_M_DoctorPerGroup");

                entity.ToTable("M_DoctorPerGroupWiseMaster");

                entity.Property(e => e.ServicePercentage).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<MDoctorPerMaster>(entity =>
            {
                entity.HasKey(e => e.DoctorShareId);

                entity.ToTable("M_DoctorPerMaster");

                entity.Property(e => e.DocShrTypeS)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.OpIpType).HasColumnName("Op_IP_Type");

                entity.Property(e => e.ServiceAmount).HasColumnType("money");

                entity.Property(e => e.ServicePercentage).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<MDoctorQualificationDetail>(entity =>
            {
                entity.HasKey(e => e.DocQualfiId);

                entity.ToTable("M_DoctorQualificationDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PassingYear).HasMaxLength(10);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorQualificationDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_M_DoctorQualificationDetails_DoctorMaster");
            });

            modelBuilder.Entity<MDoctorScheduleDetail>(entity =>
            {
                entity.HasKey(e => e.DocSchedId);

                entity.ToTable("M_DoctorScheduleDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleDays).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorScheduleDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_M_DoctorScheduleDetails_DoctorMaster");
            });

            modelBuilder.Entity<MDoctorSignPageDetail>(entity =>
            {
                entity.HasKey(e => e.DocSignId);

                entity.ToTable("M_DoctorSignPageDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MDoctorSignPageDetails)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_M_DoctorSignPageDetails_DoctorMaster");
            });

            modelBuilder.Entity<MDoseMaster>(entity =>
            {
                entity.HasKey(e => e.DoseId);

                entity.ToTable("M_DoseMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoseName).HasMaxLength(200);

                entity.Property(e => e.DoseNameInEnglish).HasMaxLength(500);

                entity.Property(e => e.DoseNameInMarathi).HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MDrugMaster>(entity =>
            {
                entity.HasKey(e => e.DrugId);

                entity.ToTable("M_DrugMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DrugName).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MExaminationMaster>(entity =>
            {
                entity.HasKey(e => e.ExaminationId);

                entity.ToTable("M_ExaminationMaster");

                entity.Property(e => e.ExaminationDescr).HasMaxLength(50);
            });

            modelBuilder.Entity<MExpensesHeadMaster>(entity =>
            {
                entity.HasKey(e => e.ExpHedId);

                entity.ToTable("M_ExpensesHeadMaster");

                entity.Property(e => e.HeadName)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MGenericMaster>(entity =>
            {
                entity.HasKey(e => e.GenericId);

                entity.ToTable("M_GenericMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GenericName).HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MIcdcdeMainMaster>(entity =>
            {
                entity.HasKey(e => e.IcdcdeMid);

                entity.ToTable("M_ICDCdeMainMaster");

                entity.Property(e => e.IcdcdeMid).HasColumnName("ICDCdeMId");

                entity.Property(e => e.IcdcodeName)
                    .HasMaxLength(1000)
                    .HasColumnName("ICDCodeName");
            });

            modelBuilder.Entity<MIcdcodingMaster>(entity =>
            {
                entity.HasKey(e => e.IcdcodingId);

                entity.ToTable("M_ICDCodingMaster");

                entity.Property(e => e.IcdcodingId).HasColumnName("ICDCodingId");

                entity.Property(e => e.Icdcode)
                    .HasMaxLength(100)
                    .HasColumnName("ICDCode");

                entity.Property(e => e.IcdcodeName)
                    .HasMaxLength(1000)
                    .HasColumnName("ICDCodeName");

                entity.Property(e => e.MainIcdcdeId).HasColumnName("MainICDCdeId");
            });

            modelBuilder.Entity<MInstructionMaster>(entity =>
            {
                entity.HasKey(e => e.InstructionId);

                entity.ToTable("M_InstructionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InstructionDescription).HasMaxLength(200);

                entity.Property(e => e.InstructioninMarathi).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.ItemCategoryId);

                entity.ToTable("M_ItemCategoryMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ItemCategoryName).HasMaxLength(50);

                entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemClassMaster>(entity =>
            {
                entity.HasKey(e => e.ItemClassId);

                entity.ToTable("M_ItemClassMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ItemClassName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemCompanyMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("M_ItemCompanyMaster");

                entity.Property(e => e.CompShortName).HasMaxLength(100);

                entity.Property(e => e.CompanyName).HasMaxLength(500);
            });

            modelBuilder.Entity<MItemDrugTypeMaster>(entity =>
            {
                entity.HasKey(e => e.ItemDrugTypeId);

                entity.ToTable("M_ItemDrugTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DrugTypeName).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemGenericNameMaster>(entity =>
            {
                entity.HasKey(e => e.ItemGenericNameId);

                entity.ToTable("M_ItemGenericNameMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ItemGenericName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemManufactureMaster>(entity =>
            {
                entity.HasKey(e => e.ItemManufactureId);

                entity.ToTable("M_ItemManufactureMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ManufactureCode).HasMaxLength(50);

                entity.Property(e => e.ManufactureName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemMaster>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("M_ItemMaster");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Cgst).HasColumnName("CGST");

                entity.Property(e => e.ConversionFactor).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoseName).HasMaxLength(10);

                entity.Property(e => e.DrugTypeName).HasMaxLength(100);

                entity.Property(e => e.Hsncode)
                    .HasMaxLength(20)
                    .HasColumnName("HSNcode");

                entity.Property(e => e.Igst).HasColumnName("IGST");

                entity.Property(e => e.Instruction).HasMaxLength(100);

                entity.Property(e => e.IsCreatedBy).HasColumnType("datetime");

                entity.Property(e => e.IsH1drug).HasColumnName("IsH1Drug");

                entity.Property(e => e.IsLasa).HasColumnName("IsLASA");

                entity.Property(e => e.IsUpdatedBy).HasColumnType("datetime");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.ItemShortName).HasMaxLength(100);

                entity.Property(e => e.ItemTime).HasColumnType("datetime");

                entity.Property(e => e.ItemTypeId).HasColumnName("ItemTypeID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProdLocation).HasMaxLength(100);

                entity.Property(e => e.PurchaseUomid).HasColumnName("PurchaseUOMId");

                entity.Property(e => e.Sgst).HasColumnName("SGST");

                entity.Property(e => e.StockUomid).HasColumnName("StockUOMId");
            });

            modelBuilder.Entity<MItemTypeMaster>(entity =>
            {
                entity.HasKey(e => e.ItemTypeId);

                entity.ToTable("M_ItemTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ItemTypeName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MItemWiseSupplierRate>(entity =>
            {
                entity.HasKey(e => e.DefId);

                entity.ToTable("M_ItemWiseSupplierRate");

                entity.Property(e => e.SupplierRate).HasColumnType("money");
            });

            modelBuilder.Entity<MLoginAccessConfig>(entity =>
            {
                entity.HasKey(e => e.LoginConfigId);

                entity.ToTable("M_LoginAccessConfig");

                entity.Property(e => e.AccessValueId).HasMaxLength(50);
            });

            modelBuilder.Entity<MLvwRetrievePathologyResultUpdateIpageWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_lvw_Retrieve_PathologyResultUpdate_IPAgeWise");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.AdmissionTime).HasColumnType("datetime");

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.ConsultantDocName).HasMaxLength(101);

                entity.Property(e => e.FootNote).HasMaxLength(400);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.Ipdno)
                    .HasMaxLength(50)
                    .HasColumnName("IPDNo");

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.NormalRange).HasMaxLength(50);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PathResultDrName).HasMaxLength(101);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PisNumeric).HasColumnName("PIsNumeric");

                entity.Property(e => e.PrintParameterName).HasMaxLength(100);

                entity.Property(e => e.PrintTestName).HasMaxLength(200);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SubTestNamePrint).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(500);

                entity.Property(e => e.TechniqueName).HasMaxLength(200);

                entity.Property(e => e.TestName).HasMaxLength(200);
            });

            modelBuilder.Entity<MLvwRetrievePathologyResultUpdateOpageWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_lvw_Retrieve_PathologyResultUpdate_OPAgeWise");

                entity.Property(e => e.AgeYear)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(500);

                entity.Property(e => e.ConsultantDocName).HasMaxLength(101);

                entity.Property(e => e.FootNote).HasMaxLength(400);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.NormalRange).HasMaxLength(50);

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.ParaBoldFlag).HasMaxLength(1);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PathResultDrName).HasMaxLength(101);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PisNumeric).HasColumnName("PIsNumeric");

                entity.Property(e => e.PrintParameterName).HasMaxLength(100);

                entity.Property(e => e.PrintTestName).HasMaxLength(200);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SubTestNamePrint).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(500);

                entity.Property(e => e.TechniqueName).HasMaxLength(200);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MLvwRtrvPathologyResultIpwithAge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_lvwRtrv_PathologyResultIPWithAge");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParaBoldFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<MLvwRtrvPathologyResultIpwithAgeMachineUpload>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_lvwRtrv_PathologyResultIPWithAge_MachineUpload");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParaBoldFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.RegNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResultValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.Tmpvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMPVALUE");

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<MLvwRtrvPathologyResultOpwithAge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_lvwRtrv_PathologyResultOPWithAge");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParaBoldFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<MLvwRtrvPathologyResultOpwithAgeMachineUpload>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("m_lvwRtrv_PathologyResultOPWithAge_MachineUpload");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParaBoldFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.RegNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResultValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rundate)
                    .HasColumnType("datetime")
                    .HasColumnName("RUNDATE");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.Tmpvalue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMPVALUE");

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<MManufactureMaster>(entity =>
            {
                entity.HasKey(e => e.ManufId);

                entity.ToTable("M_ManufactureMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ManufName).HasMaxLength(50);

                entity.Property(e => e.ManufShortName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MMaritalStatusMaster>(entity =>
            {
                entity.HasKey(e => e.MaritalStatusId);

                entity.ToTable("M_MaritalStatusMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MaritalStatusName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MMemberCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.MemberCategoryId);

                entity.ToTable("M_MemberCategoryMaster");

                entity.Property(e => e.MemberCategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<MMessageTemplate>(entity =>
            {
                entity.HasKey(e => e.MsgId);

                entity.ToTable("M_MessageTemplate");

                entity.Property(e => e.Dbname)
                    .HasMaxLength(100)
                    .HasColumnName("DBName");

                entity.Property(e => e.Msg)
                    .HasMaxLength(500)
                    .HasColumnName("msg");

                entity.Property(e => e.MsgCategory).HasMaxLength(50);

                entity.Property(e => e.TemplateId).HasMaxLength(200);
            });

            modelBuilder.Entity<MModeOfDeliveryMaster>(entity =>
            {
                entity.ToTable("M_ModeOfDeliveryMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ModeOfDelivery).HasMaxLength(500);
            });

            modelBuilder.Entity<MModeOfDischarge>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("m_modeOfDischarge");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModeOfDischargeId).ValueGeneratedOnAdd();

                entity.Property(e => e.ModeOfDischargeName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MModeOfPayment>(entity =>
            {
                entity.ToTable("M_ModeOfPayment");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModeOfPayment).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MNursingTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.NursingId);

                entity.ToTable("M_NursingTemplateMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NursTempName).HasMaxLength(100);
            });

            modelBuilder.Entity<MOctroiMaster>(entity =>
            {
                entity.ToTable("M_OctroiMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Octroi).HasMaxLength(500);
            });

            modelBuilder.Entity<MOpcasepaperDignosisMaster>(entity =>
            {
                entity.ToTable("M_OPCasepaperDignosisMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DescriptionName).HasMaxLength(500);

                entity.Property(e => e.DescriptionType).HasMaxLength(50);
            });

            modelBuilder.Entity<MOtSiteDescriptionMaster>(entity =>
            {
                entity.HasKey(e => e.SiteDescId)
                    .HasName("PK_M_SiteDescriptionMaster");

                entity.ToTable("M_OT_SiteDescriptionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SiteDescriptionName).HasMaxLength(50);

                entity.Property(e => e.SurgeryCategoryId).HasColumnName("SurgeryCategoryID");
            });

            modelBuilder.Entity<MOtSurgeryCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.SurgeryCategoryId)
                    .HasName("PK_M_OTCategoryMaster");

                entity.ToTable("M_OT_SurgeryCategoryMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SurgeryCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<MOtSurgeryMaster>(entity =>
            {
                entity.HasKey(e => e.SurgeryId)
                    .HasName("PK_M_SurgeryMaster");

                entity.ToTable("M_OT_SurgeryMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OttemplateId).HasColumnName("OTTemplateID");

                entity.Property(e => e.SurgeryAmount).HasColumnType("money");

                entity.Property(e => e.SurgeryName).HasMaxLength(100);
            });

            modelBuilder.Entity<MOtcomplicationsMaster>(entity =>
            {
                entity.HasKey(e => e.ComplicationId);

                entity.ToTable("M_OTComplicationsMaster");

                entity.Property(e => e.ComplicationDescr).HasMaxLength(50);
            });

            modelBuilder.Entity<MOthistopathologyMaster>(entity =>
            {
                entity.HasKey(e => e.HistopathologyId);

                entity.ToTable("M_OTHistopathologyMaster");

                entity.Property(e => e.HistopathologyDescr).HasMaxLength(50);
            });

            modelBuilder.Entity<MOtnotesTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.OtnoteTempId)
                    .HasName("PK_M_OTNotesTemplate");

                entity.ToTable("M_OTNotesTemplateMaster");

                entity.Property(e => e.OtnoteTempId).HasColumnName("OTNoteTempId");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishId2).HasColumnName("AnesthetishID2");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.BloodLoss).HasMaxLength(200);

                entity.Property(e => e.BostOporders)
                    .HasMaxLength(2000)
                    .HasColumnName("BostOPOrders");

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.ComplicationMode).HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Histopathology).HasMaxLength(2000);

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.OtreservationId).HasColumnName("OTReservationId");

                entity.Property(e => e.OttemplateName)
                    .HasMaxLength(300)
                    .HasColumnName("OTTemplateName");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SiteDescId).HasColumnName("SiteDescID");

                entity.Property(e => e.SorubNurse).HasMaxLength(200);

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryId).HasColumnName("SurgeryID");

                entity.Property(e => e.SurgeryName).HasMaxLength(300);

                entity.Property(e => e.SurgeryType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ToTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MOttableMaster>(entity =>
            {
                entity.HasKey(e => e.OttableId);

                entity.ToTable("M_OTTableMaster");

                entity.Property(e => e.OttableId).HasColumnName("OTTableId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OttableName)
                    .HasMaxLength(100)
                    .HasColumnName("OTTableName");
            });

            modelBuilder.Entity<MOttypeMaster>(entity =>
            {
                entity.HasKey(e => e.OttypeId);

                entity.ToTable("M_OTTypeMaster");

                entity.Property(e => e.OttypeId).HasColumnName("OTTypeId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<MPackageDetail>(entity =>
            {
                entity.HasKey(e => e.PackageId);

                entity.ToTable("M_PackageDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<MParameterDescriptiveMaster>(entity =>
            {
                entity.HasKey(e => e.DescriptiveId);

                entity.ToTable("M_ParameterDescriptiveMaster");

                entity.Property(e => e.DescriptiveId).HasColumnName("DescriptiveID");

                entity.Property(e => e.DefaultValue).HasMaxLength(500);

                entity.Property(e => e.ParameterValues).HasMaxLength(500);

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.MParameterDescriptiveMasters)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_ParameterDescriptiveMaster_M_PathParameterMaster");
            });

            modelBuilder.Entity<MPastHistoryMaster>(entity =>
            {
                entity.HasKey(e => e.PastHistoryId);

                entity.ToTable("M_PastHistoryMaster");

                entity.Property(e => e.PastHistoryDescr).HasMaxLength(50);
            });

            modelBuilder.Entity<MPathCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("M_PathCategoryMaster");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MPathParaRangeMaster>(entity =>
            {
                entity.HasKey(e => e.PathparaRangeId);

                entity.ToTable("M_PathParaRangeMaster");

                entity.Property(e => e.Maxvalue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);
            });

            modelBuilder.Entity<MPathParaRangeWithAgeMaster>(entity =>
            {
                entity.HasKey(e => e.PathparaRangeId);

                entity.ToTable("M_PathParaRangeWithAgeMaster");

                entity.Property(e => e.AgeType)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.HasOne(d => d.Para)
                    .WithMany(p => p.MPathParaRangeWithAgeMasters)
                    .HasForeignKey(d => d.ParaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_PathParaRangeWithAgeMaster_M_PathParameterMaster");
            });

            modelBuilder.Entity<MPathParameterMaster>(entity =>
            {
                entity.HasKey(e => e.ParameterId);

                entity.ToTable("M_PathParameterMaster");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.IsBoldFlag).HasMaxLength(1);

                entity.Property(e => e.IsPrintDisSummary).HasDefaultValueSql("((0))");

                entity.Property(e => e.MethodName).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParaBoldFlag).HasMaxLength(1);

                entity.Property(e => e.ParaMultipleRange).HasMaxLength(1000);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PrintParameterName).HasMaxLength(100);
            });

            modelBuilder.Entity<MPathTemplateDetail>(entity =>
            {
                entity.HasKey(e => e.PtemplateId);

                entity.ToTable("M_Path_TemplateDetails");

                entity.Property(e => e.PtemplateId).HasColumnName("PTemplateId");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.MPathTemplateDetails)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_Path_TemplateDetails_M_PathTestMaster");
            });

            modelBuilder.Entity<MPathTemplateDetail1>(entity =>
            {
                entity.HasKey(e => e.PtemplateId)
                    .HasName("PK_M_Path_TemplateDetails1");

                entity.ToTable("M_PathTemplateDetails");

                entity.Property(e => e.PtemplateId).HasColumnName("PTemplateId");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.MPathTemplateDetail1s)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK_M_PathTemplateDetails_M_PathTestMaster");
            });

            modelBuilder.Entity<MPathTestDetailMaster>(entity =>
            {
                entity.HasKey(e => e.TestDetId);

                entity.ToTable("M_PathTestDetailMaster");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.MPathTestDetailMasters)
                    .HasForeignKey(d => d.ParameterId)
                    .HasConstraintName("FK_M_PathTestDetailMaster_M_PathParameterMaster");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.MPathTestDetailMasters)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_PathTestDetailMaster_M_PathTestMaster");
            });

            modelBuilder.Entity<MPathTestFormula>(entity =>
            {
                entity.HasKey(e => e.FormulaId);

                entity.ToTable("M_PathTestFormula");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Formula).HasMaxLength(300);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParameterName).HasMaxLength(200);
            });

            modelBuilder.Entity<MPathTestMaster>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("M_PathTestMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FootNote).HasMaxLength(400);

                entity.Property(e => e.MachineName).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PrintTestName).HasMaxLength(200);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TechniqueName).HasMaxLength(200);

                entity.Property(e => e.TestDate).HasColumnType("datetime");

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.TestTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MPathUnitMaster>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("M_PathUnitMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<MPresTemplateD>(entity =>
            {
                entity.HasKey(e => e.PresDetId);

                entity.ToTable("M_PresTemplateD");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Instruction).HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<MPresTemplateH>(entity =>
            {
                entity.HasKey(e => e.PresId)
                    .HasName("PK_M_PresTemplateH_1");

                entity.ToTable("M_PresTemplateH");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PresTemplateName).HasMaxLength(200);
            });

            modelBuilder.Entity<MPrescriptionInstructionMaster>(entity =>
            {
                entity.HasKey(e => e.InstructionId)
                    .HasName("PK_M_PresInstructionMaster");

                entity.ToTable("M_PrescriptionInstructionMaster");

                entity.Property(e => e.InstructionHindi).HasMaxLength(200);

                entity.Property(e => e.InstructionName).HasMaxLength(500);
            });

            modelBuilder.Entity<MPrescriptionTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.TemplateId)
                    .HasName("PK_M_Presc_TemplateMaster");

                entity.ToTable("M_Prescription_TemplateMaster");

                entity.Property(e => e.TemplateDesc).HasColumnType("text");

                entity.Property(e => e.TemplateName).HasMaxLength(100);
            });

            modelBuilder.Entity<MRadiologyCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("M_Radiology_CategoryMaster");

                entity.Property(e => e.CategoryName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MRadiologyTemplateDetail>(entity =>
            {
                entity.HasKey(e => e.PtemplateId);

                entity.ToTable("M_RadiologyTemplateDetails");

                entity.Property(e => e.PtemplateId).HasColumnName("PTemplateId");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.MRadiologyTemplateDetails)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_RadiologyTemplateDetails_M_Radiology_TemplateMaster");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.MRadiologyTemplateDetails)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_RadiologyTemplateDetails_M_RadiologyTestMaster");
            });

            modelBuilder.Entity<MRadiologyTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("M_Radiology_TemplateMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateDesc).HasColumnType("text");

                entity.Property(e => e.TemplateDescInHtml).HasColumnName("TemplateDescInHTML");

                entity.Property(e => e.TemplateName).HasMaxLength(100);
            });

            modelBuilder.Entity<MRadiologyTestMaster>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("M_RadiologyTestMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PrintTestName).HasMaxLength(200);

                entity.Property(e => e.TestName).HasMaxLength(200);
            });

            modelBuilder.Entity<MRefByPatientMaster>(entity =>
            {
                entity.HasKey(e => e.RefById);

                entity.ToTable("M_RefByPatientMaster");

                entity.Property(e => e.RefByName).HasMaxLength(100);
            });

            modelBuilder.Entity<MRefBySubCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.RefBySubId);

                entity.ToTable("M_RefBySubCategoryMaster");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.RefBySubName).HasMaxLength(100);
            });

            modelBuilder.Entity<MRelationshipMaster>(entity =>
            {
                entity.HasKey(e => e.RelationshipId);

                entity.ToTable("M_RelationshipMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RelationshipName).HasMaxLength(100);
            });

            modelBuilder.Entity<MReligionMaster>(entity =>
            {
                entity.HasKey(e => e.ReligionId);

                entity.ToTable("M_ReligionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReligionName).HasMaxLength(100);
            });

            modelBuilder.Entity<MReportConfig>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("M_ReportConfig");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ReportSpname).HasColumnName("ReportSPName");

                entity.Property(e => e.SummaryLabel).HasColumnName("summaryLabel");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MReportConfigDetail>(entity =>
            {
                entity.HasKey(e => e.ReportColId);

                entity.ToTable("M_ReportConfigDetails");

                entity.Property(e => e.ProcedureName).HasMaxLength(200);

                entity.Property(e => e.ReportColumn).HasMaxLength(100);

                entity.Property(e => e.ReportColumnAligment).HasMaxLength(50);

                entity.Property(e => e.ReportColumnWidth).HasMaxLength(50);

                entity.Property(e => e.ReportGroupByLabel).HasMaxLength(100);

                entity.Property(e => e.ReportHeader).HasMaxLength(100);

                entity.Property(e => e.ReportTotalField).HasMaxLength(100);

                entity.Property(e => e.SummaryLabel).HasMaxLength(50);

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.MReportConfigDetails)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK_M_ReportConfigDetails_M_ReportConfig");
            });

            modelBuilder.Entity<MReportConfiguration>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("M_ReportConfiguration");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ReportName).HasMaxLength(200);

                entity.Property(e => e.ReportSection).HasMaxLength(100);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MReportTemplateConfig>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("M_ReportTemplateConfig");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateName).HasMaxLength(100);
            });

            modelBuilder.Entity<MReportconfigBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("m_reportconfig_backup");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ReportId).ValueGeneratedOnAdd();

                entity.Property(e => e.ReportSpname).HasColumnName("ReportSPName");

                entity.Property(e => e.SummaryLabel).HasColumnName("summaryLabel");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MSalesTypeMaster>(entity =>
            {
                entity.HasKey(e => e.SalesTypeId);

                entity.ToTable("M_SalesTypeMaster");

                entity.Property(e => e.SalesHeadName).HasMaxLength(50);
            });

            modelBuilder.Entity<MSmsmappingTemplate>(entity =>
            {
                entity.HasKey(e => e.TMappingId)
                    .HasName("PK_T_SMSMappingTemplate");

                entity.ToTable("M_SMSMappingTemplate");

                entity.Property(e => e.TMappingId).HasColumnName("T_MappingId");

                entity.Property(e => e.UserDefName).HasMaxLength(100);
            });

            modelBuilder.Entity<MStateMaster>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("M_StateMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(100);
            });

            modelBuilder.Entity<MStoreMaster>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.ToTable("M_StoreMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DlNo)
                    .HasMaxLength(100)
                    .HasColumnName("DL_NO");

                entity.Property(e => e.GrnNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.GrnPrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.GrnreturnNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.GrnreturnNoPrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Gstin)
                    .HasMaxLength(100)
                    .HasColumnName("GSTIN");

                entity.Property(e => e.HospitalEmailId).HasMaxLength(500);

                entity.Property(e => e.HospitalMobileNo).HasMaxLength(20);

                entity.Property(e => e.IndentNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.IndentPrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IsSmsmsg).HasColumnName("IsSMSMsg");

                entity.Property(e => e.IssueToDeptNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.IssueToDeptPrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PharSalCountId).HasColumnName("PharSalCountID");

                entity.Property(e => e.PharSalRecCountId).HasColumnName("PharSalRecCountID");

                entity.Property(e => e.PharSalReturnCountId).HasColumnName("PharSalReturnCountID");

                entity.Property(e => e.PrintStoreName).HasMaxLength(200);

                entity.Property(e => e.PrintStoreUnitName).HasMaxLength(100);

                entity.Property(e => e.PurchaseNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.PurchasePrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ReturnFromDeptNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.ReturnFromDeptNoPrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SmstemplateId)
                    .HasMaxLength(100)
                    .HasColumnName("SMSTemplateId");

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName).HasMaxLength(100);

                entity.Property(e => e.StoreShortName).HasMaxLength(50);

                entity.Property(e => e.TermsAndCondition)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.WhatsAppTemplateId).HasMaxLength(100);

                entity.Property(e => e.WorkOrderNo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.WorkOrderPrefix)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MSubGroupMaster>(entity =>
            {
                entity.HasKey(e => e.SubGroupId);

                entity.ToTable("M_SubGroupMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SubGroupName).HasMaxLength(100);
            });

            modelBuilder.Entity<MSubTpacompanyMaster>(entity =>
            {
                entity.HasKey(e => e.SubCompanyId)
                    .HasName("PK_SubTPACompanyMaster");

                entity.ToTable("M_SubTPACompanyMaster");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CompanyName).HasMaxLength(255);

                entity.Property(e => e.CompanyShortName).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FaxNo)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MSupplierMaster>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("M_SupplierMaster");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Bankname).HasMaxLength(100);

                entity.Property(e => e.Branch).HasMaxLength(100);

                entity.Property(e => e.ContactPerson).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreditPeriod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DlNo).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ExpDate).HasColumnType("datetime");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Gstno)
                    .HasMaxLength(20)
                    .HasColumnName("GSTNo");

                entity.Property(e => e.Ifsccode).HasMaxLength(20);

                entity.Property(e => e.LicNo).HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpeningBalance).HasColumnType("money");

                entity.Property(e => e.PanNo).HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.PinCode).HasMaxLength(20);

                entity.Property(e => e.SupplierName).HasMaxLength(100);

                entity.Property(e => e.SupplierTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MSystemConfig>(entity =>
            {
                entity.HasKey(e => e.SystemConfigId);

                entity.ToTable("M_SystemConfig");

                entity.Property(e => e.SystemInputValue).HasMaxLength(50);

                entity.Property(e => e.SystemName).HasMaxLength(50);
            });

            modelBuilder.Entity<MTalukaMaster>(entity =>
            {
                entity.HasKey(e => e.TalukaId);

                entity.ToTable("M_TalukaMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TalukaName).HasMaxLength(100);
            });

            modelBuilder.Entity<MTaxNatureMaster>(entity =>
            {
                entity.ToTable("M_TaxNatureMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TaxNature).HasMaxLength(500);
            });

            modelBuilder.Entity<MTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("M_TemplateMaster");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateDesc).HasColumnType("text");

                entity.Property(e => e.TemplateDescInHtml).HasColumnName("TemplateDescInHTML");

                entity.Property(e => e.TemplateName).HasMaxLength(100);
            });

            modelBuilder.Entity<MTermsOfPaymentMaster>(entity =>
            {
                entity.ToTable("M_TermsOfPaymentMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TermsOfPayment).HasMaxLength(500);
            });

            modelBuilder.Entity<MUnitofMeasurementMaster>(entity =>
            {
                entity.HasKey(e => e.UnitofMeasurementId);

                entity.ToTable("M_UnitofMeasurementMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UnitofMeasurementName).HasMaxLength(50);
            });

            modelBuilder.Entity<MUploadCategoryMaster>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("M_UploadCategoryMaster");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.UploadCategoryId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MVillage>(entity =>
            {
                entity.HasKey(e => e.VillageId)
                    .HasName("PK_M_Village_1");

                entity.ToTable("M_Village");

                entity.Property(e => e.AddedByName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TalukaName).HasMaxLength(100);

                entity.Property(e => e.VillageName).HasMaxLength(100);
            });

            modelBuilder.Entity<Mdimenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_MDIMenu_1");

                entity.ToTable("MDIMenu");

                entity.Property(e => e.MenuKey).HasMaxLength(100);

                entity.Property(e => e.MenuName).HasMaxLength(100);
            });

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.ToTable("MenuMaster");

                entity.Property(e => e.Icon).HasMaxLength(250);

                entity.Property(e => e.LinkAction).HasMaxLength(250);

                entity.Property(e => e.LinkName).HasMaxLength(250);

                entity.Property(e => e.PermissionCode).HasMaxLength(50);
            });

            modelBuilder.Entity<MessageConfiguration>(entity =>
            {
                entity.HasKey(e => e.MessageConfigId)
                    .HasName("PK_MessageConfiguration1");

                entity.ToTable("MessageConfiguration");

                entity.Property(e => e.BaseUrl)
                    .HasColumnType("ntext")
                    .HasColumnName("BaseURL");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MessageType).HasMaxLength(50);

                entity.Property(e => e.OriginalUrl)
                    .HasColumnType("ntext")
                    .HasColumnName("OriginalURL");

                entity.Property(e => e.ParameterUrl)
                    .HasColumnType("ntext")
                    .HasColumnName("ParameterURL");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.VendorName).HasMaxLength(500);
            });

            modelBuilder.Entity<MessageTypeParameter>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IdColumn).HasMaxLength(50);

                entity.Property(e => e.MessageType).HasMaxLength(50);

                entity.Property(e => e.MessageUrl).HasColumnName("MessageURL");

                entity.Property(e => e.TemplateName).HasMaxLength(150);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.ViewName).HasColumnType("ntext");
            });

            modelBuilder.Entity<MisIpgroWiseTot>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MIS_IPGroWiseTot");

                entity.Property(e => e.ChargeNetAmt).HasColumnType("money");

                entity.Property(e => e.ChargesConAmt).HasColumnType("money");

                entity.Property(e => e.DocNameId).HasColumnName("DocNameID");

                entity.Property(e => e.DoctorName).HasMaxLength(101);

                entity.Property(e => e.GroupNameS).HasMaxLength(100);

                entity.Property(e => e.Label)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("LABEL");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ServiceCnt).HasColumnName("ServiceCNT");
            });

            modelBuilder.Entity<MisOpdocPatCnt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MIS_OPDocPatCNT");

                entity.Property(e => e.DoctorName).HasMaxLength(101);

                entity.Property(e => e.Label)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("LABEL");
            });

            modelBuilder.Entity<MisOpdocRevTot>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MIS_OPDocRevTot");

                entity.Property(e => e.DoctorName).HasMaxLength(101);

                entity.Property(e => e.Label)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("LABEL");

                entity.Property(e => e.NetBillAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.TotalBillAmount).HasColumnType("money");
            });

            modelBuilder.Entity<MisOpgroWiseTot>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MIS_OPGroWiseTot");

                entity.Property(e => e.ChargeNetAmt).HasColumnType("money");

                entity.Property(e => e.ChargesConAmt).HasColumnType("money");

                entity.Property(e => e.DoctorName).HasMaxLength(101);

                entity.Property(e => e.GroupNameS).HasMaxLength(100);

                entity.Property(e => e.Label)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("LABEL");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ServiceCnt).HasColumnName("ServiceCNT");
            });

            modelBuilder.Entity<NeroOtdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NeroOTDetail");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdviceonDischarge).HasColumnType("text");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BloodLoss).HasMaxLength(500);

                entity.Property(e => e.Findings).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.OtneroId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OTNeroId");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.Position).HasMaxLength(500);

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.Surgery).HasColumnType("text");

                entity.Property(e => e.SurgeryName).HasMaxLength(300);
            });

            modelBuilder.Entity<NewPriceList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NewPriceList");

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.ClassRate).HasColumnType("money");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.NewPrice)
                    .HasColumnType("money")
                    .HasColumnName("New_Price");

                entity.Property(e => e.ServiceName).HasMaxLength(500);
            });

            modelBuilder.Entity<NotificationMaster>(entity =>
            {
                entity.ToTable("NotificationMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ReadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Obst>(entity =>
            {
                entity.ToTable("Obst");

                entity.Property(e => e.ObstId).HasColumnName("ObstID");

                entity.Property(e => e.AbdCircms).HasMaxLength(50);

                entity.Property(e => e.AbdCriWks).HasMaxLength(50);

                entity.Property(e => e.Anomalies).HasMaxLength(200);

                entity.Property(e => e.ApproxEddbyUsg)
                    .HasMaxLength(50)
                    .HasColumnName("ApproxEDDByUSG");

                entity.Property(e => e.ApproxWeight).HasMaxLength(50);

                entity.Property(e => e.Bpdcms)
                    .HasMaxLength(50)
                    .HasColumnName("BPDcms");

                entity.Property(e => e.Bpdwks)
                    .HasMaxLength(50)
                    .HasColumnName("BPDWks");

                entity.Property(e => e.CardiacActivity).HasMaxLength(50);

                entity.Property(e => e.CervicalLength).HasMaxLength(50);

                entity.Property(e => e.Clrwks)
                    .HasMaxLength(50)
                    .HasColumnName("CLRWks");

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.Crlcms)
                    .HasMaxLength(50)
                    .HasColumnName("CRLcms");

                entity.Property(e => e.EddbyLmp)
                    .HasMaxLength(50)
                    .HasColumnName("EDDbyLMP");

                entity.Property(e => e.FemurWks).HasMaxLength(50);

                entity.Property(e => e.Femurcms).HasMaxLength(50);

                entity.Property(e => e.FetalMovements).HasMaxLength(50);

                entity.Property(e => e.Fhs)
                    .HasMaxLength(50)
                    .HasColumnName("FHS");

                entity.Property(e => e.Grade).HasMaxLength(50);

                entity.Property(e => e.HeadCriWks).HasMaxLength(50);

                entity.Property(e => e.HeadCricms).HasMaxLength(50);

                entity.Property(e => e.InternalOs)
                    .HasMaxLength(50)
                    .HasColumnName("InternalOS");

                entity.Property(e => e.Liquor).HasMaxLength(50);

                entity.Property(e => e.Lmp)
                    .HasMaxLength(50)
                    .HasColumnName("LMP");

                entity.Property(e => e.Msdcms)
                    .HasMaxLength(50)
                    .HasColumnName("MSDcms");

                entity.Property(e => e.Msdwks)
                    .HasMaxLength(50)
                    .HasColumnName("MSDWks");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.Placenta).HasMaxLength(50);

                entity.Property(e => e.PresentationLie).HasMaxLength(50);

                entity.Property(e => e.SingleTwinPregnancyDays).HasMaxLength(50);

                entity.Property(e => e.SingleTwinPregnancyWks).HasMaxLength(50);

                entity.Property(e => e.TheFetalMaturity).HasMaxLength(50);

                entity.Property(e => e.TranDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OpIpColCashRefund>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OP_IP_ColCash_Refund");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.TodRefCollection).HasColumnType("money");
            });

            modelBuilder.Entity<OpIpCollection>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OP_IP_Collection");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.TodaysCollection).HasColumnType("money");
            });

            modelBuilder.Entity<OpIpCollectionRefund>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OP_IP_Collection_Refund");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.TodRefCollection).HasColumnType("money");
            });

            modelBuilder.Entity<OpphAppSmsqueryTempleteSm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OPPhAppSMSQueryTemplete_SMS");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AppDate).HasMaxLength(11);

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.DoctorName).HasMaxLength(152);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.PatientName).HasMaxLength(152);

                entity.Property(e => e.PhAppDate).HasMaxLength(10);

                entity.Property(e => e.PhAppTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneAppDate).HasColumnType("datetime");

                entity.Property(e => e.SeqNo).HasMaxLength(50);

                entity.Property(e => e.TodaysDate).HasMaxLength(10);
            });

            modelBuilder.Entity<OpsmsqueryTempleteSm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OPSMSQueryTemplete_SMS");

                entity.Property(e => e.AgeYear).HasMaxLength(10);

                entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.DoctorName).HasMaxLength(105);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.PbillNo).HasColumnName("PBillNo");

                entity.Property(e => e.RefDoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.RefDoctorName).HasMaxLength(105);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.TodaysDate).HasMaxLength(10);
            });

            modelBuilder.Entity<Otcharge>(entity =>
            {
                entity.HasKey(e => e.OtchargesId);

                entity.ToTable("OTCharges");

                entity.Property(e => e.OtchargesId).HasColumnName("OTChargesId");

                entity.Property(e => e.ChargesDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");
            });

            modelBuilder.Entity<Otdetail>(entity =>
            {
                entity.ToTable("OTDetails");

                entity.Property(e => e.OtdetailId).HasColumnName("OTDetailID");

                entity.Property(e => e.BirthRegNo).HasMaxLength(50);

                entity.Property(e => e.BirthTime).HasColumnType("datetime");

                entity.Property(e => e.OpDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OperationNotes).HasMaxLength(500);

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.OtheaderNo).HasColumnName("OTHeaderNo");

                entity.Property(e => e.TheaterName).HasMaxLength(50);

                entity.Property(e => e.TranDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientTypeMaster>(entity =>
            {
                entity.HasKey(e => e.PatientTypeId);

                entity.ToTable("PatientTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PatientType).HasMaxLength(100);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardDate).HasColumnType("datetime");

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChAdvanceUsedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_AdvanceUsedAmount");

                entity.Property(e => e.ChCardPayAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_CardPayAmount");

                entity.Property(e => e.ChCashPayAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_CashPayAmount");

                entity.Property(e => e.ChChequePayAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_ChequePayAmount");

                entity.Property(e => e.ChNeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_NEFTPayAmount");

                entity.Property(e => e.ChPayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("Ch_PayTMAmount");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsSelfOrcompany).HasColumnName("IsSelfORCompany");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(50)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.PayTmdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PayTMDate");

                entity.Property(e => e.PayTmtranNo)
                    .HasMaxLength(20)
                    .HasColumnName("PayTMTranNo");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.Tdsamount).HasColumnName("TDSAmount");

                entity.Property(e => e.TranMode).HasMaxLength(30);

                entity.Property(e => e.Wfamount)
                    .HasColumnType("money")
                    .HasColumnName("WFAmount");
            });

            modelBuilder.Entity<PaymentPharmacy>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("PaymentPharmacy");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardDate).HasColumnType("datetime");

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsSelfOrcompany).HasColumnName("IsSelfORCompany");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(100)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.Opdipdtype).HasColumnName("OPDIPDType");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.PayTmdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PayTMDate");

                entity.Property(e => e.PayTmtranNo)
                    .HasMaxLength(20)
                    .HasColumnName("PayTMTranNo");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.TranMode).HasMaxLength(30);

                entity.Property(e => e.Wfamount)
                    .HasColumnType("money")
                    .HasColumnName("WFAmount");
            });

            modelBuilder.Entity<PermissionMaster>(entity =>
            {
                entity.ToTable("PermissionMaster");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.PermissionMasters)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionMaster_MenuMaster");
            });

            modelBuilder.Entity<PharTotalSalesV>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PharTotalSales_v");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.TotalSales).HasColumnType("money");
            });

            modelBuilder.Entity<ProcedureMaster>(entity =>
            {
                entity.ToTable("ProcedureMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProcedureName).HasMaxLength(500);
            });

            modelBuilder.Entity<PsLvwRtrvPathologyResultIpwithAge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ps_lvwRtrv_PathologyResultIPWithAge");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParaBoldFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<PsLvwRtrvPathologyResultOpwithAge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ps_lvwRtrv_PathologyResultOPWithAge");

                entity.Property(e => e.AgeType).HasMaxLength(10);

                entity.Property(e => e.Formula).HasMaxLength(100);

                entity.Property(e => e.MaxValue).HasMaxLength(50);

                entity.Property(e => e.MinValue).HasMaxLength(50);

                entity.Property(e => e.NormalRange).HasMaxLength(155);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.ParaBoldFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterName).HasMaxLength(100);

                entity.Property(e => e.ParameterShortName).HasMaxLength(100);

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.SubTestId).HasColumnName("SubTestID");

                entity.Property(e => e.SubTestName).HasMaxLength(200);

                entity.Property(e => e.SuggestionNote).HasMaxLength(400);

                entity.Property(e => e.TestName).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<Refund>(entity =>
            {
                entity.ToTable("Refund");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundNo).HasMaxLength(20);

                entity.Property(e => e.RefundTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(255);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.RegId);

                entity.ToTable("Registration");

                entity.Property(e => e.AadharCardNo).HasMaxLength(25);

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Age).HasMaxLength(10);

                entity.Property(e => e.AgeDay).HasMaxLength(10);

                entity.Property(e => e.AgeMonth).HasMaxLength(10);

                entity.Property(e => e.AgeYear).HasMaxLength(10);

                entity.Property(e => e.AnnualIncome)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.EmgAadharCardNo).HasMaxLength(20);

                entity.Property(e => e.EmgContactPersonName).HasMaxLength(50);

                entity.Property(e => e.EmgDrivingLicenceNo).HasMaxLength(20);

                entity.Property(e => e.EmgLandlineNo).HasMaxLength(12);

                entity.Property(e => e.EmgMobileNo).HasMaxLength(12);

                entity.Property(e => e.EngAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IsCharity).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsIndientOrWeaker).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MedTourismDateOfEntry).HasColumnType("datetime");

                entity.Property(e => e.MedTourismNationalityId)
                    .HasMaxLength(20)
                    .HasColumnName("MedTourismNationalityID");

                entity.Property(e => e.MedTourismOfficeWorkAddress).HasMaxLength(100);

                entity.Property(e => e.MedTourismPassportNo).HasMaxLength(20);

                entity.Property(e => e.MedTourismPortOfEntry).HasMaxLength(20);

                entity.Property(e => e.MedTourismResidentialAddress).HasMaxLength(100);

                entity.Property(e => e.MedTourismVisaIssueDate).HasColumnType("datetime");

                entity.Property(e => e.MedTourismVisaValidityDate).HasColumnType("datetime");

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PanCardNo).HasMaxLength(10);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.Photo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PinNo).HasMaxLength(10);

                entity.Property(e => e.RationCardNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RegDate).HasColumnType("datetime");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.RegPrefix).HasMaxLength(10);

                entity.Property(e => e.RegTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<RegistrationSmsquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RegistrationSMSQuery");

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(651)
                    .HasColumnName("SMSString");
            });

            modelBuilder.Entity<ReportBatchLog>(entity =>
            {
                entity.HasKey(e => e.BatchId)
                    .HasName("PK__ReportBa__5D55CE58D0FFE440");

                entity.ToTable("ReportBatchLog");

                entity.Property(e => e.BatchId).ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ExecutedBy)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<ReportConfig>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__ReportCo__D5BD480551430DDE");

                entity.ToTable("ReportConfig");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReportName).HasMaxLength(100);

                entity.Property(e => e.ReportType).HasMaxLength(50);
            });

            modelBuilder.Entity<ReportDatum>(entity =>
            {
                entity.HasKey(e => e.ReportDataId)
                    .HasName("PK__ReportDa__8F548817C2C64344");

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.Dbyamount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("DBYAmount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LoadDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Lysd)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LYSD")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lytd)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("LYTD")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Mtd)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("MTD")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pmt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PMT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReportDate).HasColumnType("date");

                entity.Property(e => e.ReportName).HasMaxLength(100);

                entity.Property(e => e.TodayAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.YesterdayAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ytd)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("YTD")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ReportDemo>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("ReportDemo");

                entity.Property(e => e.ReportId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ReportSpname).HasColumnName("ReportSPName");

                entity.Property(e => e.SummaryLabel).HasColumnName("summaryLabel");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReportExecutionLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__ReportEx__5E5486483F23FF55");

                entity.ToTable("ReportExecutionLog");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ExecutedBy)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ReportName).HasMaxLength(100);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.ReportExecutionLogs)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_ReportExecutionLog_Batch");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportExecutionLogs)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("FK_ReportExecutionLog_Config");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("RoleMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(250);
            });

            modelBuilder.Entity<RoleTemplateDetail>(entity =>
            {
                entity.HasKey(e => e.RoleDetailId);

                entity.ToTable("RoleTemplateDetail");

                entity.Property(e => e.MenuKey).HasMaxLength(100);
            });

            modelBuilder.Entity<RoleTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("RoleTemplateMaster");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<RoomMaster>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("RoomMaster");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RoomName).HasMaxLength(100);
            });

            modelBuilder.Entity<RtrvAdvDetForPay>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Rtrv_AdvDetForPay");

                entity.Property(e => e.AdvanceAmount).HasColumnType("money");

                entity.Property(e => e.AdvanceDetailId).HasColumnName("AdvanceDetailID");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardDate).HasColumnType("datetime");

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequeBankName).HasMaxLength(100);

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.Date)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(50)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.PayTmdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PayTMDate");

                entity.Property(e => e.PayTmtranNo)
                    .HasMaxLength(20)
                    .HasColumnName("PayTMTranNo");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Reason).HasMaxLength(200);

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.UsedAmount).HasColumnType("money");
            });

            modelBuilder.Entity<SalesGststoreWiseOpip>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesGSTStoreWise_OPIP");

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");

                entity.Property(e => e.Lbl)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("lbl");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TaxableAmount).HasColumnType("money");
            });

            modelBuilder.Entity<SalesPaymentDateWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesPaymentDateWise");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SalesRefundDateWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesRefundDateWise");

                entity.Property(e => e.CashPay).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");
            });

            modelBuilder.Entity<SalesReturnAmt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesReturnAmt");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Credit)
                    .HasMaxLength(500)
                    .HasColumnName("CREDIT");

                entity.Property(e => e.Debit)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("DEBIT");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");
            });

            modelBuilder.Entity<SalesReturnGststoreWise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesReturnGSTStoreWise");

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");

                entity.Property(e => e.Lbl)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("lbl");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TaxableAmount).HasColumnType("money");
            });

            modelBuilder.Entity<SalesReturnGststoreWiseOpip>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SalesReturnGSTStoreWise_OPIP");

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");

                entity.Property(e => e.Lbl)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("lbl");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TaxableAmount).HasColumnType("money");
            });

            modelBuilder.Entity<ScheduleLog>(entity =>
            {
                entity.Property(e => e.ExecuteDay).HasColumnType("date");

                entity.Property(e => e.ExecuteOn).HasColumnType("datetime");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ScheduleLogs)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleLogs_ScheduleMaster");
            });

            modelBuilder.Entity<ScheduleMaster>(entity =>
            {
                entity.ToTable("ScheduleMaster");

                entity.Property(e => e.Days).HasMaxLength(250);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ScheduleName).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<SerPer>(entity =>
            {
                entity.HasKey(e => e.SerId);

                entity.ToTable("SerPer");

                entity.Property(e => e.SerPer1).HasColumnName("SerPer");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            });

            modelBuilder.Entity<ServiceDetail>(entity =>
            {
                entity.ToTable("ServiceDetail");

                entity.Property(e => e.ClassRate).HasColumnType("money");

                entity.Property(e => e.DiscountAmount).HasColumnType("money");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceDetails)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ServiceDetail_ServiceMaster");
            });

            modelBuilder.Entity<ServiceMaster>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("ServiceMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmgAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmgEndTime).HasColumnType("datetime");

                entity.Property(e => e.EmgPer).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmgStartTime).HasColumnType("datetime");

                entity.Property(e => e.IsDocEditable).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsEmergency).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PackageConsumableAmount).HasColumnType("money");

                entity.Property(e => e.PackageIcudays).HasColumnName("PackageICUDays");

                entity.Property(e => e.PackageMedicineAmount).HasColumnType("money");

                entity.Property(e => e.ServiceName).HasMaxLength(200);

                entity.Property(e => e.ServiceShortDesc).HasMaxLength(200);

                entity.Property(e => e.SubGroupId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ServiceWiseCompanyCode>(entity =>
            {
                entity.HasKey(e => e.ServiceDetCompId);

                entity.ToTable("ServiceWiseCompanyCode");

                entity.Property(e => e.CompanyCode).HasMaxLength(50);

                entity.Property(e => e.CompanyServicePrint).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SmsoutGoing>(entity =>
            {
                entity.ToTable("SMSOutGoing");

                entity.HasIndex(e => e.IsSent, "_dta_index_SMSOutGoing_11_1949249999__K4_1_2_3")
                    .HasFillFactor(90);

                entity.Property(e => e.SmsoutGoingId).HasColumnName("SMSOutGoingID");

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Smsdate)
                    .HasColumnType("datetime")
                    .HasColumnName("SMSDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(700)
                    .HasColumnName("SMSString");

                entity.Property(e => e.Smsurl)
                    .HasMaxLength(1000)
                    .HasColumnName("smsurl");
            });

            modelBuilder.Entity<Sonography>(entity =>
            {
                entity.ToTable("Sonography");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Liver).HasMaxLength(500);

                entity.Property(e => e.LtKideny).HasMaxLength(500);

                entity.Property(e => e.LtOvary).HasMaxLength(500);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.Pancrea).HasMaxLength(500);

                entity.Property(e => e.RtKidney).HasMaxLength(500);

                entity.Property(e => e.RtOvary).HasMaxLength(500);

                entity.Property(e => e.Spleen).HasMaxLength(500);

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.Uterus).HasMaxLength(500);
            });

            modelBuilder.Entity<SsConfigInitiateDischarge>(entity =>
            {
                entity.HasKey(e => e.SsInitateDiscId);

                entity.ToTable("SS_Config_initiateDischarge");

                entity.Property(e => e.SsInitateDiscId).HasColumnName("ssInitateDiscId");

                entity.Property(e => e.AddedByDatetime).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentName).HasMaxLength(100);

                entity.Property(e => e.UpdatedByDatetime).HasColumnType("datetime");
            });

            modelBuilder.Entity<SsGstperConfig>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SS_GSTPer_Config");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");
            });

            modelBuilder.Entity<SsMenuMaster>(entity =>
            {
                entity.ToTable("SS_Menu_master");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MenuMasterAction)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_action");

                entity.Property(e => e.MenuMasterBlock)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_block");

                entity.Property(e => e.MenuMasterController)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_controller");

                entity.Property(e => e.MenuMasterDisplaySrNo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_display_sr_no");

                entity.Property(e => e.MenuMasterIcon)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_icon");

                entity.Property(e => e.MenuMasterId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_id");

                entity.Property(e => e.MenuMasterLinkName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_link_name");
            });

            modelBuilder.Entity<SsMenuMasterDetail>(entity =>
            {
                entity.HasKey(e => e.MenuSubId)
                    .HasName("PK_Menu_master_sub");

                entity.ToTable("SS_Menu_Master_detail");

                entity.Property(e => e.MenuSubId).HasColumnName("Menu_SubID");

                entity.Property(e => e.MenuMasterDetailAction)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_detail_action");

                entity.Property(e => e.MenuMasterDetailBlock)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_block");

                entity.Property(e => e.MenuMasterDetailDisplaySrNo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_display_sr_no");

                entity.Property(e => e.MenuMasterDetailIcon)
                    .HasMaxLength(100)
                    .HasColumnName("menu_master_detail_icon");

                entity.Property(e => e.MenuMasterDetailLinkName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_detail_link_name");

                entity.Property(e => e.MenuMasterDetailMasterId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_master_id");

                entity.Property(e => e.MenuMasterDetailSrNo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_sr_no");
            });

            modelBuilder.Entity<SsMenuMasterDetailDetail>(entity =>
            {
                entity.HasKey(e => e.MenuSubSubId)
                    .HasName("PK_Menu_master_Sub_Sub");

                entity.ToTable("SS_Menu_Master_detail_detail");

                entity.Property(e => e.MenuSubSubId).HasColumnName("Menu_Sub_SubID");

                entity.Property(e => e.MenuMasterDetailDetailAction)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_detail_detail_action");

                entity.Property(e => e.MenuMasterDetailDetailBlock)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_detail_block");

                entity.Property(e => e.MenuMasterDetailDetailDisplaySrNo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_detail_display_sr_no");

                entity.Property(e => e.MenuMasterDetailDetailIcon)
                    .HasMaxLength(100)
                    .HasColumnName("menu_master_detail_detail_icon");

                entity.Property(e => e.MenuMasterDetailDetailLinkName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("menu_master_detail_detail_link_name");

                entity.Property(e => e.MenuMasterDetailDetailMasterId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_detail_master_id");

                entity.Property(e => e.MenuMasterDetailDetailMasterSrNo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_detail_master_sr_no");

                entity.Property(e => e.MenuMasterDetailDetailSrNo)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("menu_master_detail_detail_sr_no");
            });

            modelBuilder.Entity<SsMobileAppLogin>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("SS_MobileAppLogin");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CustomerAddress).HasMaxLength(500);

                entity.Property(e => e.CustomerMobileNo).HasMaxLength(20);

                entity.Property(e => e.CustomerName).HasMaxLength(100);

                entity.Property(e => e.CustomerPasscode).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SsRoleTemplateDetail>(entity =>
            {
                entity.HasKey(e => e.RoleDetId);

                entity.ToTable("SS_RoleTemplateDetail");

                entity.Property(e => e.AddedByDate).HasColumnType("datetime");

                entity.Property(e => e.MenuMasterAction)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_action");

                entity.Property(e => e.MenuMasterDetailAction)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_detail_action");

                entity.Property(e => e.MenuMasterDetailDetailAction)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_detail_detail_action");

                entity.Property(e => e.MenuMasterDetailDetailIcon)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_detail_detail_icon");

                entity.Property(e => e.MenuMasterDetailDetailLinkName)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_detail_detail_link_name");

                entity.Property(e => e.MenuMasterDetailIcon)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_detail_icon");

                entity.Property(e => e.MenuMasterDetailLinkName)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_detail_link_name");

                entity.Property(e => e.MenuMasterIcon)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_icon");

                entity.Property(e => e.MenuMasterId).HasColumnName("menu_master_id");

                entity.Property(e => e.MenuMasterLinkName)
                    .HasMaxLength(300)
                    .HasColumnName("menu_master_link_name");
            });

            modelBuilder.Entity<SsRoleTemplateMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("SS_RoleTemplateMaster");

                entity.Property(e => e.AddedByDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(100);

                entity.Property(e => e.UpdatedByDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SsSmsConfig>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SS_SMS_Config");

                entity.Property(e => e.Campaign)
                    .HasMaxLength(10)
                    .HasColumnName("campaign");

                entity.Property(e => e.ConType).HasMaxLength(20);

                entity.Property(e => e.Keys)
                    .HasMaxLength(30)
                    .HasColumnName("keys");

                entity.Property(e => e.Routeid).HasColumnName("routeid");

                entity.Property(e => e.SenderId).HasMaxLength(10);

                entity.Property(e => e.Spassword)
                    .HasMaxLength(100)
                    .HasColumnName("SPassword");

                entity.Property(e => e.StorageLocLink).HasMaxLength(500);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .HasColumnName("url");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<StockLog>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockLogs)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockLogs_T_CurrentStock");
            });

            modelBuilder.Entity<TAbill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_ABill");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillTime).HasColumnType("datetime");

                entity.Property(e => e.CompanyRefNo).HasMaxLength(50);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmt).HasColumnType("money");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.TotalAdvanceAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<TAbillDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_ABillDetails");

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");
            });

            modelBuilder.Entity<TAddCharge>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_AddCharges");

                entity.Property(e => e.ChargesDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesTime).HasColumnType("datetime");

                entity.Property(e => e.ConcessionAmount).HasColumnType("money");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsDoctorShareGenerated).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsInterimBillFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PackageMainChargeId).HasColumnName("PackageMainChargeID");

                entity.Property(e => e.TchId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TChId");
            });

            modelBuilder.Entity<TAdditionalDocPay>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_AdditionalDocPay");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.DocAmount).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(200);

                entity.Property(e => e.PbillNo).HasColumnName("PBillNo");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ServiceName).HasMaxLength(200);

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranId).ValueGeneratedOnAdd();

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TAdmRefInfo>(entity =>
            {
                entity.HasKey(e => e.AdmVisitId);

                entity.ToTable("T_AdmRefInfo");

                entity.Property(e => e.AdmVisitId).HasColumnName("AdmVisitID");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.RefVdate)
                    .HasColumnType("datetime")
                    .HasColumnName("RefVDate");

                entity.Property(e => e.RefVtime)
                    .HasColumnType("datetime")
                    .HasColumnName("RefVTime");
            });

            modelBuilder.Entity<TAdvDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_AdvDetail");

                entity.Property(e => e.TadvDaddedBy).HasColumnName("TAdvDAddedBy");

                entity.Property(e => e.TadvDadvanceAmount)
                    .HasColumnType("money")
                    .HasColumnName("TAdvDAdvanceAmount");

                entity.Property(e => e.TadvDadvanceDetailId).HasColumnName("TAdvDAdvanceDetailID");

                entity.Property(e => e.TadvDadvanceId).HasColumnName("TAdvDAdvanceId");

                entity.Property(e => e.TadvDadvanceNo)
                    .HasMaxLength(50)
                    .HasColumnName("TAdvDAdvanceNo");

                entity.Property(e => e.TadvDbalanceAmount)
                    .HasColumnType("money")
                    .HasColumnName("TAdvDBalanceAmount");

                entity.Property(e => e.TadvDdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TAdvDDate");

                entity.Property(e => e.TadvDisCancelled).HasColumnName("TAdvDIsCancelled");

                entity.Property(e => e.TadvDisCancelledDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TAdvDIsCancelledDate");

                entity.Property(e => e.TadvDisCancelledby).HasColumnName("TAdvDIsCancelledby");

                entity.Property(e => e.TadvDopdIpdId).HasColumnName("TAdvDOPD_IPD_Id");

                entity.Property(e => e.TadvDopdIpdType).HasColumnName("TAdvDOPD_IPD_Type");

                entity.Property(e => e.TadvDreason)
                    .HasMaxLength(200)
                    .HasColumnName("TAdvDReason");

                entity.Property(e => e.TadvDreasonOfAdvanceId).HasColumnName("TAdvDReasonOfAdvanceId");

                entity.Property(e => e.TadvDrefId).HasColumnName("TAdvDRefId");

                entity.Property(e => e.TadvDrefundAmount)
                    .HasColumnType("money")
                    .HasColumnName("TAdvDRefundAmount");

                entity.Property(e => e.TadvDtime)
                    .HasColumnType("datetime")
                    .HasColumnName("TAdvDTime");

                entity.Property(e => e.TadvDtransactionId).HasColumnName("TAdvDTransactionID");

                entity.Property(e => e.TadvDusedAmount)
                    .HasColumnType("money")
                    .HasColumnName("TAdvDUsedAmount");
            });

            modelBuilder.Entity<TAdvHeader>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_AdvHeader");

                entity.Property(e => e.TadvAddedBy).HasColumnName("TAdvAddedBy");

                entity.Property(e => e.TadvAdvanceAmount).HasColumnName("TAdvAdvanceAmount");

                entity.Property(e => e.TadvAdvanceId).HasColumnName("TAdvAdvanceId");

                entity.Property(e => e.TadvAdvanceUsedAmount).HasColumnName("TAdvAdvanceUsedAmount");

                entity.Property(e => e.TadvBalanceAmount).HasColumnName("TAdvBalanceAmount");

                entity.Property(e => e.TadvDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TAdvDate");

                entity.Property(e => e.TadvIsCancelled).HasColumnName("TAdvIsCancelled");

                entity.Property(e => e.TadvIsCancelledBy).HasColumnName("TAdvIsCancelledBy");

                entity.Property(e => e.TadvIsCancelledDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TAdvIsCancelledDate");

                entity.Property(e => e.TadvOpdIpdId).HasColumnName("TAdvOPD_IPD_Id");

                entity.Property(e => e.TadvOpdIpdType).HasColumnName("TAdvOPD_IPD_Type");

                entity.Property(e => e.TadvRefId).HasColumnName("TAdvRefId");
            });

            modelBuilder.Entity<TApayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_APayment");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardDate).HasColumnType("datetime");

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsSelfOrcompany).HasColumnName("IsSelfORCompany");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<TBatchAdjustment>(entity =>
            {
                entity.HasKey(e => e.BatchAdjId);

                entity.ToTable("T_BatchAdjustment");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");

                entity.Property(e => e.NewBatchNo).HasMaxLength(50);

                entity.Property(e => e.NewExpDate).HasColumnType("datetime");

                entity.Property(e => e.OldBatchNo).HasMaxLength(50);

                entity.Property(e => e.OldExpDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TBedOccupancy>(entity =>
            {
                entity.HasKey(e => e.BedOccId);

                entity.ToTable("T_BedOccupancy");

                entity.Property(e => e.BedOccupancyDate).HasColumnType("datetime");

                entity.Property(e => e.BedOccupancyTime).HasColumnType("datetime");

                entity.Property(e => e.WardId).HasColumnName("WardID");
            });

            modelBuilder.Entity<TBedTransferDetail>(entity =>
            {
                entity.HasKey(e => e.TransferId);

                entity.ToTable("T_BedTransferDetails");

                entity.Property(e => e.FromClassId).HasColumnName("FromClassID");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.FromWardId).HasColumnName("FromWardID");

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.ToClassId).HasColumnName("ToClassID");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.ToTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TCanteenBillDetail>(entity =>
            {
                entity.HasKey(e => e.CdetId);

                entity.ToTable("T_CanteenBillDetails");

                entity.Property(e => e.CdetId).HasColumnName("CDetId");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Gstamount)
                    .HasColumnType("money")
                    .HasColumnName("GSTAmount");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.LandedPrice).HasColumnType("money");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TotalLandedAmount).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");
            });

            modelBuilder.Entity<TCanteenBillHeader>(entity =>
            {
                entity.HasKey(e => e.BillNo);

                entity.ToTable("T_CanteenBillHeader");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CashCounterId).HasColumnName("CashCounterID");

                entity.Property(e => e.ConcessionReasonId).HasColumnName("ConcessionReasonID");

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.Gstamount)
                    .HasColumnType("money")
                    .HasColumnName("GSTAmount");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<TCanteenRequestDetail>(entity =>
            {
                entity.HasKey(e => e.ReqDetId);

                entity.ToTable("T_CanteenRequestDetails");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.HasOne(d => d.Req)
                    .WithMany(p => p.TCanteenRequestDetails)
                    .HasForeignKey(d => d.ReqId)
                    .HasConstraintName("FK_T_CanteenRequestDetails_T_CanteenRequestHeader");
            });

            modelBuilder.Entity<TCanteenRequestHeader>(entity =>
            {
                entity.HasKey(e => e.ReqId);

                entity.ToTable("T_CanteenRequestHeader");

                entity.Property(e => e.CashCounterId).HasColumnName("CashCounterID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.ReqNo).HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<TCardMemberDet>(entity =>
            {
                entity.HasKey(e => e.MemDetId);

                entity.ToTable("T_CardMemberDet");

                entity.Property(e => e.Ipper).HasColumnName("IPPer");

                entity.Property(e => e.Opper).HasColumnName("OPPer");

                entity.Property(e => e.RelationshipId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TCardMemberDetGrp>(entity =>
            {
                entity.HasKey(e => e.MemDetId);

                entity.ToTable("T_CardMemberDetGrp");
            });

            modelBuilder.Entity<TCardMemberHeader>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_T_MemberReg");

                entity.ToTable("T_CardMemberHeader");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.MemberCardNo).HasMaxLength(100);

                entity.Property(e => e.MemberCardValidateDate).HasColumnType("datetime");

                entity.Property(e => e.Mtime)
                    .HasColumnType("datetime")
                    .HasColumnName("MTime");
            });

            modelBuilder.Entity<TCertificateInformation>(entity =>
            {
                entity.HasKey(e => e.CertificateId);

                entity.ToTable("T_CertificateInformation");

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.Property(e => e.CertificateDate).HasColumnType("datetime");

                entity.Property(e => e.CertificateText).HasColumnType("text");

                entity.Property(e => e.CertificateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");
            });

            modelBuilder.Entity<TChrgComp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_ChrgComp");

                entity.Property(e => e.ChargesDate).HasColumnType("datetime");

                entity.Property(e => e.ChargesId).ValueGeneratedOnAdd();

                entity.Property(e => e.ChargesTime).HasColumnType("datetime");

                entity.Property(e => e.ConcessionAmount).HasColumnType("money");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PackageMainChargeId).HasColumnName("PackageMainChargeID");

                entity.Property(e => e.RefundAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TCompBl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_CompBl");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillTime).HasColumnType("datetime");

                entity.Property(e => e.CompanyRefNo).HasMaxLength(50);

                entity.Property(e => e.Drbno)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DRBNo");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmt).HasColumnType("money");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.TaxAmount).HasColumnType("money");

                entity.Property(e => e.TotalAdvanceAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<TCompBlDt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_CompBlDt");

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");

                entity.Property(e => e.DrbillDetId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DRBillDetId");

                entity.Property(e => e.Drno).HasColumnName("DRNo");
            });

            modelBuilder.Entity<TCompanyDetail>(entity =>
            {
                entity.HasKey(e => e.CompanyDetId);

                entity.ToTable("T_CompanyDetail");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.BillAmount).HasColumnType("money");

                entity.Property(e => e.NetPayAmount).HasColumnType("money");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.WrfAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TCompanyHeader>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("T_CompanyHeader");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PayAmount).HasColumnType("money");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");

                entity.Property(e => e.SettlementTime).HasColumnType("datetime");

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.WrfAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TConsentInformation>(entity =>
            {
                entity.HasKey(e => e.ConsentId);

                entity.ToTable("T_ConsentInformation");

                entity.Property(e => e.ConsentDate).HasColumnType("datetime");

                entity.Property(e => e.ConsentName).HasMaxLength(500);

                entity.Property(e => e.ConsentTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Opipid).HasColumnName("OPIPID");

                entity.Property(e => e.Opiptype).HasColumnName("OPIPType");
            });

            modelBuilder.Entity<TCurrentStk>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_CurrentStk");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StockId).ValueGeneratedOnAdd();

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TCurrentStkWithDaily>(entity =>
            {
                entity.HasKey(e => e.StockId);

                entity.ToTable("T_CurrentStkWith_Daily");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Date");

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.MItemMaster)
                    .HasMaxLength(100)
                    .HasColumnName("M_ItemMaster");

                entity.Property(e => e.PurUnitRateWf)
                    .HasMaxLength(50)
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.StoreMaster).HasMaxLength(50);

                entity.Property(e => e.StoreName).HasMaxLength(100);

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TCurrentStock>(entity =>
            {
                entity.HasKey(e => e.StockId)
                    .HasName("PK_T_CurrentStock_1");

                entity.ToTable("T_CurrentStock");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TCurrentStock2322>(entity =>
            {
                entity.HasKey(e => e.StockId)
                    .HasName("PK_T_CurrentStock");

                entity.ToTable("T_CurrentStock_2322");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TCurrentStockLog>(entity =>
            {
                entity.ToTable("T_CurrentStockLogs");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TCustomerInformation>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("T_CustomerInformation");

                entity.Property(e => e.Amcdate)
                    .HasColumnType("date")
                    .HasColumnName("AMCDate");

                entity.Property(e => e.ContactPersonMobileNo).HasMaxLength(15);

                entity.Property(e => e.ContactPersonName).HasMaxLength(50);

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.CustomerAddress).HasMaxLength(200);

                entity.Property(e => e.CustomerMobileNo).HasMaxLength(15);

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.CustomerPinCode).HasMaxLength(10);

                entity.Property(e => e.InstallationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TCustomerInvoiceRaise>(entity =>
            {
                entity.HasKey(e => e.InvoiceRaiseId);

                entity.ToTable("T_CustomerInvoiceRaise");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.InvDate).HasColumnType("datetime");

                entity.Property(e => e.InvNumber).HasMaxLength(10);

                entity.Property(e => e.InvTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TCustomerPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("T_CustomerPayment");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.CreatedByDateTime).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TDeathCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateId);

                entity.ToTable("T_DeathCertificate");

                entity.Property(e => e.CauseofDeath).HasMaxLength(500);

                entity.Property(e => e.CertificateDate).HasColumnType("datetime");

                entity.Property(e => e.CertificateNo).HasMaxLength(50);

                entity.Property(e => e.CertificateTime).HasColumnType("datetime");

                entity.Property(e => e.DateofDeath).HasColumnType("datetime");

                entity.Property(e => e.Diagnsis).HasMaxLength(500);

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_Id");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PlaceOfDeath).HasMaxLength(100);

                entity.Property(e => e.ResponsiblePersonName).HasMaxLength(200);

                entity.Property(e => e.Smcno)
                    .HasMaxLength(50)
                    .HasColumnName("SMCNo");

                entity.Property(e => e.TimeOfDeath).HasColumnType("datetime");
            });

            modelBuilder.Entity<TDialysi>(entity =>
            {
                entity.HasKey(e => e.DialysisId);

                entity.ToTable("T_Dialysis");

                entity.Property(e => e.BillNo).HasMaxLength(10);

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.DialysisDate).HasColumnType("datetime");

                entity.Property(e => e.DialysisStartEnd).HasColumnType("datetime");

                entity.Property(e => e.DialysisStartTime).HasColumnType("datetime");

                entity.Property(e => e.DialysisTime).HasColumnType("datetime");

                entity.Property(e => e.FutureAptDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.TechinicianName).HasMaxLength(100);
            });

            modelBuilder.Entity<TDiscCaseSheet>(entity =>
            {
                entity.HasKey(e => e.DiscCaseId);

                entity.ToTable("T_DiscCaseSheet");

                entity.Property(e => e.Cod1)
                    .HasMaxLength(500)
                    .HasColumnName("COD1");

                entity.Property(e => e.Cod2)
                    .HasMaxLength(500)
                    .HasColumnName("COD2");

                entity.Property(e => e.Cod3)
                    .HasMaxLength(500)
                    .HasColumnName("COD3");

                entity.Property(e => e.ExtCinj)
                    .HasMaxLength(500)
                    .HasColumnName("ExtCInj");

                entity.Property(e => e.ExtCinjIcdcode)
                    .HasMaxLength(500)
                    .HasColumnName("ExtCInjICDcode");

                entity.Property(e => e.FinalDiag1).HasMaxLength(500);

                entity.Property(e => e.FinalDiag2).HasMaxLength(500);

                entity.Property(e => e.FinalDiag3).HasMaxLength(500);

                entity.Property(e => e.Icdcode1)
                    .HasMaxLength(500)
                    .HasColumnName("ICDCode1");

                entity.Property(e => e.Icdcode2)
                    .HasMaxLength(500)
                    .HasColumnName("ICDCode2");

                entity.Property(e => e.Icdcode3)
                    .HasMaxLength(500)
                    .HasColumnName("ICDCode3");

                entity.Property(e => e.IsMlc).HasColumnName("IsMLC");

                entity.Property(e => e.Mlcno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MLCNo")
                    .IsFixedLength();

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_Id");

                entity.Property(e => e.ProvisionalDiag1).HasMaxLength(500);

                entity.Property(e => e.ProvisionalDiag2).HasMaxLength(500);

                entity.Property(e => e.ProvisionalDiag3).HasMaxLength(500);
            });

            modelBuilder.Entity<TDlabRequest>(entity =>
            {
                entity.HasKey(e => e.ReqDetId)
                    .HasName("PK_T_DRequest");

                entity.ToTable("T_DLabRequest");

                entity.Property(e => e.AddedByDate).HasColumnType("datetime");

                entity.Property(e => e.AddedByTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.TDlabRequests)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_T_DLabRequest_T_HLabRequest");
            });

            modelBuilder.Entity<TDoctorPatientHandover>(entity =>
            {
                entity.HasKey(e => e.DocHandId);

                entity.ToTable("T_Doctor_PatientHandover");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PatHandA)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_A");

                entity.Property(e => e.PatHandB)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_B");

                entity.Property(e => e.PatHandI)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_I");

                entity.Property(e => e.PatHandR)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_R");

                entity.Property(e => e.PatHandS)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_S");

                entity.Property(e => e.ShiftInfo).HasMaxLength(50);

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDate");

                entity.Property(e => e.Ttime)
                    .HasColumnType("datetime")
                    .HasColumnName("TTime");
            });

            modelBuilder.Entity<TDoctorShareDetail>(entity =>
            {
                entity.HasKey(e => e.DocDetId);

                entity.ToTable("T_DoctorShareDetails");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");
            });

            modelBuilder.Entity<TDoctorShareGenerationLog>(entity =>
            {
                entity.HasKey(e => e.DocShareId);

                entity.ToTable("T_DoctorShareGenerationLog");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DocGeneratedDate).HasColumnType("datetime");

                entity.Property(e => e.DocGeneratedTime).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            });

            modelBuilder.Entity<TDoctorShareHeader>(entity =>
            {
                entity.HasKey(e => e.DocShareId);

                entity.ToTable("T_DoctorShareHeader");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Gdate)
                    .HasColumnType("datetime")
                    .HasColumnName("GDate");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Gtime)
                    .HasColumnType("datetime")
                    .HasColumnName("GTime");

                entity.Property(e => e.NetPayableAmount).HasColumnType("money");

                entity.Property(e => e.OpIpType).HasColumnName("Op_Ip_Type");

                entity.Property(e => e.PerAmount).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.Tdspercentage).HasColumnName("TDSPercentage");

                entity.Property(e => e.TotalAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TDoctorShareHeaderBack>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_DoctorShareHeaderBack");

                entity.Property(e => e.DocShareId).ValueGeneratedOnAdd();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Gdate)
                    .HasColumnType("datetime")
                    .HasColumnName("GDate");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Gtime)
                    .HasColumnType("datetime")
                    .HasColumnName("GTime");

                entity.Property(e => e.NetPayableAmount).HasColumnType("money");

                entity.Property(e => e.OpIpType).HasColumnName("Op_Ip_Type");

                entity.Property(e => e.PerAmount).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Tdsamount)
                    .HasColumnType("money")
                    .HasColumnName("TDSAmount");

                entity.Property(e => e.Tdspercentage).HasColumnName("TDSPercentage");

                entity.Property(e => e.TotalAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TDoctorsNote>(entity =>
            {
                entity.HasKey(e => e.DoctNoteId);

                entity.ToTable("T_Doctors_Notes");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDate");

                entity.Property(e => e.Ttime)
                    .HasColumnType("datetime")
                    .HasColumnName("TTime");
            });

            modelBuilder.Entity<TDrbill>(entity =>
            {
                entity.HasKey(e => e.Drbno);

                entity.ToTable("T_DRBill");

                entity.Property(e => e.Drbno).HasColumnName("DRBNo");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BalanceAmt).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.BillTime).HasColumnType("datetime");

                entity.Property(e => e.CompanyRefNo).HasMaxLength(50);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetPayableAmt).HasColumnType("money");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PaidAmt).HasColumnType("money");

                entity.Property(e => e.PbillNo)
                    .HasMaxLength(50)
                    .HasColumnName("PBillNo");

                entity.Property(e => e.TaxAmount).HasColumnType("money");

                entity.Property(e => e.TotalAdvanceAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmt).HasColumnType("money");
            });

            modelBuilder.Entity<TDrbillDet>(entity =>
            {
                entity.HasKey(e => e.DrbillDetId)
                    .IsClustered(false);

                entity.ToTable("T_DRBillDet");

                entity.Property(e => e.DrbillDetId).HasColumnName("DRBillDetId");

                entity.Property(e => e.ChargesId).HasColumnName("ChargesID");

                entity.Property(e => e.Drno).HasColumnName("DRNo");
            });

            modelBuilder.Entity<TEmergencyAdm>(entity =>
            {
                entity.HasKey(e => e.EmgId);

                entity.ToTable("T_EmergencyAdm");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.EmgDate).HasColumnType("datetime");

                entity.Property(e => e.EmgTime).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsMlc).HasColumnName("IsMLC");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.MobileNo).HasMaxLength(10);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.SeqNo).HasMaxLength(50);
            });

            modelBuilder.Entity<TEmergencyMedicalHistory>(entity =>
            {
                entity.HasKey(e => e.EmgHistoryId);

                entity.ToTable("T_EmergencyMedicalHistory");

                entity.Property(e => e.Advice).HasMaxLength(500);

                entity.Property(e => e.Bmi)
                    .HasMaxLength(20)
                    .HasColumnName("BMI");

                entity.Property(e => e.Bp)
                    .HasMaxLength(10)
                    .HasColumnName("BP");

                entity.Property(e => e.Bsl)
                    .HasMaxLength(20)
                    .HasColumnName("BSL");

                entity.Property(e => e.ChiefComplaint).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Diagnosis).HasMaxLength(500);

                entity.Property(e => e.Examination).HasMaxLength(500);

                entity.Property(e => e.Height).HasMaxLength(10);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Pulse).HasMaxLength(10);

                entity.Property(e => e.Pweight)
                    .HasMaxLength(20)
                    .HasColumnName("PWeight");

                entity.Property(e => e.SpO2).HasMaxLength(20);

                entity.Property(e => e.Temp).HasMaxLength(10);
            });

            modelBuilder.Entity<TEndoscopyBooking>(entity =>
            {
                entity.HasKey(e => e.OtendoscopyBookingId);

                entity.ToTable("T_EndoscopyBooking");

                entity.Property(e => e.OtendoscopyBookingId).HasColumnName("OTEndoscopyBookingID");

                entity.Property(e => e.AnesthType)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Instruction).HasMaxLength(1000);

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Opdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OPDate");

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.OttableId).HasColumnName("OTTableID");

                entity.Property(e => e.OttypeId).HasColumnName("OTTypeID");

                entity.Property(e => e.Surgeryname).HasMaxLength(100);

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TEndoscopyNote>(entity =>
            {
                entity.HasKey(e => e.Otid);

                entity.ToTable("T_EndoscopyNotes");

                entity.Property(e => e.Otid).HasColumnName("OTId");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishId2).HasColumnName("AnesthetishID2");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryName).HasMaxLength(300);

                entity.Property(e => e.SurgeryType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ToTime).HasColumnType("datetime");

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TExpense>(entity =>
            {
                entity.HasKey(e => e.ExpId);

                entity.ToTable("T_Expense");

                entity.Property(e => e.ExpId).HasColumnName("ExpID");

                entity.Property(e => e.ExpAmount).HasColumnType("money");

                entity.Property(e => e.ExpDate).HasColumnType("datetime");

                entity.Property(e => e.ExpTime).HasColumnType("datetime");

                entity.Property(e => e.Narration).HasMaxLength(200);

                entity.Property(e => e.PersonName).HasMaxLength(100);

                entity.Property(e => e.VoucharNo).HasMaxLength(50);
            });

            modelBuilder.Entity<TFavouriteUserList>(entity =>
            {
                entity.HasKey(e => e.FavouriteId);

                entity.ToTable("T_FavouriteUserList");
            });

            modelBuilder.Entity<TFileLocation>(entity =>
            {
                entity.ToTable("T_FileLocation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");
            });

            modelBuilder.Entity<TGeneralSurgeryOtnote>(entity =>
            {
                entity.HasKey(e => e.OtgenSurId);

                entity.ToTable("T_GeneralSurgeryOTNotes");

                entity.Property(e => e.OtgenSurId).HasColumnName("OTGenSurId");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishId2).HasColumnName("AnesthetishID2");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryName).HasMaxLength(300);

                entity.Property(e => e.SurgeryType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ToTime).HasColumnType("datetime");

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TGrndetail>(entity =>
            {
                entity.HasKey(e => e.GrndetId);

                entity.ToTable("T_GRNDetails");

                entity.Property(e => e.GrndetId).HasColumnName("GRNDetID");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.DiscAmt2).HasColumnType("money");

                entity.Property(e => e.Grnid).HasColumnName("GRNId");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Igstamt)
                    .HasColumnType("money")
                    .HasColumnName("IGSTAmt");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IsVerifiedDatetime).HasColumnType("datetime");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.Mrp)
                    .HasColumnType("money")
                    .HasColumnName("MRP");

                entity.Property(e => e.MrpStrip)
                    .HasColumnType("money")
                    .HasColumnName("MRP_Strip");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.Pono).HasColumnName("PONo");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.ReturnQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StkId).HasColumnName("StkID");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.Uomid).HasColumnName("UOMId");

                entity.Property(e => e.VatAmount).HasColumnType("money");

                entity.HasOne(d => d.Grn)
                    .WithMany(p => p.TGrndetails)
                    .HasForeignKey(d => d.Grnid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_GRNDetails_T_GRNHeader");
            });

            modelBuilder.Entity<TGrnheader>(entity =>
            {
                entity.HasKey(e => e.Grnid);

                entity.ToTable("T_GRNHeader");

                entity.Property(e => e.Grnid).HasColumnName("GRNID");

                entity.Property(e => e.BalAmount).HasColumnType("money");

                entity.Property(e => e.BillDiscAmt).HasColumnType("money");

                entity.Property(e => e.CashCreditType).HasColumnName("Cash_CreditType");

                entity.Property(e => e.CreditNote)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DebitNote)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryNo).HasMaxLength(50);

                entity.Property(e => e.EwayBillDate).HasColumnType("datetime");

                entity.Property(e => e.EwayBillNo).HasMaxLength(50);

                entity.Property(e => e.GateEntryNo).HasMaxLength(50);

                entity.Property(e => e.GrnNumber).HasMaxLength(50);

                entity.Property(e => e.Grndate)
                    .HasColumnType("datetime")
                    .HasColumnName("GRNDate");

                entity.Property(e => e.Grntime)
                    .HasColumnType("datetime")
                    .HasColumnName("GRNTime");

                entity.Property(e => e.Grntype).HasColumnName("GRNType");

                entity.Property(e => e.InvDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNo).HasMaxLength(50);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPaymentProcess).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OtherCharge)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentPrcDate).HasColumnType("datetime");

                entity.Property(e => e.Prefix).HasMaxLength(20);

                entity.Property(e => e.ProcessDes).HasMaxLength(500);

                entity.Property(e => e.ReceivedBy).HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RoundingAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotCgstamt)
                    .HasColumnType("money")
                    .HasColumnName("TotCGSTAmt");

                entity.Property(e => e.TotIgstamt)
                    .HasColumnType("money")
                    .HasColumnName("TotIGSTAmt");

                entity.Property(e => e.TotSgstamt)
                    .HasColumnType("money")
                    .HasColumnName("TotSGSTAmt");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TotalDiscAmount).HasColumnType("money");

                entity.Property(e => e.TotalVatamount)
                    .HasColumnType("money")
                    .HasColumnName("TotalVATAmount");

                entity.Property(e => e.TranProcessMode).HasMaxLength(50);
            });

            modelBuilder.Entity<TGrnreturnDetail>(entity =>
            {
                entity.HasKey(e => e.GrnreturnDetailId);

                entity.ToTable("T_GRNReturnDetail");

                entity.Property(e => e.GrnreturnDetailId).HasColumnName("GRNReturnDetailId");

                entity.Property(e => e.BatchExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(15);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.DiscAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Grnid).HasColumnName("GRNId");

                entity.Property(e => e.GrnreturnId).HasColumnName("GRNReturnId");

                entity.Property(e => e.Gstamount)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("GSTAmount");

                entity.Property(e => e.Gstpercentage).HasColumnName("GSTPercentage");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.LandedRate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.LandedTotalAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Mrp)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("MRP");

                entity.Property(e => e.MrptotalAmount)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("MRPTotalAmount");

                entity.Property(e => e.PurchaseTotalAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.UnitPurchaseRate).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.Grnreturn)
                    .WithMany(p => p.TGrnreturnDetails)
                    .HasForeignKey(d => d.GrnreturnId)
                    .HasConstraintName("FK_T_GRNReturnDetail_T_GRNReturnHeader");
            });

            modelBuilder.Entity<TGrnreturnHeader>(entity =>
            {
                entity.HasKey(e => e.GrnreturnId);

                entity.ToTable("T_GRNReturnHeader");

                entity.Property(e => e.GrnreturnId).HasColumnName("GRNReturnId");

                entity.Property(e => e.CashCredit).HasColumnName("Cash_Credit");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GrnReturnAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GrnType).HasMaxLength(100);

                entity.Property(e => e.Grnid).HasColumnName("GRNID");

                entity.Property(e => e.GrnreturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("GRNReturnDate");

                entity.Property(e => e.GrnreturnNo)
                    .HasMaxLength(20)
                    .HasColumnName("GRNReturnNo");

                entity.Property(e => e.GrnreturnTime)
                    .HasColumnType("datetime")
                    .HasColumnName("GRNReturnTime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Prefix).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalDiscAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalOctroiAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalOtherTaxAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TotalVatAmount).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<TGrnsupPayment>(entity =>
            {
                entity.HasKey(e => e.SupPayId)
                    .HasName("PK_T_RetailerGRNSupPayment");

                entity.ToTable("T_GRNSupPayment");

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmt).HasColumnType("money");

                entity.Property(e => e.CardPayDate).HasColumnType("datetime");

                entity.Property(e => e.CashPayAmt).HasColumnType("money");

                entity.Property(e => e.ChequeBankName).HasMaxLength(50);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ChequePayAmt).HasColumnType("money");

                entity.Property(e => e.ChequePayDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDatetime).HasColumnType("datetime");

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(100)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PartyReceiptNo)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.PayTmdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PayTMDate");

                entity.Property(e => e.PayTmtranNo)
                    .HasMaxLength(20)
                    .HasColumnName("PayTMTranNo");

                entity.Property(e => e.Remarks).HasMaxLength(100);

                entity.Property(e => e.SupPayDate).HasColumnType("datetime");

                entity.Property(e => e.SupPayNo).HasMaxLength(50);

                entity.Property(e => e.SupPayTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TGstadjustment>(entity =>
            {
                entity.HasKey(e => e.GstadgId);

                entity.ToTable("T_GSTAdjustment");

                entity.Property(e => e.GstadgId).HasColumnName("GSTAdgId");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OldCgstper).HasColumnName("OldCGSTPer");

                entity.Property(e => e.OldIgstper).HasColumnName("OldIGSTPer");

                entity.Property(e => e.OldSgstper).HasColumnName("OldSGSTPer");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");
            });

            modelBuilder.Entity<THlabRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK_T_HRequest");

                entity.ToTable("T_HLabRequest");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledTime).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.ReqDate).HasColumnType("datetime");

                entity.Property(e => e.ReqTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<THomeDeliveryOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("T_HomeDeliveryOrder");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNo).HasMaxLength(50);

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.UploadDocument).HasMaxLength(500);
            });

            modelBuilder.Entity<TIndentDetail>(entity =>
            {
                entity.HasKey(e => e.IndentDetailsId);

                entity.ToTable("T_IndentDetails");

                entity.Property(e => e.IsClosed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Indent)
                    .WithMany(p => p.TIndentDetails)
                    .HasForeignKey(d => d.IndentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_IndentDetails_T_IndentHeader");
            });

            modelBuilder.Entity<TIndentHeader>(entity =>
            {
                entity.HasKey(e => e.IndentId)
                    .HasName("PK_t_IndentHeader");

                entity.ToTable("T_IndentHeader");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IndentDate).HasColumnType("datetime");

                entity.Property(e => e.IndentNo).HasMaxLength(50);

                entity.Property(e => e.IndentTime).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsInchargeVerify).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsInchargeVerifyDate).HasColumnType("datetime");

                entity.Property(e => e.IsInchargeVerifyId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<TIpPrescription>(entity =>
            {
                entity.HasKey(e => e.IppreId);

                entity.ToTable("T_IP_Prescription");

                entity.Property(e => e.IppreId).HasColumnName("IPPreId");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IpmedId).HasColumnName("IPMedID");

                entity.Property(e => e.IsClosed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.Pdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PDate");

                entity.Property(e => e.Ptime)
                    .HasColumnType("datetime")
                    .HasColumnName("PTime");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.WardId).HasColumnName("WardID");

                entity.HasOne(d => d.Ipmed)
                    .WithMany(p => p.TIpPrescriptions)
                    .HasForeignKey(d => d.IpmedId)
                    .HasConstraintName("FK_T_IP_Prescription_T_IPMedicalRecord");
            });

            modelBuilder.Entity<TIpPrescriptionDischarge>(entity =>
            {
                entity.HasKey(e => e.PrecriptionId);

                entity.ToTable("T_IP_Prescription_Discharge");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Instruction).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.Ptime)
                    .HasColumnType("datetime")
                    .HasColumnName("PTime");

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<TIpmedicalRecord>(entity =>
            {
                entity.HasKey(e => e.MedicalRecoredId);

                entity.ToTable("T_IPMedicalRecord");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RoundVisitDate).HasColumnType("datetime");

                entity.Property(e => e.RoundVisitTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TIpprescriptionReturnD>(entity =>
            {
                entity.HasKey(e => e.PresDetailsId);

                entity.ToTable("T_IPPrescriptionReturnD");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(20);

                entity.Property(e => e.IsClosed).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.PresRe)
                    .WithMany(p => p.TIpprescriptionReturnDs)
                    .HasForeignKey(d => d.PresReId)
                    .HasConstraintName("FK_T_IPPrescriptionReturnD_T_IPPrescriptionReturnH");
            });

            modelBuilder.Entity<TIpprescriptionReturnH>(entity =>
            {
                entity.HasKey(e => e.PresReId);

                entity.ToTable("T_IPPrescriptionReturnH");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_Id");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PresDate).HasColumnType("datetime");

                entity.Property(e => e.PresNo).HasMaxLength(50);

                entity.Property(e => e.PresTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TIssueToDepartmentDetail>(entity =>
            {
                entity.HasKey(e => e.IssueDepId);

                entity.ToTable("T_IssueToDepartmentDetails");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.LandedTotalAmount).HasColumnType("money");

                entity.Property(e => e.MrptotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("MRPTotalAmount");

                entity.Property(e => e.PerUnitLandedRate).HasColumnType("money");

                entity.Property(e => e.PurTotalAmount).HasColumnType("money");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.UnitPurRate).HasColumnType("money");

                entity.Property(e => e.VatAmount).HasColumnType("money");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.TIssueToDepartmentDetails)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_IssueToDepartmentDetails_T_IssueToDepartmentHeader");
            });

            modelBuilder.Entity<TIssueToDepartmentHeader>(entity =>
            {
                entity.HasKey(e => e.IssueId);

                entity.ToTable("T_IssueToDepartmentHeader");

                entity.Property(e => e.AcceptedDatetime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.IssueTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Receivedby).HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            modelBuilder.Entity<TItemMovementReport>(entity =>
            {
                entity.HasKey(e => e.MovementId);

                entity.ToTable("T_ItemMovementReport");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(100);

                entity.Property(e => e.ConAmount).HasColumnType("money");

                entity.Property(e => e.DisAmount).HasColumnType("money");

                entity.Property(e => e.DocumentNo).HasMaxLength(100);

                entity.Property(e => e.MovementNo).HasMaxLength(100);

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_type");

                entity.Property(e => e.PerUnitLandedPrice).HasColumnType("money");

                entity.Property(e => e.PerUnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("PerUnitMRP");

                entity.Property(e => e.PerUnitPurRate).HasColumnType("money");

                entity.Property(e => e.TaxAmount).HasColumnType("money");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TotalConAmount).HasColumnType("money");

                entity.Property(e => e.TotalDisAmount).HasColumnType("money");

                entity.Property(e => e.TotalLandedAmount).HasColumnType("money");

                entity.Property(e => e.TotalMrp)
                    .HasColumnType("money")
                    .HasColumnName("TotalMRP");

                entity.Property(e => e.TotalPurAmount).HasColumnType("money");

                entity.Property(e => e.TotalTaxAmount).HasColumnType("money");

                entity.Property(e => e.TotalVatAmount).HasColumnType("money");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionTime).HasColumnType("datetime");

                entity.Property(e => e.TransactionType).HasMaxLength(50);

                entity.Property(e => e.VatAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TLoginAccessDetail>(entity =>
            {
                entity.HasKey(e => e.LoginAccessId);

                entity.ToTable("T_LoginAccessDetails");

                entity.Property(e => e.AccessInputValue).HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.TLoginAccessDetails)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK_T_LoginAccessDetails_LoginManager");
            });

            modelBuilder.Entity<TLoginStoreDetail>(entity =>
            {
                entity.HasKey(e => e.LoginStoreDetId);

                entity.ToTable("T_LoginStoreDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.TLoginStoreDetails)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK_T_LoginStoreDetails_LoginManager");
            });

            modelBuilder.Entity<TLoginUnitDetail>(entity =>
            {
                entity.HasKey(e => e.LoginUnitDetId);

                entity.ToTable("T_LoginUnitDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.TLoginUnitDetails)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK_T_LoginUnitDetails_LoginManager");
            });

            modelBuilder.Entity<TMailOutGoing1>(entity =>
            {
                entity.HasKey(e => e.MailId);

                entity.ToTable("T_MailOutGoing");

                entity.Property(e => e.Attachment).HasMaxLength(500);

                entity.Property(e => e.MailDate).HasColumnType("datetime");

                entity.Property(e => e.MailDescription).HasColumnType("text");

                entity.Property(e => e.MailFrom).HasMaxLength(500);

                entity.Property(e => e.MailTime).HasColumnType("datetime");

                entity.Property(e => e.MailTo).HasMaxLength(500);

                entity.Property(e => e.Subject).HasMaxLength(500);
            });

            modelBuilder.Entity<TMailOutgoing>(entity =>
            {
                entity.ToTable("T_Mail_Outgoing");

                entity.Property(e => e.AttachmentLink).HasMaxLength(1000);

                entity.Property(e => e.AttachmentName).HasMaxLength(250);

                entity.Property(e => e.Bcc)
                    .HasMaxLength(250)
                    .HasColumnName("BCC");

                entity.Property(e => e.Cc)
                    .HasMaxLength(250)
                    .HasColumnName("CC");

                entity.Property(e => e.EmailDate).HasColumnType("datetime");

                entity.Property(e => e.EmailType).HasMaxLength(250);

                entity.Property(e => e.FromEmail).HasMaxLength(250);

                entity.Property(e => e.FromName).HasMaxLength(250);

                entity.Property(e => e.LastResponse).HasMaxLength(200);

                entity.Property(e => e.LastTry).HasColumnType("datetime");

                entity.Property(e => e.MailBody).HasMaxLength(4000);

                entity.Property(e => e.MailSubject).HasMaxLength(250);

                entity.Property(e => e.ToEmail).HasMaxLength(250);
            });

            modelBuilder.Entity<TMaterialConsumptionDetail>(entity =>
            {
                entity.HasKey(e => e.MaterialConDetId);

                entity.ToTable("T_MaterialConsumptionDetails");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LandedTotalAmount).HasColumnType("money");

                entity.Property(e => e.MrptotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("MRPTotalAmount");

                entity.Property(e => e.PerUnitLandedRate).HasColumnType("money");

                entity.Property(e => e.PerUnitMrprate)
                    .HasColumnType("money")
                    .HasColumnName("PerUnitMRPRate");

                entity.Property(e => e.PerUnitPurchaseRate).HasColumnType("money");

                entity.Property(e => e.PurTotalAmount).HasColumnType("money");

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.MaterialConsumption)
                    .WithMany(p => p.TMaterialConsumptionDetails)
                    .HasForeignKey(d => d.MaterialConsumptionId)
                    .HasConstraintName("FK_T_MaterialConsumptionDetails_T_MaterialConsumptionHeader");
            });

            modelBuilder.Entity<TMaterialConsumptionHeader>(entity =>
            {
                entity.HasKey(e => e.MaterialConsumptionId);

                entity.ToTable("T_MaterialConsumptionHeader");

                entity.Property(e => e.ConsumptionDate).HasColumnType("datetime");

                entity.Property(e => e.ConsumptionNo).HasMaxLength(50);

                entity.Property(e => e.ConsumptionTime).HasColumnType("datetime");

                entity.Property(e => e.LandedTotalAmount).HasColumnType("money");

                entity.Property(e => e.MrptotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("MRPTotalAmount");

                entity.Property(e => e.PurTotalAmount).HasColumnType("money");

                entity.Property(e => e.Remark).HasMaxLength(100);
            });

            modelBuilder.Entity<TMedicolegalCertificate>(entity =>
            {
                entity.HasKey(e => e.DocId);

                entity.ToTable("T_MedicolegalCertificate");

                entity.Property(e => e.AccidentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accident_Date");

                entity.Property(e => e.AccidentTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Accident_Time");

                entity.Property(e => e.AgeofInjuries).HasMaxLength(1000);

                entity.Property(e => e.CauseofInjuries).HasMaxLength(1000);

                entity.Property(e => e.CertificateNo).HasMaxLength(50);

                entity.Property(e => e.DetailsInjuries)
                    .HasColumnType("text")
                    .HasColumnName("Details_Injuries");

                entity.Property(e => e.Mlcdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MLCDate");

                entity.Property(e => e.Mlctime)
                    .HasColumnType("datetime")
                    .HasColumnName("MLCTime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_Id");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");
            });

            modelBuilder.Entity<TMlcinformation>(entity =>
            {
                entity.HasKey(e => e.Mlcid);

                entity.ToTable("T_MLCInformation");

                entity.Property(e => e.Mlcid).HasColumnName("MLCId");

                entity.Property(e => e.AuthorityName).HasMaxLength(100);

                entity.Property(e => e.BuckleNo).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DetailGiven).HasMaxLength(2000);

                entity.Property(e => e.Mlcno)
                    .HasMaxLength(50)
                    .HasColumnName("MLCNo");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PoliceStation).HasMaxLength(255);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.ReportingDate).HasColumnType("datetime");

                entity.Property(e => e.ReportingTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TMrdAdmFile>(entity =>
            {
                entity.ToTable("T_MRD_AdmFile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.FilePathLocation).HasMaxLength(500);

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");
            });

            modelBuilder.Entity<TMrdcasePaperIssueReturn>(entity =>
            {
                entity.HasKey(e => e.MrdissueId);

                entity.ToTable("T_MRDCasePaperIssueReturn");

                entity.Property(e => e.MrdissueId).HasColumnName("MRDIssueID");

                entity.Property(e => e.CasePaperIssueName).HasMaxLength(300);

                entity.Property(e => e.IsStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Issuetime).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Reason).HasMaxLength(300);

                entity.Property(e => e.ReturnByPersonName).HasMaxLength(300);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TMrpAdjustment>(entity =>
            {
                entity.HasKey(e => e.MrpAdjId);

                entity.ToTable("T_MrpAdjustment");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.Mrp).HasColumnType("money");

                entity.Property(e => e.OldLandedRate).HasColumnType("money");

                entity.Property(e => e.OldMrp).HasColumnType("money");

                entity.Property(e => e.OldPurRate).HasColumnType("money");

                entity.Property(e => e.PurRate).HasColumnType("money");
            });

            modelBuilder.Entity<TMrpstockAdjestment>(entity =>
            {
                entity.HasKey(e => e.StockAdgId);

                entity.ToTable("T_MRPStockAdjestment");

                entity.Property(e => e.AdDdQty).HasColumnName("Ad_DD_Qty");

                entity.Property(e => e.AdDdType).HasColumnName("Ad_DD_Type");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Mrpadg)
                    .HasColumnType("money")
                    .HasColumnName("MRPAdg");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TNeroSurgeryOtnote>(entity =>
            {
                entity.HasKey(e => e.OtneroId);

                entity.ToTable("T_NeroSurgeryOTNotes");

                entity.Property(e => e.OtneroId).HasColumnName("OTNeroId");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AdviceonDischarge).HasColumnType("text");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishId2).HasColumnName("AnesthetishID2");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BloodLoss).HasMaxLength(500);

                entity.Property(e => e.Findings).HasColumnType("text");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.Position).HasMaxLength(500);

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeonId2).HasColumnName("SurgeonID2");

                entity.Property(e => e.Surgery).HasColumnType("text");

                entity.Property(e => e.SurgeryName).HasMaxLength(300);

                entity.Property(e => e.SurgeryType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TNurNote>(entity =>
            {
                entity.HasKey(e => e.DocNoteId);

                entity.ToTable("T_Nur_Notes");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDate");

                entity.Property(e => e.Ttime)
                    .HasColumnType("datetime")
                    .HasColumnName("TTime");
            });

            modelBuilder.Entity<TNurPatientHandover>(entity =>
            {
                entity.HasKey(e => e.PatHandId);

                entity.ToTable("T_Nur_PatientHandover");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.PatHandA)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_A");

                entity.Property(e => e.PatHandB)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_B");

                entity.Property(e => e.PatHandI)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_I");

                entity.Property(e => e.PatHandR)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_R");

                entity.Property(e => e.PatHandS)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_S");

                entity.Property(e => e.ShiftInfo).HasMaxLength(50);

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDate");

                entity.Property(e => e.Ttime)
                    .HasColumnType("datetime")
                    .HasColumnName("TTime");
            });

            modelBuilder.Entity<TNursingCaseSheet>(entity =>
            {
                entity.HasKey(e => e.NursingCaseSheetId);

                entity.ToTable("T_NursingCaseSheet");

                entity.Property(e => e.ArrangeBlood)
                    .HasMaxLength(100)
                    .HasColumnName("Arrange Blood");

                entity.Property(e => e.ArrangeBlood1)
                    .HasMaxLength(100)
                    .HasColumnName("Arrange_Blood");

                entity.Property(e => e.Bp)
                    .HasMaxLength(50)
                    .HasColumnName("BP");

                entity.Property(e => e.Diagnosis).HasMaxLength(500);

                entity.Property(e => e.DrugsReceived)
                    .HasMaxLength(500)
                    .HasColumnName("Drugs_Received");

                entity.Property(e => e.Ecg)
                    .HasMaxLength(100)
                    .HasColumnName("ECG");

                entity.Property(e => e.FitnessOf)
                    .HasMaxLength(100)
                    .HasColumnName("Fitness of");

                entity.Property(e => e.FitnessOf1)
                    .HasMaxLength(100)
                    .HasColumnName("Fitness_of");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.Mtime)
                    .HasColumnType("datetime")
                    .HasColumnName("MTime");

                entity.Property(e => e.NurseName).HasMaxLength(100);

                entity.Property(e => e.Operation).HasMaxLength(500);

                entity.Property(e => e.Pulse).HasMaxLength(50);

                entity.Property(e => e.ReferToDr)
                    .HasMaxLength(500)
                    .HasColumnName("Refer_to_Dr");
            });

            modelBuilder.Entity<TNursingChart>(entity =>
            {
                entity.HasKey(e => e.NursingChartId);

                entity.ToTable("T_NursingChart");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.Mtime)
                    .HasColumnType("datetime")
                    .HasColumnName("MTime");

                entity.Property(e => e.NurseName).HasMaxLength(100);

                entity.Property(e => e.NursingBp)
                    .HasMaxLength(50)
                    .HasColumnName("Nursing_BP");

                entity.Property(e => e.NursingDo)
                    .HasMaxLength(100)
                    .HasColumnName("Nursing_DO");

                entity.Property(e => e.NursingIvo)
                    .HasMaxLength(100)
                    .HasColumnName("Nursing_IVO");

                entity.Property(e => e.NursingP)
                    .HasMaxLength(50)
                    .HasColumnName("Nursing_P");

                entity.Property(e => e.NursingR)
                    .HasMaxLength(50)
                    .HasColumnName("Nursing_R");

                entity.Property(e => e.NursingT)
                    .HasMaxLength(50)
                    .HasColumnName("Nursing_T");

                entity.Property(e => e.NursingTotalInput)
                    .HasMaxLength(100)
                    .HasColumnName("Nursing_TotalInput");

                entity.Property(e => e.NursingTotalOutput)
                    .HasMaxLength(100)
                    .HasColumnName("Nursing_TotalOutput");

                entity.Property(e => e.NursingUo)
                    .HasMaxLength(100)
                    .HasColumnName("Nursing_UO");
            });

            modelBuilder.Entity<TNursingFluidChart>(entity =>
            {
                entity.HasKey(e => e.FluidChartId);

                entity.ToTable("T_NursingFluidChart");

                entity.Property(e => e.FendTime)
                    .HasColumnType("datetime")
                    .HasColumnName("FEndTime");

                entity.Property(e => e.FstartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("FStartTime");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.Mtime)
                    .HasColumnType("datetime")
                    .HasColumnName("MTime");

                entity.Property(e => e.NurseName).HasMaxLength(50);
            });

            modelBuilder.Entity<TNursingMedicationChart>(entity =>
            {
                entity.HasKey(e => e.MedChartId);

                entity.ToTable("T_Nursing_MedicationChart");

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.DoseId).HasColumnName("DoseID");

                entity.Property(e => e.DoseName).HasMaxLength(100);

                entity.Property(e => e.Freq).HasMaxLength(50);

                entity.Property(e => e.IsCancelledDateTime).HasColumnType("datetime");

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Mtime)
                    .HasColumnType("datetime")
                    .HasColumnName("MTime");

                entity.Property(e => e.NurseName).HasMaxLength(50);

                entity.Property(e => e.Route).HasMaxLength(50);
            });

            modelBuilder.Entity<TNursingMedicationChart1>(entity =>
            {
                entity.HasKey(e => e.MedChartId);

                entity.ToTable("T_NursingMedicationChart");

                entity.Property(e => e.DoseId).HasColumnName("DoseID");

                entity.Property(e => e.DoseName).HasMaxLength(100);

                entity.Property(e => e.Freq).HasMaxLength(50);

                entity.Property(e => e.Mdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MDate");

                entity.Property(e => e.Mtime)
                    .HasColumnType("datetime")
                    .HasColumnName("MTime");

                entity.Property(e => e.NurseName).HasMaxLength(50);

                entity.Property(e => e.Route).HasMaxLength(50);
            });

            modelBuilder.Entity<TNursingNote>(entity =>
            {
                entity.HasKey(e => e.DocNoteId);

                entity.ToTable("T_Nursing_Notes");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.NursingNotes).HasMaxLength(2000);

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDate");

                entity.Property(e => e.Ttime)
                    .HasColumnType("datetime")
                    .HasColumnName("TTime");
            });

            modelBuilder.Entity<TNursingOrygenVentilator>(entity =>
            {
                entity.ToTable("T_NursingOrygenVentilator");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.EntryTime).HasColumnType("datetime");

                entity.Property(e => e.Epap)
                    .HasMaxLength(10)
                    .HasColumnName("EPAP");

                entity.Property(e => e.Fio2)
                    .HasMaxLength(10)
                    .HasColumnName("FIO2");

                entity.Property(e => e.FlowTrigger).HasMaxLength(10);

                entity.Property(e => e.Ie)
                    .HasMaxLength(10)
                    .HasColumnName("IE");

                entity.Property(e => e.Ipap)
                    .HasMaxLength(10)
                    .HasColumnName("IPAP");

                entity.Property(e => e.MinuteV).HasMaxLength(10);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Mvpercentage)
                    .HasMaxLength(10)
                    .HasColumnName("MVPercentage");

                entity.Property(e => e.OxygenRate).HasMaxLength(10);

                entity.Property(e => e.Pc)
                    .HasMaxLength(10)
                    .HasColumnName("PC");

                entity.Property(e => e.Peep).HasMaxLength(10);

                entity.Property(e => e.PrSup).HasMaxLength(10);

                entity.Property(e => e.RateTotal).HasMaxLength(10);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.SaturationWithO2).HasMaxLength(10);

                entity.Property(e => e.SetRange).HasMaxLength(10);

                entity.Property(e => e.TidolV).HasMaxLength(10);
            });

            modelBuilder.Entity<TNursingPainAssessment>(entity =>
            {
                entity.HasKey(e => e.PainAssessmentId);

                entity.ToTable("T_NursingPainAssessment");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PainAssessmentDate).HasColumnType("datetime");

                entity.Property(e => e.PainAssessmentTime).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(50);
            });

            modelBuilder.Entity<TNursingPatientHandover>(entity =>
            {
                entity.HasKey(e => e.PatHandId);

                entity.ToTable("T_Nursing_PatientHandover");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.PatHandA)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_A");

                entity.Property(e => e.PatHandB)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_B");

                entity.Property(e => e.PatHandI)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_I");

                entity.Property(e => e.PatHandR)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_R");

                entity.Property(e => e.PatHandS)
                    .HasMaxLength(500)
                    .HasColumnName("PatHand_S");

                entity.Property(e => e.ShiftInfo).HasMaxLength(50);

                entity.Property(e => e.Tdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDate");

                entity.Property(e => e.Ttime)
                    .HasColumnType("datetime")
                    .HasColumnName("TTime");
            });

            modelBuilder.Entity<TNursingSugarLevel>(entity =>
            {
                entity.ToTable("T_NursingSugarLevel");

                entity.Property(e => e.Bodies).HasMaxLength(10);

                entity.Property(e => e.Bsl)
                    .HasMaxLength(10)
                    .HasColumnName("BSL");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.EntryTime).HasColumnType("datetime");

                entity.Property(e => e.Ettpressure)
                    .HasMaxLength(10)
                    .HasColumnName("ETTPressure");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.ReportedToRmo)
                    .HasMaxLength(10)
                    .HasColumnName("ReportedToRMO");

                entity.Property(e => e.UrineKetone).HasMaxLength(10);

                entity.Property(e => e.UrineSugar).HasMaxLength(10);
            });

            modelBuilder.Entity<TNursingVital>(entity =>
            {
                entity.HasKey(e => e.VitalId);

                entity.ToTable("T_NursingVitals");

                entity.Property(e => e.AbdominalGrith).HasMaxLength(10);

                entity.Property(e => e.Apnea).HasMaxLength(10);

                entity.Property(e => e.ArterialBloodPressure).HasMaxLength(10);

                entity.Property(e => e.BloodPresure).HasMaxLength(10);

                entity.Property(e => e.Brady).HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Cvp)
                    .HasMaxLength(10)
                    .HasColumnName("CVP");

                entity.Property(e => e.Desaturation).HasMaxLength(10);

                entity.Property(e => e.Fio2)
                    .HasMaxLength(10)
                    .HasColumnName("FIO2");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PapressureReading)
                    .HasMaxLength(10)
                    .HasColumnName("PAPressureReading");

                entity.Property(e => e.Peep).HasMaxLength(10);

                entity.Property(e => e.Pfration)
                    .HasMaxLength(10)
                    .HasColumnName("PFRation");

                entity.Property(e => e.Po2)
                    .HasMaxLength(10)
                    .HasColumnName("PO2");

                entity.Property(e => e.Pulse).HasMaxLength(10);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.Respiration).HasMaxLength(10);

                entity.Property(e => e.SaturationWithO2).HasMaxLength(10);

                entity.Property(e => e.SaturationWithoutO2).HasMaxLength(10);

                entity.Property(e => e.Temperature).HasMaxLength(10);

                entity.Property(e => e.VitalDate).HasColumnType("datetime");

                entity.Property(e => e.VitalTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TNursingWeight>(entity =>
            {
                entity.HasKey(e => e.PatWeightId);

                entity.ToTable("T_NursingWeight");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PatWeightDate).HasColumnType("datetime");

                entity.Property(e => e.PatWeightTime).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(50);
            });

            modelBuilder.Entity<TOpeningTransactionDetail>(entity =>
            {
                entity.HasKey(e => e.OpeningId)
                    .HasName("PK_T_OpeningTransaction");

                entity.ToTable("T_OpeningTransactionDetails");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Gstper).HasColumnName("GSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.OpeningDate).HasColumnType("datetime");

                entity.Property(e => e.OpeningTime).HasColumnType("datetime");

                entity.Property(e => e.PerUnitLandedRate).HasColumnType("money");

                entity.Property(e => e.PerUnitMrp).HasColumnType("money");

                entity.Property(e => e.PerUnitPurRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");
            });

            modelBuilder.Entity<TOpeningTransactionHeader>(entity =>
            {
                entity.HasKey(e => e.OpeningHid)
                    .HasName("PK_T_OpeningTransaction_1");

                entity.ToTable("T_OpeningTransactionHeader");

                entity.Property(e => e.OpeningHid).HasColumnName("OpeningHId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpeningDate).HasColumnType("datetime");

                entity.Property(e => e.OpeningDocNo).HasMaxLength(50);

                entity.Property(e => e.OpeningTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TOpinvAdviceList>(entity =>
            {
                entity.HasKey(e => e.InvestigationAdvId);

                entity.ToTable("T_OPInvAdviceList");

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.VisitId).HasColumnName("VisitID");
            });

            modelBuilder.Entity<TOprequestList>(entity =>
            {
                entity.HasKey(e => e.RequestTranId);

                entity.ToTable("T_OPRequestList");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");
            });

            modelBuilder.Entity<TOtReservation>(entity =>
            {
                entity.HasKey(e => e.OtreservationId);

                entity.ToTable("T_OT_Reservation");

                entity.Property(e => e.OtreservationId).HasColumnName("OTReservationId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Instruction).HasMaxLength(1000);

                entity.Property(e => e.IsCancelledDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Opdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OPDate");

                entity.Property(e => e.OpendTime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPEndTime");

                entity.Property(e => e.OpstartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPStartTime");

                entity.Property(e => e.OtrequestId).HasColumnName("OTRequestId");

                entity.Property(e => e.OttableId).HasColumnName("OTTableID");

                entity.Property(e => e.OttypeId).HasColumnName("OTTypeID");

                entity.Property(e => e.Reason).HasMaxLength(200);

                entity.Property(e => e.ReservationDate).HasColumnType("datetime");

                entity.Property(e => e.ReservationTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TOtbookingRequest>(entity =>
            {
                entity.HasKey(e => e.OtbookingId);

                entity.ToTable("T_OTBooking_Request");

                entity.Property(e => e.OtbookingId).HasColumnName("OTBookingId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_Id");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.OtbookingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTbookingDate");

                entity.Property(e => e.OtbookingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTbookingTime");

                entity.Property(e => e.OtrequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTRequestDate");

                entity.Property(e => e.OtrequestTime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTRequestTime");

                entity.Property(e => e.Reason).HasMaxLength(50);
            });

            modelBuilder.Entity<TOtcathLabBooking>(entity =>
            {
                entity.HasKey(e => e.OtcathLabBokingId);

                entity.ToTable("T_OTCathLabBooking");

                entity.Property(e => e.OtcathLabBokingId).HasColumnName("OTCathLabBokingID");

                entity.Property(e => e.AnesthType)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Instruction).HasMaxLength(1000);

                entity.Property(e => e.IsNormalOrFuture).HasColumnName("isNormalOrFuture");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.Opdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OPDate");

                entity.Property(e => e.Optime)
                    .HasColumnType("datetime")
                    .HasColumnName("OPTime");

                entity.Property(e => e.OttableId).HasColumnName("OTTableID");

                entity.Property(e => e.OttypeId).HasColumnName("OTTypeID");

                entity.Property(e => e.PatientName).HasMaxLength(200);

                entity.Property(e => e.Surgeryname).HasMaxLength(100);

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPatIcdcdeD>(entity =>
            {
                entity.HasKey(e => e.Did);

                entity.ToTable("T_PatICDCdeD");

                entity.Property(e => e.Did).HasColumnName("DId");

                entity.Property(e => e.Hid).HasColumnName("HId");

                entity.Property(e => e.IcdCode).HasMaxLength(100);

                entity.Property(e => e.IcdCodeDesc).HasMaxLength(1000);

                entity.Property(e => e.IcdcdeMainName)
                    .HasMaxLength(100)
                    .HasColumnName("ICDCdeMainName");

                entity.Property(e => e.MainIcdcdeId).HasColumnName("MainICDCdeId");
            });

            modelBuilder.Entity<TPatIcdcdeH>(entity =>
            {
                entity.HasKey(e => e.Hid);

                entity.ToTable("T_PatICDCdeH");

                entity.Property(e => e.Hid).HasColumnName("HId");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.ReqDate).HasColumnType("datetime");

                entity.Property(e => e.ReqTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPathologyReportDetail>(entity =>
            {
                entity.HasKey(e => e.PathReportDetId)
                    .HasName("PK_T_PathologyReportDetails_H");

                entity.ToTable("T_PathologyReportDetails");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(150);

                entity.Property(e => e.NormalRange).HasMaxLength(50);

                entity.Property(e => e.ParaBoldFlag).HasMaxLength(1);

                entity.Property(e => e.ParameterName).HasMaxLength(150);

                entity.Property(e => e.PatientName).HasMaxLength(150);

                entity.Property(e => e.PisNumeric).HasColumnName("PIsNumeric");

                entity.Property(e => e.RegNo).HasMaxLength(50);

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.SampleId)
                    .HasMaxLength(20)
                    .HasColumnName("SampleID");

                entity.Property(e => e.SubTestName).HasMaxLength(150);

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestName).HasMaxLength(150);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<TPathologyReportDetailsArch>(entity =>
            {
                entity.HasKey(e => e.PathReportDetId)
                    .HasName("PK_T_PathologyReportDetails");

                entity.ToTable("T_PathologyReportDetails_ARCH");

                entity.HasIndex(e => e.PathReportId, "_dta_index_T_PathologyReportDetails_11_1254295528__K2")
                    .HasFillFactor(90);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(150);

                entity.Property(e => e.NormalRange).HasMaxLength(50);

                entity.Property(e => e.ParameterName).HasMaxLength(150);

                entity.Property(e => e.PatientName).HasMaxLength(150);

                entity.Property(e => e.PisNumeric).HasColumnName("PIsNumeric");

                entity.Property(e => e.RegNo).HasMaxLength(50);

                entity.Property(e => e.ResultValue).HasMaxLength(500);

                entity.Property(e => e.SampleId)
                    .HasMaxLength(20)
                    .HasColumnName("SampleID");

                entity.Property(e => e.SubTestName).HasMaxLength(150);

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestName).HasMaxLength(150);

                entity.Property(e => e.UnitName).HasMaxLength(50);
            });

            modelBuilder.Entity<TPathologyReportHeader>(entity =>
            {
                entity.HasKey(e => e.PathReportId)
                    .HasName("PK_PathologyReportHeader");

                entity.ToTable("T_PathologyReportHeader");

                entity.Property(e => e.PathReportId).HasColumnName("PathReportID");

                entity.Property(e => e.AdmVisitDoctorId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsVerifySign).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsVerifyedDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.PathDate).HasColumnType("datetime");

                entity.Property(e => e.PathTestId).HasColumnName("PathTestID");

                entity.Property(e => e.PathTime).HasColumnType("datetime");

                entity.Property(e => e.RefDoctorId)
                    .HasColumnName("RefDoctorID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.ReportTime).HasColumnType("datetime");

                entity.Property(e => e.SampleCollectionTime).HasColumnType("datetime");

                entity.Property(e => e.SampleNo).HasMaxLength(50);

                entity.Property(e => e.SuggestionNotes).HasMaxLength(500);
            });

            modelBuilder.Entity<TPathologyReportTemplateDetail>(entity =>
            {
                entity.HasKey(e => e.PathReportTemplateDetId);

                entity.ToTable("T_PathologyReportTemplateDetails");

                entity.HasIndex(e => new { e.PathReportId, e.TestId }, "_dta_index_T_PathologyReportTemplateDetails_11_86291367__K2_K5_3")
                    .HasFillFactor(90);

                entity.Property(e => e.PathTemplateDetailsResult).HasColumnType("text");

                entity.Property(e => e.SuggestionNotes).HasMaxLength(500);

                entity.Property(e => e.TemplateResultInHtml).HasColumnName("TemplateResultInHTML");
            });

            modelBuilder.Entity<TPatientDetail>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("T_PatientDetails");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Admin')");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Admin')");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PatientName).HasMaxLength(250);
            });

            modelBuilder.Entity<TPatientFeedback>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_PatientFeedback");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FeedbackComments).HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PatientFeedbackId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TPaymentCanteen>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("T_PaymentCanteen");

                entity.Property(e => e.AdvanceUsedAmount).HasColumnType("money");

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.CardBankName).HasMaxLength(100);

                entity.Property(e => e.CardDate).HasColumnType("datetime");

                entity.Property(e => e.CardNo).HasMaxLength(50);

                entity.Property(e => e.CardPayAmount).HasColumnType("money");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.ChequePayAmount).HasColumnType("money");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsSelfOrcompany).HasColumnName("IsSelfORCompany");

                entity.Property(e => e.NeftbankMaster)
                    .HasMaxLength(100)
                    .HasColumnName("NEFTBankMaster");

                entity.Property(e => e.Neftdate)
                    .HasColumnType("datetime")
                    .HasColumnName("NEFTDate");

                entity.Property(e => e.Neftno)
                    .HasMaxLength(20)
                    .HasColumnName("NEFTNo");

                entity.Property(e => e.NeftpayAmount)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPayAmount");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.PayTmdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PayTMDate");

                entity.Property(e => e.PayTmtranNo)
                    .HasMaxLength(20)
                    .HasColumnName("PayTMTranNo");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<TPhColHadOvToAcc>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.ToTable("T_PhCol_HadOv_ToAcc");

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PayDate).HasColumnType("datetime");

                entity.Property(e => e.PersonName).HasMaxLength(100);

                entity.Property(e => e.PhAmount)
                    .HasColumnType("money")
                    .HasColumnName("Ph_Amount");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPhRefund>(entity =>
            {
                entity.HasKey(e => e.RefundId)
                    .HasName("PK_T_PhRefundOfBill");

                entity.ToTable("T_PhRefund");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundNo).HasMaxLength(20);

                entity.Property(e => e.RefundTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<TPhSm>(entity =>
            {
                entity.HasKey(e => e.PhSmId);

                entity.ToTable("T_PhSM");

                entity.Property(e => e.CharityAmt).HasColumnType("money");

                entity.Property(e => e.IsAddedDate).HasColumnType("datetime");

                entity.Property(e => e.IsUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.MedColAmt).HasColumnType("money");

                entity.Property(e => e.MedNetSales).HasColumnType("money");

                entity.Property(e => e.PhColAmt).HasColumnType("money");

                entity.Property(e => e.PhNetSales).HasColumnType("money");

                entity.Property(e => e.PhSmDate).HasColumnType("datetime");

                entity.Property(e => e.PhSmTime).HasColumnType("datetime");

                entity.Property(e => e.RgAmt).HasColumnType("money");

                entity.Property(e => e.VajpayAmt).HasColumnType("money");
            });

            modelBuilder.Entity<TPhadvRefundDetail>(entity =>
            {
                entity.HasKey(e => e.AdvRefId);

                entity.ToTable("T_PHAdvRefundDetail");

                entity.Property(e => e.RefundDate).HasColumnType("datetime");

                entity.Property(e => e.RefundTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPhadvanceDetail>(entity =>
            {
                entity.HasKey(e => e.AdvanceDetailId);

                entity.ToTable("T_PHAdvanceDetail");

                entity.Property(e => e.AdvanceDetailId).HasColumnName("AdvanceDetailID");

                entity.Property(e => e.AdvanceAmount).HasColumnType("money");

                entity.Property(e => e.AdvanceNo).HasMaxLength(50);

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.Reason).HasMaxLength(200);

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.UsedAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TPhadvanceHeader>(entity =>
            {
                entity.HasKey(e => e.AdvanceId);

                entity.ToTable("T_PHAdvanceHeader");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_Id");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");
            });

            modelBuilder.Entity<TPharPatientInformation>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("T_PharPatientInformation");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.DoctorName).HasMaxLength(500);

                entity.Property(e => e.PatientAddress).HasMaxLength(500);

                entity.Property(e => e.PatientName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDatetime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPhoneAppointment>(entity =>
            {
                entity.HasKey(e => e.PhoneAppId);

                entity.ToTable("T_PhoneAppointment");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AppDate).HasColumnType("datetime");

                entity.Property(e => e.AppTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhAppDate).HasColumnType("datetime");

                entity.Property(e => e.PhAppTime).HasColumnType("datetime");

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.SeqNo).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPrePostOperativeNote>(entity =>
            {
                entity.HasKey(e => e.Otid);

                entity.ToTable("T_PrePostOperativeNotes");

                entity.Property(e => e.Otid).HasColumnName("OTId");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AnesthetishId).HasColumnName("AnesthetishID");

                entity.Property(e => e.AnesthetishId1).HasColumnName("AnesthetishID1");

                entity.Property(e => e.AnesthetishId2).HasColumnName("AnesthetishID2");

                entity.Property(e => e.AnesthetishType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Assistant).HasMaxLength(500);

                entity.Property(e => e.ClosureTechnique).HasColumnType("text");

                entity.Property(e => e.DetSpecimenForLab).HasColumnType("text");

                entity.Property(e => e.ExtraProPerformed).HasColumnType("text");

                entity.Property(e => e.FromTime).HasColumnType("datetime");

                entity.Property(e => e.Incision).HasMaxLength(1000);

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.OperativeDiagnosis).HasColumnType("text");

                entity.Property(e => e.OperativeFindings).HasColumnType("text");

                entity.Property(e => e.OperativeProcedure).HasColumnType("text");

                entity.Property(e => e.Otdate)
                    .HasColumnType("datetime")
                    .HasColumnName("OTDate");

                entity.Property(e => e.Ottime)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTime");

                entity.Property(e => e.PostOpertiveInstru).HasColumnType("text");

                entity.Property(e => e.SurgeonId).HasColumnName("SurgeonID");

                entity.Property(e => e.SurgeryName).HasMaxLength(300);

                entity.Property(e => e.SurgeryType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ToTime).HasColumnType("datetime");

                entity.Property(e => e.TranDate).HasColumnType("datetime");

                entity.Property(e => e.TranTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPrescription>(entity =>
            {
                entity.HasKey(e => e.PrecriptionId);

                entity.ToTable("T_Prescription");

                entity.Property(e => e.Advice).HasMaxLength(500);

                entity.Property(e => e.Allergy).HasMaxLength(50);

                entity.Property(e => e.BloodGroup).HasMaxLength(50);

                entity.Property(e => e.Bmi)
                    .HasMaxLength(20)
                    .HasColumnName("BMI");

                entity.Property(e => e.Bp)
                    .HasMaxLength(10)
                    .HasColumnName("BP");

                entity.Property(e => e.Bsl)
                    .HasMaxLength(20)
                    .HasColumnName("BSL");

                entity.Property(e => e.ChiefComplaint).HasMaxLength(500);

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Diagnosis).HasMaxLength(500);

                entity.Property(e => e.Examination).HasMaxLength(500);

                entity.Property(e => e.Height).HasMaxLength(10);

                entity.Property(e => e.Instruction).HasMaxLength(200);

                entity.Property(e => e.IsClosed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdIp).HasColumnName("OPD_IPD_IP");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.Ptime)
                    .HasColumnType("datetime")
                    .HasColumnName("PTime");

                entity.Property(e => e.Pulse).HasMaxLength(10);

                entity.Property(e => e.Pweight)
                    .HasMaxLength(20)
                    .HasColumnName("PWeight");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.SpO2).HasMaxLength(20);

                entity.Property(e => e.Temp).HasMaxLength(10);
            });

            modelBuilder.Entity<TPurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.PurDetId)
                    .HasName("PK_M_Purchase Detail");

                entity.ToTable("T_PurchaseDetail");

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.DefRate).HasColumnType("money");

                entity.Property(e => e.Igstamt)
                    .HasColumnType("money")
                    .HasColumnName("IGSTAmt");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IsClosed).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsGrnQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Mrp).HasColumnName("MRP");

                entity.Property(e => e.PobalQty)
                    .HasColumnName("POBalQty")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.Specification).HasMaxLength(200);

                entity.Property(e => e.Uomid).HasColumnName("UOMID");

                entity.Property(e => e.VendDiscAmt).HasColumnType("money");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.TPurchaseDetails)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_PurchaseDetail_T_PurchaseHeader");
            });

            modelBuilder.Entity<TPurchaseHeader>(entity =>
            {
                entity.HasKey(e => e.PurchaseId)
                    .HasName("PK_m_PurchaseOrder");

                entity.ToTable("T_PurchaseHeader");

                entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("decimal(15, 4)");

                entity.Property(e => e.FreightCharges).HasColumnType("money");

                entity.Property(e => e.GrandTotal).HasColumnType("money");

                entity.Property(e => e.HandlingCharges).HasColumnType("money");

                entity.Property(e => e.InchVerifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsInchVerified).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Prefix).HasMaxLength(20);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseNo).HasMaxLength(20);

                entity.Property(e => e.PurchaseTime).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TaxId).HasColumnName("TaxID");

                entity.Property(e => e.TotCgstamt)
                    .HasColumnType("money")
                    .HasColumnName("TotCGSTAmt");

                entity.Property(e => e.TotIgstamt)
                    .HasColumnType("money")
                    .HasColumnName("TotIGSTAmt");

                entity.Property(e => e.TotSgstamt)
                    .HasColumnType("money")
                    .HasColumnName("TotSGSTAmt");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TransportChanges).HasColumnType("money");

                entity.Property(e => e.VerifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Worrenty).HasMaxLength(50);
            });

            modelBuilder.Entity<TRadiologyReportHeader>(entity =>
            {
                entity.HasKey(e => e.RadReportId);

                entity.ToTable("T_RadiologyReportHeader");

                entity.Property(e => e.AdmVisitDoctorId).HasColumnName("AdmVisitDoctorID");

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.OpdIpdId).HasColumnName("OPD_IPD_ID");

                entity.Property(e => e.OpdIpdType).HasColumnName("OPD_IPD_Type");

                entity.Property(e => e.RadDate).HasColumnType("datetime");

                entity.Property(e => e.RadTestId).HasColumnName("RadTestID");

                entity.Property(e => e.RadTime).HasColumnType("datetime");

                entity.Property(e => e.RefDoctorId).HasColumnName("RefDoctorID");

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.ReportTime).HasColumnType("datetime");

                entity.Property(e => e.ResultEntry).HasColumnType("text");

                entity.Property(e => e.SuggestionNotes).HasMaxLength(500);
            });

            modelBuilder.Entity<TRefundDetail>(entity =>
            {
                entity.HasKey(e => e.RefundDetId);

                entity.ToTable("T_RefundDetails");

                entity.Property(e => e.DoctorAmount).HasColumnType("money");

                entity.Property(e => e.HospitalAmount).HasColumnType("money");

                entity.Property(e => e.RefundAmount).HasColumnType("money");

                entity.Property(e => e.RefundId).HasColumnName("RefundID");

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.ServiceAmount).HasColumnType("money");

                entity.HasOne(d => d.Refund)
                    .WithMany(p => p.TRefundDetails)
                    .HasForeignKey(d => d.RefundId)
                    .HasConstraintName("constraint_name");
            });

            modelBuilder.Entity<TReturnFromDepartmentDetail>(entity =>
            {
                entity.HasKey(e => e.ReturnDetId);

                entity.ToTable("T_ReturnFromDepartmentDetail");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.TotalLandedRate).HasColumnType("money");

                entity.Property(e => e.TotalMrpamount)
                    .HasColumnType("money")
                    .HasColumnName("TotalMRPAmount");

                entity.Property(e => e.TotalPurAmount).HasColumnType("money");

                entity.Property(e => e.UnitLandedRate).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.UnitPurchaseRate).HasColumnType("money");
            });

            modelBuilder.Entity<TReturnFromDepartmentHeader>(entity =>
            {
                entity.HasKey(e => e.ReturnId);

                entity.ToTable("T_ReturnFromDepartmentHeader");

                entity.Property(e => e.LandedRateTotalAmount).HasColumnType("money");

                entity.Property(e => e.MrptotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("MRPTotalAmount");

                entity.Property(e => e.PurchaseTotalAmount).HasColumnType("money");

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnNo).HasMaxLength(50);

                entity.Property(e => e.ReturnTime).HasColumnType("datetime");

                entity.Property(e => e.TotalVatAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TSalesDetail>(entity =>
            {
                entity.HasKey(e => e.SalesDetId);

                entity.ToTable("T_SalesDetails");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Igstamt)
                    .HasColumnType("money")
                    .HasColumnName("IGSTAmt");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.LandedPrice).HasColumnType("money");

                entity.Property(e => e.Mrp).HasColumnType("money");

                entity.Property(e => e.MrpTotal).HasColumnType("money");

                entity.Property(e => e.PurRateWf).HasColumnType("money");

                entity.Property(e => e.PurTotAmt).HasColumnType("money");

                entity.Property(e => e.ReturnQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StkId).HasColumnName("StkID");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TotalLandedAmount).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatAmount).HasColumnType("money");

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.TSalesDetails)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_SalesDetails_T_SalesHeader");
            });

            modelBuilder.Entity<TSalesDraftDet>(entity =>
            {
                entity.HasKey(e => e.SalDetId);

                entity.ToTable("T_SalesDraftDet");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.DsalesId).HasColumnName("DSalesId");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.LandedPrice).HasColumnType("money");

                entity.Property(e => e.PurRateWf).HasColumnType("money");

                entity.Property(e => e.PurTotAmt).HasColumnType("money");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TotalLandedAmount).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TSalesDraftHeader>(entity =>
            {
                entity.HasKey(e => e.DsalesId);

                entity.ToTable("T_SalesDraftHeader");

                entity.Property(e => e.DsalesId).HasColumnName("DSalesId");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CashCounterId).HasColumnName("CashCounterID");

                entity.Property(e => e.ConcessionReasonId).HasColumnName("ConcessionReasonID");

                entity.Property(e => e.CreditReason).HasMaxLength(100);

                entity.Property(e => e.CreditReasonId).HasColumnName("CreditReasonID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.DoctorName).HasMaxLength(200);

                entity.Property(e => e.ExtAddress)
                    .HasMaxLength(500)
                    .HasColumnName("extAddress");

                entity.Property(e => e.ExtMobileNo).HasMaxLength(20);

                entity.Property(e => e.ExternalPatientName).HasMaxLength(200);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.SalesNo).HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.VatAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TSalesHeader>(entity =>
            {
                entity.HasKey(e => e.SalesId);

                entity.ToTable("T_SalesHeader");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CashCounterId).HasColumnName("CashCounterID");

                entity.Property(e => e.ConcessionReasonId).HasColumnName("ConcessionReasonID");

                entity.Property(e => e.CreditReason).HasMaxLength(100);

                entity.Property(e => e.CreditReasonId).HasColumnName("CreditReasonID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.DiscperH).HasColumnName("Discper_H");

                entity.Property(e => e.DoctorName).HasMaxLength(200);

                entity.Property(e => e.ExtAddress).HasMaxLength(200);

                entity.Property(e => e.ExtMobileNo).HasMaxLength(11);

                entity.Property(e => e.ExtRegNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExternalPatientName).HasMaxLength(200);

                entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.PatientName).HasMaxLength(500);

                entity.Property(e => e.RefundAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RegNo).HasMaxLength(50);

                entity.Property(e => e.RoundOff).HasColumnType("money");

                entity.Property(e => e.SalesHeadName).HasMaxLength(100);

                entity.Property(e => e.SalesNo).HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.VatAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TSalesReturnDetail>(entity =>
            {
                entity.HasKey(e => e.SalesReturnDetId);

                entity.ToTable("T_SalesReturnDetails");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstamt)
                    .HasColumnType("money")
                    .HasColumnName("CGSTAmt");

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.Igstamt)
                    .HasColumnType("money")
                    .HasColumnName("IGSTAmt");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.LandedPrice).HasColumnType("money");

                entity.Property(e => e.Mrp)
                    .HasColumnType("money")
                    .HasColumnName("MRP");

                entity.Property(e => e.Mrptotal)
                    .HasColumnType("money")
                    .HasColumnName("MRPTotal");

                entity.Property(e => e.PurRate).HasColumnType("money");

                entity.Property(e => e.PurTot).HasColumnType("money");

                entity.Property(e => e.SalesDetId).HasColumnName("SalesDetID");

                entity.Property(e => e.SalesId).HasColumnName("SalesID");

                entity.Property(e => e.Sgstamt)
                    .HasColumnType("money")
                    .HasColumnName("SGSTAmt");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StkId).HasColumnName("StkID");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.TotalLandedAmount).HasColumnType("money");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatAmount).HasColumnType("money");

                entity.HasOne(d => d.SalesReturn)
                    .WithMany(p => p.TSalesReturnDetails)
                    .HasForeignKey(d => d.SalesReturnId)
                    .HasConstraintName("FK_T_SalesReturnDetails_T_SalesReturnHeader");
            });

            modelBuilder.Entity<TSalesReturnHeader>(entity =>
            {
                entity.HasKey(e => e.SalesReturnId);

                entity.ToTable("T_SalesReturnHeader");

                entity.Property(e => e.BalanceAmount).HasColumnType("money");

                entity.Property(e => e.CashCounterId).HasColumnName("CashCounterID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.Narration).HasMaxLength(500);

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.OpIpType).HasColumnName("OP_IP_Type");

                entity.Property(e => e.PaidAmount).HasColumnType("money");

                entity.Property(e => e.SalesId).HasColumnName("SalesID");

                entity.Property(e => e.SalesReturnNo).HasMaxLength(50);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.VatAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TSmsOutgoing>(entity =>
            {
                entity.HasKey(e => e.SmsoutGoingId);

                entity.ToTable("T_SMS_Outgoing");

                entity.Property(e => e.SmsoutGoingId).HasColumnName("SMSOutGoingID");

                entity.Property(e => e.LastResponse).HasMaxLength(200);

                entity.Property(e => e.LastTry).HasColumnType("datetime");

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Smsdate)
                    .HasColumnType("datetime")
                    .HasColumnName("SMSDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Smsflag)
                    .HasMaxLength(20)
                    .HasColumnName("SMSFlag");

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(500)
                    .HasColumnName("SMSString");

                entity.Property(e => e.Smstype)
                    .HasMaxLength(100)
                    .HasColumnName("SMSType");

                entity.Property(e => e.Smsurl)
                    .HasMaxLength(1000)
                    .HasColumnName("SMSurl");
            });

            modelBuilder.Entity<TSmsoutGoing1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_SMSOutGoing");

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Smsdate)
                    .HasColumnType("datetime")
                    .HasColumnName("SMSDate");

                entity.Property(e => e.SmsoutGoingId).HasColumnName("SMSOutGoingID");

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(700)
                    .HasColumnName("SMSString");

                entity.Property(e => e.Smsurl)
                    .HasMaxLength(1000)
                    .HasColumnName("smsurl");
            });

            modelBuilder.Entity<TStockAdjustment>(entity =>
            {
                entity.HasKey(e => e.StockAdgId);

                entity.ToTable("T_StockAdjustment");

                entity.Property(e => e.AdDdQty).HasColumnName("Ad_DD_Qty");

                entity.Property(e => e.AdDdType).HasColumnName("Ad_DD_Type");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");
            });

            modelBuilder.Entity<TStockLedger>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_StockLedger");

                entity.HasIndex(e => new { e.StockId, e.StoreId, e.ItemId, e.BatchNo }, "idx_Covering_idx");

                entity.HasIndex(e => e.LedgerDate, "idx_LedgerDate")
                    .IsClustered();

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.LedgerDate).HasColumnType("datetime");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TStockUpdate>(entity =>
            {
                entity.HasKey(e => e.StockId);

                entity.ToTable("T_StockUpdate");
            });

            modelBuilder.Entity<TSupPayDet>(entity =>
            {
                entity.HasKey(e => e.SupTranId);

                entity.ToTable("T_SupPayDet");

                entity.HasOne(d => d.SupPay)
                    .WithMany(p => p.TSupPayDets)
                    .HasForeignKey(d => d.SupPayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_SupPayDet_T_GRNSupPayment");
            });

            modelBuilder.Entity<TTokenNoDoctorWiseMannual>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("T_TokenNoDoctorWise_Mannual");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TTokenNumberGroupWise>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("T_TokenNumberGroupWise");

                entity.Property(e => e.BillDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TTokenNumberWithDoctorWise>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("T_TokenNumberWithDoctorWise");

                entity.Property(e => e.VisitDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TWhatsAppSmsOutgoing>(entity =>
            {
                entity.HasKey(e => e.SmsoutGoingId);

                entity.ToTable("T_WhatsAppSMS_Outgoing");

                entity.Property(e => e.SmsoutGoingId).HasColumnName("SMSOutGoingID");

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.LastTry).HasColumnType("datetime");

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Smsdate)
                    .HasColumnType("datetime")
                    .HasColumnName("SMSDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Smsflag)
                    .HasMaxLength(20)
                    .HasColumnName("SMSFlag");

                entity.Property(e => e.Smsstring)
                    .HasMaxLength(500)
                    .HasColumnName("SMSString");

                entity.Property(e => e.Smstype)
                    .HasMaxLength(100)
                    .HasColumnName("SMSType");

                entity.Property(e => e.Smsurl)
                    .HasMaxLength(1000)
                    .HasColumnName("SMSurl");
            });

            modelBuilder.Entity<TWorkOrderDetail>(entity =>
            {
                entity.HasKey(e => e.WodetId)
                    .HasName("PK_TWODet");

                entity.ToTable("T_WorkOrderDetail");

                entity.Property(e => e.WodetId).HasColumnName("WODetId");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.DiscPer).HasColumnType("money");

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.Vatamount)
                    .HasColumnType("money")
                    .HasColumnName("VATAmount");

                entity.Property(e => e.Vatper)
                    .HasColumnType("money")
                    .HasColumnName("VATPer");

                entity.Property(e => e.Woid).HasColumnName("WOId");
            });

            modelBuilder.Entity<TWorkOrderHeader>(entity =>
            {
                entity.HasKey(e => e.Woid);

                entity.ToTable("T_WorkOrderHeader");

                entity.Property(e => e.Woid).HasColumnName("WOId");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DiscAmount).HasColumnType("money");

                entity.Property(e => e.IsCancelDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.Remark).HasMaxLength(250);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.VatAmount).HasColumnType("money");

                entity.Property(e => e.Wono)
                    .HasMaxLength(50)
                    .HasColumnName("WONo");
            });

            modelBuilder.Entity<TableSubhash>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("table subhash");

                entity.Property(e => e.CashCounterId).HasColumnName("CashCounterID");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.OpIpId).HasColumnName("OP_IP_ID");

                entity.Property(e => e.PayTmamount)
                    .HasColumnType("money")
                    .HasColumnName("PayTMAmount");

                entity.Property(e => e.SalesNo).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("money");
            });

            modelBuilder.Entity<TariffMaster>(entity =>
            {
                entity.HasKey(e => e.TariffId);

                entity.ToTable("TariffMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TariffName).HasMaxLength(100);
            });

            modelBuilder.Entity<TempPathReportId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Temp_PathReportId");
            });

            modelBuilder.Entity<TempPkCurrStkIssueToDept>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempPK_CurrStk_IssueToDept");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.UnitPurRate).HasColumnType("money");
            });

            modelBuilder.Entity<TempPkCurrStkNewBatchNo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempPK_CurrStk_NewBatchNo");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StockId).ValueGeneratedOnAdd();

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TempPkCurrStkNewRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempPK_CurrStk_NewRecords");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Cgstper).HasColumnName("CGSTPer");

                entity.Property(e => e.Igstper).HasColumnName("IGSTPer");

                entity.Property(e => e.IstkId).HasColumnName("IStkId");

                entity.Property(e => e.LandedRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRate).HasColumnType("money");

                entity.Property(e => e.PurUnitRateWf)
                    .HasColumnType("money")
                    .HasColumnName("PurUnitRateWF");

                entity.Property(e => e.PurchaseRate).HasColumnType("money");

                entity.Property(e => e.Sgstper).HasColumnName("SGSTPer");

                entity.Property(e => e.StockId).ValueGeneratedOnAdd();

                entity.Property(e => e.UnitMrp)
                    .HasColumnType("money")
                    .HasColumnName("UnitMRP");

                entity.Property(e => e.VatPercentage).HasColumnType("money");
            });

            modelBuilder.Entity<TempPkCurrStkReceivedQty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempPK_CurrStk_ReceivedQty");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.UnitPurRate).HasColumnType("money");
            });

            modelBuilder.Entity<TempPkCurrStkSale>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempPK_CurrStk_Sales");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.StkId).HasColumnName("StkID");
            });

            modelBuilder.Entity<TempPkCurrStkSales1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TempPK_CurrStk_Sales_1");

                entity.Property(e => e.BatchExpDate).HasColumnType("datetime");

                entity.Property(e => e.BatchNo).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PurRateWf).HasColumnType("money");

                entity.Property(e => e.StkId).HasColumnName("StkID");
            });

            modelBuilder.Entity<TempStrId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Temp_StrId");
            });

            modelBuilder.Entity<TempSulpId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Temp_SulpId");
            });

            modelBuilder.Entity<TemppkCurrstkNegativeBalQty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temppk_currstk_NegativeBalQty");
            });

            modelBuilder.Entity<TgHtlTmp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Tg_Htl_Tmp");

                entity.Property(e => e.TempKeys).HasMaxLength(1000);

                entity.Property(e => e.TempName).HasMaxLength(100);
            });

            modelBuilder.Entity<Tpr>(entity =>
            {
                entity.ToTable("TPR");

                entity.Property(e => e.Tprid).HasColumnName("TPRId");

                entity.Property(e => e.Avpu).HasColumnName("AVPU");

                entity.Property(e => e.Bp)
                    .HasMaxLength(10)
                    .HasColumnName("BP")
                    .IsFixedLength();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Height)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IsSynchronised).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pulse)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Respiration)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Temperature)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.Weight)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserChatMailSystem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserChatMAil_System");

                entity.Property(e => e.LoopId).HasColumnName("loop_id");

                entity.Property(e => e.Mgs)
                    .HasMaxLength(50)
                    .HasColumnName("mgs");

                entity.Property(e => e.UserReceive)
                    .HasMaxLength(50)
                    .HasColumnName("user_receive");

                entity.Property(e => e.UserSend)
                    .HasMaxLength(50)
                    .HasColumnName("user_send");

                entity.Property(e => e.Xdate)
                    .HasColumnType("datetime")
                    .HasColumnName("xdate");

                entity.Property(e => e.Xdefine)
                    .HasMaxLength(50)
                    .HasColumnName("xdefine");

                entity.Property(e => e.Xtime)
                    .HasColumnType("datetime")
                    .HasColumnName("xtime");
            });

            modelBuilder.Entity<UserMailSystemBlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UserMAilSystem_blog");

                entity.Property(e => e.Whatmind)
                    .HasMaxLength(50)
                    .HasColumnName("whatmind");

                entity.Property(e => e.Xdate)
                    .HasColumnType("datetime")
                    .HasColumnName("xdate");

                entity.Property(e => e.Xdefine)
                    .HasMaxLength(50)
                    .HasColumnName("xdefine");

                entity.Property(e => e.Xtime)
                    .HasColumnType("datetime")
                    .HasColumnName("xtime");

                entity.Property(e => e.Xuser)
                    .HasMaxLength(50)
                    .HasColumnName("xuser");
            });

            modelBuilder.Entity<VAdmissionMsg>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_AdmissionMsg");

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.AgeYear).HasMaxLength(10);

                entity.Property(e => e.BedNo).HasMaxLength(100);

                entity.Property(e => e.Doa)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DOA");

                entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.DoctorName).HasMaxLength(202);

                entity.Property(e => e.Dot)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DOT");

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.PatientName).HasMaxLength(403);

                entity.Property(e => e.RefDoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.RefDoctorName).HasMaxLength(202);

                entity.Property(e => e.RegNo).HasMaxLength(20);

                entity.Property(e => e.WardName).HasMaxLength(100);
            });

            modelBuilder.Entity<VCheckingBalQty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_CheckingBalQty");

                entity.Property(e => e.StockId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VPaymentBillwisesumAmount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_PAYMENT_BILLWISESUM_AMOUNT");

                entity.Property(e => e.AdvUsedPay).HasColumnType("money");

                entity.Property(e => e.CardPay).HasColumnType("money");

                entity.Property(e => e.CashPay).HasColumnType("money");

                entity.Property(e => e.ChequePay).HasColumnType("money");

                entity.Property(e => e.Neftpay)
                    .HasColumnType("money")
                    .HasColumnName("NEFTPay");

                entity.Property(e => e.PayTmpay)
                    .HasColumnType("money")
                    .HasColumnName("PayTMPay");

                entity.Property(e => e.Tdsamount).HasColumnName("TDSAmount");
            });

            modelBuilder.Entity<VVisitMsg>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_VisitMsg");

                entity.Property(e => e.AgeYear).HasMaxLength(10);

                entity.Property(e => e.Doa)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DOA");

                entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.DoctorName).HasMaxLength(202);

                entity.Property(e => e.Dot)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DOT");

                entity.Property(e => e.GenderName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.PatientName).HasMaxLength(302);

                entity.Property(e => e.RefDoctorMobileNo).HasMaxLength(20);

                entity.Property(e => e.RefDoctorName).HasMaxLength(202);

                entity.Property(e => e.RegNo).HasMaxLength(20);
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_1");

                entity.Property(e => e.CashPayAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.TranMode).HasMaxLength(30);
            });

            modelBuilder.Entity<ViewDoctorshare>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_doctorshare");

                entity.Property(e => e.Doctorname).HasMaxLength(163);

                entity.Property(e => e.ServiceShortDesc).HasMaxLength(200);
            });

            modelBuilder.Entity<ViewTallyPharSalesReceiptNewOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Tally_PharSalesReceiptNewOld");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreditLedger)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DebitLedger).HasMaxLength(403);

                entity.Property(e => e.ReceiptNo).HasMaxLength(50);
            });

            modelBuilder.Entity<VisitDetail>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.Property(e => e.AppPurposeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Bmi)
                    .HasMaxLength(20)
                    .HasColumnName("BMI");

                entity.Property(e => e.Bp)
                    .HasMaxLength(10)
                    .HasColumnName("BP");

                entity.Property(e => e.Bsl)
                    .HasMaxLength(20)
                    .HasColumnName("BSL");

                entity.Property(e => e.CheckInTime).HasColumnType("datetime");

                entity.Property(e => e.CheckOutTime).HasColumnType("datetime");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.ConEndTime).HasColumnType("datetime");

                entity.Property(e => e.ConStartTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstFollowupVisit).HasDefaultValueSql("((0))");

                entity.Property(e => e.FollowupDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1900))");

                entity.Property(e => e.Height).HasMaxLength(10);

                entity.Property(e => e.IsCancelledDate).HasColumnType("datetime");

                entity.Property(e => e.IsConvertRequestForIp).HasColumnName("IsConvertRequestForIP");

                entity.Property(e => e.IsMark).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsXray).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Opdno)
                    .HasMaxLength(50)
                    .HasColumnName("OPDNo");

                entity.Property(e => e.PatientOldNew).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pulse).HasMaxLength(10);

                entity.Property(e => e.Pweight)
                    .HasMaxLength(20)
                    .HasColumnName("PWeight");

                entity.Property(e => e.RegId).HasColumnName("RegID");

                entity.Property(e => e.SpO2).HasMaxLength(20);

                entity.Property(e => e.Temp).HasMaxLength(10);

                entity.Property(e => e.VisitDate).HasColumnType("datetime");

                entity.Property(e => e.VisitTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
