using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Radiology
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

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MRadiologyTestMaster> MRadiologyTestMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MRadiologyTestMasterList.ToGridResponse(objGrid, "MRadiologyTestMaster  List"));
        }


        [HttpPost("RadiologyTestList")]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> RadiologyTestList(GridRequestModel objGrid)
        {
            IPagedList<RadiologyTestListDto> RadiologyTestList = await _RadiologyTestService.RadiologyTestList(objGrid);
            return Ok(RadiologyTestList.ToGridResponse(objGrid, "RadiologyTest List "));
        }

        [HttpPost("RadiologyPatientList")]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> RadiologyPatientList(GridRequestModel objGrid)
        {
            IPagedList<RadiologyPatientListDto> RadiologyPatientList = await _RadiologyTestService.GetListAsyn(objGrid);
            return Ok(RadiologyPatientList.ToGridResponse(objGrid, "RadiologyPatient List "));
        }
        [HttpPost("RetriveTemplateMasterList")]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> RadTemplateMasterList(GridRequestModel objGrid)
        {
            IPagedList<RadTemplateMasterListDto> RadTemplateMasterList = await _RadiologyTestService.TemplateListAsyn(objGrid);
            return Ok(RadTemplateMasterList.ToGridResponse(objGrid, "RadiologyPatient List "));
        }

        //[HttpPost("Insert")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(RadiologyTestModel obj)
        //{
        //    MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
        //    if (obj.TestId == 0)
        //    {
        //        model.CreatedDate = AppTime.Now;
        //        model.Addedby = CurrentUserId;
        //        model.IsActive = true;
        //        await _RadiologyTestService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        //}
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                await _RadiologyTestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.Updatedby = CurrentUserId;
                model.IsActive = true;
                await _RadiologyTestService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        //Delete API
        [HttpDelete]
        [Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MRadiologyTestMaster? model = await _repository.GetById(x => x.TestId == Id);
            if ((model?.TestId ?? 0) > 0)
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
