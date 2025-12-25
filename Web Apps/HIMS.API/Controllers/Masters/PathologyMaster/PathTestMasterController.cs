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

namespace HIMS.API.Controllers.Masters.PathologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathTestMasterController : BaseController
    {
        private readonly IGenericService<MPathTestMaster> _repository;
        private readonly ITestMasterServices _ITestmasterService;
        public PathTestMasterController(ITestMasterServices repository, IGenericService<MPathTestMaster> repository1)
        {
            _ITestmasterService = repository;
            _repository = repository1;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathTestMaster> MPathTestMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MPathTestMasterList.ToGridResponse(objGrid, "MPathTestMaster  List"));
        }

        [HttpPost("PathTestList")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> PathList(GridRequestModel objGrid)
        {
            IPagedList<PathTestListDto> PathTestList = await _ITestmasterService.PetListAsync(objGrid);
            return Ok(PathTestList.ToGridResponse(objGrid, "PathTestList"));
        }

        [HttpPost("PathTestDetailList")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> PathSubTestList(GridRequestModel objGrid)
        {
            IPagedList<PathTestDetailDto> TestMasterList = await _ITestmasterService.PathTestDetailListAsync(objGrid);
            return Ok(TestMasterList.ToGridResponse(objGrid, "PathTestDetailList"));
        }

        [HttpPost("PathTestForUpdateList")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> TestForUpdateList(GridRequestModel objGrid)
        {
            IPagedList<PathTestForUpdateListdto> PathTestForUpdateList = await _ITestmasterService.ListAsync(objGrid);
            return Ok(PathTestForUpdateList.ToGridResponse(objGrid, "PathTestForUpdateList"));
        }

        [HttpPost("PathSubTestList")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<SubTestMasterListDto> TestMasterList = await _ITestmasterService.GetListAsync(objGrid);
            return Ok(TestMasterList.ToGridResponse(objGrid, "PathSubTestList"));
        }

        [HttpPost("PathTemplateForUpdateList")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> TList(GridRequestModel objGrid)
        {
            IPagedList<PathTemplateForUpdateListDto> PathTemplateForUpdateList = await _ITestmasterService.PathTemplateList(objGrid);
            return Ok(PathTemplateForUpdateList.ToGridResponse(objGrid, "PathTemplateForUpdateList"));
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public ApiResponse Insert(TestMasterModel obj)
        {
            MPathTestMaster model = obj.PathTest.MapTo<MPathTestMaster>();
            List<MPathTemplateDetail> PathTemplate = obj.PathTemplateDetail.MapTo<List<MPathTemplateDetail>>();
            List<MPathTestDetailMaster> TemplateDetail = obj.PathTestDetail.MapTo<List<MPathTestDetailMaster>>();


            if (obj.PathTest.TestId == 0)
            {
                //PathTemplate.ForEach(x => { x.TestId = obj.PathTest.TestId;});
                //TemplateDetail.ForEach(x => { x.TestId = obj.PathTest.TestId; });
                model.IsActive = true;

                _ITestmasterService.InsertSP(model, PathTemplate, TemplateDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathTest  added successfully.");
        }

        [HttpPut("Update/{id:int}")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public ApiResponse Update(TestMasterUpdate obj)
        {
            MPathTestMaster model = obj.PathTest.MapTo<MPathTestMaster>();
            List<MPathTemplateDetail> PathTemplate = obj.PathTemplateDetail.MapTo<List<MPathTemplateDetail>>();
            List<MPathTestDetailMaster> TemplateDetail = obj.PathTestDetail.MapTo<List<MPathTestDetailMaster>>();


            if (obj.PathTest.TestId != 0)
            {
                //PathTemplate.ForEach(x => { x.TestId = obj.PathTest.TestId;});
                //TemplateDetail.ForEach(x => { x.TestId = obj.PathTest.TestId; });
                model.IsActive = true;

                _ITestmasterService.UpdateSP(model, PathTemplate, TemplateDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathTest  Update successfully.");
        }


        //[HttpPost("InsertEDMX")]
        ////[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(PathTestMasterModel obj)
        //{
        //    MPathTestMaster model = obj.MapTo<MPathTestMaster>();
        //    if (obj.TestId == 0)
        //    {
        //        //model.TestTime = Convert.ToDateTime(obj.TestTime);
        //        model.AddedBy = CurrentUserId;
        //        model.IsActive = true;
        //        await _ITestmasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathTest  added successfully.");
        //}


        [HttpDelete("PathTestDelete")]
        [Permission(PageCode = "TestMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {

            MPathTestMaster model = await _repository.GetById(x => x.TestId == Id);
            if ((model?.TestId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
