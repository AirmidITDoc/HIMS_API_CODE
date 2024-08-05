using Asp.Versioning;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;
using HIMS.Api.Controllers;
using HIMS.Services.Inventory;


namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyTemplateController : BaseController
    {

        private readonly IPathologyTemplateService _IPathologyTemplateService;
        public PathologyTemplateController(IPathologyTemplateService repository)
        {
            _IPathologyTemplateService = repository;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathologyTemplateModel obj)
        {
            MTemplateMaster model = obj.MapTo<MTemplateMaster>();
            if (obj.TemplateId == 0)
            {
              await _IPathologyTemplateService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathologyTemplate added successfully.");
        }
    }
}
