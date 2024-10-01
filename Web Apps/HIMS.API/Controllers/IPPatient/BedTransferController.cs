using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BedTransferController : BaseController
    {
        private readonly IBedTransferService _IBedTransferService;
        public BedTransferController(IBedTransferService repository)
        {
            _IBedTransferService = repository;
        }


        //[HttpPost("Insert")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertSP(BedTransferModel obj)
        //{
        //    TBedTransferDetail model = obj.MapTo<TBedTransferDetail>();

        //    model.FromTime = Convert.ToDateTime(obj.FromTime);
        //    model.ToTime = Convert.ToDateTime(obj.ToTime);
        //    model.AddedBy = CurrentUserId;

        //    await _IBedTransferService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
        //    //}
        //    //else
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "BedTransfer successfully.");
        //}

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(BedTransferModel obj)
        {
            TBedTransferDetail model = obj.MapTo<TBedTransferDetail>();
            if (obj.TransferId == 0)
            {
                model.FromTime = Convert.ToDateTime(obj.FromTime);
                model.AddedBy = CurrentUserId;
                await _IBedTransferService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "BedTransfer added successfully.");
        }


    }
    
}