using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ScheduleController : BaseController
    {
        private readonly IGenericService<ScheduleMaster> _repository;
        public ScheduleController(IGenericService<ScheduleMaster> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "Schedule", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ScheduleMaster> ScheduleList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ScheduleList.ToGridResponse(objGrid, "Schedule List"));
        }
        [HttpGet("{ScheduleName}")]
        //[Permission(PageCode = "Schedule", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(string ScheduleName)
        {
            ScheduleName = (string.IsNullOrWhiteSpace(ScheduleName) ? "" : ScheduleName);
            var data = await _repository.GetAll(x => x.IsActive == true && x.ScheduleName.Contains(ScheduleName));
            return new ApiResponse { StatusCode = 200, Data = data };
        }
        [HttpPost]
        //[Permission(PageCode = "Schedule", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(ScheduleModel obj)
        {
            ScheduleMaster model = obj.MapTo<ScheduleMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
            {
                model.IsActive = true;

                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Schedule added successfully.");
        }
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "Schedule", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ScheduleModel obj)
        {
            ScheduleMaster model = obj.MapTo<ScheduleMaster>();
            model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                await _repository.Update(model, CurrentUserId, CurrentUserName, null);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Schedule updated successfully.");
        }
        [HttpDelete]
        //[Permission(PageCode = "Schedule", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            ScheduleMaster model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = false;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Schedule deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }

}
