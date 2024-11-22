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

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RelationshipMasterController : BaseController
    {
        private readonly IGenericService<MRelationshipMaster> _repository;
        public RelationshipMasterController(IGenericService<MRelationshipMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "RelationshipMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MRelationshipMaster> RelationshipMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RelationshipMasterList.ToGridResponse(objGrid, "RelationshipMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "RelationshipMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.RelationshipId == id);
            return data.ToSingleResponse<MRelationshipMaster, RelationshipMasterModel>("RelationshipMaster");
        }

        //Add API
        [HttpPost]
       // [Permission(PageCode = "RelationshipMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(RelationshipMasterModel obj)
        {
            MRelationshipMaster model = obj.MapTo<MRelationshipMaster>();
            model.IsActive = true;
            if (obj.RelationshipId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Relationship  Name added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "RelationshipMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RelationshipMasterModel obj)
        {
            MRelationshipMaster model = obj.MapTo<MRelationshipMaster>();
            model.IsActive = true;
            if (obj.RelationshipId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Relationship Name updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "RelationshipMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MRelationshipMaster model = await _repository.GetById(x => x.RelationshipId == Id);
            if ((model?.RelationshipId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Relationship  Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
