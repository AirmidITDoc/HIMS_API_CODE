using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPBILLShilpaController : BaseController
    {
        private readonly IOPBillShilpaService _OPBillShilpaService;
        public OPBILLShilpaController(IOPBillShilpaService repository)
        {
            _OPBillShilpaService = repository;
        }
        [HttpPost("OPBillInsert")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPBillModelShilpa obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _OPBillShilpaService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.");
        }
    }
}

