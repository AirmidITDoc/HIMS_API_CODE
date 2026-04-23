using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Services.Pharmacy;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Pharmacy;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class InPatientController : BaseController
    {
        private readonly IInPatientService _IInPatientService;
        public InPatientController(IInPatientService repository)
        {
            _IInPatientService = repository;
        }
        [HttpPost("SalesInPatientBillList")]
        //[Permission(PageCode = "InPatient", Permission = PagePermission.View)]
        public async Task<IActionResult> salesbrowselist(GridRequestModel objGrid)
        {
            IPagedList<SalesBillListDto> salesbrowselist = await _IInPatientService.salesbrowselist(objGrid);
            return Ok(salesbrowselist.ToGridResponse(objGrid, "Sales InPatient BillList"));
        }
        [HttpPost("SalesInPatientDetailsList")]
        //[Permission(PageCode = "InPatient", Permission = PagePermission.View)]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<InPatientSalesDetailsListDto> SalesBrowseDetailList = await _IInPatientService.Getsalesdetaillist(objGrid);
            return Ok(SalesBrowseDetailList.ToGridResponse(objGrid, "Sales InPatient Details List"));
        }
        [HttpPost("SalesInPatientReturnBillList")]
        //[Permission(PageCode = "InPatient", Permission = PagePermission.View)]
        public async Task<IActionResult> salesreturnlist(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnBillListDto> salesreturnlist = await _IInPatientService.salesreturnlist(objGrid);
            return Ok(salesreturnlist.ToGridResponse(objGrid, "Sales InPatient ReturnBil lList"));
        }
        [HttpPost("salesInPatientReturnBrowseDetaillist")]
        //[Permission(PageCode = "InPatient", Permission = PagePermission.View)]
        public async Task<IActionResult> salesInPatientReturnBrowseDetaillist(GridRequestModel objGrid)
        {
            IPagedList<SalesInPatientReturnDetailsListDto> salesInPatientReturnBrowseDetaillist = await _IInPatientService.salesreturndetaillist(objGrid);
            return Ok(salesInPatientReturnBrowseDetaillist.ToGridResponse(objGrid, "salesInPatient ReturnBrowseDetail list"));
        }

        [HttpPost("SaveSalesInpatient")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> SaveSalesInpatient(SaleReqModelInpatient obj)
        {
            TSalesInpatientHeader model = obj.Sales.MapTo<TSalesInpatientHeader>();
            List<TCurrentStock> CurrentStock = obj.TCurrentStock.MapTo<List<TCurrentStock>>();
            TIpPrescription modelPrescription = obj.Prescription.MapTo<TIpPrescription>();
            TSalesDraftHeader modelDraftHeader = obj.SalesDraft.MapTo<TSalesDraftHeader>();
            string stockmsg = "";
            foreach (var item in model.TSalesInpatientDetails)
            {
                var stock = await _IInPatientService.GetStock(item.StkId.Value);
                if (stock < item.Qty)
                {
                    stockmsg += item.BatchNo + " has only " + stock + " available stock. \n";
                }
            }
            if (!string.IsNullOrWhiteSpace(stockmsg))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, stockmsg);
            }
            if (obj.Sales.SalesId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Sales.Date);
                model.Time = Convert.ToDateTime(obj.Sales.Time);
                model.AddedBy = CurrentUserId;
                await _IInPatientService.InsertSalesInPatientAsyncSPC(model, CurrentStock, modelPrescription, modelDraftHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.SalesId);
        }
        [HttpPost("SalesReturnInPatient")]
        //[Permission(PageCode = "SalesReturn", Permission = PagePermission.Add)]
        public ApiResponse Insert(SalesReturnsModels obj)
        {
            TSalesInPatientReturnHeader model = obj.SalesReturn.MapTo<TSalesInPatientReturnHeader>();
            List<TSalesInPatientReturnDetail> model1 = obj.SalesReturnDetails.MapTo<List<TSalesInPatientReturnDetail>>();
            List<TCurrentStock> model2 = obj.CurrentStock.MapTo<List<TCurrentStock>>();
            List<TSalesDetail> model3 = obj.SalesDetail.MapTo<List<TSalesDetail>>();
            List<TIpprescriptionReturnH> model4 = obj.prescriptionReturn.MapTo<List<TIpprescriptionReturnH>>();
            TIpprescriptionReturnD model5 = new TIpprescriptionReturnD
            {
                PresDetailsId = obj.prescriptionReturn
                                  .FirstOrDefault()?.PresDetailsId ?? 0
            };


            if (obj.SalesReturn.SalesReturnId == 0)
            {
                model.Date = Convert.ToDateTime(obj.SalesReturn.Date);
                model.Time = Convert.ToDateTime(obj.SalesReturn.Time);
                _IInPatientService.InsertInPatient(model, model1, model2, model3, model4, model5,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.SalesReturnId);
        }

    }
}
