using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CertificateTemplateController : BaseController
    {
        private readonly IGenericService<MCertificateTemplateMaster> _repository;
        public CertificateTemplateController(IGenericService<MCertificateTemplateMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "CertificateTemplateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCertificateTemplateMaster> MCertificateTemplateList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MCertificateTemplateList.ToGridResponse(objGrid, "CertificateTemplte List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "CertificateTemplateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TemplateId == id);
            return data.ToSingleResponse<MCertificateTemplateMaster, CertificateTemplateModel>("CertificateTemplate");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "CertificateTemplateMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CertificateTemplateModel obj)
        {
            MCertificateTemplateMaster model = obj.MapTo<MCertificateTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CertificateTemplate added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "CertificateTemplateMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CertificateTemplateModel obj)
        {
            MCertificateTemplateMaster model = obj.MapTo<MCertificateTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Certificate updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "CertificateTemplateMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MCertificateTemplateMaster model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Certificate Template deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



    }
}
