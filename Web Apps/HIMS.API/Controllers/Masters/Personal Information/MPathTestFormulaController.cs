using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MPathTestFormulaController : BaseController
    {

        private readonly IGenericService<MPathTestFormula> _repository;
        public MPathTestFormulaController(IGenericService<MPathTestFormula> repository)
        {
            _repository = repository;
        }

        //List API
        //[HttpPost]
        //[Route("[action]")]
        //[Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        //public async Task<IActionResult> List(GridRequestModel objGrid)
        //{
        //    IPagedList<MAreaMaster> AreaMasterList = await _repository.GetAllPagedAsync(objGrid);
        //    return Ok(AreaMasterList.ToGridResponse(objGrid, "Area List"));
        //}
        [HttpGet("{id?}")]
       // [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.FormulaId == id);
            return data.ToSingleResponse<MPathTestFormula, MPathTestFormulaModel>("MPathTestFormula");
        }


        [HttpPost]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MPathTestFormulaModel obj)
        {
            MPathTestFormula model = obj.MapTo<MPathTestFormula>();
            model.IsActive = true;
            if (obj.FormulaId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " MPathTestFormula added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
       // [Permission(PageCode = "AreaMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MPathTestFormulaModel obj)
        {
            MPathTestFormula model = obj.MapTo<MPathTestFormula>();
            model.IsActive = true;
            if (obj.FormulaId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MPathTestFormula updated successfully.");
        }
        //Delete API
        [HttpDelete]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MPathTestFormula? model = await _repository.GetById(x => x.FormulaId == Id);
            if ((model?.FormulaId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MPathTestFormula deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
