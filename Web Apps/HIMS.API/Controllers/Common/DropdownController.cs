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
        private readonly IGenericService<MClassMaster> _IMClassService;
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
        



        public DropdownController(IGenericService<MAreaMaster> areaservice, IGenericService<DbPrefixMaster> iPrefixService, IGenericService<DbGenderMaster> iGenderService, IGenericService<MRelationshipMaster> iRelationshipMaster, 
                                  IGenericService<MMaritalStatusMaster> iMaritalStatusMaster, IGenericService<MReligionMaster> iMreligionMaster, IGenericService<PatientTypeMaster> iPatientTypeMaster, IGenericService<TariffMaster> tariffMaster, 
                                  IGenericService<MDepartmentMaster> iMDepartmentMaster, IGenericService<DoctorMaster> iDoctorMaster, IGenericService<DbPurposeMaster> iMDoPurposeMaster, IGenericService<MCityMaster> iMDoCityMaster
            , IGenericService<MStateMaster> iMDoStateMaster, IGenericService<MCountryMaster> iMDoCountryMaster, IGenericService<MClassMaster> iMDoClassMaster, IGenericService<CompanyMaster> iMDoCompanyMaster
                                  , IGenericService<MSubTpacompanyMaster> iMDoSubCompanyMaster, IGenericService<Bedmaster> iMDoBedMaster, IGenericService<RoomMaster> iMDoRoomMaster,
                                 IGenericService<MRelationshipMaster> iMDoRelationshipMaster, IGenericService<ServiceMaster> iMDoServiceMaster, IGenericService<MItemMaster> iMDoItemMaster
            , IGenericService<HospitalMaster> iMDoHospitalMaster, IGenericService<DischargeTypeMaster> iMDoDischargetypelMaster, IGenericService<MModeOfPayment> iMDoModeofpaymentMaster
            , IGenericService<MBankMaster> iMDoBankMaster, IGenericService<MStoreMaster> iMDoStoreMaster, IGenericService<MTermsOfPaymentMaster> iMDoTemofpaymentMaster
            , IGenericService<CashCounter> iMDoCashcounterMaster, IGenericService<DoctorTypeMaster> iMDoDoctorTyperMaster)
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

        }

        [HttpGet]
        [Route("[action]")]
        //[Permission]
        public async Task<ActionResult> GetBindDropDown(string mode, int? Id)
        {
            IList<SelectListItem> Result = mode switch
            {
                //"Hospital" => (await _IMHospitalService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(HospitalMaster.HospitalId), nameof(HospitalMaster.HospitalName)),

                "Area" => (await _IAreaService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MAreaMaster.AreaId), nameof(MAreaMaster.AreaName)),
                "Prefix" => (await _IPrefixService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixName), nameof(DbPrefixMaster.SexId)),
                "PrefixByGender" => (await _IPrefixService.GetAll(x => x.IsActive.Value && x.SexId == Id.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixName), nameof(DbPrefixMaster.SexId)),
                "GenderByPrefix" => (await _IGenderService.GetAll(x => x.IsActive.Value && x.GenderId == Id.Value)).ToList().ToDropDown(nameof(DbGenderMaster.GenderId), nameof(DbGenderMaster.GenderName)),
                "Gender" => (await _IGenderService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbGenderMaster.GenderId), nameof(DbGenderMaster.GenderName)),
                "Relationship" => (await _IRelationshipMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRelationshipMaster.RelationshipId), nameof(MRelationshipMaster.RelationshipName)),
                "MaritalStatus" => (await _IMaritalStatusMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MMaritalStatusMaster.MaritalStatusId), nameof(MMaritalStatusMaster.MaritalStatusName)),
                "City" => (await _IMCityService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MCityMaster.CityId), nameof(MCityMaster.CityName)),
                "State" => (await _IMStateService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MStateMaster.StateId), nameof(MStateMaster.StateName)),
                "StateByCity" => (await _IMStateService.GetAll(x => x.IsActive.Value && x.StateId == Id.Value)).ToList().ToDropDown(nameof(MStateMaster.StateId), nameof(MStateMaster.StateId)),
                "CountryByState" => (await _IMCountryService.GetAll(x => x.CountryId == Id.Value)).ToList().ToDropDown(nameof(MCountryMaster.CountryId), nameof(MCountryMaster.CountryId)),

                "Religion" => (await _IMreligionMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MReligionMaster.ReligionId), nameof(MReligionMaster.ReligionName)),
                "PatientType" => (await _IPatientTypeMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(PatientTypeMaster.PatientTypeId), nameof(PatientTypeMaster.PatientType)),
                "Tariff" => (await _TariffMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(TariffMaster.TariffId), nameof(TariffMaster.TariffName)),
                "Department" => (await _IMDepartmentMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MDepartmentMaster.DepartmentId), nameof(MDepartmentMaster.DepartmentName)),
                "RefDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsRefDoc.Value)).ToList().ToDropDown(nameof(DoctorMaster.DoctorId), nameof(DoctorMaster.FirstName)),
                "ConDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsConsultant.Value)).ToList().ToDropDown(nameof(DoctorMaster.DoctorId), nameof(DoctorMaster.FirstName)),
                "DoctorType" => (await _IMDoctorMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DoctorTypeMaster.Id), nameof(DoctorTypeMaster.DoctorType)),

                "PathologyDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsConsultant.Value)).ToList().ToDropDown(nameof(DoctorMaster.DoctorId), nameof(DoctorMaster.FirstName)),

                "Class" => (await _IMClassService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(ClassMaster.ClassId), nameof(ClassMaster.ClassName)),
                "Bed" => (await _IMBedService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(Bedmaster.BedId), nameof(Bedmaster.BedName)),
                "Room" => (await _IMRoomService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(RoomMaster.RoomId), nameof(RoomMaster.RoomName)),
                                    
                "Company" => (await _IMCompanysService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(CompanyMaster.CompanyId), nameof(CompanyMaster.CompanyName)),
                "SubCompany" => (await _IMSubCompanysService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MSubTpacompanyMaster.SubCompanyId), nameof(MSubTpacompanyMaster.CompanyName)),
                "RelationShip" => (await _IMRelationshipService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRelationshipMaster.RelationshipId), nameof(MRelationshipMaster.RelationshipName)),
                "Service" => (await _IMServiceService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(ServiceMaster.ServiceId), nameof(ServiceMaster.ServiceName)),
                "Item" => (await _IMItemService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MItemMaster.ItemId), nameof(MItemMaster.ItemName)),
                "DichargeType" => (await _IMDischargetypelService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DischargeTypeMaster.DischargeTypeId), nameof(DischargeTypeMaster.DischargeTypeName)),

                "Bank" => (await _IMbankService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MBankMaster.BankId), nameof(MBankMaster.BankName)),
                "TermofPayment" => (await _IMTermofpaymentService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MTermsOfPaymentMaster.Id), nameof(MTermsOfPaymentMaster.TermsOfPayment)),
                "Store" => (await _IMStoreService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MStoreMaster.StoreId), nameof(MStoreMaster.StoreName)),

                "PaymentMode" => (await _IMModeofpaymentService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MModeOfPayment.Id), nameof(MModeOfPayment.ModeOfPayment)),
                "CashCounter" => (await _IMModeofpaymentService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(CashCounter.CashCounterId), nameof(CashCounter.CashCounterName)),
                
                // "Purpose" => (await _IMDoPurposeMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbPurposeMaster.PurposeId), nameof(DbPurposeMaster.PurposeName)),
                "LogSource" => CommonExtensions.ToSelectListItems(typeof(EnmSalesApprovalStartMeterType)),
                _ => new List<SelectListItem>()
            };
            return Result.Select(x => new { x.Value, x.Text }).ToResponse("Get Data Successfully.");
        }
    }
}
