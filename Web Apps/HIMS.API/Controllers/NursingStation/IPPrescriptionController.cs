using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.API.Utility;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.Nursing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPPrescriptionController : BaseController
    {
        private readonly INotificationUtility _notificationUtility;
        private readonly IPriscriptionReturnService _IPriscriptionReturnService;
        private readonly ILabRequestService _ILabRequestService;
        private readonly HIMSDbContext _context;
        private readonly IAdmissionService _admissionService;

        public IPPrescriptionController(INotificationUtility notificationUtility, IPriscriptionReturnService repository1, ILabRequestService repository2, HIMSDbContext HIMSDbContext, IAdmissionService admissionService)
        {
            _notificationUtility = notificationUtility;
            _IPriscriptionReturnService = repository1;
            _ILabRequestService = repository2;
            _context = HIMSDbContext;
            _admissionService = admissionService;
        }


        [HttpPost("PrescriptionPatientList")]
        //[Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionListDto> PrescriptiontList = await _IPriscriptionReturnService.GetPrescriptionListAsync(objGrid);
            return Ok(PrescriptiontList.ToGridResponse(objGrid, "PrescriptionWard  List "));
        }

        [HttpPost("PrescriptionDetailList")]
        //[Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> ListDetail(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetailListDto> PrescriptiontDetailList = await _IPriscriptionReturnService.GetListAsyncDetail(objGrid);
            return Ok(PrescriptiontDetailList.ToGridResponse(objGrid, "PrescriptionDetail  List "));
        }

        [HttpPost("IPPrescriptionReturnList")]
        //[Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> ListReturn(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnListDto> PrescriptiontReturnList = await _IPriscriptionReturnService.GetListAsyncReturn(objGrid);
            return Ok(PrescriptiontReturnList.ToGridResponse(objGrid, "PrescriptionReturn  List "));
        }
        [HttpPost("IPPrescReturnItemDetList")]
        //[Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionReturnList(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnDto> PrescriptionReturnList = await _IPriscriptionReturnService.GetListAsync(objGrid);
            return Ok(PrescriptionReturnList.ToGridResponse(objGrid, "PrescriptionReturnList "));
        }

        [HttpPost("LabRadRequestList")]
        //[Permission(PageCode = "RequestforLab", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestListDto> LabRequestList = await _ILabRequestService.GetListAsync(objGrid);
            return Ok(LabRequestList.ToGridResponse(objGrid, "LabRequestList "));
        }

        [HttpPost("LabRadRequestDetailList")]
        //[Permission(PageCode = "RequestforLab", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestDetailsList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestDetailsListDto> LabRequestDetailsListDto = await _ILabRequestService.SPGetListAsync(objGrid);
            return Ok(LabRequestDetailsListDto.ToGridResponse(objGrid, "LabRequestDetailsList "));
        }


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

                //get patient details
                var objPatient = await _admissionService.PatientByAdmissionId(model.OpIpId.Value);

                // Get all UserIds for StoreId = 2
                var userIds = await _context.LoginManagers
                    .Where(x => x.StoreId == 2 && x.IsActive == true)
                    .Select(x => x.UserId)
                    .Distinct()
                    .ToListAsync();
                foreach (var userId in userIds)
                {
                    // Added by vimal on 06/05/25 for testing - binding notification on bell icon of layout... later team can change..
                    await _notificationUtility.SendNotificationAsync("IP | Request For Billing", $"{objPatient.RegNo} | {objPatient.FirstName} {objPatient.LastName}", $"ipd/add-billing?Mode=Bill&Id={model.OpIpId}", userId);
                    //NotificationMaster objNotification = new() { CreatedDate = AppTime.Now, IsActive = true, IsDeleted = false, IsRead = false, NotiBody = $"Patient: {objPatient.FirstName} {objPatient.LastName}| {objPatient.RegNo}", NotiTitle = "Lab and Radi Request For Billing", UserId = userId, RedirectUrl = $"ipd/add-billing?Mode=Bill&Id={obj.OpIpId}" };
                    //await _INotificationService.Save(objNotification);
                    //await _hubContext.Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(new { objNotification.CreatedDate, objNotification.NotiBody, objNotification.NotiTitle, objNotification.Id, objNotification.RedirectUrl }), objNotification.UserId);
                }
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.RequestId);
        }

        [HttpPost("LabRequestCancel")]
        //[Permission(PageCode = "RequestforLab", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(LabRequestCancel obj)
        {
            THlabRequest model = new();
            if (obj.RequestId != 0)
            {
                model.RequestId = obj.RequestId;
                await _ILabRequestService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }



    }
}