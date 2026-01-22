using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.PathologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CompanyExecutiveInfoController : BaseController
    {
        private readonly IGenericService<MCompanyExecutiveInfo> _repository;
        public CompanyExecutiveInfoController(IGenericService<MCompanyExecutiveInfo> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "CompanyExecutiveInfo", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCompanyExecutiveInfo> CompanyExecutiveInfo = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CompanyExecutiveInfo.ToGridResponse(objGrid, "Company Executive Info List"));
        }


        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "CompanyExecutiveInfo", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<MCompanyExecutiveInfo, CompanyExecutiveInfoModel>("CompanyExecutiveInfo");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "CompanyExecutiveInfo", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CompanyExecutiveInfoModel obj)
        {
            MCompanyExecutiveInfo model = obj.MapTo<MCompanyExecutiveInfo>();
           // model.IsActive = true;
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "CompanyExecutiveInfo", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CompanyExecutiveInfoModel obj)
        {
            MCompanyExecutiveInfo model = obj.MapTo<MCompanyExecutiveInfo>();
           // model.IsActive = true;
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

    }
}
