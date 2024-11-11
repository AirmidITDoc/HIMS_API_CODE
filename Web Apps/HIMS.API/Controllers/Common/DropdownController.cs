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
        public DropdownController(IGenericService<MAreaMaster> areaservice, IGenericService<DbPrefixMaster> iPrefixService, IGenericService<DbGenderMaster> iGenderService, IGenericService<MRelationshipMaster> iRelationshipMaster, 
                                  IGenericService<MMaritalStatusMaster> iMaritalStatusMaster, IGenericService<MReligionMaster> iMreligionMaster, IGenericService<PatientTypeMaster> iPatientTypeMaster, IGenericService<TariffMaster> tariffMaster, 
                                  IGenericService<MDepartmentMaster> iMDepartmentMaster, IGenericService<DoctorMaster> iDoctorMaster, IGenericService<DbPurposeMaster> iMDoPurposeMaster)
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
        }

        [HttpGet]
        [Route("[action]")]
        [Permission]
        public async Task<ActionResult> GetBindDropDown(string mode, int? Id)
        {
            IList<SelectListItem> Result = mode switch
            {
                "Area" => (await _IAreaService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MAreaMaster.AreaId), nameof(MAreaMaster.AreaName)),
                "Prefix" => (await _IPrefixService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixName)),
                "PrefixByGender" => (await _IPrefixService.GetAll(x => x.IsActive.Value && x.SexId == Id.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixName)),
                "Gender" => (await _IGenderService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbGenderMaster.GenderId), nameof(DbGenderMaster.GenderName)),
                "Relationship" => (await _IRelationshipMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MRelationshipMaster.RelationshipId), nameof(MRelationshipMaster.RelationshipName)),
                "MaritalStatus" => (await _IMaritalStatusMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MMaritalStatusMaster.MaritalStatusId), nameof(MMaritalStatusMaster.MaritalStatusName)),
                "Religion" => (await _IMreligionMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MReligionMaster.ReligionId), nameof(MReligionMaster.ReligionName)),
                "PatientType" => (await _IPatientTypeMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(PatientTypeMaster.PatientTypeId), nameof(PatientTypeMaster.PatientType)),
                "Tariff" => (await _TariffMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(TariffMaster.TariffId), nameof(TariffMaster.TariffName)),
                "Department" => (await _IMDepartmentMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MDepartmentMaster.DepartmentId), nameof(MDepartmentMaster.DepartmentName)),
                "RefDoctor" => (await _IMDoctorMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DoctorMaster.DoctorId), nameof(DoctorMaster.FirstName)),
                //"Purpose" => (await _IMDoPurposeMaster.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbPurposeMaster.PurposeId), nameof(DbPurposeMaster.PurposeName)),
                "LogSource" => CommonExtensions.ToSelectListItems(typeof(EnmSalesApprovalStartMeterType)),
                _ => new List<SelectListItem>()
            };
            return Result.Select(x => new { x.Value, x.Text }).ToResponse("Get Data Successfully.");
        }
    }
}
