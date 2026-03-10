using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PurchaseRequisitionFinalController : BaseController
    {
        private readonly IPurchaseRequisitionFinalService _IPurchaseRequisitionFinalService;
        private readonly IGenericService<TPrdetail> _repository;
        public PurchaseRequisitionFinalController(IPurchaseRequisitionFinalService repository, IGenericService<TPrdetail> repository1)
        {
            _IPurchaseRequisitionFinalService = repository;
            _repository = repository1;
        }

        [HttpPost("Insert")]
       // [Permission]
        public async Task<ApiResponse> Insert(PurchaseRequisitionFinalModel obj)
        {
            TPrheader model = obj.MapTo<TPrheader>();
            List<TPurchaseRequisitionHeader> objPurchaseRequisitionHeader = obj.Tpr.Select(x => new TPurchaseRequisitionHeader
            {
                PurchaseRequisitionId = (long)x.PrrequestHeaderId,
            }).ToList();
            if (obj.Prid == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPurchaseRequisitionFinalService.InsertAsync(model, objPurchaseRequisitionHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.Prid);
        }

        [HttpPost("PurchaseRequisitionFinalHeaderList")]
        [Permission]
        public async Task<IActionResult> PurchaseRequisitionFinalHeaderList(GridRequestModel objGrid)
        {
            IPagedList<PurchaseRequisitionFinalHeaderListDto> PurchaseRequisitionFinalHeaderList = await _IPurchaseRequisitionFinalService.PurchaseRequisitionFinalHeaderListAsync(objGrid);
            return Ok(PurchaseRequisitionFinalHeaderList.ToGridResponse(objGrid, "Purchase Requisition Final HeaderList"));
        }

        [HttpPost("PurchaseRequisitionFinalDetailList")]
        [Permission]
        public async Task<IActionResult> PurchaseRequisitionFinalDetailList(GridRequestModel objGrid)
        {
            IPagedList<PurchaseRequisitionFinalDetailListDto> PurchaseRequisitionFinalDetailList = await _IPurchaseRequisitionFinalService.PurchaseRequisitionFinalDetailListAsync(objGrid);
            return Ok(PurchaseRequisitionFinalDetailList.ToGridResponse(objGrid, "Purchase Requisition Final Detail List"));
        }

        [HttpPost("PRToPOInsert")]
        // [Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Add)]
        public async Task<ApiResponse> PRToPOInsert(List<PurchaseModel> objList)
        {
            foreach (var obj in objList)
            {
                TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();

                if (obj.PurchaseId == 0)
                {
                    model.PurchaseDate = Convert.ToDateTime(obj.PurchaseDate);
                    model.PurchaseTime = AppTime.Now;
                    model.AddedBy = CurrentUserId;
                    model.CreatedBy = CurrentUserId;
                    model.CreatedDate = AppTime.Now;
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = AppTime.Now;

                    await _IPurchaseRequisitionFinalService.PRToPOInsertAsync(model, CurrentUserId, CurrentUserName);
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records added successfully.");
        }
    }
}
