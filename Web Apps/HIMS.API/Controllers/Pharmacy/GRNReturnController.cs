using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Purchase;
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
        [Permission(PageCode = "GRNReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNReturnlistbynameListAsync(GridRequestModel objGrid)
        {
            IPagedList<GrnListByNameListDto> List1 = await _gRNReturnService.GetGRnListbynameAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, " GRN Return List By Name"));
        }


        [HttpPost("GRNReturnList")]
        [Permission(PageCode = "GRNReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> GeGrnReturnListAsync(GridRequestModel objGrid)
        {
            IPagedList<GRNReturnListDto> List1 = await _gRNReturnService.GetGRNReturnList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "GRN Return  List"));
        }


        [HttpPost("GRNListBynameforGrnReturn")]
        [Permission(PageCode = "GRNReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNListBynameforGrnReturnAsync(GridRequestModel objGrid)
        {
            IPagedList<grnlistbynameforgrnreturnlistDto> List1 = await _gRNReturnService.Getgrnlistbynameforgrnreturn(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Grn List By name for GRN Return"));
        }

        [HttpPost("ItemListBYSupplierName")]
        [Permission(PageCode = "GRNReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> ItemListBysupplierNameAsync(GridRequestModel objGrid)
        {
            IPagedList<ItemListBysupplierNameDto> List1 = await _gRNReturnService.ItemListBysupplierNameAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "ItemListBYSupplierName"));
        }


        //[HttpPost("Insert")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(GRNReturnReqDto obj)
        //{
        //    TGrnreturnHeader model = obj.GrnReturn.MapTo<TGrnreturnHeader>();
        //    List<TCurrentStock> objCStock = obj.GrnReturnCurrentStock.MapTo<List<TCurrentStock>>();
        //    List<TGrndetail> objReturnQty = obj.GrnReturnReturnQt.MapTo<List<TGrndetail>>();
        //    if (obj.GrnReturn.GrnreturnId == 0)
        //    {
        //        model.GrnreturnDate = Convert.ToDateTime(obj.GrnReturn.GrnreturnDate);
        //        model.GrnreturnTime = Convert.ToDateTime(obj.GrnReturn.GrnreturnTime);
        //        model.AddedBy = CurrentUserId;
        //        model.UpdatedBy = 0;
        //        await _gRNReturnService.InsertAsync(model, objCStock, objReturnQty, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model.GrnreturnId);
        //}
        [HttpPost("Insert")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(GRNReturnReqDto obj)
        {
            TGrnreturnHeader model = obj.GrnReturn.MapTo<TGrnreturnHeader>();
            List<TGrnreturnDetail> model1 = obj.tGrnreturnDetails.MapTo<List<TGrnreturnDetail>>();
            List<TCurrentStock> objCStock = obj.GrnReturnCurrentStock.MapTo<List<TCurrentStock>>();
            List<TGrndetail> objReturnQty = obj.GrnReturnReturnQt.MapTo<List<TGrndetail>>();
            if (obj.GrnReturn.GrnreturnId == 0)
            {
                model.GrnreturnDate = Convert.ToDateTime(obj.GrnReturn.GrnreturnDate);
                model.GrnreturnTime = Convert.ToDateTime(obj.GrnReturn.GrnreturnTime);
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _gRNReturnService.InsertAsyncsp(model, model1, objCStock, objReturnQty, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.GrnreturnId);
        }

        [HttpPut("UpdateGRNReturn")]
        //[Permission(PageCode = "GRNReturn", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Update(GRNReturnUpdatereqDto obj)
        {
            TGrnreturnHeader model = obj.GrnReturn.MapTo<TGrnreturnHeader>();
            List<TGrnreturnDetail> model1 = obj.tGrnreturnDetails.MapTo<List<TGrnreturnDetail>>();
            List<TCurrentStock> model2= obj.GrnReturnCurrentStock.MapTo<List<TCurrentStock>>();
            List<TGrndetail> model3 = obj.GrnReturnReturnQt.MapTo<List<TGrndetail>>();
            if (obj.GrnReturn.GrnreturnId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            else
            {
                model.AddedBy = CurrentUserId;
                model.GrnreturnDate = DateTime.Now;


                await _gRNReturnService.UpdateAsyncsp(model, model1, model2, model3, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPost("Verify")]
        [Permission(PageCode = "GRNReturn", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(GRNReturnVerifyModel obj)
        {
            TGrnreturnHeader model = obj.MapTo<TGrnreturnHeader>();
            if (obj.GrnreturnId != 0)
            {

                await _gRNReturnService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record verify successfully.");
        }
    }
}
