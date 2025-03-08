using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NursingController: BaseController
    {
        private readonly ILabRequestService _ILabRequestService;
        private readonly IMPrescriptionService _IMPrescriptionService;
        private readonly IPriscriptionReturnService _IPriscriptionReturnService;
        private readonly ICanteenRequestService _ICanteenRequestService;
        private readonly INursingNoteService _INursingNoteService;

        

        private readonly IGenericService<TNursingNote> _repository;
        public NursingController(ILabRequestService repository, IMPrescriptionService repository1 ,IPriscriptionReturnService repository2, ICanteenRequestService repository3, IGenericService<TNursingNote> repository4,
            INursingNoteService INursingNoteService)
        {
            _ILabRequestService = repository;
            _IMPrescriptionService = repository1;
            _IPriscriptionReturnService = repository2;
            _ICanteenRequestService = repository3;
            _repository = repository4;
            _INursingNoteService = INursingNoteService;

        }
        [HttpPost("PrescriptionWardList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionListDto> PrescriptiontList = await _IPriscriptionReturnService.GetPrescriptionListAsync(objGrid);
            return Ok(PrescriptiontList.ToGridResponse(objGrid, "PrescriptionWard  List "));
        }
        [HttpPost("PrescriptionReturn List")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ListReturn(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnListDto> PrescriptiontReturnList = await _IPriscriptionReturnService.GetListAsyncReturn(objGrid);
            return Ok(PrescriptiontReturnList.ToGridResponse(objGrid, "PrescriptionReturn  List "));
        }
        [HttpPost("PrescriptionDetailList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ListDetail(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetailListDto> PrescriptiontDetailList = await _IPriscriptionReturnService.GetListAsyncDetail(objGrid);
            return Ok(PrescriptiontDetailList.ToGridResponse(objGrid, "PrescriptionDetail  List "));
        }

        [HttpPost("LabRequestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestListDto> LabRequestList = await _ILabRequestService.GetListAsync(objGrid);
            return Ok(LabRequestList.ToGridResponse(objGrid, "LabRequestList "));
        }

        [HttpPost("LabRequestDetailsList")] 
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestDetailsList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestDetailsListDto> LabRequestDetailsListDto = await _ILabRequestService.SPGetListAsync(objGrid);
            return Ok(LabRequestDetailsListDto.ToGridResponse(objGrid, "LabRequestDetailsList "));
        }

    
         [HttpPost("NursingInsertLabRequest")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(LabRequestModel obj)
        {
        THlabRequest model = obj.MapTo<THlabRequest>();
        if (obj.RequestId == 0)
        {
            model.ReqDate = Convert.ToDateTime(obj.ReqDate);
            model.ReqTime = Convert.ToDateTime(obj.ReqTime);
            model.IsAddedBy = CurrentUserId;
            await _ILabRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
        }
        else
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "labRequest added successfully.", model);
        }

         [HttpPost("InsertPrescription")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(MPrescriptionModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                //model.IsAddedBy = CurrentUserId;
                await _IMPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }

        [HttpPost("PrescriptionReturnList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionReturnList(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionReturnDto> PrescriptionReturnList = await _IPriscriptionReturnService.GetListAsync(objGrid);
            return Ok(PrescriptionReturnList.ToGridResponse(objGrid, "PrescriptionReturnList "));
        }

        [HttpPost("PrescriptionReturnInsert")]
        [Permission(PageCode = "PrescriptionReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.Addedby = CurrentUserId;
                await _IPriscriptionReturnService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PrescriptionReturn added successfully.");
        }

        [HttpPut("PrescriptionReturnUpdate")]
        [Permission(PageCode = "PrescriptionReturn", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.PresDate = Convert.ToDateTime(obj.PresDate);
                await _IPriscriptionReturnService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PrescriptionReturn updated successfully.");
        }
        [HttpPost("CanteenInsert")]

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


        //Nursing Note 
       

        [HttpPost("NursingNoteList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingNote_List(GridRequestModel objGrid)
        {
            IPagedList<NursingNoteListDto> List = await _INursingNoteService.GetListAsync(objGrid);
            return Ok(List.ToGridResponse(objGrid, "Nursing Note List "));
        }


        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DocNoteId == id);
            return data.ToSingleResponse<TNursingNote, NursingNoteModel>("TNursingNote");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(NursingNoteModel obj)
        {
            TNursingNote model = obj.MapTo<TNursingNote>();
            //model.IsActive = true;
            if (obj.DocNoteId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.ModifiedDateTime = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(NursingNoteModel obj)
        {
            TNursingNote model = obj.MapTo<TNursingNote>();
            //model.IsActive = true;
            if (obj.DocNoteId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.CreatedDatetime = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDatetime" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TNursingNote model = await _repository.GetById(x => x.DocNoteId == Id);
            if ((model?.DocNoteId ?? 0) > 0)
            {
                //model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.CreatedDatetime = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


        //[HttpPost("TDoctorPatientHandoverList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> TDoctorPatientHandoverList(GridRequestModel objGrid)
        //{
        //    IPagedList<TDoctorPatientHandoverListDto> TDoctorPatientHandoverList = await _ICanteenRequestService.TDoctorPatientHandoverList(objGrid);
        //    return Ok(TDoctorPatientHandoverList.ToGridResponse(objGrid, "TDoctorPatientHandover App List"));
        //}
        //[HttpPost("CanteenRequestList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> List1(GridRequestModel objGrid)
        //{
        //    IPagedList<CanteenRequestListDto> CanteenRequestList = await _ICanteenRequestService.CanteenRequestsList(objGrid);
        //    return Ok(CanteenRequestList.ToGridResponse(objGrid, "CanteenRequest App List"));
        //}
        //[HttpPost("CanteenRequestHeaderList")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        //public async Task<IActionResult> HeaderList(GridRequestModel objGrid)
        //{
        //    IPagedList<CanteenRequestHeaderListDto> CanteenRequestHeaderList = await _ICanteenRequestService.CanteenRequestHeaderList(objGrid);
        //    return Ok(CanteenRequestHeaderList.ToGridResponse(objGrid, "CanteenRequestHeader App List"));
        //}

        //[HttpPost("CanteenInsert")]

        ////[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> CanteenInsert(CanteenRequestModel obj)
        //{
        //    TCanteenRequestHeader model = obj.MapTo<TCanteenRequestHeader>();
        //    if (obj.ReqId == 0)
        //    {
        //        model.Date = Convert.ToDateTime(obj.Date);
        //        model.Time = Convert.ToDateTime(obj.Time);

        //        await _ICanteenRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CanteenRequest added successfully.", model);
        //}
    }
}
