using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pharmacy;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class GRNReturnController : BaseController
    {
        private readonly IGRNReturnService _IGRNReturnService;
        public GRNReturnController(IGRNReturnService repository)
        {
            _IGRNReturnService = repository;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Add)]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ApiResponse> Insert(GRNReturnReqDto obj)
        {
            TGrnreturnHeader model = obj.GrnReturn.MapTo<TGrnreturnHeader>();
            List<TCurrentStock> objCStock = obj.GrnReturnCurrentStock.MapTo<List<TCurrentStock>>();
            List<TGrndetail> objReturnQty = obj.GrnReturnReturnQt.MapTo<List<TGrndetail>>();
            if (obj.GrnReturn.GrnreturnId == 0)
            {
                model.GrnreturnDate = DateTime.Now.Date;
                model.GrnreturnTime = DateTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _IGRNReturnService.InsertAsync(model, objCStock, objReturnQty, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN return added successfully.");
        }

        [HttpPost("Verify")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Edit)]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ApiResponse> Verify(GRNReturnVerifyModel obj)
        {
            TGrnreturnDetail model = obj.MapTo<TGrnreturnDetail>();
            if (obj.GrnreturnId != 0)
            {

                await _IGRNReturnService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN verify successfully.");
        }
    }
}
