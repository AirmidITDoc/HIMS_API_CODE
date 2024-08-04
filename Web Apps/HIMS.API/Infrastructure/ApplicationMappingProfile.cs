using AutoMapper;
using HIMS.API.Controllers.Masters.Personal_Information;
using HIMS.API.Models;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;

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
            CreateMap<MCertificateTemplateMaster, CertificateTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ClassMaster, ClassMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TariffMaster, TarifMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDepartmentMaster, DepartmentMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCityMaster, CityMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MMaritalStatusMaster, MaritalStatusModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRelationshipMaster, RelationshipMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCountryMaster, CountryMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<RoomMaster, WardMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DischargeTypeMaster, DischargeTypeModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MAssignSupplierToStore, AssignSupplierModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSupplierMaster, SupplierModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MTalukaMaster, TalukaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MAreaMaster, AreaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            
            CreateMap<CompanyMaster, CompanyMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CompanyTypeMaster, CompanyTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<GroupMaster, GroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSubGroupMaster, SubGroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDrugMaster, DrugMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MConcessionReasonMaster, ConcessionReasonMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorTypeMaster, DoctorTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoseMaster, DoseMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MInstructionMaster, InstructionMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MStateMaster, StateMasterModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathParameterMaster, ParameterMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MModeOfPayment, ModeOfPaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathUnitMaster, PathUnitMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<LocationMaster, LocationMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MBankMaster, BankMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();






            //Sales
            CreateMap<TSalesHeader, SalesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDetail, SalesDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Payment, PaymentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseHeader, PurchaseModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();


            //Inventory 
            CreateMap<TIndentHeader, IndentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentDetail, IndentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentHeader, IndentVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemManufactureMaster, ItemManufactureModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemGenericNameMaster, ItemGenericNameModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemCategoryMaster, ItemCategoryModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemTypeMaster, ItemTypeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MCurrencyMaster, CurrencyMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRadiologyTestMaster, RadiologyTestModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MRadiologyTemplateMaster, RadiologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MGenericMaster, GenericMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();




            CreateMap<VisitDetail, VisitDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Registration, RegistrationSaveModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MTemplateMaster, PathologyTemplateModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();









            //Purchase
            CreateMap<TPurchaseHeader, PurchaseVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseDetail, PurchaseDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnheader, GRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrndetail, GRNDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MItemMaster, ItemModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseHeader, POHeaderAganistGRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TPurchaseDetail, POAganistGRNModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnreturnHeader, GRNReturnModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrnreturnDetail, GRNReturnDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TCurrentStock, GRNReturnCurrentStock>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGrndetail, GRNReturnReturnQty>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ScheduleMaster, ScheduleModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TFavouriteUserList, FavouriteDtoModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<SearchFields, SearchGrid>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<PatientTypeMaster, PatientTypeModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            

            CreateMap<TIndentHeader, IndentModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentDetail, IndentDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TIndentHeader, IndentVerifyModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           
            CreateMap<MTalukaMaster, TalukaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MAreaMaster, AreaMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MBankMaster, BankMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CompanyMaster, CompanyMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CompanyTypeMaster, CompanyTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<GroupMaster, GroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MSubGroupMaster, SubGroupMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDrugMaster, DrugMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MConcessionReasonMaster, ConcessionReasonMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorMaster, DoctorMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DoctorTypeMaster, DoctorTypeMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MDoseMaster, DoseMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MInstructionMaster, InstructionMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MStateMaster, StateMasterModel1>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<MPathParameterMaster, ParameterMasterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();




        }
    }
}
