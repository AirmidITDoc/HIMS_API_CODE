using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;
using HIMS.Services.Masters;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Administration;
using HIMS.API.Utility;
using System.Net.Mime;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace HIMS.API.Controllers.Masters.DoctorMasterm
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorController : BaseController
    {
        private readonly IDoctorMasterService _IDoctorMasterService;
        private readonly IGenericService<LvwDoctorMasterList> _repository1;
        private readonly IFileUtility _FileUtility;
        private readonly IGenericService<MDoctorScheduleDetail> _repository2;
        private readonly IGenericService<MDoctorExperienceDetail> _repository3;
        private readonly IGenericService<MDoctorChargesDetail> _repository4;
        private readonly IGenericService<MDoctorSignPageDetail> _repository5;
        private readonly IGenericService<MConstant> _repository6;
        private readonly IGenericService<FileMaster> _repository7;

        public DoctorController(IDoctorMasterService repository, IGenericService<LvwDoctorMasterList> repository1, IFileUtility fileUtility, IGenericService<MDoctorScheduleDetail> repository2, IGenericService<MDoctorExperienceDetail> repository3, IGenericService<MDoctorChargesDetail> repository4, IGenericService<MDoctorSignPageDetail> repository5, IGenericService<MConstant> repository6,
            IGenericService<FileMaster> repository7)
        {
            _IDoctorMasterService = repository;
            _repository1 = repository1;
            _FileUtility = fileUtility;
            _repository2 = repository2;
            _repository3 = repository3;
            _repository4 = repository4;
            _repository5 = repository5;
            _repository6 = repository6;
            _repository7 = repository7;
        }
        //List API
        [HttpPost]
        [Route("DoctorScheduleDetailList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<MDoctorScheduleDetail> DoctorScheduleDetailList = await _repository2.GetAllPagedAsync(objGrid);
            return Ok(DoctorScheduleDetailList.ToGridResponse(objGrid, "DoctorScheduleDetail List"));
        }
        //List API
        [HttpPost]
        [Route("DoctorExperienceDetailList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListD(GridRequestModel objGrid)
        {
            IPagedList<MDoctorExperienceDetail> DoctorExperienceDetailList = await _repository3.GetAllPagedAsync(objGrid);
            return Ok(DoctorExperienceDetailList.ToGridResponse(objGrid, "DoctorExperienceDetailList "));
        }
        //List API
        [HttpPost]
        [Route("MDoctorSignPageDetailList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListS(GridRequestModel objGrid)
        {
            IPagedList<MDoctorSignPageDetail> MDoctorSignPageDetailList = await _repository5.GetAllPagedAsync(objGrid);
            return Ok(MDoctorSignPageDetailList.ToGridResponse(objGrid, "DoctorExperienceDetailList "));
        }
        //List API
        [HttpPost]
        [Route("DoctorLeaveDetailList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListL(GridRequestModel objGrid)
        {
            IPagedList<DoctorLeaveDetailsListDto> DoctorLeaveDetailList = await _IDoctorMasterService.ListAsyncL(objGrid);
            return Ok(DoctorLeaveDetailList.ToGridResponse(objGrid, "DoctorLeaveDetailList"));
        }
        //List API
        [HttpPost]
        [Route("DoctorQualificationDetailList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListQ(GridRequestModel objGrid)
        {
            IPagedList<DoctorQualificationDetailsListDto> DoctorChargesDetailList = await _IDoctorMasterService.ListAsyncQ(objGrid);
            return Ok(DoctorChargesDetailList.ToGridResponse(objGrid, "DoctorChargesDetailList"));
        }

        [HttpPost("DoctorChargesDetailList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListC(GridRequestModel objGrid)
        {
            IPagedList<DoctorChargesDetailListDto> DoctorChargesDetailList = await _IDoctorMasterService.ListAsync(objGrid);
            return Ok(DoctorChargesDetailList.ToGridResponse(objGrid, "DoctorChargesDetailList"));
        }

        [HttpPost("DoctorSignpagelist")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<DoctorSignpageListDto> DoctorSignpagelist = await _IDoctorMasterService.ListAsyncs(objGrid);
            return Ok(DoctorSignpagelist.ToGridResponse(objGrid, "DoctorSignpagelist"));
        }

        [HttpPost("DoctorList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctorMasterListDto> DoctorList = await _IDoctorMasterService.GetListAsync(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "DoctorList"));
        }

        [HttpGet("GetConstantList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetConstantList(string ConstantType)
        {
            var result = await _IDoctorMasterService.ConstantListAsync(ConstantType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetConstantList", result);
        }
        //List API
        [HttpGet]
        [Route("get-DoctorConstantsList")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdownD()
        {
            var DoctorConstantsList = await _repository6.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorContant dropdown", DoctorConstantsList.Select(x => new { x.ConstantType, x.ConstantId }));
        }


        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListLinq(GridRequestModel objGrid)
        {
            IPagedList<DoctorMaster> DoctorMasterList = await _IDoctorMasterService.GetAllPagedAsync(objGrid);
            return Ok(DoctorMasterList.ToGridResponse(objGrid, "DoctorMaster List "));
        }


        [HttpGet("{id?}")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _IDoctorMasterService.GetById(id);
            return data.ToSingleResponse<DoctorMaster, DoctorModel>("Doctor Master");
        }

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "DoctorMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DoctorModel obj)
        {
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth);
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.MahRegDate = Convert.ToDateTime(obj.MahRegDate);
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _IDoctorMasterService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name  added successfully.");
        }
        //[HttpPost("InsertEDMX")]
        ////   [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(DoctorModel obj)
        //{
        //    if (!string.IsNullOrWhiteSpace(obj.Signature))
        //        obj.Signature = _FileUtility.SaveImageFromBase64(obj.Signature, "Doctors\\Signature");
        //    DoctorMaster model = obj.MapTo<DoctorMaster>();
        //    if (obj.DoctorId == 0)
        //    {
        //        model.DateofBirth = Convert.ToDateTime(obj.DateofBirth.Value.ToLocalTime());
        //        if (model.RegDate.HasValue)
        //            model.RegDate = Convert.ToDateTime(obj.RegDate.Value.ToLocalTime()).Date;
        //        if (model.MahRegDate.HasValue)
        //            model.MahRegDate = Convert.ToDateTime(obj.MahRegDate.Value.ToLocalTime()).Date;
        //        model.Addedby = CurrentUserId;
        //        model.IsActive = true;
        //        await _IDoctorMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //    {
        //        model.DateofBirth = Convert.ToDateTime(obj.DateofBirth.Value.ToLocalTime());
        //        if (model.RegDate.HasValue)
        //            model.RegDate = Convert.ToDateTime(obj.RegDate.Value.ToLocalTime());
        //        if (model.MahRegDate.HasValue)
        //            model.MahRegDate = Convert.ToDateTime(obj.MahRegDate.Value.ToLocalTime());
        //        await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Name  added successfully.");
        //}

        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX([FromForm] DoctorModel obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.Signature))
                obj.Signature = _FileUtility.SaveImageFromBase64(obj.Signature, "Doctors\\Signature");
            DoctorMaster model = obj.MapTo<DoctorMaster>();
            if (obj.DoctorId == 0)
            {
                foreach (var q in model.MDoctorQualificationDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var v in model.MDoctorExperienceDetails)
                {
                    v.CreatedBy = CurrentUserId;
                    v.CreatedDate = DateTime.Now;

                }
                foreach (var p in model.MDoctorScheduleDetails)
                {
                    p.CreatedBy = CurrentUserId;
                    p.CreatedDate = DateTime.Now;
                }
                foreach (var x in model.MDoctorChargesDetails)
                {
                    x.CreatedBy = CurrentUserId;
                    x.CreatedDate = DateTime.Now;
                }
                foreach (var y in model.MDoctorLeaveDetails)
                {
                    y.CreatedBy = CurrentUserId;
                    y.CreatedDate = DateTime.Now;
                }
                foreach (var z in model.MDoctorSignPageDetails)
                {
                    z.CreatedBy = CurrentUserId;
                    z.CreatedDate = DateTime.Now;
                }
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth.Value.ToLocalTime());
                if (model.RegDate.HasValue)
                    model.RegDate = Convert.ToDateTime(obj.RegDate.Value.ToLocalTime()).Date;
                if (model.MahRegDate.HasValue)
                    model.MahRegDate = Convert.ToDateTime(obj.MahRegDate.Value.ToLocalTime()).Date;
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _IDoctorMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
                if (model.DoctorId > 0)
                {
                    List<FileMaster> Files = new List<FileMaster>();
                    foreach (var item in obj.MDoctorFiles)
                    {
                        if (item.DocName != null)
                        {
                            Files.Add(new FileMaster
                            {
                                DocName = item.DocName,
                                DocSavedName = await _FileUtility.UploadFileAsync(item.Document, "Doctors\\Files"),
                                CreatedById = CurrentUserId,
                                Id = 0,
                                IsDelete = false,
                                RefId = item.RefId,
                                RefType = item.RefType,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }
                    if (Files.Count > 0)
                        await _repository7.Add(Files, CurrentUserId, CurrentUserName);
                }
            }
            else
            {
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth.Value.ToLocalTime());
                if (model.RegDate.HasValue)
                    model.RegDate = Convert.ToDateTime(obj.RegDate.Value.ToLocalTime());
                if (model.MahRegDate.HasValue)
                    model.MahRegDate = Convert.ToDateTime(obj.MahRegDate.Value.ToLocalTime());
                await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit([FromForm] DoctorModel obj)

        {
            if (obj.DoctorId <= 0)

            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            if (!string.IsNullOrWhiteSpace(obj.Signature))
                obj.Signature = _FileUtility.SaveImageFromBase64(obj.Signature, "Doctors\\Signature");

            DoctorMaster model = obj.MapTo<DoctorMaster>();

            model.DateofBirth = obj.DateofBirth?.ToLocalTime();
            model.MahRegDate = obj.MahRegDate?.ToLocalTime();

            foreach (var q in model.MDoctorQualificationDetails)
            {
                if (q.DocQualfiId == 0)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = DateTime.Now;
                }
                q.ModifiedBy = CurrentUserId;
                q.ModifiedDate = DateTime.Now;
                q.DocQualfiId = 0;
            }

            foreach (var v in model.MDoctorExperienceDetails)
            {
                if (v.DocExpId == 0)
                {
                    v.CreatedBy = CurrentUserId;
                    v.CreatedDate = DateTime.Now;
                }
                v.ModifiedBy = CurrentUserId;
                v.ModifiedDate = DateTime.Now;
                v.DocExpId = 0;
            }

            foreach (var p in model.MDoctorScheduleDetails)
            {
                if (p.DocSchedId == 0)
                {
                    p.CreatedBy = CurrentUserId;
                    p.CreatedDate = DateTime.Now;
                }
                p.ModifiedBy = CurrentUserId;
                p.ModifiedDate = DateTime.Now;
                p.DocSchedId = 0;
            }

            foreach (var x in model.MDoctorChargesDetails)
            {
                if (x.DocChargeId == 0)
                {
                    x.CreatedBy = CurrentUserId;
                    x.CreatedDate = DateTime.Now;
                }
                x.ModifiedBy = CurrentUserId;
                x.ModifiedDate = DateTime.Now;
                x.DocChargeId = 0;
            }
            foreach (var y in model.MDoctorLeaveDetails)
            {
                if (y.DocLeaveId == 0)
                {
                    y.CreatedBy = CurrentUserId;
                    y.CreatedDate = DateTime.Now;
                }
                y.ModifiedBy = CurrentUserId;
                y.ModifiedDate = DateTime.Now;
                y.DocLeaveId = 0;
            }
            foreach (var z in model.MDoctorSignPageDetails)
            {
                if (z.DocSignId == 0)
                {
                    z.CreatedBy = CurrentUserId;
                    z.CreatedDate = DateTime.Now;
                }
                z.ModifiedBy = CurrentUserId;
                z.ModifiedDate = DateTime.Now;
                z.DocSignId = 0;
            }
            await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            if (model.DoctorId > 0)
            {
                List<FileMaster> Files = new List<FileMaster>();
                foreach (var item in obj.MDoctorFiles)
                {
                    if (item.DocName != null)
                    {
                        Files.Add(new FileMaster
                        {
                            DocName = item.DocName,
                            DocSavedName = await _FileUtility.UploadFileAsync(item.Document, "Doctors\\Files"),
                            CreatedById = CurrentUserId,
                            Id = 0,
                            IsDelete = false,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
                if (Files.Count > 0)
                    await _repository7.Add(Files, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        [HttpDelete]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            DoctorMaster model = await _IDoctorMasterService.GetById(Id);
            if ((model?.DoctorId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _IDoctorMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }




        [HttpGet]
        [Route("get-Doctor")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var List = await _repository1.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor dropdown", List.Select(x => new { x.DoctorId, x.FirstName, x.MiddleName, x.LastName }));
        }
        [HttpGet]
        [Route("get-Doctor-depts")]
        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDoctorWithDept()
        {
            var List = await _IDoctorMasterService.GetDoctorWithDepartment();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor dropdown", List.Select(x => new { value = x.DoctorId, text = x.FirstName + " " + x.MiddleName + " " + x.LastName, x.DeptNames }));
        }
        [HttpGet("get-file")]
        public ApiResponse DownloadFiles(string FileName)
        {
            var data = _FileUtility.GetBase64FromFolder("Doctors\\Signature", FileName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "File", data.Result);
        }

    }
}
