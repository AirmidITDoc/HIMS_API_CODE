using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.API.Models.Pharmacy;
using HIMS.Services.Pharmacy;
using Asp.Versioning;
using HIMS.Services.IPPatient;
using HIMS.Core.Domain.Grid;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PurchaseRequisitionController : BaseController
    {
        private readonly IPurchaseRequisitionService _IPurchaseRequisitionService;
        private readonly IGenericService<TPurchaseRequisitionDetail> _repository;
        public PurchaseRequisitionController(IPurchaseRequisitionService repository, IGenericService<TPurchaseRequisitionDetail> repository1)
        {
            _IPurchaseRequisitionService = repository;
            _repository = repository1;
        }
        [HttpPost("PurchaseRequisitionHeaderList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PurchaseRequitionListDto> PurchaseRequisitionHeaderList = await _IPurchaseRequisitionService.GetListAsync(objGrid);
            return Ok(PurchaseRequisitionHeaderList.ToGridResponse(objGrid, "PurchaseRequisitionHeader List"));
        }
        //List API
        [HttpPost("PurchaseRequisitionDetailList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> ListP(GridRequestModel objGrid)
        {
            IPagedList<TPurchaseRequisitionDetail> PurchaseRequisitionDetailList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PurchaseRequisitionDetailList.ToGridResponse(objGrid, "PurchaseRequisitionDetail List"));
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PurchaseRequisitionModel obj)
        {
            TPurchaseRequisitionHeader model = obj.MapTo<TPurchaseRequisitionHeader>();
            model.IsActive = true;
            if (obj.PurchaseRequisitionId == 0)
            {
              
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.Addedby = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPurchaseRequisitionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.PurchaseRequisitionId);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PurchaseRequisitionModel obj)
        {
            TPurchaseRequisitionHeader model = obj.MapTo<TPurchaseRequisitionHeader>();
            if (obj.PurchaseRequisitionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                 model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPurchaseRequisitionService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.PurchaseRequisitionId);
        }
        [HttpPost("Cancel")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Delete)]
        public ApiResponse Cancel(PurchaseRequisitionCancel obj)
        {
            TPurchaseRequisitionHeader model = obj.MapTo<TPurchaseRequisitionHeader>();
            if (obj.PurchaseRequisitionId != 0)
            {
                model.PurchaseRequisitionId = obj.PurchaseRequisitionId;
                _IPurchaseRequisitionService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
        [HttpPost("Verify")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(PurchaseRequisitionVarifyModel obj)
        {
            TPurchaseRequisitionHeader model = obj.MapTo<TPurchaseRequisitionHeader>();
            if (obj.PurchaseRequisitionId != 0)
            {
                model.IsInchargeVerify = true;
                model.Isverify = true;
                model.IsInchargeVerifyDate = AppTime.Now.Date;
                await _IPurchaseRequisitionService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record verify successfully.");
        }

    }
}
