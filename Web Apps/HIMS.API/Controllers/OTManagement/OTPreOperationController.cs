using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.OTManagement;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.OTManagement;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTPreOperationController : BaseController
    {

        private readonly IOTPreOperationService _IOTPreOperationService;
        public OTPreOperationController(IOTPreOperationService repository)
        {
            _IOTPreOperationService = repository;
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
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtPreOperationCathlabDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtPreOperationDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtPreOperationSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }

                model.CreatedDate = DateTime.Now;
                model.Createdby = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
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
                        q.CreatedDate = DateTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    q.OtpreOperationAttendingDetId = 0;
                }

                foreach (var v in model.TOtPreOperationCathlabDiagnoses)
                {
                    if (v.OtpreOperationCathLabDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtpreOperationCathLabDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtPreOperationDiagnoses)
                {
                    if (v.OtpreOperationDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtpreOperationDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtPreOperationSurgeryDetails)
                {
                    if (v.OtpreOperationSurgeryDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtpreOperationSurgeryDetId = 0;
                }
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTPreOperationService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "Createdby", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
