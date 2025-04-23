using Asp.Versioning;
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
        private readonly IGenericService<TNurNote> _repository;
        private readonly IGenericService<MNursingTemplateMaster> _repository1;
        private readonly IGenericService<MDoctorNotesTemplateMaster> _repository2;


        public NursingController(ILabRequestService repository, IMPrescriptionService repository1 ,IPriscriptionReturnService repository2, ICanteenRequestService repository3, IGenericService<TNurNote> repository4,IGenericService<MNursingTemplateMaster> repository5, IGenericService<MDoctorNotesTemplateMaster> repository6, INursingNoteService INursingNoteService)
        {
            _ILabRequestService = repository;
            _IMPrescriptionService = repository1;
            _IPriscriptionReturnService = repository2;
            _ICanteenRequestService = repository3;
            _repository = repository4;
            _repository1 = repository5;
            _repository2 = repository6;

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
        //[Permission(PageCode = "NursingNote", Permission = PagePermission.View)]
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
        [HttpPost("DoctorsNotesList")]
        [Permission(PageCode = "DoctorNote", Permission = PagePermission.View)]
        public async Task<IActionResult> DoctorsNotesList(GridRequestModel objGrid)
        {
            IPagedList<DoctorsNoteListDto> DoctorsNotesList = await _INursingNoteService.DoctorsNoteAsync(objGrid);
            return Ok(DoctorsNotesList.ToGridResponse(objGrid, "DoctorsNotesList"));
        }
        [HttpPost("MedicationChartlist")]
        //[Permission(PageCode = "DoctorNote", Permission = PagePermission.View)]
        public async Task<IActionResult> MedicationChartlist(GridRequestModel objGrid)
        {
            IPagedList<MedicationChartListDto> MedicationChartlist = await _INursingNoteService.MedicationChartlist(objGrid);
            return Ok(MedicationChartlist.ToGridResponse(objGrid, "MedicationChartlist"));
        }
        [HttpPost("NursingPatientHandoverList")]
        //[Permission(PageCode = "DoctorNote", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingPatientHandoverList(GridRequestModel objGrid)
        {
            IPagedList<NursingPatientHandoverListDto> NursingPatientHandoverListDto = await _INursingNoteService.NursingPatientHandoverList(objGrid);
            return Ok(NursingPatientHandoverListDto.ToGridResponse(objGrid, "NursingPatientHandoverListDto"));
        }
        [HttpPost("NursingMedicationList")]
        //[Permission(PageCode = "DoctorNote", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingMedicationList(GridRequestModel objGrid)
        {
            IPagedList<NursingMedicationListDto> NursingMedicationList = await _INursingNoteService.NursingMedicationlist(objGrid);
            return Ok(NursingMedicationList.ToGridResponse(objGrid, "NursingMedicationList"));
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
        [Permission(PageCode = "NursingNote", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DocNoteId == id);
            return data.ToSingleResponse<TNurNote, NursingNoteModel>("TNursingNote");
        }
        //Add API
        [HttpPost("NursingNoteInsert")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NursingNoteModel obj)
        {
            TNurNote model = obj.MapTo<TNurNote>();
            if (obj.DocNoteId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDatetime = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  added successfully.");
        }
        //Edit API
        [HttpPut("NursingNoteUpdate/{id:int}")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(NursingNoteModel obj)
        {
            TNurNote model = obj.MapTo<TNurNote>();
            if (obj.DocNoteId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDatetime = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDatetime" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  updated successfully.");
        }
        //Delete API
        [HttpDelete]
       // [Permission(PageCode = "NursingNote", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TNurNote model = await _repository.GetById(x => x.DocNoteId == Id);
            if ((model?.DocNoteId ?? 0) > 0)
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDatetime = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpPost("DoctorNoteInsert")]
        [Permission(PageCode = "DoctorNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DoctorNoteModel obj)
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
        [Permission(PageCode = "DoctorNote", Permission = PagePermission.Add)]
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
       

        [HttpPost("DoctorPatientHandoverInsert")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(TDoctorPatientHandoverModel obj)
        {
            TDoctorPatientHandover model = obj.MapTo<TDoctorPatientHandover>();
            if (obj.DocHandId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _INursingNoteService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorPatientHandover  added successfully.");
        }

        [HttpPut("DoctorPatientHandover/{id:int}")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(TDoctorPatientHandoverModel obj)
        {
            TDoctorPatientHandover model = obj.MapTo<TDoctorPatientHandover>();
            if (obj.DocHandId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _INursingNoteService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorPatientHandover updated successfully.");
        }

        [HttpPost("NursingPatientHandoverInsert")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NursingPatientHandoverModel obj)
        {
            TNursingPatientHandover model = obj.MapTo<TNursingPatientHandover>();
            if (obj.PatHandId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDatetime = DateTime.Now;
                await _INursingNoteService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingPatientHandover  added successfully.");
        }

        [HttpPut("NursingPatientHandover/{id:int}")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(NursingPatientHandoverModel obj)
        {
            TNursingPatientHandover model = obj.MapTo<TNursingPatientHandover>();
            if (obj.PatHandId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDateTime = DateTime.Now;
                await _INursingNoteService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingPatientHandover updated successfully.");
        }

        [HttpPost("NursingTemplateInsert")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NursingTemplateModel obj)
        {
            MNursingTemplateMaster model = obj.MapTo<MNursingTemplateMaster>();
            if (obj.NursingId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository1.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingTemplate  added successfully.");
        }


        [HttpPut("NursingTemplateUpdate/{id:int}")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(NursingTemplateModel obj)
        {
            MNursingTemplateMaster model = obj.MapTo<MNursingTemplateMaster>();
            if (obj.NursingId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository1.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingTemplate updated successfully.");
        }

        [HttpPost("DoctorNotesTemplateInsert")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DoctorNotesTemplateModel obj)
        {
            MDoctorNotesTemplateMaster model = obj.MapTo<MDoctorNotesTemplateMaster>();
            if (obj.DocNoteTempId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository2.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorNotesTemplate  added successfully.");
        }

        [HttpPut("DoctorNotesTemplate/{id:int}")]
        [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(DoctorNotesTemplateModel obj)
        {
            MDoctorNotesTemplateMaster model = obj.MapTo<MDoctorNotesTemplateMaster>();
            if (obj.DocNoteTempId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository2.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorNotesTemplate updated successfully.");
        }


        [HttpPost("NursingMedicationChartInsert")]
      //  [Permission(PageCode = "NursingNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(TNursingMedicationChartModel obj)
        {
            TNursingMedicationChart1 model = obj.MapTo<TNursingMedicationChart1>();
            if (obj.MedChartId == 0)
            {
               
                  model.IsAddedBy = CurrentUserId;
                await _INursingNoteService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TNursingMedicationChart  added successfully.");
        }

    }
}
