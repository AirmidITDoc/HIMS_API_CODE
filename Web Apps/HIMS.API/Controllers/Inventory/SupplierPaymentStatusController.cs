using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SupplierPaymentStatusController : BaseController
    {
        private readonly ISupplierPaymentStatusService _SupplierPaymentStatusService;
        public SupplierPaymentStatusController(ISupplierPaymentStatusService repository)
        {
            _SupplierPaymentStatusService = repository;
        }
        
        [HttpPost("supplierPaymentList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<SupplierNamelistDto> SupplierNamelist = await _SupplierPaymentStatusService.SupplierNamelist(objGrid);
            return Ok(SupplierNamelist.ToGridResponse(objGrid, "SupplierName   List"));
        }

        [HttpPost("Insert")]
     //   [Permission(PageCode = "GRNReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(TGRNSupPayment obj)
        {
            TGrnsupPayment model = obj.GrnsupPayment.MapTo<TGrnsupPayment>();
            List<TGrnheader> model1 = obj.GRN.MapTo<List<TGrnheader>>();
            List<TSupPayDet> Model2 = obj.SupPayDet.MapTo<List<TSupPayDet>>();
            if (obj.GrnsupPayment.SupPayId == 0)
            {
                model.SupPayDate = Convert.ToDateTime(obj.GrnsupPayment.SupPayDate);
                model.SupPayTime = Convert.ToDateTime(obj.GrnsupPayment.SupPayTime);
                model.IsAddedBy = CurrentUserId;
                //   model.UpdatedBy = 0;
                await _SupplierPaymentStatusService.InsertAsyncSP(model,model1, Model2, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SupplierName added successfully.");
        }


    }
}
