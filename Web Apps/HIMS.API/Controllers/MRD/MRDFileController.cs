using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.MRD;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Services.IPPatient;
using HIMS.API.Models.MRD;
using Asp.Versioning;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.MRD;
using HIMS.API.Models.DoctorPayout;

namespace HIMS.API.Controllers.MRD
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class MRDFileController : BaseController
    {
        private readonly IMRDFileService _IMRDFileService;

        public MRDFileController(IMRDFileService repository)
        {
            _IMRDFileService = repository;
        }
        [HttpPost("MRDFileReceivedList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MRDFileReceivedListDto> MRDFileReceivedList = await _IMRDFileService.GetListAsync(objGrid);
            return Ok(MRDFileReceivedList.ToGridResponse(objGrid, "MRDFileReceived List"));
        }
        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(MrdFileReceivedModel obj)
        {
            TMrdfileReceived model = obj.MapTo<TMrdfileReceived>();
            if (obj.RmdrecordId == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IMRDFileService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.RmdrecordId);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(MrdFileReceivedModel obj)
        {
            TMrdfileReceived model = obj.MapTo<TMrdfileReceived>();
            if (obj.RmdrecordId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
              
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IMRDFileService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.RmdrecordId);
        }
        [HttpPost("InsertOutFile")]
        //[Permission]
        public async Task<ApiResponse> InsertOutFile(MRDOutInFileModel obj)
        {
            TMrdoutInFile Outfilemodel = obj.MapTo<TMrdoutInFile>();
            if (obj.OutFileId == 0)
            {
                Outfilemodel.OutDate = Convert.ToDateTime(obj.OutDate);
                Outfilemodel.OutTime = Convert.ToDateTime(obj.OutTime);
                Outfilemodel.CreatedDate = AppTime.Now;
                Outfilemodel.CreatedBy = CurrentUserId == 0 ? 1 : CurrentUserId;
                Outfilemodel.ModifiedDate = AppTime.Now;
                Outfilemodel.ModifiedBy = CurrentUserId;
                await _IMRDFileService.InsertOutFileAsync(Outfilemodel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", Outfilemodel.OutFileId);
        }
        [HttpPost("InsertInFile")]
        //[Permission]
        public async Task<ApiResponse> InsertInFile(MRDOutInFileModel obj)
        {
            TMrdoutInFile Outfilemodel = obj.MapTo<TMrdoutInFile>();
            if (obj.OutFileId != 0)
            {
                Outfilemodel.OutDate = Convert.ToDateTime(obj.OutDate);
                Outfilemodel.OutTime = Convert.ToDateTime(obj.OutTime);
                Outfilemodel.CreatedDate = AppTime.Now;
                Outfilemodel.CreatedBy = CurrentUserId == 0 ? 1 : CurrentUserId;
                Outfilemodel.ModifiedDate = AppTime.Now;
                Outfilemodel.ModifiedBy = CurrentUserId;
                await _IMRDFileService.InsertInFileAsync(Outfilemodel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
    }
}
