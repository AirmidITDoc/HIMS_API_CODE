using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    public class DoctorMasterController : BaseController
    {
        private readonly IGenericService<DoctorMaster> _repository;
        public DoctorMasterController(IGenericService<DoctorMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctorMaster> DoctorMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DoctorMasterList.ToGridResponse(objGrid, "Doctor List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DoctorId == id);
            return data.ToSingleResponse<DoctorMaster, DoctorMasterModel>("DoctorMaster");
        }


        [HttpPost]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(DoctorMasterModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            model.IsActive = true;
            if (obj.DoctorId == 0)
            {
                //model.CreatedBy = CurrentUserId;
                //model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorMasterModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            model.IsActive = true;
            if (obj.DoctorId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            DoctorMaster model = await _repository.GetById(x => x.DoctorId == Id);
            if ((model?.DoctorId ?? 0) > 0)
            {
                model.IsActive = false;
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
