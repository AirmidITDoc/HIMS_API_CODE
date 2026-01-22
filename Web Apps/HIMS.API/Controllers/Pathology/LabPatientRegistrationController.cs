using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DoctorPayout;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Pathology;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.DoctorPayout;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.Pathology;
using HIMS.API.Models.OutPatient;
using HIMS.Data;
using HIMS.Data.DTO.OPPatient;
using HIMS.Core.Infrastructure;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabPatientRegistrationController : BaseController
    {
        private readonly ILabPatientRegistrationService _ILabPatientRegistrationService;
        private readonly IGenericService<TLabPatientRegistration> _repository;
        private readonly IGenericService<TLabPatientRegisteredMaster> _repository1;


        public LabPatientRegistrationController(ILabPatientRegistrationService repository, IGenericService<TLabPatientRegistration> repository1, IGenericService<TLabPatientRegisteredMaster> repository2)
        {
            _ILabPatientRegistrationService = repository;
            _repository = repository1;
            _repository1 = repository2;


        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            var data = await _repository.GetById(x => x.LabPatientId == id);
            return data.ToSingleResponse<TLabPatientRegistration, TLabPatientRegistrationModel>("LabPatientRegistration");
        }
        [HttpGet("GetLabPatientRegisteredMaster")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetLab(int id)
        {
            var data = await _repository1.GetById(x => x.LabPatRegId == id);
            return data.ToSingleResponse<TLabPatientRegisteredMaster, LabPatientRegistrationMasterModels>("TLabPatientRegisteredMaster");
        }


        [HttpPost("PrevLabDoctorVisitList")]
        public async Task<IActionResult> OPPrevDrVisistList(GridRequestModel objGrid)
        {
            IPagedList<PrevDrVisistListDto> Oplist = await _ILabPatientRegistrationService.GeOPPreviousDrVisitListAsync(objGrid);
            return Ok(Oplist.ToGridResponse(objGrid, "OP Prev Lab Dr Visit List"));
        }

        [HttpPost("List")]
       // [Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabPatientRegistrationListDto> LabPatientRegistrationList = await _ILabPatientRegistrationService.GetListAsync(objGrid);
            return Ok(LabPatientRegistrationList.ToGridResponse(objGrid, "Lab Patient Registration List"));
        }

        [HttpPost("LabSampleCollectionList")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> LabSamColList(GridRequestModel objGrid)
        {
            IPagedList<LabSampleCollectionListDto> LabSampleCollectionList = await _ILabPatientRegistrationService.GetSamColListAsync(objGrid);
            return Ok(LabSampleCollectionList.ToGridResponse(objGrid, "Lab sample Collection List"));
        }

        [HttpPost("LabSampleCollectionDetailList")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> LabSamColDetailList(GridRequestModel objGrid)
        {
            IPagedList<LabSampleCollectionDetailListDto> LabSampleCollectionDetailList = await _ILabPatientRegistrationService.GetSamColListDetailAsync(objGrid);
            return Ok(LabSampleCollectionDetailList.ToGridResponse(objGrid, "Lab sample Collection Detail List"));
        }

        [HttpPost("LabBillDetailList")]
        [Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> BillDetailList(GridRequestModel objGrid)
        {
            IPagedList<LabregBilldetailListDto> LabPatientBillList = await _ILabPatientRegistrationService.GetBillDetailListAsync(objGrid);
            return Ok(LabPatientBillList.ToGridResponse(objGrid, "Lab Patient Bill Detail List"));
        }

        [HttpPost("LabResultList")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> LabResultList(GridRequestModel objGrid)
        {
            IPagedList<LabResultListDto> LabResultList = await _ILabPatientRegistrationService.LabResultListAsync(objGrid);
            return Ok(LabResultList.ToGridResponse(objGrid, " Lab Result List "));
        }
        [HttpPost("LabResultDetailsList")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<IActionResult> LabResultDetailsList(GridRequestModel objGrid)
        {
            IPagedList<LabResultDetailsListDto> LabResultDetailsList = await _ILabPatientRegistrationService.LabResultDetailsListAsynch(objGrid);
            return Ok(LabResultDetailsList.ToGridResponse(objGrid, " Lab ResultDetails List "));
        }

        [HttpPost("Insert")]
        [Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(LabPatientRegistrationModels obj)
        {
            TLabPatientRegistration model = obj.MapTo<TLabPatientRegistration>();
            if (obj.LabPatientId == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPost("PatientRegistrationcreditbill")]
        [Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(LabRegistrationModels obj)
        {
            TLabPatientRegistration model = obj.LabPatientRegistration.MapTo<TLabPatientRegistration>();
            Bill model2 = obj.OPBillIngModels.MapTo<Bill>();
            Payment objPayment = obj.OPBillIngModels.Payments.MapTo<Payment>();
            List<AddCharge> ObjPackagecharge = obj.OPBillIngModels.Packcagecharges.MapTo<List<AddCharge>>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (obj.LabPatientRegistration.LabPatientId == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.InsertAsyncSP(model, model2, objPayment, ObjPackagecharge, ObjTPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPost("PatientRegistrationPaidBill")]
        [Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertPaidBill(LabRegistrationModels obj)
        {
            TLabPatientRegistration model = obj.LabPatientRegistration.MapTo<TLabPatientRegistration>();
            Bill model2 = obj.OPBillIngModels.MapTo<Bill>();
            Payment objPayment = obj.OPBillIngModels.Payments.MapTo<Payment>();
            List<AddCharge> ObjPackagecharge = obj.OPBillIngModels.Packcagecharges.MapTo<List<AddCharge>>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (obj.LabPatientRegistration.LabPatientId == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.InsertPaidBillAsync(model, model2, objPayment, ObjPackagecharge, ObjTPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model2.BillNo);
        }


        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LabPatientRegistrationMasterModels obj)
        {
            TLabPatientRegisteredMaster model = obj.MapTo<TLabPatientRegisteredMaster>();
            if (obj.LabPatRegId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _ILabPatientRegistrationService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpGet("search-patient-1")]
        [Permission]
        public ApiResponse SearchPatientNew( [FromQuery] long UnitId,  [FromQuery] string Keyword)
        {
            var data = _ILabPatientRegistrationService.SearchlabRegistration(UnitId, Keyword);

            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK, "Patient Visit data", data  );
        }


        [HttpGet("Labauto-complete")]
        //[Permission(PageCode = "LabPatientRegistration", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAutoComplete(string Keyword)
        {
            var data = await _ILabPatientRegistrationService.SearchLabRegistration(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Lab Registration Data.", data.Select(x => new
            {
                Text = x.FirstName + " " + x.LastName + " | " + x.MobileNo,
                Value = x.LabPatientId,
                MobileNo = x.MobileNo,
                AgeYear = x.AgeYear,
                AgeMonth = x.AgeMonth,
                AgeDay = x.AgeDay,
                PatientName = x.FirstName + " " + x.MiddleName + " " + x.LastName
            }));
        }

    }
}
