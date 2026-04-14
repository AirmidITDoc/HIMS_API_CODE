using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HelpdeskPatientComplaintsController : BaseController
    {
        private readonly IGenericService<HelpdeskPatientComplaint> _repository;
        private readonly IHelpdeskPatientComplaintService _IHelpdeskPatientComplaintService;

        public HelpdeskPatientComplaintsController(IGenericService<HelpdeskPatientComplaint> repository, IHelpdeskPatientComplaintService repository1)
        {
            _repository = repository;
            _IHelpdeskPatientComplaintService = repository1;

        }
        [HttpPost("ComplaintList")]
        //[Permission(PageCode = "Sales", Administration = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ComplaintListDto> ComplaintList = await _IHelpdeskPatientComplaintService.GetListAsync(objGrid);
            return Ok(ComplaintList.ToGridResponse(objGrid, "Complaint List"));
        }
        //Add API
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(HelpdeskPatientComplaintModel obj)
        {
            HelpdeskPatientComplaint model = obj.MapTo<HelpdeskPatientComplaint>();
            if (obj.ComplaintId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(HelpdeskPatientComplaintModel obj)
        {
            HelpdeskPatientComplaint model = obj.MapTo<HelpdeskPatientComplaint>();
            if (obj.ComplaintId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            HelpdeskPatientComplaint model = await _repository.GetById(x => x.ComplaintId == Id);
            if ((model?.ComplaintId ?? 0) > 0)
            {
                model.IsDischarge = model.IsDischarge == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
