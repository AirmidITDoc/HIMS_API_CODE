using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Canteen;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Canteen;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("CanteenBillList")]
        //[Permission(PageCode = "CanteenRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> CaneenBillList(GridRequestModel objGrid)
        {
            IPagedList<CanteenBillListDo> CanteenBList = await _ICanteenBillService.CanteenBillList(objGrid);
            return Ok(CanteenBList.ToGridResponse(objGrid, "Bill App List"));
        }


        [HttpPost("CanteenBilldetailList")]
        //[Permission(PageCode = "CanteenRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> BilldetailList(GridRequestModel objGrid)
        {
            IPagedList<CanteenBillDetailsLisDto> List1 = await _ICanteenBillService.CanteenBilldetailList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Bill Detail App List"));
        }


        [HttpPost("Insert")]
        [Permission(PageCode = "CanteenBill", Permission = PagePermission.Add)]
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
        [Permission(PageCode = "CanteenBill", Permission = PagePermission.Edit)]
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
