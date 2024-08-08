using Asp.Versioning;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;
using HIMS.Api.Controllers;
using HIMS.Services.Inventory;
using HIMS.Services.Common;
using HIMS.Data;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.API.Models.Masters;


namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyTemplateController : BaseController
    {

        private readonly IGenericService<MTemplateMaster> _repository;
        public PathologyTemplateController(IGenericService<MTemplateMaster> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        //[Permission(PageCode = "Gender", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(PathologyTemplateModel obj)
        {
            MTemplateMaster model = obj.MapTo<MTemplateMaster>();
            if (obj.TemplateId == 0)
            {
                await _repository.Add(model, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Template added successfully.");
        }

        [HttpPut("{id:int}")]
        //[Permission(PageCode = "Gender", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathologyTemplateModel obj)
        {
            MTemplateMaster model = obj.MapTo<MTemplateMaster>();
            model.IsDeleted = true;

            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                ////model.ModifiedBy = CurrentUserId;
                ////model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology  updated successfully.");
        }
    }
}
