using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using Asp.Versioning;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Inventory;
using static HIMS.API.Models.Masters.CompanyMasterModelValidator;
using System.Transactions;

namespace HIMS.API.Controllers.Masters.Billing
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CompanyMasterController : BaseController
    {
        private readonly IGenericService<CompanyMaster> _repository;
        private readonly IGenericService<ServiceWiseCompanyCode> _temprepository;
        private readonly ICompanyMasterService _CompanyMasterService;

        public CompanyMasterController(ICompanyMasterService repository, IGenericService<CompanyMaster> repository1 , IGenericService<ServiceWiseCompanyCode> repository2)
        {
            _CompanyMasterService = repository;
            _repository = repository1;
            _temprepository = repository2;
        }

        [HttpPost("CompanyMasterList")]
        //[Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> GetList(GridRequestModel objGrid)
        {
            IPagedList<CompanyMasterListDto> CompanyMasterList = await _CompanyMasterService.GetListAsync(objGrid);
            return Ok(CompanyMasterList.ToGridResponse(objGrid, "CompanyMasterList"));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CompanyId == id);
            return data.ToSingleResponse<CompanyMaster, CompanyMasterModel>("CompanyMaster");
        }


        [HttpPost]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CompanyMasterModel obj)
        {
            CompanyMaster model = obj.MapTo<CompanyMaster>();
            model.IsActive = true;
            if (obj.CompanyId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CompanyMasterModel obj)
        {
            CompanyMaster model = obj.MapTo<CompanyMaster>();
            model.IsActive = true;
            if (obj.CompanyId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            CompanyMaster model = await _repository.GetById(x => x.CompanyId == Id);
            if ((model?.CompanyId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



        [HttpPost("ServiceWiseCompanySave")]
    //    [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ServiceWiseCompanyModel obj)
        {
            ServiceWiseCompanyCode model = obj.MapTo<ServiceWiseCompanyCode>();
       //       model.IsActive = true;
            if (obj.ServiceDetCompId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _temprepository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("ServiceWiseCompanyUpdate")]
      //  [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ServiceWiseCompanyModel obj)
        {
            ServiceWiseCompanyCode model = obj.MapTo<ServiceWiseCompanyCode>();
         //   model.IsActive = true;
            if (obj.ServiceDetCompId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _temprepository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        ////Edit API
        //[HttpPut("updatecompanywiseservicerate")]
        ////[Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Editc(updatecompanywiseservicerate obj)
        //{
        //    ServiceDetail model = obj.MapTo<ServiceDetail>();
        //    if (obj.ServiceDetailId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {

        //        await _CompanyMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}

        [HttpPut("updatecompanywiseservicerate")]
        // [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Editc(List<updatecompanywiseservicerate> objs)
        {
            if (objs == null || objs.Count == 0)
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            foreach (var obj in objs)
            {
                if (obj.ServiceDetailId == 0);

                ServiceDetail model = obj.MapTo<ServiceDetail>();

                await _CompanyMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records updated successfully.");
        }



    }
}
