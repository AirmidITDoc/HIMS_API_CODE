using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DoctorPayout;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.Pathology;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.DoctorPayout;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.Pathology;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabPatientRegistrationController : BaseController
    {
        private readonly ILabPatientRegistrationService _ILabPatientRegistrationService;
        public LabPatientRegistrationController(ILabPatientRegistrationService repository)
        {
            _ILabPatientRegistrationService = repository;
        }
        [HttpPost("List")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabPatientRegistrationListDto> LabPatientRegistrationList = await _ILabPatientRegistrationService.GetListAsync(objGrid);
            return Ok(LabPatientRegistrationList.ToGridResponse(objGrid, "Lab Patient Registration List"));
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(LabPatientRegistrationModel obj)
        {
            TLabPatientRegistration model = obj.MapTo<TLabPatientRegistration>();
            if (obj.LabPatientId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPost("PatientRegistrationcreditbill")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(LabRegistrationModels obj)
        {
            TLabPatientRegistration model = obj.LabPatientRegistration.MapTo<TLabPatientRegistration>();
            //List<TLabTestRequest> model1 = obj.TLabTestRequest.MapTo<List<TLabTestRequest>>();
            Bill model2 = obj.OPBillIngModels.MapTo<Bill>();
            Payment objPayment = obj.OPBillIngModels.Payments.MapTo<Payment>();
            List<AddCharge> ObjPackagecharge = obj.OPBillIngModels.Packcagecharges.MapTo<List<AddCharge>>();



            if (obj.LabPatientRegistration.LabPatientId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.InsertAsyncSP(model, model2, objPayment, ObjPackagecharge, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }



        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LabPatientRegistrationModel obj)
        {
            TLabPatientRegistration model = obj.MapTo<TLabPatientRegistration>();
            if (obj.LabPatientId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
       
    }
}
