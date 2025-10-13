using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.API.Models.Pathology;

namespace HIMS.API.Controllers.Masters.PathologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyTemplateController : BaseController
    {
        private readonly IGenericService<MTemplateMaster> _repository;
        private readonly IGenericService<TPathologyReportTemplateDetail> _temprepository;

        public PathologyTemplateController(IGenericService<MTemplateMaster> repository, IGenericService<TPathologyReportTemplateDetail> repository1)
        {
            _repository = repository;
            _temprepository = repository1;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "TemplateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MTemplateMaster> TemplateMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TemplateMasterList.ToGridResponse(objGrid, "TemplateMasterList"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "TemplateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TemplateId == id);
            return data.ToSingleResponse<MTemplateMaster, PathologyTemplateModel>("TemplateMaster");
        }

        //List API Get By Id
        [HttpGet("PathReportId /{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetByPathReportId(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _temprepository.GetById(x => x.PathReportId == id);
            if (data == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "No data found.");
            }
            return data.ToSingleResponse<TPathologyReportTemplateDetail, PathologyReportTemplateModel>("TPathologyReportTemplateDetail");
        }

        //Add API
        [HttpPost]
        [Permission(PageCode = "TemplateMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PathologyTemplateModel obj)
        {
            MTemplateMaster model = obj.MapTo<MTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " TemplateName  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "TemplateMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathologyTemplateModel obj)
        {
            MTemplateMaster model = obj.MapTo<MTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TemplateName updated successfully.");
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "TemplateMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MTemplateMaster model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TemplateName deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
