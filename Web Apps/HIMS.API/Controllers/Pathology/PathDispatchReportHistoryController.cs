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

namespace HIMS.API.Controllers.Pathology
{
    public class PathDispatchReportHistoryController : BaseController
    {
        private readonly IGenericService<TPathDispatchReportHistory> _repository;
        public PathDispatchReportHistoryController(IGenericService<TPathDispatchReportHistory> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TPathDispatchReportHistory> TPathDispatchReportHistoryList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TPathDispatchReportHistoryList.ToGridResponse(objGrid, "PathDispatchReportHistory List"));
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
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
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
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
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
