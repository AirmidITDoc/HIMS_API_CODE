using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.API.Models.IPPatient;

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
                model.Otdate = DateTime.Now;
                model.CreatedDate = DateTime.Now;
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
                model.Otdate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
    }
}
