using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.TrustMembershipRegistration;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using HIMS.Services.TrustMembershipRegistration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.TrustMembershipRegistration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TrustMemershipRegController : BaseController
    {
        private readonly ITrustMembershipRegService _ITrustMembershipRegService;
        private readonly IGenericService<TMembershipRegistration> _repository;


        public TrustMemershipRegController(ITrustMembershipRegService repository, IGenericService<TMembershipRegistration> repository1)
        {
            _ITrustMembershipRegService = repository;
            _repository = repository1;
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _ITrustMembershipRegService.GetById(id);
            return data.ToSingleResponse<TMembershipRegistration, TrustMembershipRegModel>("Doctor Master");
        }


        [HttpPost("Insert")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(TrustMembershipRegModel obj)
        {
            TMembershipRegistration model = obj.MapTo<TMembershipRegistration>();
            if (obj.MembershipId == 0)
            {
                foreach (var q in model.TMembershipChildren)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;
                    q.ModifiedDate = AppTime.Now;
                    q.ModifiedBy = CurrentUserId;

                }
                foreach (var q in model.TMembershipEmrgencies)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;
                    q.ModifiedDate = AppTime.Now;
                    q.ModifiedBy = CurrentUserId;

                }
                foreach (var q in model.TMembershipRelatives)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;
                    q.ModifiedDate = AppTime.Now;
                    q.ModifiedBy = CurrentUserId;

                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ITrustMembershipRegService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.MembershipId);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TrustMembershipRegModel obj)
        {
            TMembershipRegistration model = obj.MapTo<TMembershipRegistration>();
            if (obj.MembershipId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TMembershipChildren)
                {
                    if (q.ChildId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.ChildId = 0;
                }

                foreach (var v in model.TMembershipEmrgencies)
                {
                    if (v.EmrgencyId == 0)
                    {
                        v.CreatedBy = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.EmrgencyId = 0;
                }
                foreach (var v in model.TMembershipRelatives)
                {
                    if (v.RelativeId == 0)
                    {
                        v.CreatedBy = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.RelativeId = 0;
                }
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ITrustMembershipRegService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.MembershipId);
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TMembershipRegistration model = await _repository.GetById(x => x.MembershipId == Id);
            if ((model?.MembershipId ?? 0) > 0)
            {
                //model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
