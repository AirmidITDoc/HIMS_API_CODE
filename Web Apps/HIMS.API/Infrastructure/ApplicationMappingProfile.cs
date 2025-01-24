using AutoMapper;
using HIMS.API.Controllers.Masters.Personal_Information;
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
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using static HIMS.API.Models.OutPatient.RefundAdvanceModelValidator;




namespace HIMS.API.Infrastructure
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {

            // Master 
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


            CreateMap<MUnitofMeasurementMaster, UnitOfMeasurementModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCreditReasonMaster, CreditReasonModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MPathTestMaster, PathTestMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathTemplateDetail, PathTemplateDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathTestDetailMaster, PathTestDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ACustomerInformation, CustomerInformationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ACustomerPaymentSummary, CustomerPaymentSummaryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MTemplateMaster, PathTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctoreMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MTermsOfPaymentMaster, TermsOfPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<DbPrefixMaster, PrefixMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



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
           




            CreateMap<AdvanceHeader, AdvanceModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AdvanceDetail, AdvanceDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
             CreateMap<Payment,AdvancePayment>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, AdmissionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bedmaster, BedMasterModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Discharge, DischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeSummary, DischargeSummaryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<Admission, DischargeADMISSIONModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Discharge, IPDischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Bedmaster, BedReleaseModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MReportConfiguration, MReportConfiguration>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            

            CreateMap<LoginManager, LoginManagerModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSubTpacompanyMaster, SubTpaModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //Radiology//
            CreateMap<MRadiologyTemplateMaster, RadiologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TPrescription, PrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<VisitDetail, VisitDetailsModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

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
            CreateMap<MPathParameterMaster, ParameterMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathParaRangeMaster, MPathParaRangeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MParameterDescriptiveMaster, MParameterDescriptiveMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
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


            //Inventory 
            CreateMap<TIndentHeader, IndentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentDetail, IndentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentHeader, IndentVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIssueToDepartmentHeader, IssueToDepartmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIssueToDepartmentDetail, IssueToDepartmentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, CurrentStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<IssueToDepartmentDetailModel, TIssueToDepartmentDetail>();




            //CreateMap<TIssueToDepartmentDetail, IssueToDepartmentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
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
            //CreateMap<Bedmaster, BedMasterTofreebedModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Admission, AdmissionforBedModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TMlcinformation, MlcInformationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           





            //Appointment
            CreateMap<VisitDetail, AppVisitDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, AppReistrationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, AppReistrationUpdateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
             CreateMap<Payment, OPPaymentdetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPhoneAppointment, PhoneAppointment2Model>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<Bill, DoctorSharePerCalculationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCanteenRequestHeader, CanteenRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCanteenRequestDetail, TCanteenRequestDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            //Admission
            CreateMap<Admission, ADMISSIONModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, AdmissionRegModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<AddCharge, OPAddchargesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Payment, OPSettlementPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Bill,OPSettlementCreditModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, OPSettlementpayment>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<Discharge, IPDischargeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Admission, DischargeADMISSIONModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

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
            CreateMap<Admission, DischargeCancellationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            //CreateMap<AdvanceHeader, IPAdvanceHeaderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<AdvanceDetail, IPAdvanceDetail>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Payment, AdvancePaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<TIpPrescription,NewPrescription>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TPathologyReportHeader, IPLabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescription, IPPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<THlabRequest, IPLabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<THlabRequest, LabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDlabRequest, TDlabRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpmedicalRecord, MPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpPrescription, TIpPrescriptionModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();






            CreateMap<TCanteenRequestHeader, CanteenRequestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCanteenRequestDetail, TCanteenRequestDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TDoctorsNote, DoctorNoteModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpprescriptionReturnH, PriscriptionReturnModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIpprescriptionReturnD, IpprescriptionReturnDModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();










            CreateMap<MTemplateMaster, PathologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, RegistrationModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, NewPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            //Purchase



            CreateMap<TPurchaseHeader, PurchaseVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseDetail, PurchaseDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnheader, GRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
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
            CreateMap<CompanyMaster, CompanyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

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
            //CreateMap<AddCharge, AddChargesModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Payment, PaymentModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<Payment, PaymentModel2>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TIssueToDepartmentDetail, StockAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TIssueToDepartmentDetail, StockAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            //CreateMap<TCurrentStock, TCurrentStockModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<TStockAdjustment, PharStockAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModell>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();



            CreateMap<TBatchAdjustment, BatchAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModelll>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<TMrpAdjustment, MRPAdjustmentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, TCurrentStockModellll>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

        }
    }
}
