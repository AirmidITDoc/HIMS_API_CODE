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
using HIMS.API.Models.Pharmacy;
using HIMS.API.Models.Administration;
using HIMS.Services.Common;
using static HIMS.API.Models.Masters.CompanyMasterModelValidator;
using System.Transactions;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Controllers.Masters.Billing
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CompanyMasterController : BaseController
    {
        private readonly IGenericService<CompanyMaster> _repository;
        private readonly IGenericService<MCompanyWiseServiceDiscount> _repository1;

        private readonly IGenericService<ServiceWiseCompanyCode> _temprepository;
        private readonly ICompanyMasterService _CompanyMasterService;

        public CompanyMasterController(ICompanyMasterService repository, IGenericService<CompanyMaster> repository1, IGenericService<MCompanyWiseServiceDiscount> repository2,IGenericService<ServiceWiseCompanyCode> repository3)
        {
            _CompanyMasterService = repository;
            _repository = repository1;
            _repository1 = repository2;
            _temprepository = repository3;

        }

        [HttpPost("CompanyMasterList")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> GetList(GridRequestModel objGrid)
        {
            IPagedList<CompanyMasterListDto> CompanyMasterList = await _CompanyMasterService.GetListAsync(objGrid);
            return Ok(CompanyMasterList.ToGridResponse(objGrid, "CompanyMasterList"));
        }

        [HttpPost("ServiceTariffWiseList")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> SGetList(GridRequestModel objGrid)
        {
            IPagedList<ServiceTariffWiseListDto> ServiceTariffWiseList = await _CompanyMasterService.SGetListAsync(objGrid);
            return Ok(ServiceTariffWiseList.ToGridResponse(objGrid, "ServiceTariffWiseList"));
        }

        [HttpPost("ServiceCompanyTariffWiseList")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> CGetList(GridRequestModel objGrid)
        {
            IPagedList<ServiceCompanyTariffWiseListDto> ServiceCompanyTariffWiseList = await _CompanyMasterService.CGetListAsync(objGrid);
            return Ok(ServiceCompanyTariffWiseList.ToGridResponse(objGrid, "ServiceCompanyTariffWiseList"));
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
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Insert(ServiceWiseModel obj)
        {
            List<ServiceWiseCompanyCode> model = obj.ServiceWise.MapTo<List<ServiceWiseCompanyCode>>();

            if (model.Count > 0)
            {

                await _CompanyMasterService.InsertAsyncsp(model, CurrentUserId, CurrentUserName, obj.userId);
            }

            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

  
      
        [HttpPut("updatecompanywiseservicerate")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Editc(List<updatecompanywiseservicerate> objs)
        {
            if (objs == null || objs.Count == 0)
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            foreach (var obj in objs)
            {
                if (obj.ServiceId == 0);

                ServiceDetail model = obj.MapTo<ServiceDetail>();

                await _CompanyMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records updated successfully.");
        }

     

        [HttpPost("CompanyWiseServiceDiscount")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> postc(CompanyWiseServiceModel obj)
        {
            List<MCompanyWiseServiceDiscount> model = obj.CompanyWiseService.MapTo<List<MCompanyWiseServiceDiscount>>();

            if (model.Count > 0)
            {

                await _CompanyMasterService.InsertAsyncs(model, CurrentUserId, CurrentUserName, obj.userId);
            }

            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }


    }
}
