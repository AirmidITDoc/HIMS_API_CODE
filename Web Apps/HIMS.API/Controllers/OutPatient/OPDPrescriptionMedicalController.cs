﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
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
        private readonly IGenericService<ServiceMaster> _serviceMasterrepository;
        private readonly IGenericService<MOpcasepaperDignosisMaster> _Dignos;
        private readonly IGenericService<MExaminationMaster> _Examination;
        private readonly IGenericService<MComplaintMaster> _MComplaintMaster;

        public OPDPrescriptionMedicalController(IOPDPrescriptionMedicalService repository, IGenericService<ServiceMaster> ServiceMasterrepository, IGenericService<MOpcasepaperDignosisMaster> MOpcasepaperDignosisMasterrepository, IGenericService<MExaminationMaster> MExaminationMasterrepository
            , IGenericService<MComplaintMaster> MComplaintMasterrepository)
        {
            _OPDPrescriptionService = repository;
            _serviceMasterrepository = ServiceMasterrepository;
            _Dignos = MOpcasepaperDignosisMasterrepository;
            _Examination = MExaminationMasterrepository;
            _MComplaintMaster = MComplaintMasterrepository;
        }


        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelTPrescription obj)
        {
            TPrescription model = obj.TPrescription.MapTo<TPrescription>();
            List<TOprequestList> objTOPRequest = obj.TOPRequestList.MapTo<List<TOprequestList>>();
            List<MOpcasepaperDignosisMaster>objmOpcasepaperDignosis = obj.MOPCasepaperDignosisMaster.MapTo<List<MOpcasepaperDignosisMaster>>();

            if (model.OpdIpdIp != 0)
            {
                model.Date = Convert.ToDateTime(obj.TPrescription.Date);
                model.CreatedBy = CurrentUserId;
                objTOPRequest.ForEach(x => { x.OpIpId = obj.TPrescription.OpdIpdIp; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });
                objmOpcasepaperDignosis.ForEach(x => { x.VisitId = obj.TPrescription.OpdIpdIp; });

               await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, objTOPRequest, objmOpcasepaperDignosis, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }




        //List API
        [HttpGet]
        [Route("get-Service")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MServicetMasterList = await _serviceMasterrepository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service dropdown", MServicetMasterList.Select(x => new { x.ServiceName, x.ServiceId }));
        }

        //List API
        [HttpGet]
        [Route("get-Dignosis")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDignosDropdown()
        {
            var MDignosMasterList = await _Dignos.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dignosis dropdown", MDignosMasterList.Select(x => new { x.DescriptionName, x.VisitId, x.DescriptionType }));
        }

        //List API
        [HttpGet]
        [Route("get-Examination")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetExaminationDropdown()
        {
            var MExaminationMasterList = await _Examination.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Examination dropdown", MExaminationMasterList.Select(x => new { x.ExaminationId, x.ExaminationDescr }));
        }
        //List API
        [HttpGet]
        [Route("get-ChiefComplaint")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetChiefComplaintDropdown()
        {
            var MChiefComplaintMasterList = await _MComplaintMaster.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ChiefComplaint dropdown", MChiefComplaintMasterList.Select(x => new { x.ComplaintId, x.ComplaintDescr }));
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
