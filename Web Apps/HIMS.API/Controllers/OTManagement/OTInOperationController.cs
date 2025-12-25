using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.OTManagement;
using HIMS.Services.OTManagment;
using HIMS.Core.Infrastructure;


namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OTInOperationController : BaseController
    {
        private readonly IOTInOperationService _IOTInOperationService;
        private readonly IGenericService<TOtInOperationHeader> _repository;
        private readonly IGenericService<TOtInOperationDiagnosis> _repository1;
        private readonly IGenericService<TOtInOperationPostOperDiagnosis> _repository2;
        private readonly IGenericService<TOtInOperationAttendingDetail> _repository3;

        public OTInOperationController(IOTInOperationService repository, IGenericService<TOtInOperationHeader> repository1, IGenericService<TOtInOperationDiagnosis> repository2, IGenericService<TOtInOperationPostOperDiagnosis> repository3, IGenericService<TOtInOperationAttendingDetail> repository4)
        {
            _IOTInOperationService = repository;
            _repository = repository1;
            _repository1 = repository2;
            _repository2 = repository3;
            _repository3 = repository4;


        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.OtinOperationId == id);
            return data.ToSingleResponse<TOtInOperationHeader, OTInOperationHeaderModel>("TOtPreOperationHeader");
        }
        [HttpPost("InOperationAttendingDetailsList")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> InOperationAttendingDetailsList(GridRequestModel objGrid)
        {
            IPagedList<InOperationAttendingDetailsListDto> InOperationAttendingDetailsList = await _IOTInOperationService.InOperationAttengingDetailsAsync(objGrid);
            return Ok(InOperationAttendingDetailsList.ToGridResponse(objGrid, "InOperationAttendingDetails List"));
        }



        [HttpGet("GetOtInOperationPostOperDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetOtInOperationPostOperDiagnosisList(string DescriptionType)
        {
            var result = await _IOTInOperationService.InOperationPostOperDiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetOtInOperationPostOperDiagnosis List", result);
        }
        [HttpGet("GetOtInOperationDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetOtInOperationDiagnosisList(string DescriptionType)
        {
            var result = await _IOTInOperationService.InOperationDiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetOtInOperationDiagnosis List", result);
        }
       
        [HttpPost("InOperationSurgeryDetailsList")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> InOperationSurgeryDetailsList(GridRequestModel objGrid)
        {
            IPagedList<InOperationSurgeryDetailsDto> InOperationSurgeryDetailsList = await _IOTInOperationService.InOperationSurgeryDetailsAsync(objGrid);
            return Ok(InOperationSurgeryDetailsList.ToGridResponse(objGrid, "InOperationSurgeryDetails List"));
        }

        //List API
        [HttpPost("OTInOperationHeaderList")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TOtInOperationHeader> OTInOperationHeaderList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(OTInOperationHeaderList.ToGridResponse(objGrid, "OTInOperationHeader List"));
        }
        //List API
        [HttpPost("OTInOperationDiagnosisList")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> DiagnosisList(GridRequestModel objGrid)
        {
            IPagedList<TOtInOperationDiagnosis> OTInOperationDiagnosisList = await _repository1.GetAllPagedAsync(objGrid);
            return Ok(OTInOperationDiagnosisList.ToGridResponse(objGrid, "OTInOperationDiagnosis List"));
        }
        //List API
        [HttpPost("OTInOperationPostOperDiagnosisList")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> OTList(GridRequestModel objGrid)
        {
            IPagedList<TOtInOperationPostOperDiagnosis> OTInOperationPostOperDiagnosisList = await _repository2.GetAllPagedAsync(objGrid);
            return Ok(OTInOperationPostOperDiagnosisList.ToGridResponse(objGrid, "OTInOperationPostOperDiagnosis List"));
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OTInOperationModel obj)
        {
            TOtInOperationHeader model = obj.MapTo<TOtInOperationHeader>();
            if (obj.OtinOperationId == 0)
            {
                foreach (var q in model.TOtInOperationAttendingDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TOtInOperationDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TOtInOperationPostOperDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TOtInOperationSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTInOperationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTInOperationModel obj)
        {
            TOtInOperationHeader model = obj.MapTo<TOtInOperationHeader>();
            if (obj.OtinOperationId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TOtInOperationAttendingDetails)
                {
                    if (q.OtinOperationAttendingDetId == 0)
                    {
                        q.Createdby = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.OtinOperationAttendingDetId = 0;
                }

                foreach (var v in model.TOtInOperationDiagnoses)
                {
                    if (v.OtinOperationDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.OtinOperationDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtInOperationPostOperDiagnoses)
                {
                    if (v.OtinOperationPostOperDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.OtinOperationPostOperDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtInOperationSurgeryDetails)
                {
                    if (v.OtinOperationSurgeryDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }

                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.OtinOperationSurgeryDetId = 0;
                }
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTInOperationService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
