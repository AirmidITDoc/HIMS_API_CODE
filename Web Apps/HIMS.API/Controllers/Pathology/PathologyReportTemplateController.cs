using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;



using HIMS.Api.Controllers;
using HIMS.Services.Inventory;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyReportTemplateController : BaseController
    {
        private readonly IIndentService _IIndentService;
        public PathologyReportTemplateController(IIndentService repository)
        {
            _IIndentService = repository;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(PathologyReportTemplateModel obj)
        //{
        //    T_PathologyReportTemplateDetails model = obj.MapTo<T_PathologyReportTemplateDetails>();
        //    if (obj.PathReportTemplateDetId == 0)
        //    {

        //        model.Addedby = CurrentUserId;
        //        await _IIndentService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathologyReport added successfully.");
        //}
    
    

