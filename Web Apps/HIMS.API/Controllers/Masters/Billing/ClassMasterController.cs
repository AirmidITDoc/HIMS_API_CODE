using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Inventory;
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ClassMasterController : BaseController
    {
        private readonly IGenericService<ClassMaster> _repository;
        private readonly IClassMasterService _IClassMasterService;

        public ClassMasterController(IGenericService<ClassMaster> repository, IClassMasterService repository1)
        {
            _repository = repository;
            _IClassMasterService = repository1;

        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ClassMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ClassMaster> ClassMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ClassMasterList.ToGridResponse(objGrid, "Class Master List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "ClassMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ClassId == id);
            return data.ToSingleResponse<ClassMaster, ClassMasterModel>("Class Master");
        }
        //[HttpPost("classApplyToAllService")]
        ////[Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(ApplytoAllServiceModel obj)
        //{
        //    ServiceDetail model = obj.MapTo<ServiceDetail>();
        //    if (obj.ServiceDetailId == 0)
        //    {
                

        //        long oldTariffId = obj.ServiceDetail?.FirstOrDefault()?.OldTariffId ?? 0;

        //        await _IClassMasterService.InsertAsync(model, CurrentUserId, CurrentUserName, (int)oldTariffId);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        //}
       
        [HttpPost("classApplyToAllService")]
        // [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ApplytoAllServiceModel obj)
        {
            if (obj == null)
            {
                return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status500InternalServerError,  "Invalid params");
            }

            if (obj.OldClassId <= 0 || obj.NewClassId <= 0)
            {
                return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status500InternalServerError,   "Invalid tariff parameters");
            }
            ServiceDetail model = obj.MapTo<ServiceDetail>();
            await _IClassMasterService.InsertAsync( model, CurrentUserId, CurrentUserName, (int)obj.OldClassId, (int)obj.NewClassId);

            return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status200OK,  "Record added successfully.");
        }

        
        //Add API
        [HttpPost]
        [Permission(PageCode = "ClassMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ClassMasterModel obj)
        {
            ClassMaster model = obj.MapTo<ClassMaster>();
            model.IsActive = true;
            if (obj.ClassId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "ClassMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ClassMasterModel obj)
        {
            ClassMaster model = obj.MapTo<ClassMaster>();
            model.IsActive = true;
            if (obj.ClassId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "ClassMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            ClassMaster model = await _repository.GetById(x => x.ClassId == Id);
            if ((model?.ClassId ?? 0) > 0)
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
