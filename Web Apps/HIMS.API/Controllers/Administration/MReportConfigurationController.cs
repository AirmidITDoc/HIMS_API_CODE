using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;

using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MReportConfigurationController : BaseController
    {
            private readonly IGenericService<MReportConfiguration> _repository;
            public MReportConfigurationController(IGenericService<MReportConfiguration> repository)
            {
                _repository = repository;
            }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ReportConfiguration", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MReportConfiguration> MReportConfigurationList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MReportConfigurationList.ToGridResponse(objGrid, "MReportConfiguration List "));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ReportConfiguration", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ReportId == id);
            return data.ToSingleResponse<MReportConfiguration, MReportConfigurationModel>("MReportConfiguration");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "ReportConfiguration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MReportConfigurationModel obj)
            {
            MReportConfiguration model = obj.MapTo<MReportConfiguration>();
                 model.IsActive = true;
                if (obj.ReportId == 0)
                {
                    model.CreatedBy = CurrentUserId;
                    model.CreatedOn = DateTime.Now;
                    await _repository.Add(model, CurrentUserId, CurrentUserName);
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MReportConfiguration  added successfully.");
            }

        [HttpPut("{id:int}")]
        //[Permission(PageCode = "ReportConfiguration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MReportConfigurationModel obj)
        {
            MReportConfiguration model = obj.MapTo<MReportConfiguration>();
           
            if (obj.ReportId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdateBy = CurrentUserId;
                model.UpdatedOn = DateTime.Now;
                model.IsActive = true;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "UpdateBy", "UpdatedOn" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MReportConfiguration  updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "ReportConfiguration", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MReportConfiguration model = await _repository.GetById(x => x.ReportId == Id);
            if ((model?.ReportId ?? 0) > 0)
            {
                model.IsActive = false;
                model.UpdateBy = CurrentUserId;
                model.UpdatedOn = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MReportConfiguration  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}

