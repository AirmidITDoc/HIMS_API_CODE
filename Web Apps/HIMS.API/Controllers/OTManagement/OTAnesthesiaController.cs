using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Services.OTManagment;
using HIMS.Core.Infrastructure;
using HIMS.API.Models.Masters;

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
        //[Permission(PageCode = "AnesthesiaRecord", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TOtAnesthesiaRecord> OtAnesthesiaRecordList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(OtAnesthesiaRecordList.ToGridResponse(objGrid, "OtAnesthesiaRecord List"));
        }
        [HttpGet("GetOtAnesthesiaPreOpdiagnosisList")]
        //[Permission(PageCode = "AnesthesiaRecord", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetOtInOperationDiagnosisList(string DescriptionType)
        {
            var result = await _IOTAnesthesiaService.OtAnesthesiaPreOpdiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetOtAnesthesiaPreOpdiagnosis List", result);
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _IOTAnesthesiaService.GetById(id);
            return data.ToSingleResponse<TOtAnesthesiaRecord, OTAnesthesiaModel>("TOtAnesthesiaRecord");
        }

       

        [HttpPost("Insert")]
        //[Permission(PageCode = "AnesthesiaRecord", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OTAnesthesiaModel obj)
        {
            TOtAnesthesiaRecord model = obj.MapTo<TOtAnesthesiaRecord>();
            model.IsActive = true;

            if (obj.AnesthesiaId == 0)
            {
                foreach (var q in model.TOtAnesthesiaPreOpdiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTAnesthesiaService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.AnesthesiaId);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "AnesthesiaRecord", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTAnesthesiaModel obj)
        {
            TOtAnesthesiaRecord model = obj.MapTo<TOtAnesthesiaRecord>();
            model.IsActive = true;

            if (obj.AnesthesiaId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                foreach (var q in model.TOtAnesthesiaPreOpdiagnoses)
                {
                    if (q.OtanesthesiaPreOpdiagnosisId == 0)
                    {
                        q.Createdby = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.OtanesthesiaPreOpdiagnosisId = 0;
                }

                await _IOTAnesthesiaService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.AnesthesiaId);
        }
        [HttpDelete]
        //[Permission(PageCode = "AnesthesiaRecord", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TOtAnesthesiaRecord model = await _repository.GetById(x => x.AnesthesiaId == Id);
            if ((model?.AnesthesiaId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

        }
    }
}
