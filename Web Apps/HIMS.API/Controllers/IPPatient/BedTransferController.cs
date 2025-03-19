using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.OPPatient;
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

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "BedTransfer", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(BTransferModel obj)
        {
            TBedTransferDetail model = obj.BedTransfer.MapTo<TBedTransferDetail>();
            Bedmaster objbed = obj.BedTofreed.MapTo<Bedmaster>();
            Admission objAdd = obj.Admssion.MapTo<Admission>();
            if (obj.BedTransfer.TransferId == 0)
            {
                model.FromTime = Convert.ToDateTime(obj.BedTransfer.FromTime);
                model.AddedBy = CurrentUserId;

                obj.BedTofreed.BedId = obj.BedTofreed.BedId;

                //objbed.AddedBy = CurrentUserId;

                obj.Admssion.AdmissionId = obj.Admssion.AdmissionId;
                await _IBedTransferService.InsertAsyncSP(model, objbed, objAdd ,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "BedTransfer Updated successfully.");
        }

    }
}