using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Pathology;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HomeCollectionController : BaseController
    {
        private readonly IHomeCollectionService _IHomeCollectionService;
        private readonly IGenericService<THomeCollectionRegistrationInfo> _repository;

        public HomeCollectionController(IHomeCollectionService repository, IGenericService<THomeCollectionRegistrationInfo> repository1)
        {
            _IHomeCollectionService = repository;
            _repository = repository1;


        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.HomeCollectionId == id);
            return data.ToSingleResponse<THomeCollectionRegistrationInfo, HomeCollectionModel>("THomeCollectionRegistrationInfo");
        }
        [HttpPost("homeCollectionDetList")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<homeCollectionDetListDto> homeCollectionDetList = await _IHomeCollectionService.GetListAsync(objGrid);
            return Ok(homeCollectionDetList.ToGridResponse(objGrid, "homeCollectionDet List"));
        }
        [HttpPost("HomeCollectionRegistrationInfoList")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> ListHomeCollection(GridRequestModel objGrid)
        {
            IPagedList<HomeCollectionRegistrationInfoListDto> HomeCollectionRegistrationInfoList = await _IHomeCollectionService.HomeCollectionListAsync(objGrid);
            return Ok(HomeCollectionRegistrationInfoList.ToGridResponse(objGrid, "HomeCollectionRegistrationInfo List"));
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(HomeCollectionModel obj)
        {
            THomeCollectionRegistrationInfo model = obj.MapTo<THomeCollectionRegistrationInfo>();
            if (obj.HomeCollectionId == 0)
            {
                foreach (var q in model.THomeCollectionServiceDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IHomeCollectionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.HomeCollectionId);
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(HomeCollectionModel obj)
        {
            THomeCollectionRegistrationInfo model = obj.MapTo<THomeCollectionRegistrationInfo>();
            if (obj.HomeCollectionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.THomeCollectionServiceDetails)
                {
                    if (q.HomeCollectionId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.HomeCollectionId = 0;
                }

              
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IHomeCollectionService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.HomeCollectionId);
        }
    }
}
