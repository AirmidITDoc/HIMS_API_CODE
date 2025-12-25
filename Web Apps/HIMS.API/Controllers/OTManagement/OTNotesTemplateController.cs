using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OTNotesTemplateController : BaseController
    {
        private readonly IGenericService<MOtnotesTemplateMaster> _repository;

        public OTNotesTemplateController(IGenericService<MOtnotesTemplateMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MOtnotesTemplateMaster> MOtnotesTemplateList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MOtnotesTemplateList.ToGridResponse(objGrid, "MOtnotesTemplateList "));
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(OTNotesTemplateModel obj)
        {
            MOtnotesTemplateMaster model = obj.MapTo<MOtnotesTemplateMaster>();
            //model.IsActive = true;
            if (obj.OtnoteTempId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.Otdate = AppTime.Now;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTNotesTemplateModel obj)
        {
            MOtnotesTemplateMaster model = obj.MapTo<MOtnotesTemplateMaster>();
            //model.IsActive = true;
            if (obj.OtnoteTempId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;
                model.ModifiedBy = CurrentUserId;
                model.Otdate = AppTime.Now;
                model.ModifiedDate = AppTime.Now;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
    }
}
