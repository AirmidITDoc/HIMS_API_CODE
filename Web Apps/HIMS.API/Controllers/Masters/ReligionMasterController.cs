using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ReligionMasterController : BaseController
    {
        private readonly IGenericService<MReligionMaster> _repository;
        public ReligionMasterController(IGenericService<MReligionMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ReligionMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MReligionMaster> MReligionMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MReligionMasterList.ToGridResponse(objGrid, "Religion List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ReligionMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ReligionId == id);
            return data.ToSingleResponse<MReligionMaster, ReligionMasterModel>("Religion ype");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "ReligionMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ReligionMasterModel obj)
        {
            MReligionMaster model = obj.MapTo<MReligionMaster>();
            //model.IsActive = true;
            if (obj.ReligionId == 0)
            {
            //    model.CreatedBy = CurrentUserId;
            //    model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Religion added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "ReligionMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReligionMasterModel obj)
        {
            MReligionMaster model = obj.MapTo<MReligionMaster>();
            //model.IsActive = true;
            if (obj.ReligionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Religion updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "ReligionMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MReligionMaster model = await _repository.GetById(x => x.ReligionId == Id);
            if ((model?.ReligionId ?? 0) > 0)
            {
                //model.IsActive = false;
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Religion deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
