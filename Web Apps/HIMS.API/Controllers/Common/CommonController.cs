using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CommonController : BaseController
    {
        private readonly ICommonService _ICommonService;
        public CommonController(ICommonService commonRepository)
        {
            _ICommonService = commonRepository;
        }

        [HttpPost]
        [Route("{mode}")]
        public ApiResponse GetByProc(string mode, ListRequestModel model)
        {
            dynamic resultList = _ICommonService.GetDataSetByProc(mode, model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, mode+ " List.", (dynamic)resultList);
        }
    }
}
