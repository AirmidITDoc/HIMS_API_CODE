using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathlogySampleCollectionController : BaseController
    {
        private readonly IPathlogySampleCollectionService _IPathlogySampleCollectionService;
        public PathlogySampleCollectionController(IPathlogySampleCollectionService repository)
        {
            _IPathlogySampleCollectionService = repository;
        }
        [HttpPut("PathlogySampleCollectionUpdate")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(PathlogySampleCollectionModel obj)
        {
            TPathologyReportHeader model = obj.MapTo<TPathologyReportHeader>();
            if (obj.PathReportId != 0)
            {
                model.PathDate = Convert.ToDateTime(obj.PathDate);
                model.PathTime = Convert.ToDateTime(obj.PathTime);
                //model.DateofBirth = Convert.ToDateTime(obj.DateOfBirth);
                //model.AddedBy = CurrentUserId;
                await _IPathlogySampleCollectionService.UpdateAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathlogySampleCollection Update successfully.");
        }

    }
}
