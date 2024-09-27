using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPAddchargesController : BaseController
    {
        private readonly IOPAddchargesService _IOPAddchargesService;
        public OPAddchargesController(IOPAddchargesService repository)
        {
            _IOPAddchargesService = repository;
        }

        [HttpPost("OPAddchargesInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPAddchargesInsert(OPAddchargesModel obj)
        {
            AddCharge model = obj.MapTo<AddCharge>();
            if (obj.ChargesId == 0)
            {
              // model.ChargesTime = Convert.ToDateTime(obj.ChargesTime);
                model.AddedBy = CurrentUserId;
                model.ChargesDate = Convert.ToDateTime(model.ChargesDate);
                model.IsCancelledDate = Convert.ToDateTime(model.IsCancelledDate);
                model.ChargesTime = Convert.ToDateTime(model.ChargesTime);
                await _IOPAddchargesService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Addc Charges added successfully.");
        }

        [HttpPost("OPAddchargesDelete")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPAddchargesDelete(OPAddchargesModel obj)
        {
            AddCharge model = obj.MapTo<AddCharge>();
            if (obj.ChargesId != 0)
            {
               
                await _IOPAddchargesService.DeleteAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Addc Charges Deleted successfully.");
        }
    }
}
