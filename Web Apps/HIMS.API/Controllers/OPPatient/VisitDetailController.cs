﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class VisitDetailController : BaseController
    {
        private readonly IVisitDetailsService _visitDetailsService;
        private readonly IGenericService<VisitDetail> _repository;
        public VisitDetailController(IVisitDetailsService repository, IGenericService<VisitDetail> repository1)
        {
            _visitDetailsService = repository;
            _repository = repository1;
        }
        [HttpPost("AppVisitList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<VisitDetailListDto> AppVisitList = await _visitDetailsService.GetListAsync(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "App Visit List"));
        }


        [HttpGet("{id?}")]
        // [Permission(PageCode = "Bed", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
         
            var data1 = await _repository.GetById(x => x.VisitId == id);
            return data1.ToSingleResponse<VisitDetail, VisitDetailModel>("VisitDetails");
        }




        [HttpPost("AppVisitInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AppVisitInsert(AppointmentReqDtovisit obj)
        {
            Registration model = obj.Registration.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.Registration.RegId == 0)
            {
                model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _visitDetailsService.InsertAsync(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment Visit added successfully.",model);
        }



        [HttpPost("Insert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(AppointmentReqDtovisit obj)
        {
            Registration model = obj.Registration.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.Registration.RegId == 0)
            {
                model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _visitDetailsService.InsertAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment added successfully.",model);
        }


        [HttpPost("Update")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(AppointmentReqDtovisit obj)
        {
            Registration model = obj.Registration.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.Registration.RegId != 0)
            {
                model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _visitDetailsService.UpdateAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment Updated successfully.",model);
        }

        [HttpPost("Cancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(CancelAppointment obj)
        {
            VisitDetail model = new();
            if (obj.VisitId != 0)
            {
                model.VisitId = obj.VisitId;
                model.IsCancelled = true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = DateTime.Now;
                await _visitDetailsService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Appointment Canceled successfully.",model);
        }

        [HttpPost("OPRegistrationList")]
        public async Task<IActionResult> OPRegistrationList(GridRequestModel objGrid)
        {
            IPagedList<OPRegistrationList> OpReglist = await _visitDetailsService.GeOPRgistrationListAsync(objGrid);
            return Ok(OpReglist.ToGridResponse(objGrid, "OP Registration List"));
        }

        [HttpPost("OPBillList")]
        public async Task<IActionResult> OPBillList(GridRequestModel objGrid)
        {
            IPagedList<OPBillListDto> OpBilllist = await _visitDetailsService.GetBillListAsync(objGrid);
            return Ok(OpBilllist.ToGridResponse(objGrid, "OP BILL List"));
        }

        [HttpPost("OPPaymentList")]
        public async Task<IActionResult> OPPaymentList(GridRequestModel objGrid)
        {
            IPagedList<OPPaymentListDto> OpPaymentlist = await _visitDetailsService.GeOpPaymentListAsync(objGrid);
            return Ok(OpPaymentlist.ToGridResponse(objGrid, "OP Payment List"));
        }

        [HttpPost("OPRefundList")]
        public async Task<IActionResult> OPRefundList(GridRequestModel objGrid)
        {
            IPagedList<OPRefundListDto> OpRefundlist = await _visitDetailsService.GeOpRefundListAsync(objGrid);
            return Ok(OpRefundlist.ToGridResponse(objGrid, "OP Refund List"));
        }

        //[HttpPost("PhoneAppointList")]
        //public async Task<IActionResult> OPphAppList(GridRequestModel objGrid)
        //{
        //    IPagedList<OPPhoneAppointmentList> OphoneList = await _visitDetailsService.GeOPPhoneAppListAsync(objGrid);
        //    return Ok(OphoneList.ToGridResponse(objGrid, "Phone Appointment List"));
        //}
    }
}
