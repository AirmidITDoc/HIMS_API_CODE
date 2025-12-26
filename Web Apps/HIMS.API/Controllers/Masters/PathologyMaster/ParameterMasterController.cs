using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.PathologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ParameterMasterController : BaseController
    {
        private readonly IGenericService<MPathParameterMaster> _repository;
        private readonly IParameterMasterService _IParameterMasterService;
        private readonly IMParameterDescriptiveMasterService _IMParameterDescriptiveMasterService;
        private readonly IMPathParaRangeWithAgeMasterService _IMPathParaRangeWithAgeMasterService;
        public ParameterMasterController(IParameterMasterService repository, IMParameterDescriptiveMasterService repository1, IGenericService<MPathParameterMaster> repository2, IMPathParaRangeWithAgeMasterService repository3)
        {
            _IParameterMasterService = repository;
            _IMParameterDescriptiveMasterService = repository1;
            _repository = repository2;
            _IMPathParaRangeWithAgeMasterService = repository3;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathParameterMaster> MPathParameterMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MPathParameterMasterList.ToGridResponse(objGrid, "MPathParameterMaster  List"));
        }
        [HttpPost("MPathParameterList")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<MPathParameterListDto> MPathParameterList = await _IParameterMasterService.MPathParameterList(objGrid);
            return Ok(MPathParameterList.ToGridResponse(objGrid, "Pathology Parameter List"));
        }

        [HttpPost("MPathParaRangeWithAgeMasterList")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> MPathParaRangeWithAgeMasterList(GridRequestModel objGrid)
        {
            IPagedList<MPathParaRangeWithAgeMasterListDto> MPathParaRangeWithAgeMasterList = await _IMPathParaRangeWithAgeMasterService.MPathParaRangeWithAgeMasterList(objGrid);
            return Ok(MPathParaRangeWithAgeMasterList.ToGridResponse(objGrid, "MPathParaRangeWithAgeMaster List"));
        }
        [HttpPost("MParameterDescriptiveMasterList")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> MParameterDescriptiveMasterList(GridRequestModel objGrid)
        {
            IPagedList<MParameterDescriptiveMasterListDto> MParameterDescriptiveMasterList = await _IMParameterDescriptiveMasterService.GetListAsync1(objGrid);
            return Ok(MParameterDescriptiveMasterList.ToGridResponse(objGrid, "MParameterDescriptiveMaster  List"));
        }
        //Add API
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ParameterMasterModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();

            if (obj.ParameterId == 0)
            {
                model.IsActive = true;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;


                await _IParameterMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter added successfully.");
        }

        //Edit API
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ParameterMasterModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();

            if (obj.ParameterId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IsActive = true;
                model.ModifiedBy = CurrentUserId;
                model.UpdatedBy = CurrentUserId;
                model.AddedBy = 0;
                model.ModifiedDate = AppTime.Now;
                await _IParameterMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate"});
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter updated successfully.");
        }
        [HttpDelete("ParameterCancel")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MPathParameterMaster model = await _repository.GetById(x => x.ParameterId == Id);
            if ((model?.ParameterId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                //return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter Deleted successfully.");
        }

        //Edit API
        [HttpPut("EditFormula/{id:int}")]
        [Permission(PageCode = "ParameterMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(UpdateParameterFormulaModel obj)
        {
            MPathParameterMaster model = obj.MapTo<MPathParameterMaster>();
            if (obj.ParameterId != 0)
            {
                model.ParameterId = obj.ParameterId;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _IParameterMasterService.UpdateFormulaAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Parameter Formula update successfully.");
        }

    }
}
