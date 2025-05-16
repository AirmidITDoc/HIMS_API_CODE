using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.Masters.SurgeryCategoryMasterController
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SurgeryCategoryMasterController : BaseController
    {
        private readonly IGenericService<MSurgeryCategoryMaster> _repository;
        public SurgeryCategoryMasterController(IGenericService<MSurgeryCategoryMaster> repository)
        {
            _repository = repository;
        }



        [HttpPost]
        [Route("[action]")]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSurgeryCategoryMaster> MSurgeryCategoryMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSurgeryCategoryMasterList.ToGridResponse(objGrid, "SurgeryCategory Master List"));
        }

        [HttpGet("{id?}")]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SurgeryCategoryId == id);
            return data.ToSingleResponse<MSurgeryCategoryMaster, SurgeryCategoryMasterModel>("MSurgeryCategoryMaster");
        }
        //Insert API
        [HttpPost]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SurgeryCategoryMasterModel obj)
        {
            MSurgeryCategoryMaster model = obj.MapTo<MSurgeryCategoryMaster>();
            model.IsActive = true;
            if (obj.SurgeryCategoryId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SurgeryCategory Master  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SurgeryCategoryMasterModel obj)
        {
            MSurgeryCategoryMaster model = obj.MapTo<MSurgeryCategoryMaster>();
            model.IsActive = true;
            if (obj.SurgeryCategoryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SurgeryCategory Master updated successfully.");
        }


        //Delete API
        [HttpDelete]
        //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSurgeryCategoryMaster? model = await _repository.GetById(x => x.SurgeryCategoryId == Id);
            if ((model?.SurgeryCategoryId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SurgeryCategory Master deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}