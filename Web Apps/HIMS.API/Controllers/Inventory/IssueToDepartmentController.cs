using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IssueToDepartmentController : BaseController
    {
        private readonly IIssueToDepService _IIssueToDepService;
        public IssueToDepartmentController(IIssueToDepService repository)
        {
            _IIssueToDepService = repository;
        }
       
       
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IssueTODepModel obj)
        {
            TIssueToDepartmentHeader model = obj.issue.MapTo<TIssueToDepartmentHeader>();
            TIssueToDepartmentDetail objDep = obj.Depissue.MapTo<TIssueToDepartmentDetail>();
            TCurrentStock objCurrunt = obj.curruntissue.MapTo<TCurrentStock>();
            if (obj.issue.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.issue.IssueDate);
                model.IssueDate = Convert.ToDateTime(obj.issue.IssueDate);

                model.Addedby = CurrentUserId;

                obj.Depissue.IssueId = obj.Depissue.IssueId;
                //objDep.AddedBy = CurrentUserId;

                obj.curruntissue.ItemId = obj.curruntissue.ItemId;



                await _IIssueToDepService.InsertAsyncSP(model, objDep, objCurrunt, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment Added successfully.");
        }

        

        
       
    }
}
