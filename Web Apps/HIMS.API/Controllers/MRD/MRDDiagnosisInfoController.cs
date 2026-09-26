using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.MRD;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
using HIMS.Services.MRD;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.MRD
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MRDDiagnosisInfoController : BaseController
    {
        private readonly IMrdDiagnosisInfoService _IMrdDiagnosisInfoService;

        public MRDDiagnosisInfoController(IMrdDiagnosisInfoService repository)
        {
            _IMrdDiagnosisInfoService = repository;
        }
        [HttpPost("MRDDiagnosisInfoList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MrdDiagnosisInfoListDto> MRDDiagnosisInfoList = await _IMrdDiagnosisInfoService.GetListAsync(objGrid);
            return Ok(MRDDiagnosisInfoList.ToGridResponse(objGrid, "MRDDiagnosisInfo List"));
        }
       
        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(MrdDiagnosisInfo obj)

        {
            TIpMrdDiagnosisInfoHeader model = obj.MrdDiagnosisInfoHeader.MapTo<TIpMrdDiagnosisInfoHeader>();
            List<TIpMrdDiagnosisInfoDetail> objTIpMrdDiagnosisInfoDetails = obj.MrdDiagnosisInfoDetail.MapTo<List<TIpMrdDiagnosisInfoDetail>>();


            if (model.IpdiagId == 0)
            {
                model.CreatedBy = CurrentUserId;
                await _IMrdDiagnosisInfoService.InsertAsync(model, objTIpMrdDiagnosisInfoDetails, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IpdiagId);
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(MrdDiagnosisInfoUpdate obj)
        {
            TIpMrdDiagnosisInfoHeader model = obj.MrdDiagnosisInfoHeader.MapTo<TIpMrdDiagnosisInfoHeader>();
            List<TIpMrdDiagnosisInfoDetail> objTIpMrdDiagnosisInfoDetails = obj.MrdDiagnosisInfoDetail.MapTo<List<TIpMrdDiagnosisInfoDetail>>();

            if (model.IpdiagId != 0)
            {
                model.ModifiedBy = CurrentUserId;

                await _IMrdDiagnosisInfoService.UpdateAsync(model, objTIpMrdDiagnosisInfoDetails, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.IpdiagId);
        }
        //[HttpPost("Insert")]
        ////[Permission]
        //public async Task<ApiResponse> Insert(MrdDiagnosisInfoModel obj)
        //{
        //    TIpMrdDiagnosisInfoHeader model = obj.MapTo<TIpMrdDiagnosisInfoHeader>();
        //    if (obj.IpdiagId == 0)
        //    {
        //        foreach (var q in model.TIpMrdDiagnosisInfoDetails)
        //        {
        //            q.CreatedBy = CurrentUserId;
        //            q.CreatedDate = AppTime.Now;

        //        }
        //        model.CreatedDate = AppTime.Now;
        //        model.CreatedBy = CurrentUserId;
        //        model.ModifiedDate = AppTime.Now;
        //        model.ModifiedBy = CurrentUserId;
        //        await _IMrdDiagnosisInfoService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IpdiagId);
        //}



    }
}
