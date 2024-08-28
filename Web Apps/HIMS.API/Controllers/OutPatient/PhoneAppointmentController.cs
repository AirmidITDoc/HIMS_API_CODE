using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
namespace HIMS.API.Controllers.OutPatient
{
    public class PhoneAppointmentController : BaseController
    {
       
            private readonly IGenericService<TPhoneAppointment> _repository;
            public PhoneAppointmentController(IGenericService<TPhoneAppointment> repository)
            {
                _repository = repository;
            }

        //Add API
        [HttpPost]
        //  [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PhoneAppointmentModel obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            //model.IsActive = true;
            if (obj.PhoneAppId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Phone Appointment added successfully.");
        }


        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PhoneAppointmentModel obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            // model.IsActive = true;
            if (obj.PhoneAppId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;
                 model.UpdatedByDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "AddedBy", "UpdatedByDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Phone Appointment updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //  [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TPhoneAppointment model = await _repository.GetById(x => x.PhoneAppId == Id);
            if ((model?.PhoneAppId ?? 0) > 0)
            {
                //  model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Phone Appointment deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
    }

