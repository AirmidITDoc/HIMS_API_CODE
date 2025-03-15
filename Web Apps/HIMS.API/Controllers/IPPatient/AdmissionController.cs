using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
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

    public class AdmissionController : BaseController
    {

        private readonly IAdmissionService _IAdmissionService;
        private readonly IGenericService<Admission> _repository1;
        public AdmissionController(IAdmissionService repository, IGenericService<Admission> repository1)
        {
            _IAdmissionService = repository;
            _repository1 = repository1;
        }


        [HttpPost("AdmissionList")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<AdmissionListDto> AdmissionListList = await _IAdmissionService.GetAdmissionListAsync(objGrid);
            return Ok(AdmissionListList.ToGridResponse(objGrid, "Admission List"));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data = await _repository1.GetById(x => x.AdmissionId == id);
            return data.ToSingleResponse<Admission, ADMISSIONModel>("Admission");
        }


        //[HttpPost("AdvanceList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> AdvanceList(GridRequestModel objGrid)
        //{
        //    IPagedList<AdvanceListDto> AdvanceList = await _IAdmissionService.GetAdvanceListAsync(objGrid);
        //    return Ok(AdvanceList.ToGridResponse(objGrid, "Advance List"));
        //}


        //[HttpPost("RefundOfAdvanceList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> RefundAdvanceList(GridRequestModel objGrid)
        //{
        //    IPagedList<RefundOfAdvanceListDto> RefundAdvanceList = await _IAdmissionService.GetRefundOfAdvanceListAsync(objGrid);
        //    return Ok(RefundAdvanceList.ToGridResponse(objGrid, "Refund Of Advance List"));
        //}


        //[HttpPost("IPBillList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> BillList(GridRequestModel objGrid)
        //{
        //    IPagedList<IPBillListDto> IPBillList = await _IAdmissionService.GetIPBillListListAsync(objGrid);
        //    return Ok(IPBillList.ToGridResponse(objGrid, "IP Bill List"));
        //}


        //[HttpPost("IPPaymentList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> PaymentList(GridRequestModel objGrid)
        //{
        //    IPagedList<IPPaymentListDto> IPPaymentList = await _IAdmissionService.GetIPPaymentListAsync(objGrid);
        //    return Ok(IPPaymentList.ToGridResponse(objGrid, "IP Payment List List"));
        //}


        //[HttpPost("IPRefundBillList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> RefundBillList(GridRequestModel objGrid)
        //{
        //    IPagedList<IPRefundBillListDto> IPRefundBillList = await _IAdmissionService.GetIPRefundBillListListAsync(objGrid);
        //    return Ok(IPRefundBillList.ToGridResponse(objGrid, "IP Refund Bill List"));
        //}



        [HttpPost("AdmissionInsertSP")]
        //[Permission(PageCode = "Admission", Permission = PagePermission.Add)]
        public ApiResponse AdmissionInsertSP(NewAdmission obj)
        {
            Registration model = obj.AdmissionReg.MapTo<Registration>();
            Admission objAdmission = obj.ADMISSION.MapTo<Admission>();
            if (obj.AdmissionReg.RegId == 0)
            {
                model.RegTime = Convert.ToDateTime(obj.AdmissionReg.RegTime);
                model.AddedBy = CurrentUserId;
                _IAdmissionService.InsertAsyncSP(model, objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Admission added successfully.", objAdmission);
        }


        [HttpPost("AdmissionRegisteredInsertSP")]
        //[Permission(PageCode = "Admission", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AdmissionRegisteredInsertSP(NewAdmission obj)
        {

            Admission objAdmission = obj.ADMISSION.MapTo<Admission>();

            await _IAdmissionService.InsertRegAsyncSP(objAdmission, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Registered Admission added successfully.", objAdmission);
        }


        //[HttpPost("AdmissionUpdateSP")]
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Admission", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> AdmissionUpdateSP(NewAdmission obj)
        {

            Admission objAdmission = obj.ADMISSION.MapTo<Admission>();
            if (obj.ADMISSION.AdmissionId != 0)
            {

                objAdmission.IsUpdatedBy = CurrentUserId;

                await _IAdmissionService.UpdateAdmissionAsyncSP(objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Admission  Updated successfully.", objAdmission);
        }
        [HttpGet("search-patient")]
        //[Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<ApiResponse> SearchPatient(string Keyword)
        {
            var data = await _IAdmissionService.PatientAdmittedListSearch(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Admission data", data);
        }



    }
}
