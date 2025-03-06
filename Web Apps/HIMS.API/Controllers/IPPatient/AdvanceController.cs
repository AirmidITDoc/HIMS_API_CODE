﻿using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Inventory;
using HIMS.API.Models.Inventory;
using static HIMS.API.Models.OutPatient.RefundAdvanceModelValidator;
using HIMS.Services.IPPatient;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Core;
using HIMS.Services.OutPatient;
using static HIMS.API.Models.IPPatient.UpdateAdvanceModelValidator;
using HIMS.Data.DTO.Administration;
using HIMS.API.Models.Masters;
using HIMS.Data;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdvanceController : BaseController
    {
        private readonly IAdvanceService _IAdvanceService;
        private readonly IGenericService<AdvanceHeader> _repository1;
        public AdvanceController(IAdvanceService repository,IGenericService<AdvanceHeader> AdvanceHeaderrepository)
        {
            _IAdvanceService = repository;
            _repository1 = AdvanceHeaderrepository;
        }
        [HttpPost("PatientWiseAdvanceList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientWiseAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<PatientWiseAdvanceListDto> PatientWiseAdvanceList = await _IAdvanceService.PatientWiseAdvanceList(objGrid);
            return Ok(PatientWiseAdvanceList.ToGridResponse(objGrid, "PatientWiseAdvance List"));
        }


        [HttpPost("AdvanceList")]
        [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> AdvanceList(GridRequestModel objGrid)
        {
            IPagedList<AdvanceListDto> AdvanceList = await _IAdvanceService.GetAdvanceListAsync(objGrid);
            return Ok(AdvanceList.ToGridResponse(objGrid, "Advance List"));
        }
        [HttpPost("RefundOfAdvancesList")]
     //   [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundOfAdvancesList(GridRequestModel objGrid)
        {
            IPagedList<RefundOfAdvancesListDto> RefundOfAdvancesList = await _IAdvanceService.GetAdvancesListAsync(objGrid);
            return Ok(RefundOfAdvancesList.ToGridResponse(objGrid, "RefundOfAdvances List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data1 = await _repository1.GetById(x => x.OpdIpdId == id);
            return data1.ToSingleResponse<AdvanceHeader, AdvanceHeaderModel>("Advance Details ");
        }

        [HttpPost("RefundOfAdvanceList")]
        [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<RefundOfAdvanceListDto> RefundAdvanceList = await _IAdvanceService.GetRefundOfAdvanceListAsync(objGrid);
            return Ok(RefundAdvanceList.ToGridResponse(objGrid, "Refund Of Advance List"));
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelAdvance1 obj)
        {
            AdvanceHeader model = obj.Advance.MapTo<AdvanceHeader>();
            AdvanceDetail objAdvanceDetail = obj.AdvanceDetail.MapTo<AdvanceDetail>();
            Payment objpayment = obj.AdvancePayment.MapTo<Payment>();
            if (obj.Advance.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Advance.Date);
                model.AddedBy = CurrentUserId;

                objAdvanceDetail.Date = Convert.ToDateTime(obj.AdvanceDetail.Date);
                objAdvanceDetail.AddedBy = CurrentUserId;
                objpayment.PaymentTime = Convert.ToDateTime(objpayment.PaymentTime);
                objpayment.AddBy = CurrentUserId;

               await _IAdvanceService.InsertAdvanceAsyncSP(model, objAdvanceDetail, objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance added successfully.");
        }
        [HttpPut("Edit")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(UpdateAdvance obj)
        {
            AdvanceHeader model = obj.Advance.MapTo<AdvanceHeader>();
            AdvanceDetail objAdvanceDetail = obj.AdvanceDetail.MapTo<AdvanceDetail>();
            Payment objpayment = obj.AdvancePayment.MapTo<Payment>();

            if (obj.Advance.AdvanceId != 0)
            {
                model.AddedBy = CurrentUserId;
                objAdvanceDetail.Date = Convert.ToDateTime(obj.AdvanceDetail.Date);
                objAdvanceDetail.AddedBy = CurrentUserId;
                objpayment.PaymentTime = Convert.ToDateTime(objpayment.PaymentTime);
                objpayment.AddBy = CurrentUserId;

            await _IAdvanceService.UpdateAdvanceSP(model, objAdvanceDetail, objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance Update successfully.");
        }
    }
}
