using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.OutPatient.TPrescriptionModel;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPDPrescriptionMedicalController : BaseController

    {
        private readonly IOPDPrescriptionMedicalService _OPDPrescriptionService;
        public OPDPrescriptionMedicalController(IOPDPrescriptionMedicalService repository)
        {
            _OPDPrescriptionService = repository;
        }


        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelTPrescription obj)
        {
            TPrescription model = obj.TPrescription.MapTo<TPrescription>();
          List<TOprequestList> objTOPRequest = obj.TOPRequestList.MapTo<List<TOprequestList>>();
        //    TOprequestList objTOPRequest = obj.TOPRequestList.MapTo<TOprequestList>();
            MOpcasepaperDignosisMaster objmOpcasepaperDignosis = obj.MOPCasepaperDignosisMaster.MapTo<MOpcasepaperDignosisMaster>();

            if (model.OpdIpdIp != 0)
            {
                model.Date = Convert.ToDateTime(obj.TPrescription.Date);
                model.CreatedBy = CurrentUserId;
            //   objTOPRequest.CreatedBy = CurrentUserId;

                await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, objTOPRequest, objmOpcasepaperDignosis, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }

        ////Ashu//
        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(ModelTPrescription obj)
        //{
        //    TPrescription model = obj.MapTo<TPrescription>();
        //    if (obj.PrecriptionId == 0)
        //    {
        //        model.Ptime = Convert.ToDateTime(obj.Ptime);
        //        model.CreatedBy = CurrentUserId;
        //        //model.IsActive = true;
        //        await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPDPrescription   added successfully.");
        //}






    }
}
