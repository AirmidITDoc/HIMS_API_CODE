using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Radiology;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Radiology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RadiologyTemplateController : BaseController
    {

        private readonly IGenericService<MRadiologyTemplateMaster> _repository;
        private readonly IGenericService<MRadiologyTemplateMaster> _radiorepository;
        private readonly IGenericService<TRadiologyReportHeader> _radiorepository2;

        public RadiologyTemplateController(IGenericService<MRadiologyTemplateMaster> repository, IGenericService<MRadiologyTemplateMaster> repository1, IGenericService<TRadiologyReportHeader> repository12)
        {
            _repository = repository;
            _radiorepository = repository1;
            _radiorepository2 = repository12;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "RadiologyTemplateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MRadiologyTemplateMaster> RadiologyTemplateMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RadiologyTemplateMasterList.ToGridResponse(objGrid, "RadiologyTemplate Master List "));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "RadiologyTemplateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TemplateId == id);
            return data.ToSingleResponse<MRadiologyTemplateMaster, RadiologyTemplateModel>("MRadiologyTemplateMaster");
        }

        //List API Get By Id
        [HttpGet("RadReportId/{id?}")]
        //[Permission(PageCode = "RadiologyTemplateMaster", Permission = PagePermission.View)]
        [Permission]

        public async Task<ApiResponse> RGet(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _radiorepository2.GetById(x => x.RadReportId == id);
            if (data == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "No data found.");
            }
            return data.ToSingleResponse<TRadiologyReportHeader, TRadiologyReportModel>("TRadiologyReportHeader");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "RadiologyTemplateMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(RadiologyTemplateModel obj)
        {
            MRadiologyTemplateMaster model = obj.MapTo<MRadiologyTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "RadiologyTemplateMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RadiologyTemplateModel obj)
        {
            MRadiologyTemplateMaster model = obj.MapTo<MRadiologyTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "RadiologyTemplateMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MRadiologyTemplateMaster model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


        [HttpGet]
        [Route("get-RdioTemplates")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MMasterList = await _radiorepository.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology Template dropdown", MMasterList.Select(x => new { x.TemplateId, x.TemplateName, x.TemplateDesc }));
        }


        //[HttpGet]
        //[Route("get-Templates")]
        ////[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        //public async Task<ApiResponse> GetTemplates()
        //{
        //    var MMasterList = await _radiorepository.GetAll();
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Template dropdown", MMasterList.Select(x => new { x.TemplateId, x.TemplateName, x.TemplateDescription }));
        //}

    }
}
