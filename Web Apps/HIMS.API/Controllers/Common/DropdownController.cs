using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HIMS.Core.Infrastructure;
using Microsoft.VisualBasic;
using ClosedXML.Excel;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DropdownController : BaseController
    {
        private readonly IGenericService<MAreaMaster> _IAreaService;
        private readonly IGenericService<DbPrefixMaster> _IPrefixService;
        private readonly IGenericService<DbGenderMaster> _IGenderService;
        private readonly IGenericService<MRelationshipMaster> _IRelationshipMaster;
        private readonly IGenericService<MMaritalStatusMaster> _IMaritalStatusMaster;
        private readonly IGenericService<MReligionMaster> _IMreligionMaster;
        private readonly IGenericService<PatientTypeMaster> _IPatientTypeMaster;
        private readonly IGenericService<TariffMaster> _TariffMaster;
        private readonly IGenericService<MDepartmentMaster> _IMDepartmentMaster;
        private readonly IGenericService<DoctorMaster> _IMDoctorMaster;
        private readonly IGenericService<DbPurposeMaster> _IMDoPurposeMaster;

        private readonly IGenericService<MCityMaster> _IMCityService;
        private readonly IGenericService<MStateMaster> _IMStateService;
        private readonly IGenericService<MCountryMaster> _IMCountryService;
        private readonly IGenericService<ClassMaster> _IMClassService;
        private readonly IGenericService<CompanyMaster> _IMCompanysService;
        private readonly IGenericService<MSubTpacompanyMaster> _IMSubCompanysService;
        private readonly IGenericService<Bedmaster> _IMBedService;
        private readonly IGenericService<RoomMaster> _IMRoomService;
        private readonly IGenericService<MRelationshipMaster> _IMRelationshipService;
        private readonly IGenericService<ServiceMaster> _IMServiceService;
        private readonly IGenericService<MItemMaster> _IMItemService;
        private readonly IGenericService<HospitalMaster> _IMHospitalService;
        private readonly IGenericService<DischargeTypeMaster> _IMDischargetypelService;
        private readonly IGenericService<MModeOfPayment> _IMModeofpaymentService;
        private readonly IGenericService<MBankMaster> _IMbankService;
        private readonly IGenericService<MStoreMaster> _IMStoreService;
        private readonly IGenericService<MTermsOfPaymentMaster> _IMTermofpaymentService;
        private readonly IGenericService<CashCounter> _IMCashcounterService;
        private readonly IGenericService<DoctorTypeMaster> _IMDoctorTypeService;
        private readonly IGenericService<MPathCategoryMaster> _IMpathCateggoryService;
        private readonly IGenericService<MRadiologyCategoryMaster> _IMradioCateggoryService;
        private readonly IGenericService<MPathUnitMaster> _IMpathunitService;
        private readonly IGenericService<CompanyTypeMaster> _IMcompanytypeService;
        private readonly IGenericService<GroupMaster> _IMgroupService;
        private readonly IGenericService<MSubGroupMaster> _IMsubgroupService;
        private readonly IGenericService<MTemplateMaster> _IMtemplateService;

        private readonly IGenericService<MItemClassMaster> _IMItemclassService;
        private readonly IGenericService<MItemCategoryMaster> _IMItemcategoryService;
        private readonly IGenericService<MItemTypeMaster> _IMItemtypeService;
        private readonly IGenericService<MItemGenericNameMaster> _IMItemgenericService;
        private readonly IGenericService<MCurrencyMaster> _IMCurrencyeService;
        private readonly IGenericService<MItemDrugTypeMaster> _IMItemDrugtypeService;

        private readonly IGenericService<MItemManufactureMaster> _IMItemManufService;
        private readonly IGenericService<MUnitofMeasurementMaster> _IMUnitOfMeasurmentService;

        private readonly IGenericService<MConcessionReasonMaster> _IMConcessService;
       
        private readonly IGenericService<MPathParameterMaster> _IMparameterservice;
        private readonly IGenericService<RoleMaster> _IMRoleMasterservice;
        private readonly IGenericService<MenuMaster> _IMmenuMasterService;
        private readonly IGenericService<MDoseMaster> _IMDoseMaster;
        private readonly IGenericService<MPresTemplateH> _IMPresTemplateH;
        private readonly IGenericService<MOpcasepaperDignosisMaster> _IMOPCasepaperDignosisMaster;
        private readonly IGenericService<MReportTemplateConfig> _IMReportConfTemplate;
        private readonly IGenericService<MTalukaMaster> _IMTalukaMaster;
        private readonly IGenericService<MModeOfDischarge> _IMModeofdischarge;
        private readonly IGenericService<MSupplierMaster> _IMSupplierMaster;
        private readonly IGenericService<MConstant> _IMConstant;
        private readonly IGenericService<MRadiologyTemplateMaster> _IMradiotemp;
        private readonly IGenericService<MCanItemMaster> _IMcanteen;
        private readonly IGenericService<MDoctorNotesTemplateMaster> _IMDrnote;
        private readonly IGenericService<MNursingTemplateMaster> _IMNurNoteervice;
        private readonly IGenericService<MExpensesHeadMaster> _IMExpHeade;
        private readonly IGenericService<MConsentMaster> _MConsentMaster;




        public DropdownController(IGenericService<MAreaMaster> areaservice, IGenericService<DbPrefixMaster> iPrefixService, IGenericService<DbGenderMaster> iGenderService, IGenericService<MRelationshipMaster> iRelationshipMaster,
                                  IGenericService<MMaritalStatusMaster> iMaritalStatusMaster, IGenericService<MReligionMaster> iMreligionMaster, IGenericService<PatientTypeMaster> iPatientTypeMaster, IGenericService<TariffMaster> tariffMaster,
                                  IGenericService<MDepartmentMaster> iMDepartmentMaster, IGenericService<DoctorMaster> iDoctorMaster, IGenericService<DbPurposeMaster> iMDoPurposeMaster, IGenericService<MCityMaster> iMDoCityMaster
            , IGenericService<MStateMaster> iMDoStateMaster, IGenericService<MCountryMaster> iMDoCountryMaster, IGenericService<ClassMaster> iMDoClassMaster, IGenericService<CompanyMaster> iMDoCompanyMaster
                                  , IGenericService<MSubTpacompanyMaster> iMDoSubCompanyMaster, IGenericService<Bedmaster> iMDoBedMaster, IGenericService<RoomMaster> iMDoRoomMaster,
                                 IGenericService<MRelationshipMaster> iMDoRelationshipMaster, IGenericService<ServiceMaster> iMDoServiceMaster, IGenericService<MItemMaster> iMDoItemMaster
            , IGenericService<HospitalMaster> iMDoHospitalMaster, IGenericService<DischargeTypeMaster> iMDoDischargetypelMaster,
                                 IGenericService<MModeOfPayment> iMDoModeofpaymentMaster
                                , IGenericService<MBankMaster> iMDoBankMaster, IGenericService<MStoreMaster> iMDoStoreMaster, IGenericService<MTermsOfPaymentMaster> iMDoTemofpaymentMaster
            , IGenericService<CashCounter> iMDoCashcounterMaster, IGenericService<DoctorTypeMaster> iMDoDoctorTyperMaster, IGenericService<MPathCategoryMaster> iMDopathcateMaster,
             IGenericService<MRadiologyCategoryMaster> iMDoradiologycateMaster, IGenericService<MPathUnitMaster> iMDpathunitMaster, IGenericService<CompanyTypeMaster> iMDcompanytypeMaster,
              IGenericService<GroupMaster> iMDgroupMaster, IGenericService<MSubGroupMaster> iMDsubgroupMaster, IGenericService<MTemplateMaster> iMDtemplateMaster,
              IGenericService<MItemClassMaster> iMDItemClassMaster, IGenericService<MItemCategoryMaster> iMDItemCategoryMaster, IGenericService<MItemTypeMaster> iMDItemTypeMaster
              , IGenericService<MItemGenericNameMaster> iMDItemgenericeMaster, IGenericService<MCurrencyMaster> iMDCurrencyMaster
            , IGenericService<MItemDrugTypeMaster> iMDItemdrugtypeMaster, IGenericService<MItemManufactureMaster> iMDItemanufMaster, IGenericService<MUnitofMeasurementMaster> iMDunitofmeasurementMaster
            , IGenericService<MConcessionReasonMaster> iMDConcessionMaster, IGenericService<MNursingTemplateMaster> iMDnurNoteMaster, IGenericService<MPathParameterMaster> iMDparameterMaster
            , IGenericService<RoleMaster> iMDrolerMaster, IGenericService<MDoctorNotesTemplateMaster> iMDrNote, IGenericService<Constants> iconstant,
              IGenericService<MenuMaster> iMDmenuMaster,
              IGenericService<MDoseMaster> IMDdoseMaster,
              IGenericService<MPresTemplateH> IMDPresTemplateH,
              IGenericService<MOpcasepaperDignosisMaster> IMDOPCasepaperDignosisMaster, IGenericService<MReportTemplateConfig> IMReportTemplateConfig,
              IGenericService<MTalukaMaster> IMTalukaMaster,
              IGenericService<MModeOfDischarge> iMModeofdischarge,
              IGenericService<MSupplierMaster> iMSupplierMaster, IGenericService<MExpensesHeadMaster> iMExphede,
              IGenericService<MConstant> iMConstant, IGenericService<MRadiologyTemplateMaster> iMRadioTemp, IGenericService<MCanItemMaster> iMCanteen,
              IGenericService<MConsentMaster> iMConsentMaster
              )
        {
            _IAreaService = areaservice;
            _IPrefixService = iPrefixService;
            _IGenderService = iGenderService;
            _IRelationshipMaster = iRelationshipMaster;
            _IMaritalStatusMaster = iMaritalStatusMaster;
            _IMreligionMaster = iMreligionMaster;
            _IPatientTypeMaster = iPatientTypeMaster;
            _TariffMaster = tariffMaster;
            _IMDepartmentMaster = iMDepartmentMaster;
            _IMDoctorMaster = iDoctorMaster;
            _IMDoPurposeMaster = iMDoPurposeMaster;
            _IMCityService = iMDoCityMaster;
            _IMStateService = iMDoStateMaster;
            _IMCountryService = iMDoCountryMaster;
            _IMClassService = iMDoClassMaster;
            _IMCompanysService = iMDoCompanyMaster;
            _IMSubCompanysService = iMDoSubCompanyMaster;

            _IMBedService = iMDoBedMaster;
            _IMRoomService = iMDoRoomMaster;
            _IMRelationshipService = iMDoRelationshipMaster;
            _IMServiceService = iMDoServiceMaster;
            _IMItemService = iMDoItemMaster;
            _IMHospitalService = iMDoHospitalMaster;
            _IMDischargetypelService = iMDoDischargetypelMaster;
            _IMModeofpaymentService = iMDoModeofpaymentMaster;
            _IMbankService = iMDoBankMaster;
            _IMStoreService = iMDoStoreMaster;
            _IMTermofpaymentService = iMDoTemofpaymentMaster;
            _IMCashcounterService = iMDoCashcounterMaster;
            _IMDoctorTypeService = iMDoDoctorTyperMaster;
            _IMradioCateggoryService = iMDoradiologycateMaster;
            _IMpathCateggoryService = iMDopathcateMaster;
            _IMpathunitService = iMDpathunitMaster;
            _IMcompanytypeService = iMDcompanytypeMaster;
            _IMgroupService = iMDgroupMaster;
            _IMsubgroupService = iMDsubgroupMaster;
            _IMtemplateService = iMDtemplateMaster;

            _IMItemclassService = iMDItemClassMaster;
            _IMItemcategoryService = iMDItemCategoryMaster;
            _IMItemtypeService = iMDItemTypeMaster;
            _IMItemgenericService = iMDItemgenericeMaster;
            _IMCurrencyeService = iMDCurrencyMaster;
            _IMItemDrugtypeService = iMDItemdrugtypeMaster;
            _IMItemManufService = iMDItemanufMaster;
            _IMUnitOfMeasurmentService = iMDunitofmeasurementMaster;
            _IMConcessService = iMDConcessionMaster;
            _IMNurNoteervice = iMDnurNoteMaster;
            _IMparameterservice = iMDparameterMaster;
            _IMRoleMasterservice = iMDrolerMaster;
            _IMmenuMasterService = iMDmenuMaster;
            _IMDoseMaster = IMDdoseMaster;
            _IMPresTemplateH = IMDPresTemplateH;
            _IMOPCasepaperDignosisMaster = IMDOPCasepaperDignosisMaster;
            _IMReportConfTemplate = IMReportTemplateConfig;
            _IMTalukaMaster = IMTalukaMaster;
            _IMModeofdischarge = iMModeofdischarge;
            _IMSupplierMaster = iMSupplierMaster;
            _IMConstant = iMConstant;
            _IMradiotemp = iMRadioTemp;
            _IMcanteen = iMCanteen;
            _IMDrnote = iMDrNote;
            _IMExpHeade = iMExphede;
            _MConsentMaster = iMConsentMaster;
        }

        [HttpGet]
        [Route("[action]")]
        //[Permission]
        public async Task<ActionResult> GetBindDropDown(string mode, int? Id)
        {
            IList<SelectListItem> Result = mode switch
            {
                "Hospital" => (await _IMHospitalService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(HospitalMaster.HospitalId), nameof(HospitalMaster.HospitalName)),

                "Area" => (await _IAreaService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MAreaMaster.AreaId), nameof(MAreaMaster.AreaName)),
                "Prefix" => (await _IPrefixService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixName), nameof(DbPrefixMaster.SexId)),
                "PrefixByGender" => (await _IPrefixService.GetAll(x => x.IsActive.Value && x.SexId == Id.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixName), nameof(DbPrefixMaster.SexId)),
                "GenderByPrefix" => (await _IGenderService.GetAll(x => x.IsActive.Value && x.GenderId == Id.Value)).ToList().ToDropDown(nameof(DbGenderMaster.GenderId), nameof(DbGenderMaster.GenderName)),
                "Gender" => (await _IGenderService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbGenderMaster.GenderId), nameof(DbGenderMaster.GenderName)),
                "Relationship" => (await _IRelationshipMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRelationshipMaster.RelationshipId), nameof(MRelationshipMaster.RelationshipName)),
                "MaritalStatus" => (await _IMaritalStatusMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MMaritalStatusMaster.MaritalStatusId), nameof(MMaritalStatusMaster.MaritalStatusName)),
                "Taluka" => (await _IMTalukaMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MTalukaMaster.TalukaId), nameof(MTalukaMaster.TalukaName)),
                "City" => (await _IMCityService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MCityMaster.CityId), nameof(MCityMaster.CityName)),
                "State" => (await _IMStateService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MStateMaster.StateId), nameof(MStateMaster.StateName)),
                "Country" => (await _IMCountryService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MCountryMaster.CountryId), nameof(MCountryMaster.CountryName)),
                "StateByCity" => (await _IMStateService.GetAll(x => x.IsActive.Value && x.StateId == Id.Value)).ToList().ToDropDown(nameof(MStateMaster.StateId), nameof(MStateMaster.StateId)),
                "CountryByState" => (await _IMCountryService.GetAll(x => x.CountryId == Id.Value)).ToList().ToDropDown(nameof(MCountryMaster.CountryId), nameof(MCountryMaster.CountryId)),
                "Religion" => (await _IMreligionMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MReligionMaster.ReligionId), nameof(MReligionMaster.ReligionName)),
                "PatientType" => (await _IPatientTypeMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(PatientTypeMaster.PatientTypeId), nameof(PatientTypeMaster.PatientType)),
                "Tariff" => (await _TariffMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(TariffMaster.TariffId), nameof(TariffMaster.TariffName)),
                "Department" => (await _IMDepartmentMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MDepartmentMaster.DepartmentId), nameof(MDepartmentMaster.DepartmentName)),
               
                "RefDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsRefDoc.Value)).Select(x => new
                {
                    DoctorId = x.DoctorId,
                    FirstName = x.FirstName + " " + x.LastName // Concatenate FirstName and LastName
                })
                .ToList().ToDropDown("DoctorId", "FirstName"),

                "ConDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsConsultant.Value))
                .Select(x => new
                {
                    DoctorId = x.DoctorId,
                    FirstName = x.FirstName + " " + x.LastName // Concatenate FirstName and LastName
                })
                .ToList()
                .ToDropDown("DoctorId", "FirstName"), // Use the concatenated FullName for the dropdown


                //"ConDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsConsultant.Value)).ToList().ToDropDown(nameof(DoctorMaster.DoctorId), nameof(DoctorMaster.FirstName)),

                "DoctorType" => (await _IMDoctorTypeService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DoctorTypeMaster.Id), nameof(DoctorTypeMaster.DoctorType)),
                //"PathologyDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsConsultant.Value)).ToList().ToDropDown(nameof(DoctorMaster.DoctorId), nameof(DoctorMaster.FirstName)),
                "Class" => (await _IMClassService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(ClassMaster.ClassId), nameof(ClassMaster.ClassName)),
                "Bed" => (await _IMBedService.GetAll(x => x.IsAvailible.Value)).ToList().ToDropDown(nameof(Bedmaster.BedId), nameof(Bedmaster.BedName)),
                "Room" => (await _IMRoomService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(RoomMaster.RoomId), nameof(RoomMaster.RoomName)),
                "Company" => (await _IMCompanysService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(CompanyMaster.CompanyId), nameof(CompanyMaster.CompanyName)),
                "SubCompany" => (await _IMSubCompanysService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MSubTpacompanyMaster.SubCompanyId), nameof(MSubTpacompanyMaster.CompanyName)),
                "CompanyType" => (await _IMcompanytypeService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(CompanyTypeMaster.CompanyTypeId), nameof(CompanyTypeMaster.TypeName)),
                "RelationShip" => (await _IMRelationshipService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRelationshipMaster.RelationshipId), nameof(MRelationshipMaster.RelationshipName)),
                "Service" => (await _IMServiceService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(ServiceMaster.ServiceId), nameof(ServiceMaster.ServiceName)),
                "Item" => (await _IMItemService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemMaster.ItemId), nameof(MItemMaster.ItemName)),
                "DichargeType" => (await _IMDischargetypelService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DischargeTypeMaster.DischargeTypeId), nameof(DischargeTypeMaster.DischargeTypeName)),
                "Bank" => (await _IMbankService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MBankMaster.BankId), nameof(MBankMaster.BankName)),
                "TermofPayment" => (await _IMTermofpaymentService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MTermsOfPaymentMaster.Id), nameof(MTermsOfPaymentMaster.TermsOfPayment)),
                "Store" => (await _IMStoreService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MStoreMaster.StoreId), nameof(MStoreMaster.StoreName)),

                "PaymentMode" => (await _IMModeofpaymentService.GetAll(x => x.IsActive)).ToList().ToDropDown(nameof(MModeOfPayment.Id), nameof(MModeOfPayment.ModeOfPayment)),

                "PathCategory" => (await _IMpathCateggoryService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MPathCategoryMaster.CategoryId), nameof(MPathCategoryMaster.CategoryName)),
                "RadioCategory" => (await _IMradioCateggoryService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRadiologyCategoryMaster.CategoryId), nameof(MPathCategoryMaster.CategoryName)),
                "Unit" => (await _IMpathunitService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MPathUnitMaster.UnitId), nameof(MPathUnitMaster.UnitName)),
                "GroupName" => (await _IMgroupService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(GroupMaster.GroupId), nameof(GroupMaster.GroupName)),
                "SubGroupName" => (await _IMsubgroupService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MSubGroupMaster.SubGroupId), nameof(MSubGroupMaster.SubGroupName)),
                "Template" => (await _IMtemplateService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MTemplateMaster.TemplateId), nameof(MTemplateMaster.TemplateName)),
                "RadioTemplate" => (await _IMradiotemp.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRadiologyTemplateMaster.TemplateId), nameof(MRadiologyTemplateMaster.TemplateName)),

                "ItemClass" => (await _IMItemclassService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemClassMaster.ItemClassId), nameof(MItemClassMaster.ItemClassName)),
                "ItemCategory" => (await _IMItemcategoryService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemCategoryMaster.ItemCategoryId), nameof(MItemCategoryMaster.ItemCategoryName)),
                "ItemGeneric" => (await _IMItemgenericService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemGenericNameMaster.ItemGenericNameId), nameof(MItemGenericNameMaster.ItemGenericName)),
                "ItemType" => (await _IMItemtypeService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemTypeMaster.ItemTypeId), nameof(MItemTypeMaster.ItemTypeName)),
                "Currency" => (await _IMCurrencyeService.GetAll()).ToList().ToDropDown(nameof(MCurrencyMaster.CurrencyId), nameof(MCurrencyMaster.CurrencyName)),
                "ItemDrugType" => (await _IMItemDrugtypeService.GetAll(x => x.IsDeleted.Value)).ToList().ToDropDown(nameof(MItemDrugTypeMaster.ItemDrugTypeId), nameof(MItemDrugTypeMaster.DrugTypeName)),
                "UnitOfMeasurment" => (await _IMUnitOfMeasurmentService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MUnitofMeasurementMaster.UnitofMeasurementId), nameof(MUnitofMeasurementMaster.UnitofMeasurementName)),
                "ItemManufacture" => (await _IMItemManufService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemManufactureMaster.ItemManufactureId), nameof(MItemManufactureMaster.ManufactureName)),

                "SupplierMaster" => (await _IMSupplierMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MSupplierMaster.SupplierId), nameof(MSupplierMaster.SupplierName), nameof(MSupplierMaster.Gstno)),
                "GstCalcType" => (await _IMConstant.GetAll(x => x.IsActive.Value && x.ConstantType == "GST_CALC_TYPE")).ToList().ToDropDown(nameof(MConstant.ConstantId), nameof(MConstant.Name)),

                "CashCounter" => (await _IMCashcounterService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(CashCounter.CashCounterId), nameof(CashCounter.CashCounterName)),
                "Purpose" => (await _IMDoPurposeMaster.GetAll(x => (x.IsActive ?? 0) == 1)).ToList().ToDropDown(nameof(DbPurposeMaster.PurposeId), nameof(DbPurposeMaster.PurposeName)),
                "NurNote" => (await _IMNurNoteervice.GetAll()).ToList().ToDropDown(nameof(MNursingTemplateMaster.NursingId), nameof(MNursingTemplateMaster.NursTempName)),
                "DoctorNote" => (await _IMDrnote.GetAll()).ToList().ToDropDown(nameof(MDoctorNotesTemplateMaster.DocNoteTempId), nameof(MDoctorNotesTemplateMaster.DocsTempName)),

                "Parameter" => (await _IMparameterservice.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MPathParameterMaster.ParameterId), nameof(MPathParameterMaster.ParameterName)),
                "MenuMain" => (await _IMmenuMasterService.GetAll()).ToList().ToDropDown(nameof(MenuMaster.Id), nameof(MenuMaster.LinkName)),

                "Role" => (await _IMRoleMasterservice.GetAll()).ToList().ToDropDown(nameof(RoleMaster.RoleId), nameof(RoleMaster.RoleName)),
                "WebRole" => (await _IMRoleMasterservice.GetAll()).ToList().ToDropDown(nameof(RoleMaster.RoleId), nameof(RoleMaster.RoleName)),

                "DoseMaster" => (await _IMDoseMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MDoseMaster.DoseId), nameof(MDoseMaster.DoseName)),
                "PrescriptionTemplateMaster" => (await _IMPresTemplateH.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MPresTemplateH.PresId), nameof(MPresTemplateH.PresTemplateName)),
                "CasepaperDignosis" => (await _IMOPCasepaperDignosisMaster.GetAll()).ToList().ToDropDown(nameof(MOpcasepaperDignosisMaster.DescriptionType), nameof(MOpcasepaperDignosisMaster.DescriptionName)),
                "DischargeTemplate" => (await _IMReportConfTemplate.GetAll()).ToList().ToDropDown(nameof(MReportTemplateConfig.TemplateId), nameof(MReportTemplateConfig.TemplateName)),
                "ModeOfDischarge" => (await _IMModeofdischarge.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MModeOfDischarge.ModeOfDischargeId), nameof(MModeOfDischarge.ModeOfDischargeName)),

                "Concession" => (await _IMConcessService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MConcessionReasonMaster.ConcessionId), nameof(MConcessionReasonMaster.ConcessionReason)),
                //"CanteenItem" => (await _IMcanteen.GetAll().ToList().ToDropDown(nameof(MCanItemMaster.Ca), nameof(MConcessionReasonMaster.ConcessionReason)),
                //Create by Ashu 27 May 2025
                "ConsentMaster" => (await _MConsentMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MConsentMaster.ConsentId), nameof(MConsentMaster.ConsentName)),
                

                 "DailyExpHeade" => (await _IMExpHeade.GetAll(x => x.IsDeleted.Value)).ToList().ToDropDown(nameof(MExpensesHeadMaster.ExpHedId), nameof(MExpensesHeadMaster.HeadName)),
                "LogSource" => CommonExtensions.ToSelectListItems(typeof(EnmSalesApprovalStartMeterType)),
                _ => new List<SelectListItem>()
            };
            return Result.Select(x => new { x.Value, x.Text }).ToResponse("Get Data Successfully.");
        }
    }
}
