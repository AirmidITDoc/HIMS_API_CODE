using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NursingController: BaseController
    {
        //private readonly ILabRequestService _ILabRequestService;
        private readonly IMPrescriptionService _IMPrescriptionService;
        private readonly IPriscriptionReturnService _IPriscriptionReturnService;
        private readonly ICanteenRequestService _ICanteenRequestService;


        public NursingController(ILabRequestService repository, IMPrescriptionService repository1 ,IPriscriptionReturnService repository2, ICanteenRequestService repository3)
        {
            //_ILabRequestService = repository;
            _IMPrescriptionService = repository1;
            _IPriscriptionReturnService = repository2;
            _ICanteenRequestService = repository3;
        }
        [HttpPost("PrescriptionList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionListDto> PrescriptiontList = await _IPriscriptionReturnService.GetPrescriptionListAsync(objGrid);
            return Ok(PrescriptiontList.ToGridResponse(objGrid, "Prescription App List "));
        }
        [HttpPost("PrescriptionReturnList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ListReturn(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnListDto> PrescriptiontReturnList = await _IPriscriptionReturnService.GetListAsyncReturn(objGrid);
            return Ok(PrescriptiontReturnList.ToGridResponse(objGrid, "PrescriptionReturn App List "));
        }
        [HttpPost("PrescriptionDetailList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ListDetail(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetailListDto> PrescriptiontDetailList = await _IPriscriptionReturnService.GetListAsyncDetail(objGrid);
            return Ok(PrescriptiontDetailList.ToGridResponse(objGrid, "PrescriptionDetail App List "));
        }
        //[HttpPost("LabRequestList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> List(GridRequestModel objGrid)
        //{
        //    IPagedList<LabRequestListDto> LabRequestList = await _ILabRequestService.GetListAsync(objGrid);
        //    return Ok(LabRequestList.ToGridResponse(objGrid, "LabRequestList "));
        //}
        //[HttpPost("LabRequestDetailsList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> LabRequestDetailsList(GridRequestModel objGrid)
        //{
        //    IPagedList<LabRequestDetailsListDto> LabRequestDetailsListDto = await _ILabRequestService.GetListAsyncD(objGrid);
        //    return Ok(LabRequestDetailsListDto.ToGridResponse(objGrid, "LabRequestList "));
        //}

        //}
        //[HttpPost("InsertLab")]
        ////[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(LabRequestModel obj)
        //{
        //    THlabRequest model = obj.MapTo<THlabRequest>();
        //    if (obj.RequestId == 0)
        //    {
        //        model.ReqDate = Convert.ToDateTime(obj.ReqDate);
        //        model.ReqTime = Convert.ToDateTime(obj.ReqTime);
        //        model.IsAddedBy = CurrentUserId;
        //        await _ILabRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "labRequest added successfully.");
        //}

        [HttpPost("InsertPrescription")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(MPrescriptionModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                //model.IsAddedBy = CurrentUserId;
                await _IMPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }

        //[HttpPost("PrescriptionReturnList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> PrescriptionReturnList(GridRequestModel objGrid)
        //{
        //    IPagedList<PrescriptionReturnDto> PrescriptionReturnList = await _IPriscriptionReturnService.GetListAsync(objGrid);
        //    return Ok(PrescriptionReturnList.ToGridResponse(objGrid, "PrescriptionReturnList "));
        //}

        [HttpPost("PrescriptionReturnInsert")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.Addedby = CurrentUserId;
                await _IPriscriptionReturnService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PrescriptionReturn added successfully.");
        }

        [HttpPut("PrescriptionReturnUpdate")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.PresDate = Convert.ToDateTime(obj.PresDate);
                await _IPriscriptionReturnService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PrescriptionReturn updated successfully.");
        }
        [HttpPost("CanteenInsert")]

        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(CanteenRequestModel obj)
        {
            TCanteenRequestHeader model = obj.MapTo<TCanteenRequestHeader>();
            if (obj.ReqId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Time = Convert.ToDateTime(obj.Time);

                await _ICanteenRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CanteenRequest added successfully.", model);
        }

    }
}
