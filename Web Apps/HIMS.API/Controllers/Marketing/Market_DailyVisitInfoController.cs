using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using Asp.Versioning;
using HIMS.API.Models.Marketing;

namespace HIMS.API.Controllers.Marketing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class Market_DailyVisitInfoController : BaseController
    {
        private readonly IGenericService<TMarketingDailyVisitInformation> _repository;
        public Market_DailyVisitInfoController(IGenericService<TMarketingDailyVisitInformation> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TMarketingDailyVisitInformation> MarketDailyVisitInfoList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MarketDailyVisitInfoList.ToGridResponse(objGrid, "Bank List"));
        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<TMarketingDailyVisitInformation, Market_DailyVisitInfoModel>("BankMaster");
        }
        [HttpPost]
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(Market_DailyVisitInfoModel obj)
        {
            TMarketingDailyVisitInformation model = obj.MapTo<TMarketingDailyVisitInformation>();
            if (obj.Id == 0)
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
        //[Permission(PageCode = "MarketingDailyVisit", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(Market_DailyVisitInfoModel obj)
        {
            TMarketingDailyVisitInformation model = obj.MapTo<TMarketingDailyVisitInformation>();
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
