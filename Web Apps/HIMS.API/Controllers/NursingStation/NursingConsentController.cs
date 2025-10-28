﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NursingConsentController : BaseController
    {
        private readonly INursingConsentService _NursingConsentService;

        private readonly IGenericService<TConsentInformation> _repository;

        public NursingConsentController(INursingConsentService repository, IGenericService<TConsentInformation> repository4)
        {
            _repository = repository4;
            _NursingConsentService = repository;

        }
        [HttpGet("GetMConsentMasterList")]
        public async Task<ApiResponse> GetMConsentMaster(int DeptId)
        {
            var resultList = await _NursingConsentService.GetConsent(DeptId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ConsentMasterList.", resultList.Select(x => new
            {
                x.ConsentId,
                x.ConsentName,
                x.ConsentDesc,

            }));
        }

        [HttpGet("DeptConsentList")]
        //  [Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ConsentDeptListDto> SupplierList = await _NursingConsentService.GetListAsync(objGrid);
            return Ok(SupplierList.ToGridResponse(objGrid, "DeptConsentList "));
        }


        [HttpPost("InsertConsent")]
        //   [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public ApiResponse insert(ConsentInformationModel obj)
        {
            TConsentInformation Model = obj.MapTo<TConsentInformation>();

            if (obj.ConsentId == 0)
            {
                Model.CreatedDatetime = DateTime.Now;
                Model.CreatedBy = CurrentUserId;
                _NursingConsentService.Insert(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ConsentInformation Added successfully.", Model);
        }


        [HttpPut("UpdateConsent")]
        //   [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public ApiResponse Update(UpdateConsentInformationModel obj)
        {
            TConsentInformation Model = obj.MapTo<TConsentInformation>();

            if (obj.ConsentId != 0)
            {
                Model.ModifiedDateTime = DateTime.Now;
                Model.ModifiedBy = CurrentUserId;
                _NursingConsentService.Update(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", Model);
        }

        [HttpPost("ConsentpatientInfoList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> ConsentpatientInfoList(GridRequestModel objGrid)
        {
            IPagedList<ConsentpatientInfoListDto> ConsentpatientInfoList = await _NursingConsentService.ConsentpatientInfoList(objGrid);
            return Ok(ConsentpatientInfoList.ToGridResponse(objGrid, "ConsentpatientInfo List"));
        }
    }
}
