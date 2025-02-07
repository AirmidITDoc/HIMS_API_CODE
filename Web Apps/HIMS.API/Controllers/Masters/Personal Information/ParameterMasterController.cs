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

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ParameterMasterController : BaseController
    {

        private readonly IParameterMasterService _IParameterMasterService;
        public ParameterMasterController(IParameterMasterService repository)
        {
            _IParameterMasterService = repository;
        }
        [HttpPost("MPathParameterList")]
        //   [Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathParameterMasterListDto> MPathParameterList = await _IParameterMasterService.GetListAsync(objGrid);
            return Ok(MPathParameterList.ToGridResponse(objGrid, "MPathParameter List"));
        }

        //Add API
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ParameterMasterModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();
           
            if (obj.ParameterId == 0)
            {
                model.IsActive = true;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _IParameterMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathParameterMaster Name added successfully.");
        }

        //Edit API
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ParameterMasterModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();
           
            if (obj.ParameterId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IsActive = true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _IParameterMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathParameterMaster  updated successfully.");
        }
        [HttpPost("ParameterCancel")]
        //[Permission(PageCode = "ParameterMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(CancelParameter obj)
        {
            MPathParameterMaster model = new();
            if (obj.ParameterId != 0)
            {
                model.ParameterId = obj.ParameterId;
               
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _IParameterMasterService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathParameterMaster Deleted successfully.");
        }

    }
}
