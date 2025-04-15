using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class CanteenRequestController : BaseController
    {

        private readonly ICanteenRequestService _ICanteenRequestService;
        public CanteenRequestController(ICanteenRequestService repository)
        {
            _ICanteenRequestService = repository;
        }
        [HttpPost("DoctorNoteList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> DoctorNoteList(GridRequestModel objGrid)
        {
            IPagedList<DoctorNoteListDto> DoctorNoteList = await _ICanteenRequestService.DoctorNoteList(objGrid);
            return Ok(DoctorNoteList.ToGridResponse(objGrid, "DoctorNote App List"));
        }
        [HttpPost("TDoctorPatientHandoverList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> TDoctorPatientHandoverList(GridRequestModel objGrid)
        {
            IPagedList<TDoctorPatientHandoverListDto> TDoctorPatientHandoverList = await _ICanteenRequestService.TDoctorPatientHandoverList(objGrid);
            return Ok(TDoctorPatientHandoverList.ToGridResponse(objGrid, "TDoctorPatientHandover App List"));
        }
        [HttpPost("CanteenRequestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CanteenRequestListDto> CanteenRequestList = await _ICanteenRequestService.CanteenRequestsList(objGrid);
            return Ok(CanteenRequestList.ToGridResponse(objGrid, "CanteenRequest App List"));
        }
        [HttpPost("CanteenRequestHeaderList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> HeaderList(GridRequestModel objGrid)
        {
            IPagedList<CanteenRequestHeaderListDto> CanteenRequestHeaderList = await _ICanteenRequestService.CanteenRequestHeaderList(objGrid);
            return Ok(CanteenRequestHeaderList.ToGridResponse(objGrid, "CanteenRequestHeader App List"));
        }

        [HttpPost("Insert")]

        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(CanteenRequestModel obj)
        {
            TCanteenRequestHeader model = obj.MapTo<TCanteenRequestHeader>();
            if (obj.ReqId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Time = Convert.ToDateTime(obj.Time);

                await _ICanteenRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CanteenRequest added successfully.", model);
        }

        [HttpGet("GetItemListforCanteen")]
        public async Task<ApiResponse> GetCanteenItemList(string ItemName)
        {
            var resultList = await _ICanteenRequestService.GetItemListForCanteen(ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For Canteen List.", resultList);
        }

    }
}
