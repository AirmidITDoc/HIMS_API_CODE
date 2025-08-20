using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class CanteenRequestController : BaseController
    {

        private readonly ICanteenRequestService _ICanteenRequestService;
        private readonly IGenericService<TCanteenRequestHeader> _repository1;

        public CanteenRequestController(ICanteenRequestService repository)
        {
            _ICanteenRequestService = repository;
        }
        [HttpPost("DoctorNoteList")]
        [Permission(PageCode = "CanteenRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> DoctorNoteList(GridRequestModel objGrid)
        {
            IPagedList<DoctorNoteListDto> DoctorNoteList = await _ICanteenRequestService.DoctorNoteList(objGrid);
            return Ok(DoctorNoteList.ToGridResponse(objGrid, "DoctorNote App List"));
        }
        [HttpPost("TDoctorPatientHandoverList")]
        [Permission(PageCode = "CanteenRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> TDoctorPatientHandoverList(GridRequestModel objGrid)
        {
            IPagedList<TDoctorPatientHandoverListDto> TDoctorPatientHandoverList = await _ICanteenRequestService.TDoctorPatientHandoverList(objGrid);
            return Ok(TDoctorPatientHandoverList.ToGridResponse(objGrid, "TDoctorPatientHandover App List"));
        }
        [HttpPost("CanteenRequestList")]
        //[Permission(PageCode = "CanteenRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CanteenRequestListDto> CanteenRequestList = await _ICanteenRequestService.CanteenRequestsList(objGrid);
            return Ok(CanteenRequestList.ToGridResponse(objGrid, "CanteenRequest App List"));
        }
        [HttpPost("CanteenRequestHeaderList")]
        //[Permission(PageCode = "CanteenRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> HeaderList(GridRequestModel objGrid)
        {
            IPagedList<CanteenRequestHeaderListDto> CanteenRequestHeaderList = await _ICanteenRequestService.CanteenRequestHeaderList(objGrid);
            return Ok(CanteenRequestHeaderList.ToGridResponse(objGrid, "CanteenRequestHeader App List"));
        }

        [HttpPost("Insert")]

        //[Permission(PageCode = "CanteenRequest", Permission = PagePermission.Add)]

        public async Task<ApiResponse> Insert(CanteenRequestModel obj)
        {
            TCanteenRequestHeader model = obj.MapTo<TCanteenRequestHeader>();
            if (obj.ReqId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Date);
                model.Time = Convert.ToDateTime(obj.Time);
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedBy = CurrentUserId;
                await _ICanteenRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.ReqId);
        }
      
        [HttpGet("GetItemListforCanteen")]
        public async Task<ApiResponse> GetCanteenItemList(string ItemName)
        {
            var resultList = await _ICanteenRequestService.GetItemListForCanteen(ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetItemList For Canteen List.", resultList);
        }

    }


}
