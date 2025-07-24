using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Hubs;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Notification;
using HIMS.Services.Nursing;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security;
using System.Text.Json;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPPrescriptionController : BaseController
    {
        private readonly INotificationService _INotificationService;
        private readonly IPriscriptionReturnService _IPriscriptionReturnService;
        private readonly ILabRequestService _ILabRequestService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly HIMSDbContext _context;

        public IPPrescriptionController(INotificationService notificationService, IPriscriptionReturnService repository1, ILabRequestService repository2, IHubContext<NotificationHub> repository3, HIMSDbContext HIMSDbContext)
        {
            _INotificationService = notificationService;
            _IPriscriptionReturnService = repository1;
            _ILabRequestService = repository2;
            _hubContext = repository3;
            _context = HIMSDbContext;
        }

        ////private readonly HIMSDbContext _context;
        ////public VisitDetailsService(HIMSDbContext HIMSDbContext)
        ////{
        ////    _context = HIMSDbContext;
        ////}

        [HttpPost("PrescriptionPatientList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionListDto> PrescriptiontList = await _IPriscriptionReturnService.GetPrescriptionListAsync(objGrid);
            return Ok(PrescriptiontList.ToGridResponse(objGrid, "PrescriptionWard  List "));
        }

        [HttpPost("PrescriptionDetailList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ListDetail(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetailListDto> PrescriptiontDetailList = await _IPriscriptionReturnService.GetListAsyncDetail(objGrid);
            return Ok(PrescriptiontDetailList.ToGridResponse(objGrid, "PrescriptionDetail  List "));
        }

        [HttpPost("IPPrescriptionReturnList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ListReturn(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnListDto> PrescriptiontReturnList = await _IPriscriptionReturnService.GetListAsyncReturn(objGrid);
            return Ok(PrescriptiontReturnList.ToGridResponse(objGrid, "PrescriptionReturn  List "));
        }
        [HttpPost("IPPrescReturnItemDetList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionReturnList(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnDto> PrescriptionReturnList = await _IPriscriptionReturnService.GetListAsync(objGrid);
            return Ok(PrescriptionReturnList.ToGridResponse(objGrid, "PrescriptionReturnList "));
        }

        [HttpPost("LabRadRequestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestListDto> LabRequestList = await _ILabRequestService.GetListAsync(objGrid);
            return Ok(LabRequestList.ToGridResponse(objGrid, "LabRequestList "));
        }

        [HttpPost("LabRadRequestDetailList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestDetailsList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestDetailsListDto> LabRequestDetailsListDto = await _ILabRequestService.SPGetListAsync(objGrid);
            return Ok(LabRequestDetailsListDto.ToGridResponse(objGrid, "LabRequestDetailsList "));
        }
        //[HttpPost("InsertPrescription")]
        ////[Permission(PageCode = "MedicalRecord", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(MPrescriptionModel obj)
        //{
        //    TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
        //    if (obj.MedicalRecoredId == 0)
        //    {
        //        model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
        //        model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
        //        DateTime now = DateTime.Now;
        //        string formattedDate = now.ToString("yyyy-MM-dd");

        //        //model.IsAddedBy = CurrentUserId;
        //        await _IMPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    //else
        //    //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.", model);
        //}
        //[HttpPost("PrescriptionReturnInsert")]
        //[Permission(PageCode = "PrescriptionReturn", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(PriscriptionReturnModel obj)
        //{
        //    TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
        //    if (obj.PresReId == 0)
        //    {
        //        model.PresTime = Convert.ToDateTime(obj.PresTime);
        //        model.Addedby = CurrentUserId;
        //        await _IPriscriptionReturnService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PrescriptionReturn added successfully.", model);
        //}

        [HttpPost("LabRequestInsert")]
        //[Permission(PageCode = "RequestforLab", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IPLabRequestModel obj)
        {
            THlabRequest model = obj.MapTo<THlabRequest>();
            if (obj.RequestId == 0)
            {
                model.ReqDate = Convert.ToDateTime(obj.ReqDate);
                model.ReqTime = Convert.ToDateTime(obj.ReqTime);
                model.IsAddedBy = CurrentUserId;
                await _ILabRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);

                // Get all UserIds for StoreId = 2
                var userIds = await _context.LoginManagers
                    .Where(x => x.StoreId == 2 && x.IsActive == true)
                    .Select(x => x.UserId)
                    .Distinct()
                    .ToListAsync();
                foreach (var userId in userIds)
                {
                    // Added by vimal on 06/05/25 for testing - binding notification on bell icon of layout... later team can change..
                    NotificationMaster objNotification = new() { CreatedDate = DateTime.Now, IsActive = true, IsDeleted = false, IsRead = false, NotiBody = "This is notification body", NotiTitle = "This is title", UserId = userId };
                    await _INotificationService.Save(objNotification);
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(new { objNotification.CreatedDate, objNotification.NotiBody, objNotification.NotiTitle, objNotification.Id }), objNotification.UserId);

                }
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.RequestId);
        }

    }
}