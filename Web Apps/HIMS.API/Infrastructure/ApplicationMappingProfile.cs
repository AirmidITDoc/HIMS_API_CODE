using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using HIMS.API.Controllers.Masters.Personal_Information;
using HIMS.API.Controllers.Pharmacy;
using HIMS.API.Models.Administration;
using HIMS.API.Models.Common;
using HIMS.API.Models.Customer;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pathology;
using HIMS.API.Models.Pharmacy;
using HIMS.API.Models.Radiology;
using HIMS.API.Models.Transaction;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using static HIMS.API.Models.Inventory.Masters.ReportConfigModelModelValidator;
using static HIMS.API.Models.OutPatient.RefundAdvanceModelValidator;
namespace HIMS.API.Infrastructure
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {

            // Master
            CreateMap<MCampMaster, CampMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReportConfig, MReportConfigModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TCertificateInformation, TCertificateInformationParamModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCertificateMaster, CertificateMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPathParameterMaster, ParameterMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MParameterDescriptiveMaster, MParameterDescriptiveMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathParaRangeWithAgeMaster, MPathParaRangeWithAgeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathParameterMaster, UpdateParameterFormulaModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPathTestFormula, MPathTestFormulaModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReportConfiguration, MReportConfigurationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TOtbooking, OTBookingModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TOtbookingRequest, OTBookingRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReportConfig, ReportConfigModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReportConfigDetail, ReportConfigDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<DbPrefixMaster, PrefixModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DbGenderMaster, GenderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CashCounter, CashCounterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<PatientTypeMaster, PatientTypeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
         
            CreateMap<Bedmaster, BedMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemClassMaster, ItemClassMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReligionMaster, ReligionMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathCategoryMaster, PathCategoryMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRadiologyCategoryMaster, RadiologyCategoryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MCertificateTemplateMaster, CertificateTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ClassMaster, ClassMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MClassMaster, PriscriptionclassModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TariffMaster, TarifMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDepartmentMaster, DepartmentMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCityMaster, CityMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MMaritalStatusMaster, MaritalStatusModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRelationshipMaster, RelationshipMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCountryMaster, CountryMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<RoomMaster, WardMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeTypeMaster, DischargeTypeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MStoreMaster, StoreMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<HospitalMaster, HospitalMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MVillage, VillageMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<MUnitofMeasurementMaster, UnitOfMeasurementModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCreditReasonMaster, CreditReasonModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPathTestMaster, PathTestMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathTemplateDetail, PathTemplateDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathTestDetailMaster, PathTestDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathTestMaster, PathTestMasterModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<ACustomerInformation, CustomerInformationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ACustomerPaymentSummary, CustomerPaymentSummaryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MTemplateMaster, PathTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctoreMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MTermsOfPaymentMaster, TermsOfPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDraftHeader, TSalesDraftsHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TGrnheader, SupplierPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSupPayDet, TSupPayDetModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnsupPayment, TGrnsupPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MSupplierMaster, SupplierModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MTaxNatureMaster, TaxMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MAssignSupplierToStore, AssignSupplierToStoreModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemMaster, ItemMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MAssignItemToStore, AssignItemToStoreModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRadiologyTestMaster, RadiologyTestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRadiologyTemplateDetail, RadiologyTemplateDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ServiceMaster, BillingServiceModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ServiceDetail, ServiceDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Refund, OPRefundOfBillModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TRefundDetail, TRefundDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MenuMaster, MenuMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, DoctorShareProcessModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPackageDetail, PackageDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPackageDetail, PackageDetailsModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TWorkOrderHeader, WorkOrdersModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TWorkOrderDetail, WorkOrderDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TWorkOrderHeader, UpdateWorkOrdersModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TWorkOrderDetail, WorkOrderDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
          
            CreateMap<MOttableMaster, OtTableMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MOttypeMaster, OtTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSurgeryCategoryMaster, SurgeryCategoryMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSurgeryMaster, SurgeryMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TConsentInformation, ConsentInformationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TConsentInformation, UpdateConsentInformationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MManufactureMaster, ManufactureMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<RoleTemplateMaster, RoleTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<ConfigSetting, ConfigSettingModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<PaymentPharmacy, paymentpharmacyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpmedicalRecord, TIPmedicalRecordModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescription, IpPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<VisitDetail, VisitFollowupDateUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<PaymentPharmacy, paymentpharModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TSalesReturnHeader, SalesReturnModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesReturnDetail, SalesReturnDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, CurrentStockModels>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, SalesReturnModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDetail, SalesDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesHeader, SalesHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentModels>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, CurStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            
            CreateMap<TGrnsupPayment, GrnsupPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnheader, GRNModels>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSupPayDet, SupPayDetModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TSalesHeader, SalessModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDetail, SalesDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, CurrentStocksModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentpharModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescription, IPPrescriptionsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDraftHeader, SalesDraftHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();




            CreateMap<AdvanceHeader, UpdateAdvanceModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailModel2>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceHeader, UpdateCancel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<AdvanceHeader, AdvanceModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
             CreateMap<Payment,AdvancePayment>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bedmaster, BedMasterModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeSummary, DischargeSummaryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescriptionDischarge, PrescriptionDischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeSummary, DischargeSummaryUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeSummary, DischargeTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescriptionDischarge, PrescriptionTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeSummary, DischargeTemplateUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Discharge, DischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AdmissionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           CreateMap<Discharge, DischargeUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AdmissionUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, InitiateDischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AdmisionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AdmissionModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<InitiateDischarge, InitiateDischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TIpPrescriptionDischarge, PrescriptionTemplatUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<TIpPrescriptionDischarge, PrescriptionTemplatUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MTalukaMaster, TalukaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


           
            CreateMap<ConfigSetting, ConfigurationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();





            CreateMap<LoginManager, LoginManagerModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<LoginManager, ChangePassword>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MSubTpacompanyMaster, SubTpaCompanyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //Radiology//
            CreateMap<MRadiologyTemplateMaster, RadiologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TPrescription, IPPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<VisitDetail, VisitDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<VisitDetail, UpdateVitalInfModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MAreaMaster, AreaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            
            CreateMap<CompanyMaster, CompanyMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPathologyReportHeader, PathlogySampleCollectionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<CompanyTypeMaster, CompanyTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<GroupMaster, GroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSubGroupMaster, SubGroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDrugMaster, DrugMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MConcessionReasonMaster, ConcessionReasonMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorPerMaster, DoctorShareMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorTypeMaster, DoctorTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoseMaster, DoseMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MInstructionMaster, InstructionMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MStateMaster, StateMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            
            CreateMap<MModeOfPayment, ModeOfPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathUnitMaster, PathUnitMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<LocationMaster, LocationMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MBankMaster, BankMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<VisitDetail, CrossConsultationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            //Sales
            CreateMap<TSalesHeader, SalesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDetail, SalesDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseHeader, PurchaseModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TSalesDraftHeader, TSalesDraftHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDraftDet, TSalesDraftDetModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhadvanceHeader, PharmacyAdvanceModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhadvanceDetail, PharmacyAdvanceDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<PaymentPharmacy, PaymentPharmacyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhadvanceHeader, PharmacyHeaderUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhRefund, PharmacyRefundModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhadvanceHeader, PhAdvanceHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhadvRefundDetail, PHAdvRefundDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhadvanceDetail, PHAdvanceDetailBalAmountModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<PaymentPharmacy, PharmacyPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSiteDescriptionMaster, SiteDescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MConsentMaster, ConsentMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            

             //Inventory 
            CreateMap<TIndentHeader, IndentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentDetail, IndentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentHeader, IndentVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIssueToDepartmentHeader, IssueToDepartmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIssueToDepartmentDetail, IssueToDepartmentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, CurrentStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<IssueToDepartmentDetailModel, TIssueToDepartmentDetail>();
            CreateMap<MaterialConsumptionHeaderModel, TMaterialConsumptionHeader>();
            CreateMap<MaterialConsumptionDetailModel, TMaterialConsumptionDetail>();
            CreateMap<TIssueToDepartmentHeader, UpdateIndentStatusAganistModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIssueToDepartmentDetail, IssueToDepartmentDetailsUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentHeader, IndentHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentDetail, IndentDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();








            CreateMap<TCurrentStock, IssueToDepartmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TIssueToDepartmentHeader, IssueToDIndentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIssueToDepartmentDetail, IssueToDepartmentDetailModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MItemManufactureMaster, ItemManufactureModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemGenericNameMaster, ItemGenericNameModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemCategoryMaster, ItemCategoryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemTypeMaster, ItemTypeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCurrencyMaster, CurrencyMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MTemplateMaster, PathologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MGenericMaster, GenericMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //IP/OP
      
            CreateMap<VisitDetail, VisitDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, RegistrationSaveModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TBedTransferDetail, BedTransferModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bedmaster, BedMasterTofreebedModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bedmaster, BedMasterUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Admission, AdmissionforBedModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPrescription, TPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPrescription, UpdatePrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPrescription, UpdatePrescription>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TRadiologyReportHeader, TRadiologyReportModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPathologyReportHeader, PathlogySampleCollectionsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TMrpAdjustment, MRPAdjustmentModels>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, CurStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();






            CreateMap<TOprequestList, TOPRequestListModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MOpcasepaperDignosisMaster, MOPCasepaperDignosisMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPresTemplateD, PresTemplateDModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPresTemplateD, PrescriptionOPTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPresTemplateH, PrescriptionOPTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TMlcinformation, MlcInformationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //   CreateMap<TPrescription, OPDPrescriptionMedicalModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Bill, OPBillModelShilpa>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<BillDetail, BillDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, BillModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceHeader, AdvanceHeadersModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, AddChargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, BillsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<BillDetail, BillingDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, AdddChargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AddmissionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, paymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, BillMModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceHeader, AdvancesHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDrbill, TDrbillModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDrbillDet, TDRBillDetModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, AddChargesDeleteModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, AddChargessModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Refund, RefundModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceHeader, AdvanceHeaderMModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvRefundDetail, AdvanceRefundDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, paymentMModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPathologyReportDetail, PathologyResultEntryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPathologyReportHeader, TPathologyReportHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, AddChargeuModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, IPBilllingModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<BillDetail, BillingDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, paymentsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MNursingTemplateMaster, NursingTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymenntModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, BilllsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<SsSmsConfig, smsConfigModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorPerMaster, MDoctorPerMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPathologyReportDetail, PathReportModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TNursingMedicationChart1, TNursingMedicationChartModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
      
            CreateMap<AddCharge, AdddChargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TNursingMedicationChartModel, TNursingMedicationChart>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddChargeModell, AddCharge>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<GRNReturnVerifyModel, TGrnreturnHeader>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //Opening Balance Stock
            CreateMap<OpeningBalModel,TOpeningTransactionHeader> ().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<OpeningTransactionModel, TOpeningTransaction> ().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            //Appointment
            CreateMap<VisitDetail, AppVisitDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, AppReistrationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, AppReistrationUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
             CreateMap<Payment, OPPaymentdetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhoneAppointment, PhoneAppointment2Model>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhoneAppointment, PhoneAppointmentUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<VisitDetail, ConsulationStartEndProcess>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<VisitDetail, CheckOutProcessUpdate>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorQualificationDetail, DoctorQualificationDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorQualificationDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorExperienceDetail, DoctorExperienceDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorScheduleDetail, MDoctorScheduleDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorChargesDetail, MDoctorChargesDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorLeaveDetail, DoctorLeaveDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorSignPageDetail, DoctorSignPageDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<Payment, PharaModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesHeader, SaleModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailModel3>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
             CreateMap<AdvanceHeader, TPHAdvanceHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();












            CreateMap<Bill, DoctorSharePerCalculationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCanteenRequestHeader, CanteenRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCanteenRequestDetail, TCanteenRequestDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            //Admission
            CreateMap<Admission, ADMISSIONModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, AdmissionRegModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, OPAddchargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            // OP Credit bill Settlement
            CreateMap<Payment, OPCreditPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, BillUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Bill, BilModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //CreateMap<Bill,OPSettlementCreditModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Payment, OPSettlementpayment>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

          

            CreateMap<Bill, OPCreditBillModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<BillDetail, CreditBillDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, CreditBillChargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //IPBill Draft
            CreateMap<TDrbill,IPDraftBillModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDrbillDet,IpDraftBillDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, IpDraftChargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            //CreateMap<Payment, IPPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //IPBill
            CreateMap<Bill,IPBILLModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<BillDetail, IpBillDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, IPPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, IpChargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //OPBill
            CreateMap<Bill,OPBillIngModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<BillDetail, BillDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, ChargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, OPPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bill, OpBillCancellationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //CreateMap<AdvanceHeader, IPAdvanceHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<AdvanceDetail, IPAdvanceDetail>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Payment, AdvancePaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            //CreateMap<TPathologyReportHeader, IPLabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescription, IPPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<THlabRequest, IPLabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDlabRequest, TDLabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<THlabRequest, LabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDlabRequest, TDlabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpmedicalRecord, MPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescription, TIpPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TNurNote, NursingNoteModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDoctorPatientHandover, TDoctorPatientHandoverModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TNursingPatientHandover, NursingPatientHandoverModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoctorNotesTemplateMaster, DoctorNotesTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<TExpense, TExpenseModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TExpense, TExpenseCancelModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AdmissionsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, LabRequestsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, UpdateAddchargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, IPAddChargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, UpdateAdvanceCancel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Refund, UpdateRefundModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesHeader, PharmSalesPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ServiceDetail, DifferTraiffModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();




            





            CreateMap<TCanteenRequestHeader, CanteenRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCanteenRequestDetail, TCanteenRequestDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDoctorsNote, DoctorNoteModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpprescriptionReturnH, PriscriptionReturnModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpprescriptionReturnD, IpprescriptionReturnDModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<RoleMaster, RoleMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();








            //patholgy


            CreateMap<MTemplateMaster, PathologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, RegistrationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, NewPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TPathologyReportTemplateDetail, PathologyReportTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPathologyReportHeader, PathologyReportHeadermodel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TempPathReportId, PathPrintResultentryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            //Purchase



            CreateMap<TPurchaseHeader, PurchaseVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseDetail, PurchaseDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnheader, GRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemMaster, ItemModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnheader, GRNVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<TGrndetail, GRNDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseHeader, POHeaderAganistGRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseDetail, POAganistGRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnreturnHeader, GRNReturnModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnreturnDetail, GRNReturnDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, GRNReturnCurrentStock>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrndetail, GRNReturnReturnQty>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ScheduleMaster, ScheduleModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TFavouriteUserList, FavouriteDtoModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<PatientTypeMaster, PatientTypeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            

            CreateMap<TIndentHeader, IndentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentDetail, IndentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentHeader, IndentVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           
            
            CreateMap<MAreaMaster, AreaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MBankMaster, BankMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CompanyMaster, CompanyMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            

            CreateMap<CompanyTypeMaster, CompanyTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<GroupMaster, GroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSubGroupMaster, SubGroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDrugMaster, DrugMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MConcessionReasonMaster, ConcessionReasonMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MDoctorDepartmentDet, MDoctorDepartmentDetModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorTypeMaster, DoctorTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoseMaster, DoseMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MInstructionMaster, InstructionMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MStateMaster, StateMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathParameterMaster, ParameterMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

          
            CreateMap<VisitDetail, ConsRefDoctorModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<VisitDetail, RefDoctorModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Refund, RefundAdvanceModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceHeader, AdvanceHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvRefundDetail, AdvRefundDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentModel2>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //CreateMap<Payment, PaymentModel2>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Refund, OPRefundOfBillModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TRefundDetail, TRefundDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, AddChargesModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Payment, PaymentModel2>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TIssueToDepartmentDetail, StockAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TIssueToDepartmentDetail, StockAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TCurrentStock, TCurrentStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TStockAdjustment, PharStockAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TBatchAdjustment, BatchAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModelll>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGstadjustment, GSTUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TMrpAdjustment, MRPAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModellll>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReportTemplateConfig, ReportTemplateConfigModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

        }
    }
}
