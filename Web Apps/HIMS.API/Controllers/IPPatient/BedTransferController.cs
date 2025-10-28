using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
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
        [HttpPost("BedTransferDetailsList")]
        //[Permission(PageCode = "BedTransfer", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<BedTransferDetailListDto> BedTransferDetailList = await _IBedTransferService.BedTransferDetailList(objGrid);
            return Ok(BedTransferDetailList.ToGridResponse(objGrid, "BedTransferDetail  List"));
        }

        [HttpPost("InsertSP")]
        [Permission(PageCode = "BedTransfer", Permission = PagePermission.Add)]
        public ApiResponse Insert(BTransferModel obj)
        {
            TBedTransferDetail model = obj.BedTransfer.MapTo<TBedTransferDetail>();
            Bedmaster objbed = obj.BedTofreed.MapTo<Bedmaster>();
            Bedmaster ObjbedUpdate = obj.BedUpdate.MapTo<Bedmaster>();
            Admission objAdd = obj.Admssion.MapTo<Admission>();
            if (obj.BedTransfer.TransferId == 0)
            {
                model.FromTime = Convert.ToDateTime(obj.BedTransfer.FromTime);
                model.AddedBy = CurrentUserId;

                obj.BedTofreed.BedId = obj.BedTofreed.BedId;

                //objbed.AddedBy = CurrentUserId;

                obj.Admssion.AdmissionId = obj.Admssion.AdmissionId;
                _IBedTransferService.InsertSP(model, objbed, ObjbedUpdate, objAdd, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated successfully.");
        }

    }
}