using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Marketing;
using HIMS.Core.Domain.Grid;
using HIMS.Core;

namespace HIMS.API.Controllers.Marketing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class Market_HospitalMasterController : BaseController
    {

        private readonly IGenericService<MMarketingHospitalMaster> _repository;
        public Market_HospitalMasterController(IGenericService<MMarketingHospitalMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MMarketingHospitalMaster> MarketingHospitalList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MarketingHospitalList.ToGridResponse(objGrid, "Bank List"));
        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.HospitalId == id);
            return data.ToSingleResponse<MMarketingHospitalMaster, MarketHospitalMasterModel>("MarketingHospitalMaster");
        }
        [HttpPost]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MarketHospitalMasterModel obj)
        {
            MMarketingHospitalMaster model = obj.MapTo<MMarketingHospitalMaster>();
            model.IsActive = true;
            if (obj.HospitalId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MarketHospitalMasterModel obj)
        {
            MMarketingHospitalMaster model = obj.MapTo<MMarketingHospitalMaster>();
            model.IsActive = true;
            if (obj.HospitalId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MMarketingHospitalMaster model = await _repository.GetById(x => x.HospitalId == Id);
            if ((model?.HospitalId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
