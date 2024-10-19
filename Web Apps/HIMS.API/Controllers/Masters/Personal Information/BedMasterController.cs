using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    public class BedMasterController : BaseController
    {
        private readonly IGenericService<Bedmaster> _repository;
        public BedMasterController(IGenericService<Bedmaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
       // [Permission(PageCode = "Bed", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<Bedmaster> BedmasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(BedmasterList.ToGridResponse(objGrid, "Bed Master List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
       // [Permission(PageCode = "Bed", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.BedId == id);
            return data.ToSingleResponse<Bedmaster, BedMasterModel>("Bed Master");
        }

        //Add API
        [HttpPost]
      //  [Permission(PageCode = "Bed", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(BedMasterModel obj)
        {
            Bedmaster model = obj.MapTo<Bedmaster>();
            model.IsActive = true;
            if (obj.BedId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bed  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
       // [Permission(PageCode = "Bed", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(BedMasterModel obj)
        {
            Bedmaster model = obj.MapTo<Bedmaster>();
             model.IsActive = true;
            if (obj.BedId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bed updated successfully.");
        }

        //Delete API
        [HttpDelete]
       // [Permission(PageCode = "Bed", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            Bedmaster? model = await _repository.GetById(x => x.BedId == Id);
            if ((model?.BedId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bed deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
