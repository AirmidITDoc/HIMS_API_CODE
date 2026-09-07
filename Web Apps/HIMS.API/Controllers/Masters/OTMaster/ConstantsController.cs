using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OTManagement;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.OTMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class ConstantsController : BaseController
    {
        private readonly IConstantService _IConstantService;
        public ConstantsController(IConstantService repository)
        {
            _IConstantService = repository;

        }
        [HttpPost("ConstantsList")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ConstantsListDto> ConstantsList = await _IConstantService.GetListAsync(objGrid);
            return Ok(ConstantsList.ToGridResponse(objGrid, "Constants List "));
        }
        [HttpGet("search-ConstantsType")]
        [Permission]
        public ApiResponse SearchConstants(string Keyword)
        {
            var data = _IConstantService.SearchConstants(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Visit data", data);
        }

        [HttpPut("{id:int}")]
        [Permission]
        public ApiResponse Update(ConstantModel obj)
        {
            MConstant model = obj.MapTo<MConstant>();
            if (obj.ConstantId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;

                _IConstantService.Update(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
