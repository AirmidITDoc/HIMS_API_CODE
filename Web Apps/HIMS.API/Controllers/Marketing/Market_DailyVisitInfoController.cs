using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Marketing;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Marketing;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Marketing;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Marketing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class Market_DailyVisitInfoController : BaseController
    {
        private readonly IGenericService<TMarketingDailyVisitInformation> _repository;
        private readonly IMarketingService _IMarketingService;

        public Market_DailyVisitInfoController(IGenericService<TMarketingDailyVisitInformation> repository, IMarketingService repository1)
        {
            _repository = repository;

            _IMarketingService = repository1;
        }

        [HttpPost("MarketingAppVisitSummaryList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MarketingListDto> MarketingAppVisitSummaryList = await _IMarketingService.MarketingAsync(objGrid);
            return Ok(MarketingAppVisitSummaryList.ToGridResponse(objGrid, "MarketingAppVisitSummary List"));
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
