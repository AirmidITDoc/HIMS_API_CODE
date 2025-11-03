using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DoctorPayout;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.DoctorPayout;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("DoctorshareBillList")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> DotorshrebillList(GridRequestModel objGrid)
        {
            IPagedList<DoctorShareListDto> DoctorList = await _IDoctorPayService.GetLists(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorShareList"));
        }
        [HttpPost("DoctorshareListByName")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> DotorshreListbyname(GridRequestModel objGrid)
        {
            IPagedList<DoctorShareLbyNameListDto> DoctorList = await _IDoctorPayService.GetList1(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorShareByName"));
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
        [HttpPost("DoctorShrCalcAsPerReferDocVisitBillWise")]
        //[Permission(PageCode = "TAdditionalDocPay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(CalcAsPerReferDocVisitBillWiseModel obj)
        {
            AddCharge model = obj.MapTo<AddCharge>();
            if (obj.BillNo != 0)
            {
               
                await _IDoctorPayService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPost("DoctorProcessedList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> getDoctorprocessedlist(GridRequestModel objGrid)
        {
            IPagedList<DoctorShareprocessListDto> DoctorProcessList = await _IDoctorPayService.GetDoctorProcessList(objGrid);
            return Ok(DoctorProcessList.ToGridResponse(objGrid, "DoctorProcessList"));
        }



    }
}
