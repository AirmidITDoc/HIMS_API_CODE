using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.API.Models.OutPatient;
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
    public class AdministrationController : BaseController
    {

        private readonly IAdministrationService _IAdministrationService;
        private readonly IGenericService<MReportTemplateConfig> _repository;
        private readonly IGenericService<MAutoServiceList> _repository1;

        public AdministrationController(IAdministrationService repository, IGenericService<MReportTemplateConfig> repository1, IGenericService<MAutoServiceList> repository2)
        {
            _IAdministrationService = repository;
            _repository = repository1;
            _repository1 = repository2;

        }

        [HttpPost("RoleMasterList")]
        //[Permission(PageCode = "Sales", Administration = PagePermission.View)]
        public async Task<IActionResult> RoleMasterList(GridRequestModel objGrid)
        {
            IPagedList<RoleMasterListDto> RoleMasterList = await _IAdministrationService.RoleMasterList(objGrid);
            return Ok(RoleMasterList.ToGridResponse(objGrid, "RoleMaster App List"));
        }

        [HttpPost("PaymentModeList")]
        //[Permission(PageCode = "Sales", Administration = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PaymentModeDto> PaymentModeList = await _IAdministrationService.GetListAsync(objGrid);
            return Ok(PaymentModeList.ToGridResponse(objGrid, "PaymentMode App List"));
        }
        [HttpPost("BrowseIPAdvPayPharReceiptList1")]
        //[Permission(PageCode = "Sales", Administration = PagePermission.View)]
        public async Task<IActionResult> BrowseIPAdvPayPharReceiptList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPAdvPayPharReceiptListDto> BrowseIPAdvPayPharReceiptList = await _IAdministrationService.BrowseIPAdvPayPharReceiptList(objGrid);
            return Ok(BrowseIPAdvPayPharReceiptList.ToGridResponse(objGrid, "BrowseIPAdvPayPharReceipt1 App List"));
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "Administration", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MReportTemplateConfig model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpPost("IP_DISCHARGE_CANCELLATION")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Delete(AdmissionsModel obj)
        {
            Admission Model = obj.MapTo<Admission>();

            if (obj.AdmissionID != 0)
            {

                //  Model.AddedBy = CurrentUserId;
                await _IAdministrationService.DeleteAsync(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record delete successfully.");
        }
        [HttpPut("UpdateAdmissiondatetime{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public ApiResponse Update(AdmissionModell obj)
        {
            Admission model = obj.MapTo<Admission>();
            if (obj.AdmissionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                _IAdministrationService.Update(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpPut("UpdatePaymentdatetime{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public ApiResponse PaymentdatetimeUpdate(PaymenntModel obj)
        {
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                _IAdministrationService.PaymentUpdate(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPut("UpdateBilldatetime{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> BilldatetimeUpdate(BilllsModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IAdministrationService.BilldateUpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


     
        [HttpPost("AutoServiceListInsert")]
        public async Task<ApiResponse> Insert(List<AutoServiceModel> objList)
        {
            if (objList == null || !objList.Any())
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            var entityList = objList.Select(obj =>
            {
                var model = obj.MapTo<MAutoServiceList>();

                model.SysId = 0;  // FORCE INSERT
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                return model;
            }).ToList();

            await _IAdministrationService.InsertListAsync(entityList, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records added successfully.");
        }

        [HttpPut("TPaymentdatetime{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public ApiResponse Update(PaymenntModel obj)
        {
            TPayment model = obj.MapTo<TPayment>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                _IAdministrationService.Update(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPut("TPaymentPharmacydatetime{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public ApiResponse PaymentUpdate(PaymenntModel obj)
        {
            TPaymentPharmacy model = obj.MapTo<TPaymentPharmacy>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                _IAdministrationService.PaymentDateTimeUpdate(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
