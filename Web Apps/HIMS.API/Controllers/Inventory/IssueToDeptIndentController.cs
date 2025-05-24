using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IssueToDeptIndentController : BaseController
    {
        private readonly IIssueToDeptIndentService _IIssueToDeptIndentService;
        public IssueToDeptIndentController(IIssueToDeptIndentService repository)
        {
            _IIssueToDeptIndentService = repository;
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IssueToDIndentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDeptIndentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDept Indent added successfully.");
        }


        [HttpPost("UpdateIndentStatusAganist")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(UpdateIndentStatusModel obj)
        {
            TIssueToDepartmentHeader model = obj.UpdateIndent.MapTo<TIssueToDepartmentHeader>();
            List<TCurrentStock> model1 = obj.TCurStockModel.MapTo<List<TCurrentStock>>();
            TIndentHeader model2 = obj.IndentHeader.MapTo<TIndentHeader>();


            if (obj.UpdateIndent.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.UpdateIndent.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.UpdateIndent.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDeptIndentService.UpdateSP(model, model1, model2, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "UpdateIndentStatusAganist  successfully.", model.IssueId);
        }
    }
}
