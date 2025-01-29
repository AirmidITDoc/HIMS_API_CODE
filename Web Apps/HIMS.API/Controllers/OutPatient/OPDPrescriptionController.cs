using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    public class OPDPrescriptionController : BaseController

    {
        private readonly IOPDPrescriptionService _OPDPrescriptionService;
        public OPDPrescriptionController(IOPDPrescriptionService repository)
        {
            _OPDPrescriptionService = repository;
        }
       
       
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPDPrescriptionModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId == 0)
            {
                model.Ptime = Convert.ToDateTime(obj.Ptime);
                model.CreatedBy = CurrentUserId;
                //model.IsActive = true;
                await _OPDPrescriptionService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPDPrescription   added successfully.");
        }
    }
}
