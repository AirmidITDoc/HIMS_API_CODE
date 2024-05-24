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
    public class GRNController : BaseController
    {
        private readonly IGRNService _IGRNService;
        public GRNController(IGRNService repository)
        {
            _IGRNService = repository;
        }

        [HttpPost]
        //[Permission(PageCode = "GRN", Permission = PagePermission.Add)]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ApiResponse> post(GRNReqDto obj)
        {
            TGrnheader model = obj.MapTo<TGrnheader>();
            List<MItemMaster> objItems = obj.GrnItems.MapTo< List<MItemMaster>>();
            if (obj.Grn.Grnid == 0)
            {
                model.Grndate = DateTime.Now.Date;
                model.Grntime = DateTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _IGRNService.InsertAsync(model, objItems, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN added successfully.");
        }

        [HttpPut("{id:int}")]
        //[Permission(PageCode = "GRN", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(GRNReqDto obj)
        {
            TGrnheader model = obj.MapTo<TGrnheader>();
            List<MItemMaster> objItems = obj.GrnItems.MapTo<List<MItemMaster>>();
            if (obj.Grn.Grnid == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.Grndate = DateTime.Now.Date;
                model.Grntime = DateTime.Now;
                model.UpdatedBy = CurrentUserId;
                await _IGRNService.UpdateAsync(model, objItems, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN updated successfully.");
        }
    }
}
