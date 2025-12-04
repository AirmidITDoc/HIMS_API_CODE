using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTAnesthesiaController : BaseController
    {
        private readonly IOTAnesthesiaService _IOTAnesthesiaService;
        private readonly IGenericService<TOtAnesthesiaRecord> _repository;

        public OTAnesthesiaController(IOTAnesthesiaService repository, IGenericService<TOtAnesthesiaRecord> repository1)
        {
            _IOTAnesthesiaService = repository;
            _repository = repository1;

        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TOtAnesthesiaRecord> OtAnesthesiaRecordList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(OtAnesthesiaRecordList.ToGridResponse(objGrid, "OtAnesthesiaRecord List"));
        }
        [HttpGet("GetOtAnesthesiaPreOpdiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetOtInOperationDiagnosisList(string DescriptionType)
        {
            var result = await _IOTAnesthesiaService.OtAnesthesiaPreOpdiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetOtAnesthesiaPreOpdiagnosis List", result);
        }



        [HttpGet("{id?}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.AnesthesiaId == id);
            return data1.ToSingleResponse<TOtAnesthesiaRecord, OTAnesthesiaModel>("OTAnesthesiaModel");
        }


        [HttpPost("Insert")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OTAnesthesiaModel obj)
        {
            TOtAnesthesiaRecord model = obj.MapTo<TOtAnesthesiaRecord>();
            if (obj.AnesthesiaId == 0)
            {
                foreach (var q in model.TOtAnesthesiaPreOpdiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTAnesthesiaService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTAnesthesiaModel obj)
        {
            TOtAnesthesiaRecord model = obj.MapTo<TOtAnesthesiaRecord>();
            if (obj.AnesthesiaId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                foreach (var q in model.TOtAnesthesiaPreOpdiagnoses)
                {
                    if (q.OtanesthesiaPreOpdiagnosisId == 0)
                    {
                        q.Createdby = CurrentUserId;
                        q.CreatedDate = DateTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    q.OtanesthesiaPreOpdiagnosisId = 0;
                }

                await _IOTAnesthesiaService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
