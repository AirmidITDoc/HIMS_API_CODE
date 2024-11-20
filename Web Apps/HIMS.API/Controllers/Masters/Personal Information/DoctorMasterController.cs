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
using HIMS.API.Models.Inventory;
using HIMS.Services.Masters;
using HIMS.Data.DTO.Master;
namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorMasterController : BaseController
    {
        private readonly IDoctorMasterService _IDoctorMasterService;
        public DoctorMasterController(IDoctorMasterService repository)
        {
            _IDoctorMasterService = repository;
        }
        [HttpPost("DoctorList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctoreMasterDto> DoctorList = await _IDoctorMasterService.GetListAsync(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorList"));
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DoctorMasterModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                model.UpdatedBy = CurrentUserId;
                await _IDoctorMasterService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor  added successfully.");
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(DoctorMasterModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                model.UpdatedBy = CurrentUserId;
                await _IDoctorMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorMasterModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                model.UpdatedBy = CurrentUserId;
                await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor updated successfully.");
        }
    }
}
