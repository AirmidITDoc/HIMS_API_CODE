using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
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
        //private readonly IGRNReturnService _IGRNReturnService;

        private readonly IGRNReturnService _gRNReturnService;
        public GRNReturnController(IGRNReturnService repository)
        {
            _gRNReturnService = repository;
        }

        [HttpPost("GRNReturnlistbynameList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNReturnlistbynameListAsync(GridRequestModel objGrid)
        {
            IPagedList<GrnListByNameListDto> List1 = await _gRNReturnService.GetGRnListbynameAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, " GRN Return List By Name"));
        }


        [HttpPost("GRNReturnList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GeGrnReturnListAsync(GridRequestModel objGrid)
        {
            IPagedList<GRNReturnListDto> List1 = await _gRNReturnService.GetGRNReturnList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "GRN Return  List"));
        }


        [HttpPost("ItemListBYSupplierName")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GeSupplierrateListAsync(GridRequestModel objGrid)
        {
            IPagedList<ItemListBysupplierNameDto> List1 = await _gRNReturnService.GetItemListbysuppliernameAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, " Item List By supplier Name"));
        }


        [HttpPost("GRNListBynameforGrnReturn")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNListBynameforGrnReturnAsync(GridRequestModel objGrid)
        {
            IPagedList<grnlistbynameforgrnreturnlistDto> List1 = await _gRNReturnService.Getgrnlistbynameforgrnreturn(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Grn List By name for GRN Return"));
        }



        [HttpPost("Insert")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Add)]
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
                await _gRNReturnService.InsertAsync(model, objCStock, objReturnQty, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN return added successfully.");
        }

        [HttpPost("Verify")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(GRNReturnVerifyModel obj)
        {
            TGrnreturnDetail model = obj.MapTo<TGrnreturnDetail>();
            if (obj.GrnreturnId != 0)
            {

                await _gRNReturnService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GRN verify successfully.");
        }
    }
}
