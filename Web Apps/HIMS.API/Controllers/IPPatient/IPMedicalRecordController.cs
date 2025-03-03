using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.IPPatient;
using HIMS.API.Models.IPPatient;
using HIMS.Data;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPMedicalRecordController : BaseController
    {
        private readonly IMedicalRecordService _MedicalRecordService;
        private readonly IGenericService<ServiceMaster> _serviceMasterrepository;
        private readonly IGenericService<MOpcasepaperDignosisMaster> _Dignos;
        private readonly IGenericService<MExaminationMaster> _Examination;
        private readonly IGenericService<MComplaintMaster> _MComplaintMaster;

        
        public IPMedicalRecordController(IMedicalRecordService repository, IGenericService<ServiceMaster> ServiceMasterrepository, IGenericService<MOpcasepaperDignosisMaster> MOpcasepaperDignosisMasterrepository, IGenericService<MExaminationMaster> MExaminationMasterrepository
            ,IGenericService<MComplaintMaster> MComplaintMasterrepository)
        {
            _MedicalRecordService = repository;
            _serviceMasterrepository = ServiceMasterrepository;
            _Dignos = MOpcasepaperDignosisMasterrepository;
            _Examination = MExaminationMasterrepository;
            _MComplaintMaster = MComplaintMasterrepository;
        }
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(TIPmedicalRecordModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                //model.IsAddBy = CurrentUserId;
                //model.IsActive = true;
                await _MedicalRecordService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MedicalRecord   added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TIPmedicalRecordModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                await _MedicalRecordService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MedicalRecord  updated successfully.");
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dignosis dropdown", MDignosMasterList.Select(x => new { x.DescriptionName, x.VisitId,x.DescriptionType }));
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
    }
}


