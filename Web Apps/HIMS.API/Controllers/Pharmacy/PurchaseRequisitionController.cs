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

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PurchaseRequisitionController : BaseController
    {
        private readonly IPurchaseRequisitionService _IPurchaseRequisitionService;
        private readonly IGenericService<TPurchaseRequisitionHeader> _repository;
        public PurchaseRequisitionController(IPurchaseRequisitionService repository, IGenericService<TPurchaseRequisitionHeader> repository1)
        {
            _IPurchaseRequisitionService = repository;
            _repository = repository1;
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "PurchaseRequisitionHeader", Permission = PagePermission.Add)]
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
        //[Permission(PageCode = "PurchaseRequisitionHeader", Permission = PagePermission.Edit)]
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
    }
}
