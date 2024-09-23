using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
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
               model.ChargesTime = Convert.ToDateTime(obj.ChargeTime);
                model.AddedBy = CurrentUserId;
                await _IOPAddchargesService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Addc Charges added successfully.");
        }
    }
}
