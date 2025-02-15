using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ParameterDescriptiveMasterController : BaseController
    {

        private readonly IMParameterDescriptiveMasterService _MParameterDescriptiveMasterService;
        public ParameterDescriptiveMasterController(IMParameterDescriptiveMasterService repository)
        {
            _MParameterDescriptiveMasterService = repository;
        }
        //[HttpPost("MParameterDescriptiveMasterList")]
        ////   [Permission(PageCode = "MParameterDescriptiveMaster", Permission = PagePermission.View)]
        //public async Task<IActionResult> List(GridRequestModel objGrid)
        //{
        //    IPagedList<MParameterDescriptiveMasterListDto> MParameterDescriptiveMasterList = await _MParameterDescriptiveMasterService.GetListAsync(objGrid);
        //    return Ok(MParameterDescriptiveMasterList.ToGridResponse(objGrid, "MParameterDescriptiveMasterList List"));
        //}

        [HttpPost("MParameterDescriptiveMasterList")]
        //[Permission(PageCode = "MParameterDescriptiveMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> MParameterDescriptiveMasterList(GridRequestModel objGrid)
        {
            IPagedList<MParameterDescriptiveMasterListDto> MParameterDescriptiveMasterList = await _MParameterDescriptiveMasterService.GetListAsync1(objGrid);
            return Ok(MParameterDescriptiveMasterList.ToGridResponse(objGrid, "MParameterDescriptiveMaster  List"));
        }
    }
}
