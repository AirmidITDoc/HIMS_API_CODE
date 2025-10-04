using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CountryMasterController : BaseController
    {
        private readonly IGenericService<MCountryMaster> _repository;
        public CountryMasterController(IGenericService<MCountryMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CountryMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCountryMaster> CountryMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CountryMasterList.ToGridResponse(objGrid, "CountryMaster List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "CountryMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CountryId == id);
            return data.ToSingleResponse<MCountryMaster, CountryMasterModel>("CountryMaster");
        }

       
        [HttpPost]
        [Permission(PageCode = "CountryMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CountryMasterModel obj)
        {
            MCountryMaster model = obj.MapTo<MCountryMaster>();
            model.IsActive = true;
            if (obj.CountryId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "CountryMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CountryMasterModel obj)
        {
            MCountryMaster model = obj.MapTo<MCountryMaster>();
            model.IsActive = true;
            if (obj.CountryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "CountryMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MCountryMaster model = await _repository.GetById(x => x.CountryId == Id);
            if ((model?.CountryId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
