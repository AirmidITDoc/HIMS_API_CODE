using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DietKitchen;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class DietPatientRequestController : BaseController
    {
        private readonly IDietPatientRequestService _IDietPatientRequestService;
        public DietPatientRequestController(IDietPatientRequestService repository)
        {
            _IDietPatientRequestService = repository;

        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DietPatientRequestModel obj)
        {
            TDietPatientRequestHeader model = obj.MapTo<TDietPatientRequestHeader>();
            if (obj.DietReqId == 0)
            {
                foreach (var q in model.TDietPatReqDetails)
                {
                    //    q.CreatedBy = CurrentUserId;
                    //    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TDietPatReqDetails)
                {

                }


                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IDietPatientRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.DietReqId);
        }

      

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DietPatientRequestModel obj)
        {
            if (obj.DietReqId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            TDietPatientRequestHeader model = obj.MapTo<TDietPatientRequestHeader>();

            foreach (var q in model.TDietPatReqDetails)
            {
                q.DietReqDetId = 0;     
                q.OrderDate = AppTime.Now;
            }

            model.ModifiedDate = AppTime.Now;
            model.ModifiedBy = CurrentUserId;

            await _IDietPatientRequestService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });


            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.DietReqId);
        }
    }

}



