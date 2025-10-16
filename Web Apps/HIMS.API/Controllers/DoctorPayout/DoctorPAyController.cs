using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Services.Users;
using HIMS.API.Models.DoctorPayout;
using HIMS.Services.DoctorPayout;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;

namespace HIMS.API.Controllers.DoctorPayout
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorPAyController : BaseController
    {
        private readonly IDoctorPayService _IDoctorPayService;
        public DoctorPAyController(IDoctorPayService repository)
        {
            _IDoctorPayService = repository;
        }
        [HttpPost("DoctorPayList")]
        //[Permission(PageCode = "TAdditionalDocPay", Permission = PagePermission.View)]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<DoctorPayListDto> DoctorPayList = await _IDoctorPayService.GetList(objGrid);
            return Ok(DoctorPayList.ToGridResponse(objGrid, "DoctorPayList"));
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "TAdditionalDocPay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSPD(DoctorPayModel obj)
        {
            TAdditionalDocPay model = obj.MapTo<TAdditionalDocPay>();
            if (obj.DocId == 0)
            {
                model.TranDate = Convert.ToDateTime(obj.TranDate);
                model.TranTime = Convert.ToDateTime(obj.TranTime);
                await _IDoctorPayService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
    }
}
