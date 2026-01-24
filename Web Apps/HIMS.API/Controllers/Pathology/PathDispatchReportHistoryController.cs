using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Pathology;
using Asp.Versioning;
using HIMS.Services.Pathlogy;
using HIMS.Data.DTO.Administration;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathDispatchReportHistoryController : BaseController
    {
        private readonly IGenericService<TPathDispatchReportHistory> _repository;
        private readonly IPathDispatchReportHistoryService _IPathDispatchReportHistoryService;

        public PathDispatchReportHistoryController(IGenericService<TPathDispatchReportHistory> repository, IPathDispatchReportHistoryService repository1)
        {
            _repository = repository;
            _IPathDispatchReportHistoryService = repository1;

        }
        [Permission]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.View)]
        public async Task<IActionResult> PathResultEntryList(GridRequestModel objGrid)
        {
            IPagedList<PathDispatchReportHistoryListDto> PathDispatchReportHistoryList = await _IPathDispatchReportHistoryService.GetListAsync(objGrid);
            return Ok(PathDispatchReportHistoryList.ToGridResponse(objGrid, "PathDispatchReportHistory List"));

        }
        
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DispatchId == id);
            return data.ToSingleResponse<TPathDispatchReportHistory, PathDispatchReportHistoryModel>("PathDispatchReportHistory");
        }
        //Add API
        [HttpPost]
        [Permission]
        public async Task<ApiResponse> Post(PathDispatchReportHistoryModel obj)
        {
            TPathDispatchReportHistory model = obj.MapTo<TPathDispatchReportHistory>();
            //model.IsActive = true;
            if (obj.DispatchId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(PathDispatchReportHistoryModel obj)
        {
            TPathDispatchReportHistory model = obj.MapTo<TPathDispatchReportHistory>();
            //model.IsActive = true;
            if (obj.DispatchId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
       
    }
}
