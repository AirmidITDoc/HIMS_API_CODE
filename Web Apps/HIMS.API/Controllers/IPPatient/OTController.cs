using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTController : BaseController
    {

        private readonly IAdmissionService _IAdmissionService;
        private readonly IOTService _OTService;
        private readonly IGenericService<Admission> _repository1;
      //  private readonly IGenericService<Admission> _IOTService;

        public OTController(IAdmissionService repository, IGenericService<Admission> repository1)
        {
            _IAdmissionService = repository;
            _repository1 = repository1;
         //   _IOTService = repository1;
                }
     //   [HttpPost("OtbookingRequestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
      /*  public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TOtbookingRequest> TOtbookingRequestList = await _OTService.GetListAsync(objGrid);
            return Ok(TOtbookingRequestList.ToGridResponse(objGrid, "OtbookingRequest List"));
        }*/
        [HttpGet("{id?}")]
        // [Permission(PageCode = "Bed", Permission = PagePermission.View)]
     /*   public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _OTService.GetById(id);
            return data.ToSingleResponse<TOtbookingRequest, OTBookingRequestModel>("OtbookingRequest Master");
        }*/
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OTBookingRequestModel obj)
        {
            TOtbookingRequest model = obj.MapTo<TOtbookingRequest>();
            if (obj.OtbookingId == 0)
            {
                model.OtbookingTime = Convert.ToDateTime(obj.OtbookingTime);
                model.AddedBy = CurrentUserId;
            //    model.IsActive = true;
                await _OTService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OtbookingRequest Name  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTBookingRequestModel obj)
        {
            TOtbookingRequest model = obj.MapTo<TOtbookingRequest>();
            if (obj.OtbookingId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.OtbookingTime = Convert.ToDateTime(obj.OtbookingTime);
                await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OtbookingRequest Name updated successfully.");
        }


        [HttpPost("Cancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(OTBookingRequestModel obj)
        {
            TOtbookingRequest model = new();
            if (obj.OtbookingId != 0)
            {
                model.OtbookingId = obj.OtbookingId;
                await _OTService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OtbookingRequest Canceled successfully.");
        }

    }
}
