using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.API.Models.Radiology;

namespace HIMS.API.Controllers.Masters.Radiology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RadiologyTemplateController : BaseController
    {

        private readonly IGenericService<MRadiologyTemplateMaster> _repository;
        public RadiologyTemplateController(IGenericService<MRadiologyTemplateMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MRadiologyTemplateMaster> RadiologyTemplateMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RadiologyTemplateMasterList.ToGridResponse(objGrid, "RadiologyTemplateMaster List "));
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
            var data = await _repository.GetById(x => x.TemplateId == id);
            return data.ToSingleResponse<MRadiologyTemplateMaster, RadiologyTemplateModel>("MRadiologyTemplateMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(RadiologyTemplateModel obj)
        {
            MRadiologyTemplateMaster model = obj.MapTo<MRadiologyTemplateMaster>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RadiologyTemplateMaster  added successfully.");
        }
    }
}
