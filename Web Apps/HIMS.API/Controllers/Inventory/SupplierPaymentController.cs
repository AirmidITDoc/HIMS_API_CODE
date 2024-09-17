using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SupplierPaymentController : BaseController
    {
        private readonly ISupplierPaymentService _ISupplierPaymentService;
        public SupplierPaymentController(ISupplierPaymentService repository)
        {
            _ISupplierPaymentService = repository;
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(SupplierPaymentModel obj)
        {
            TGrnheader model = obj.MapTo<TGrnheader>();
            if (obj.Grnid == 0)
            {
                //model.Grndate = Convert.ToDateTime(obj.Grndate);
                model.AddedBy = CurrentUserId;
              
                await _ISupplierPaymentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SupplierPaymentModel obj)
        {
            TGrnheader model = obj.MapTo<TGrnheader>();
            if (obj.Grnid == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.Grndate = Convert.ToDateTime(obj.Grndate);
                await _ISupplierPaymentService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN updated successfully.");
        }


    }

}
