using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OTManagement;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.OTMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class ConstantsController : BaseController
    {
        private readonly IGenericService<MConstant> _repository;

        private readonly IConstantService _IConstantService;
        public ConstantsController(IConstantService repository, IGenericService<MConstant> repository1)
        {
            _IConstantService = repository;
            _repository = repository1;  

        }
        [HttpPost("ConstantsList")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ConstantsListDto> ConstantsList = await _IConstantService.GetListAsync(objGrid);
            return Ok(ConstantsList.ToGridResponse(objGrid, "Constants List "));
        }
        [HttpGet("search-ConstantsType")]
        [Permission]
        public ApiResponse SearchConstants(string Keyword)
        {
            var data = _IConstantService.SearchConstants(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Visit data", data);
        }

        //[HttpPut("{id:int}")]
        //[Permission]
        //public ApiResponse Update(ConstantModel obj)
        //{
        //    MConstant model = obj.MapTo<MConstant>();
        //    if (obj.ConstantId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.UpdatedBy = CurrentUserId;

        //        _IConstantService.Update(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}
        //Add API
        [HttpPost]
        //[Permission]
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ConstantModel obj)
        {
            MConstant model = obj.MapTo<MConstant>();
            model.IsActive = true;
            if (obj.ConstantId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedOn = AppTime.Now;
                model.UpdatedBy = CurrentUserId;
                model.UpdatedOn = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(ConstantModel obj)
        {
            MConstant model = obj.MapTo<MConstant>();
            model.IsActive = true;
            if (obj.ConstantId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;
                model.UpdatedOn = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedOn" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(int Id)
        {
            MConstant model = await _repository.GetById(x => x.ConstantId == Id);
            if ((model?.ConstantId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.UpdatedBy = CurrentUserId;
                model.UpdatedOn = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
