using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPBillController : BaseController
    {
        private readonly IOPBillingService _oPBillingService;
        public OPBillController(IOPBillingService repository)
        {
            _oPBillingService = repository;
        }
        [HttpPost("OPBillingInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
            {
                model.BillDate = Convert.ToDateTime(obj.BillDate);
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _oPBillingService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.");
        }
    }
}
