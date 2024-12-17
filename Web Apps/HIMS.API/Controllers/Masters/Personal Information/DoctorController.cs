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
namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorController : BaseController
    {
        private readonly IDoctorMasterService _IDoctorMasterService;
        public DoctorController(IDoctorMasterService repository)
        {
            _IDoctorMasterService = repository;
        }
        [HttpPost("DoctorList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctorMaster> DoctorList = await _IDoctorMasterService.GetListAsync(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorList"));
        }
        [HttpGet("{id?}")]
        // [Permission(PageCode = "Bed", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _IDoctorMasterService.GetById(id);
            return data.ToSingleResponse<DoctorMaster, DoctorModel>("Doctor Master");
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth);
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.MahRegDate = Convert.ToDateTime(obj.MahRegDate);
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _IDoctorMasterService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name  added successfully.");
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                //model.DateofBirth = Convert.ToDateTime(obj.DateofBirth).Date;
                //model.RegDate = Convert.ToDateTime(obj.RegDate).Date;
                //model.MahRegDate = Convert.ToDateTime(obj.MahRegDate).Date;
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _IDoctorMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth);
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.MahRegDate = Convert.ToDateTime(obj.MahRegDate);
                await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name updated successfully.");
        }
    }
}
