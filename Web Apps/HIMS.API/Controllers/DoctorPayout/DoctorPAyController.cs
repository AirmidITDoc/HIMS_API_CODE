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
using HIMS.API.Models.Inventory;

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
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<DoctorPayListDto> DoctorPayList = await _IDoctorPayService.GetList(objGrid);
            return Ok(DoctorPayList.ToGridResponse(objGrid, "DoctorPayList"));
        }

        [HttpPost("DoctorBilldetailList")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.ViewDoctorPAy
        public async Task<IActionResult> DotorbilldetailList(GridRequestModel objGrid)
        {
            IPagedList<DoctorBilldetailListDto> DoctorList = await _IDoctorPayService.GetBillDetailList(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorBilldetailList"));
        }


        [HttpPost("DoctorPaySummaryList")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> DotorPaysummaryList(GridRequestModel objGrid)
        {
            IPagedList<DcotorpaysummaryListDto> DoctorpayList = await _IDoctorPayService.GetDoctroSummaryList(objGrid);
            return Ok(DoctorpayList.ToGridResponse(objGrid, "DoctorPaySummaryList"));
        }


        [HttpPost("DoctorsharSummarydetail")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]r
        public async Task<IActionResult> Dotorshresummarydetail(GridRequestModel objGrid)
        {
            IPagedList<DoctorPaysummarydetailListDto> DoctorList = await _IDoctorPayService.GetDoctorsummaryDetailList(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "Doctor Pay Summary detail"));
        }



        [HttpPost("Insert")]
        //[Permission(PageCode = "TAdditionalDocPay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSPD(DoctorPayModel obj)
        {
            TAdditionalDocPay model = obj.MapTo<TAdditionalDocPay>();
            if (obj.TranId == 0)
            {
                model.TranDate = Convert.ToDateTime(obj.TranDate);
                model.TranTime = Convert.ToDateTime(obj.TranTime);
                await _IDoctorPayService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("ShareDocAddCharges")]
        //[Permission(PageCode = "StockAdjustment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> GSTUpdate(ShareDoctAddCharges obj)
        {
            List<AddCharge> model = obj.ShareDoctAddCharge.MapTo<List<AddCharge>>();
            if (model.Count > 0)
            {
                //model.AddedBy = CurrentUserId;
                await _IDoctorPayService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record Update successfully.");
        }
        [HttpPost("DoctorPayoutProcess")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(DoctorPayoutProcessModel obj)
        {
            TDoctorPayoutProcessHeader model = obj.MapTo<TDoctorPayoutProcessHeader>();
            if (obj.DoctorPayoutId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IDoctorPayService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }


        [HttpPut("DoctorPayoutProcess")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorPayoutProcessModel obj)
        {
            TDoctorPayoutProcessHeader model = obj.MapTo<TDoctorPayoutProcessHeader>();
            if (obj.DoctorPayoutId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IDoctorPayService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
