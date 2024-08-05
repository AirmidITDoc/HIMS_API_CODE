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
using HIMS.API.Models;

namespace HIMS.API.Controllers.Masters.Personal_Information
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TalukaMasterController1 : BaseController
    {
        private readonly IGenericService<MTalukaMaster> _repository;
        public TalukaMasterController1(IGenericService<MTalukaMaster> repository)
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
            return Ok(TalukaMasterList.ToGridResponse(objGrid, "TalukaMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TalukaId == id);
            return data.ToSingleResponse<MTalukaMaster, TalukaMasterModel1>("TalukaMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(TalukaMasterModel1 obj)
        {
            MTalukaMaster model = obj.MapTo<MTalukaMaster>();
            model.IsActive = true;
            if (obj.TalukaId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Taluka Name  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TalukaMasterModel1 obj)
        {
            MTalukaMaster model = obj.MapTo<MTalukaMaster>();
            model.IsActive = true;
            if (obj.TalukaId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Taluka Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "TalukaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MTalukaMaster model = await _repository.GetById(x => x.TalukaId == Id);
            if ((model?.TalukaId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Taluka Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
