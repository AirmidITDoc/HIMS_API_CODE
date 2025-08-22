using System.Text.Json;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Hubs;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.Notification;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PrescriptionController : BaseController
    {
        private readonly IPrescriptionService _IPrescriptionService;
        private readonly INotificationUtility _notificationUtility;
        private readonly IAdmissionService _admissionService;
        private readonly HIMSDbContext _context;
        private readonly IGenericService<TIpPrescription> _repository;
        private readonly IGenericService<TIpprescriptionReturnH> _repository1;

        
        public PrescriptionController(IPrescriptionService IPrescriptionService, INotificationUtility notificationUtility, IAdmissionService admissionService, HIMSDbContext HIMSDbContext)
        {
            _IPrescriptionService = IPrescriptionService;
            _notificationUtility = notificationUtility;
            _admissionService = admissionService;
            _context = HIMSDbContext;
        }


        //[HttpPost("InsertPrescription")]
        //[Permission(PageCode = "MedicalRecord", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(MedicalPrescriptionModel obj)
        //{
        //    TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
        //    if (obj.MedicalRecoredId == 0)
        //    {
        //        model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
        //        model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);

        //        await _IPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { model.AdmissionId, model.MedicalRecoredId });
        //}
      
        [HttpPost("InsertPrescription")]
        //[Permission(PageCode = "MedicalRecord", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(MedicalPrescriptionModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();

            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);

                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;

                foreach (var q in model.TIpPrescriptions)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = DateTime.Now;
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = DateTime.Now;

                }

                await _IPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);

                var objPatient = await _admissionService.PatientByAdmissionId(model.AdmissionId.Value);

                // Get all UserIds for StoreId = 2
                var userIds = await _context.LoginManagers
                    .Where(x => x.StoreId == 2 && x.IsActive == true)
                    .Select(x => x.UserId)
                    .Distinct()
                    .ToListAsync();
                foreach (var userId in userIds)
                {
                    // Added by vimal on 06/05/25 for testing - binding notification on bell icon of layout... later team can change..
                    await _notificationUtility.SendNotificationAsync("IP | Prescription Request for Pharmacy", $"{objPatient.RegNo} | {objPatient.FirstName} {objPatient.LastName}", $"opd/appointment?Mode=Bill&Id={model.AdmissionId}", userId);
                }

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { model.AdmissionId, model.MedicalRecoredId });
        }
    




        [HttpPost("PrescriptionReturnInsert")]
        //[Permission(PageCode = "PrescriptionReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                //model.Addedby = CurrentUserId;
                await _IPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { model.PresReId,model.PresNo});
        }

     
        [HttpPost("PrescriptionCancel")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> PrescCancel(PrescriptionCancel obj)
        {
            TIpPrescription model = new();
            if (obj.IppreId != 0)
            {
                model.IppreId = obj.IppreId;
                await _IPrescriptionService.PrescCancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }

     
        //[HttpPost("PrescriptionReturnCancel")]
        ////[Permission(PageCode = "OTReservation", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> PrescreturnCancel(PrescreturnCancelAsync obj)
        //{
        //    TIpprescriptionReturnH model = new();
        //    if (obj.PresReId != 0)
        //    {
        //        model.PresReId = obj.PresReId;
        //        await _IPrescriptionService.PrescreturnCancelAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        //}

    }


}
