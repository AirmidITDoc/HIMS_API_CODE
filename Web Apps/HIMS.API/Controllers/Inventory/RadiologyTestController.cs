using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.PathTestDetailModelModelValidator;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RadiologyTestController : BaseController
    {
        private readonly IRadiologyTestService _RadiologyTestService;
        private readonly IGenericService<MRadiologyTestMaster> _repository;
        public RadiologyTestController(IRadiologyTestService repository, IGenericService<MRadiologyTestMaster> repository1)
        {
            _RadiologyTestService = repository;
            _repository = repository1;
        }
        [HttpPost("RadiologyList")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RadiologyListDto> RadiologyList = await _RadiologyTestService.GetListAsync(objGrid);
            return Ok(RadiologyList.ToGridResponse(objGrid, "RadiologyList "));
        }
        [HttpPost("RadiologyTestList")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> RadiologyTestList(GridRequestModel objGrid)
        {
            IPagedList<RadiologyTestListDto> RadiologyTestList = await _RadiologyTestService.RadiologyTestList(objGrid);
            return Ok(RadiologyTestList.ToGridResponse(objGrid, "RadiologyTest List "));
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _RadiologyTestService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RadiologyTest  added successfully.");
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                model.Updatedby = CurrentUserId;
                await _RadiologyTestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RadiologyTest   added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _RadiologyTestService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RadiologyTest updated successfully.");
        }
        //[HttpDelete("RadilogyCancel")]
        ////[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> Cancel(PathTestDetDelete obj)
        //{
        //    MRadiologyTestMaster model = new();
        //    if (obj.TestId != 0)
        //    {
        //        model.TestId = obj.TestId;
        //        model.ModifiedBy = CurrentUserId;
        //        model.ModifiedDate = DateTime.Now;
        //        await _RadiologyTestService.CancelAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RadiologyTest deleted successfully.");
        //}
        [HttpDelete]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MRadiologyTestMaster? model = await _repository.GetById(x => x.TestId == Id);
            if ((model?.TestId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MRadiologyTest deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
