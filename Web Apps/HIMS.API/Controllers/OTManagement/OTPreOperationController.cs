using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OTManagement;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.OTManagement;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Services.OTManagment;
using HIMS.Core.Infrastructure;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTPreOperationController : BaseController
    {

        private readonly IOTPreOperationService _IOTPreOperationService;
        private readonly IGenericService<TOtPreOperationHeader> _repository;
        private readonly IGenericService<TOtPreOperationCathlabDiagnosis> _repository1;
        private readonly IGenericService<TOtPreOperationSurgeryDetail> _repository2;
        private readonly IGenericService<TOtPreOperationDiagnosis> _repository3;

        public OTPreOperationController(IOTPreOperationService repository, IGenericService <TOtPreOperationHeader> repository1, IGenericService<TOtPreOperationCathlabDiagnosis> repository2,
            IGenericService<TOtPreOperationSurgeryDetail> repository3, IGenericService<TOtPreOperationDiagnosis> repository4)
        {
            _IOTPreOperationService = repository;
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
            var data = await _repository.GetById(x => x.OtpreOperationId == id);
            return data.ToSingleResponse<TOtPreOperationHeader, PreOperationHeaderModel>("TOtPreOperationHeader");
        }

        ////List API
        //[HttpGet("OtPreOperationHeaderList")]
        ////[Route("[action]")]
        ////[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        //public async Task<IActionResult> otList(GridRequestModel objGrid)
        //{
        //    IPagedList<TOtPreOperationHeader> OtPreOperationHeaderList = await _repository.GetAllPagedAsync(objGrid);
        //    return Ok(OtPreOperationHeaderList.ToGridResponse(objGrid, "OtPreOperationHeader List"));
        //}
        //List API
        [HttpPost("OtPreOperationCathlabDiagnosisList")]
        //[Route("[action]")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> CathlabDiagnosisList(GridRequestModel objGrid)
        {
            IPagedList<TOtPreOperationCathlabDiagnosis> OtPreOperationCathlabDiagnosisList = await _repository1.GetAllPagedAsync(objGrid);
            return Ok(OtPreOperationCathlabDiagnosisList.ToGridResponse(objGrid, "OtPreOperationCathlabDiagnosis List"));
        }
        [HttpPost("OtPreOperationSurgeryDetailList")]
        //[Route("[action]")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> OtPreOperationSurgeryDetailList(GridRequestModel objGrid)
        {
            IPagedList<TOtPreOperationSurgeryDetail> OtPreOperationSurgeryDetailList = await _repository2.GetAllPagedAsync(objGrid);
            return Ok(OtPreOperationSurgeryDetailList.ToGridResponse(objGrid, "OtPreOperationSurgeryDetail List"));
        }

        [HttpPost("OtPreOperationDiagnosisList")]
        //[Route("[action]")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> PreOperationDiagnosisList(GridRequestModel objGrid)
        {
            IPagedList<TOtPreOperationDiagnosis> PreOperationDiagnosisList = await _repository3.GetAllPagedAsync(objGrid);
            return Ok(PreOperationDiagnosisList.ToGridResponse(objGrid, "OtPreOperationDiagnosisList"));
        }



        [HttpPost("perOperationsurgeryList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<perOperationsurgeryListDto> perOperationsurgeryList = await _IOTPreOperationService.GetListAsync(objGrid);
            return Ok(perOperationsurgeryList.ToGridResponse(objGrid, "perOperationsurgery List"));
        }
        [HttpPost("preOperationAttendentList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<PreOperationAttendentListDto> preOperationAttendentList = await _IOTPreOperationService.preOperationAttendentListAsync(objGrid);
            return Ok(preOperationAttendentList.ToGridResponse(objGrid, "preOperationAttendent List"));
        }
        [HttpGet("GetPreOperationDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> PreOperationDiagnosisList(string DescriptionType)
        {
            var result = await _IOTPreOperationService.PreOperationDiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetPreOperationDiagnosis List", result);
        }
        [HttpGet("GetPreOperationCathlabDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> PreOperationCathlabDiagnosisList(string DescriptionType)
        {
            var result = await _IOTPreOperationService.PreOperationCathlabDiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetPreOperationCathlabDiagnosis List", result);
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OTPreOperationModel obj)
        {
            TOtPreOperationHeader model = obj.MapTo<TOtPreOperationHeader>();
            if (obj.OtpreOperationId == 0)
            {
                foreach (var q in model.TOtPreOperationAttendingDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TOtPreOperationCathlabDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TOtPreOperationDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TOtPreOperationSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }

                model.CreatedDate = AppTime.Now;
                model.Createdby = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTPreOperationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTPreOperationModel obj)
        {
            TOtPreOperationHeader model = obj.MapTo<TOtPreOperationHeader>();
            if (obj.OtpreOperationId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TOtPreOperationAttendingDetails)
                {
                    if (q.OtpreOperationAttendingDetId == 0)
                    {
                        q.Createdby = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.OtpreOperationAttendingDetId = 0;
                }

                foreach (var v in model.TOtPreOperationCathlabDiagnoses)
                {
                    if (v.OtpreOperationCathLabDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.OtpreOperationCathLabDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtPreOperationDiagnoses)
                {
                    if (v.OtpreOperationDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.OtpreOperationDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtPreOperationSurgeryDetails)
                {
                    if (v.OtpreOperationSurgeryDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.OtpreOperationSurgeryDetId = 0;
                }
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTPreOperationService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "Createdby", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
