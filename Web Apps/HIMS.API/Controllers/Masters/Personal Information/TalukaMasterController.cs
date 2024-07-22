using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Core.Domain.Grid;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.API.Models;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    public class TalukaMasterController : BaseController
    {
        private readonly IGenericService<MTalukaMaster> _repository;
        public TalukaMasterController(IGenericService<MTalukaMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MTalukaMaster> TalukaMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TalukaMasterList.ToGridResponse(objGrid, "Taluka List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TalukaId == id);
            return data.ToSingleResponse<MTalukaMaster, TalukaMasterModel>("AreaMaster");
        }
        [HttpPost]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(TalukaMasterModel obj)
        {
            MTalukaMaster model = obj.MapTo<MTalukaMaster>();
            model.IsDeleted = true;
            if (obj.TalukaId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Taluka added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TalukaMasterModel obj)
        {
            MTalukaMaster model = obj.MapTo<MTalukaMaster>();
            model.IsDeleted = true;
            if (obj.TalukaId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Taluka updated successfully.");
        }


        //Delete API
        [HttpDelete]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MTalukaMaster model = await _repository.GetById(x => x.TalukaId == Id);
            if ((model?.TalukaId ?? 0) > 0)
            {
                model.IsDeleted = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Taluka deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
