using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;


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



        public OTInOperationController(IOTInOperationService repository, IGenericService<TOtInOperationHeader> repository1, IGenericService<TOtInOperationDiagnosis> repository2, IGenericService<TOtInOperationPostOperDiagnosis> repository3)
        {
            _IOTInOperationService = repository;
            _repository = repository1;
            _repository1 = repository2;
            _repository2 = repository3;

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
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
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
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtInOperationDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtInOperationPostOperDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtInOperationSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }

                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
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
                        q.CreatedDate = DateTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    q.OtinOperationAttendingDetId = 0;
                }

                foreach (var v in model.TOtInOperationDiagnoses)
                {
                    if (v.OtinOperationDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtinOperationDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtInOperationPostOperDiagnoses)
                {
                    if (v.OtinOperationPostOperDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtinOperationPostOperDiagnosisDetId = 0;
                }
                foreach (var v in model.TOtInOperationSurgeryDetails)
                {
                    if (v.OtinOperationSurgeryDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }

                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtinOperationSurgeryDetId = 0;
                }
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTInOperationService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
