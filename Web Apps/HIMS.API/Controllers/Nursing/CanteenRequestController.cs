using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Nursing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CanteenRequestController : BaseController
    {
        private readonly ICanteenRequestService _ICanteenRequestService;
        public CanteenRequestController(ICanteenRequestService repository)
        {
            _ICanteenRequestService = repository;
        }
        [HttpPost("Insert")]
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
        [HttpPost("AdmissionInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AdmissionInsertSP(CanteenModel obj)
        {
            TCanteenRequestHeader model = obj.CanteenR.MapTo<TCanteenRequestHeader>();
            TCanteenRequestDetail objTCanteenRequestDetail = obj.CanteenRequest.MapTo<TCanteenRequestDetail>();
            if (obj.CanteenR.ReqId == 0)
            {
                model.Time = Convert.ToDateTime(obj.CanteenR.Time);
                model.AddedBy = CurrentUserId;

                //if (obj.CanteenRequest.ReqId == 0)
                //{
                //    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                //    objVisitDetail.AddedBy = CurrentUserId;
                //    objVisitDetail.UpdatedBy = CurrentUserId;
                //}
                await _ICanteenRequestService.InsertAsyncSP(model, objTCanteenRequestDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Admission added successfully.");
        }
    }
}
