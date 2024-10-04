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
        public DropdownController(IGenericService<MAreaMaster> areaservice, IGenericService<DbPrefixMaster> iPrefixService, IGenericService<DbGenderMaster> iGenderService)
        {
            _IAreaService = areaservice;
            _IPrefixService = iPrefixService;
            _IGenderService = iGenderService;
        }
        [HttpGet]
        [Route("[action]")]
        [Permission]
        public async Task<ActionResult> GetBindDropDown(string mode, int? Id)
        {
            IList<SelectListItem> Result = mode switch
            {
                "Area" => (await _IAreaService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(MAreaMaster.AreaId), nameof(MAreaMaster.AreaName)),
                "Prefix" => (await _IPrefixService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixId)),
                "PrefixByGender" => (await _IPrefixService.GetAll(x => x.IsActive.Value && x.SexId == Id.Value)).ToList().ToDropDown(nameof(DbPrefixMaster.PrefixId), nameof(DbPrefixMaster.PrefixId)),
                "Gender" => (await _IGenderService.GetAll(x => x.IsActive.Value)).ToList().ToDropDown(nameof(DbGenderMaster.GenderId), nameof(DbGenderMaster.GenderName)),
                "LogSource" => CommonExtensions.ToSelectListItems(typeof(EnmSalesApprovalStartMeterType)),
                _ => new List<SelectListItem>()
            };
            return Result.Select(x => new { x.Value, x.Text }).ToResponse("Get Data Successfully.");
        }
    }
}
