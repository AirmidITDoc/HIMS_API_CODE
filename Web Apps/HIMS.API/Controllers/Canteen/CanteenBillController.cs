using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.API.Models.Canteen;
using HIMS.Services.Canteen;
using Asp.Versioning;

namespace HIMS.API.Controllers.Canteen
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CanteenBillController : BaseController
    {
        private readonly ICanteenBillService _ICanteenBillService;

        public CanteenBillController(ICanteenBillService repository)
        {
            _ICanteenBillService = repository;

        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(CanteenBillModel obj)
        {
            TCanteenBillHeader model = obj.MapTo<TCanteenBillHeader>();
            if (obj.BillNo == 0)
            {
                foreach (var q in model.TCanteenBillDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ICanteenBillService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.BillNo);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CanteenBillModel obj)
        {
            TCanteenBillHeader model = obj.MapTo<TCanteenBillHeader>();
            if (obj.BillNo == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TCanteenBillDetails)
                {
                    if (q.CdetId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.CdetId = 0;
                }

                
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ICanteenBillService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.BillNo);
        }
    }
}
