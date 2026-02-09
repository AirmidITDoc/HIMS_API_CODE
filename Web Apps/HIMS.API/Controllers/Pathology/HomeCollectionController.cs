using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Pathology;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HomeCollectionController : BaseController
    {
        private readonly IHomeCollectionService _IHomeCollectionService;
        public HomeCollectionController(IHomeCollectionService repository)
        {
            _IHomeCollectionService = repository;

        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(HomeCollectionModel obj)
        {
            THomeCollectionRegistrationInfo model = obj.MapTo<THomeCollectionRegistrationInfo>();
            if (obj.HomeCollectionId == 0)
            {
                foreach (var q in model.THomeCollectionServiceDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IHomeCollectionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.HomeCollectionId);
        }

        //[HttpPut("Edit/{id:int}")]
        ////[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(ReservationModel obj)
        //{
        //    TOtReservationHeader model = obj.MapTo<TOtReservationHeader>();
        //    if (obj.OtreservationId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        foreach (var q in model.TOtReservationAttendingDetails)
        //        {
        //            if (q.OtreservationAttendingDetId == 0)
        //            {
        //                q.Createdby = CurrentUserId;
        //                q.CreatedDate = AppTime.Now;
        //            }
        //            q.ModifiedBy = CurrentUserId;
        //            q.ModifiedDate = AppTime.Now;
        //            q.OtreservationAttendingDetId = 0;
        //        }

        //        foreach (var v in model.TOtReservationSurgeryDetails)
        //        {
        //            if (v.OtreservationSurgeryDetId == 0)
        //            {
        //                v.Createdby = CurrentUserId;
        //                v.CreatedDate = AppTime.Now;
        //            }
        //            v.ModifiedBy = CurrentUserId;
        //            v.ModifiedDate = AppTime.Now;
        //            v.OtreservationSurgeryDetId = 0;
        //        }
        //        foreach (var v in model.TOtReservationDiagnoses)
        //        {
        //            if (v.OtreservationDiagnosisDetId == 0)
        //            {
        //                v.Createdby = CurrentUserId;
        //                v.CreatedDate = AppTime.Now;
        //            }
        //            v.ModifiedBy = CurrentUserId;
        //            v.ModifiedDate = AppTime.Now;
        //            v.OtreservationDiagnosisDetId = 0;
        //        }
        //        model.ModifiedDate = AppTime.Now;
        //        model.ModifiedBy = CurrentUserId;
        //        await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "Createdby", "CreatedDate" });

        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.OtreservationId);
        //}
    }
}
