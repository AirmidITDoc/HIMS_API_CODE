﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
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
     
        [HttpPost("LabRequestDetailsList")] 
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabRequestDetailsList(GridRequestModel objGrid)
        {
            IPagedList<LabRequestDetailsListDto> LabRequestDetailsListDto = await _ILabRequestService.SPGetListAsync(objGrid);
            return Ok(LabRequestDetailsListDto.ToGridResponse(objGrid, "LabRequestDetailsList"));
        }

        [HttpPost("NursingNoteList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingNoteList(GridRequestModel objGrid)
        {
            IPagedList<NursingNoteListDto> List = await _INursingNoteService.GetListAsync(objGrid);
            return Ok(List.ToGridResponse(objGrid, "NursingNote List "));
        }

        [HttpPost("DoctorPatientHandoverList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> DoctorPatientHandoverList(GridRequestModel objGrid)
        {
            IPagedList<TDoctorPatientHandoverListDto> DoctorPatientHandoverList = await _INursingNoteService.SGetListAsync(objGrid);
            return Ok(DoctorPatientHandoverList.ToGridResponse(objGrid, "DoctorPatientHandoverList"));
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
                model.ModifiedBy = CurrentUserId;
                model.CreatedDatetime = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpPost("DoctorNoteInsert")]
        //[Permission(PageCode = "DoctorsNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(DoctorNoteModel obj)
        {
            TDoctorsNote model = obj.MapTo<TDoctorsNote>();
            if (obj.DoctNoteId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                await _INursingNoteService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorNote  added successfully.");
        }


        [HttpPut("DoctorNoteUpdate/{id:int}")]
        //[Permission(PageCode = "DoctorsNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Edit(DoctorNoteModel obj)
        {
            TDoctorsNote model = obj.MapTo<TDoctorsNote>();
            if (obj.DoctNoteId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _INursingNoteService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorNote updated successfully.");
        }

        [HttpPost("TDoctorPatientHandoverInsert")]
        //[Permission(PageCode = "DoctorsNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(TDoctorPatientHandoverModel obj)
        {
            TDoctorPatientHandover model = obj.MapTo<TDoctorPatientHandover>();
            if (obj.DocHandId == 0)
            {
                model.CreatedBy = CurrentUserId;
                await _INursingNoteService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TDoctorPatient Handover  added successfully.");
        }


      

    }
}
