using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Inventory;
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
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> RoleMasterList(GridRequestModel objGrid)
        {
            IPagedList<RoleMasterListDto> RoleMasterList = await _IAdministrationService.RoleMasterList(objGrid);
            return Ok(RoleMasterList.ToGridResponse(objGrid, "RoleMaster App List"));
        }

        [HttpPost("DailyExpenceList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> DailyExpenceList(GridRequestModel objGrid)
        {
            IPagedList<DailyExpenceListtDto> DailyExpenceList = await _IAdministrationService.DailyExpencesList(objGrid);
            return Ok(DailyExpenceList.ToGridResponse(objGrid, "DailyExpenceList"));
        }

        [HttpPost("PaymentModeList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PaymentModeDto> PaymentModeList = await _IAdministrationService.GetListAsync(objGrid);
            return Ok(PaymentModeList.ToGridResponse(objGrid, "PaymentMode App List"));
        }
        [HttpPost("BrowseIPAdvPayPharReceiptList1")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseIPAdvPayPharReceiptList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPAdvPayPharReceiptListDto> BrowseIPAdvPayPharReceiptList = await _IAdministrationService.BrowseIPAdvPayPharReceiptList(objGrid);
            return Ok(BrowseIPAdvPayPharReceiptList.ToGridResponse(objGrid, "BrowseIPAdvPayPharReceipt1 App List"));
        }


        [HttpPost("BrowseReportTemplateConfigList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseReportTemplateList(GridRequestModel objGrid)
        {
            IPagedList<ReportTemplateListDto> List = await _IAdministrationService.BrowseReportTemplateList(objGrid);
            return Ok(List.ToGridResponse(objGrid, "ReportTemplate  Config List"));
        }


        //Add API
        [HttpPost("TemplateInsert")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ReportTemplateConfigModel obj)
        {
            MReportTemplateConfig model = obj.MapTo<MReportTemplateConfig>();
            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

        //Edit API
        [HttpPut("TemplateUpdate{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReportTemplateConfigModel obj)
        {
            MReportTemplateConfig model = obj.MapTo<MReportTemplateConfig>();
            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record updated successfully.");
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
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



        //[HttpPost("TExpenseInsert")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Posts(TExpenseModel obj)
        //{
        //    TExpense model = obj.MapTo<TExpense>();
        //    if (obj.ExpId == 0)
        //    {

        //        await _IAdministrationService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record  added successfully.");
        //}

        //[HttpPut("TExpenseUpdate{id:int}")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edits(TExpenseModel obj)
        //{
        //    TExpense model = obj.MapTo<TExpense>();
        //    if (obj.ExpId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {

        //        await _IAdministrationService.UpdateExpensesAsync(model, CurrentUserId, CurrentUserName, new string[2]);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record updated successfully.");
        //}

        //[HttpDelete("TExpenseCancel")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> TExpenseCancel(TExpenseCancelModel obj)
        //{
        //    TExpense Model = obj.MapTo<TExpense>();

        //    if (obj.ExpId != 0)
        //    {

        //      //   Model.IsAddedby = CurrentUserId;
        //        await _IAdministrationService.TExpenseCancel(Model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record delete successfully.");
        //}
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
        public async Task<ApiResponse> Update(AdmissionModell obj)
        {
            Admission model = obj.MapTo<Admission>();
            if (obj.AdmissionID == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IAdministrationService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpPut("UpdatePaymentdatetime{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> PaymentdatetimeUpdate(PaymenntModel obj)
        {
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IAdministrationService.PaymentUpdateAsync(model, CurrentUserId, CurrentUserName);
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


        [HttpPost("InsertDoctorPerMaster")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(MDoctorPerMasterModel obj)
        {
            MDoctorPerMaster model = obj.MapTo<MDoctorPerMaster>();
            if (obj.DoctorShareId == 0)
            {
             
                await _IAdministrationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        [HttpPut("UpdateDoctorPerMaster Edit/{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MDoctorPerMasterModel obj)
        {
            MDoctorPerMaster model = obj.MapTo<MDoctorPerMaster>();
            if (obj.DoctorShareId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                
                await _IAdministrationService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        [HttpPost("DoctorShareProcess")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DoctorShareProcessModel obj)
        {
            if (obj.FromDate == new DateTime(1900/01/01))

            {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
             //👇 Manually assign fields from LabRequestsModel to AddCharge
            var model = new AddCharge
            {
                //FromDate = obj.FromDate,
            };

            await _IAdministrationService.DoctorShareInsertAsync(model, CurrentUserId, CurrentUserName,obj.FromDate ,obj.ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added successfully.");
        }

        [HttpPost("AutoServiceListInsert")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(List<AutoServiceModel> objList)
        {
            if (objList == null || !objList.Any())
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            foreach (var obj in objList)
            {
                MAutoServiceList model = obj.MapTo<MAutoServiceList>();

                if (obj.SysId == 0)
                {
                    model.CreatedBy = CurrentUserId;
                    model.CreatedDate = DateTime.Now;
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDate = DateTime.Now;

                    await _repository1.Add(model, CurrentUserId, CurrentUserName);
                }
                else
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params in list");
                }
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records added successfully.");
        }

        [HttpPut("AutoServiceListUpdate")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(List<AutoServiceModel> objList)
        {
            if (objList == null || !objList.Any())
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            foreach (var obj in objList)
            {
                if (obj.SysId == 0)
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params in list");

                MAutoServiceList model = obj.MapTo<MAutoServiceList>();
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;

                await _repository1.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records updated successfully.");
        }


    }
}
