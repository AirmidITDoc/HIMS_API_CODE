//using Asp.Versioning;
//using HIMS.Api.Controllers;
//using HIMS.Data.Models;
//using HIMS.Data;
//using Microsoft.AspNetCore.Mvc;
//using HIMS.API.Extensions;
//using HIMS.Api.Models.Common;
//using HIMS.API.Models.Masters;
//using HIMS.Core.Domain.Grid;
//using HIMS.Core;
//using HIMS.API.Models.Inventory.Masters;
//using HIMS.Services.Masters;

//namespace HIMS.API.Controllers.Masters.DoctorMasterm
//{
//    [Route("api/v{version:apiVersion}/[controller]")]
//    [ApiController]
//    [ApiVersion("1")]
//    public class DoctoreMasterController : BaseController
//    {
//        private readonly IGenericService<DoctorMaster> _repository;
//        private readonly IDoctorMasterService _IDoctorService;
//        private readonly IGenericService<LvwDoctorMasterList> _repository1;
//        public DoctoreMasterController(IGenericService<DoctorMaster> repository, IDoctorMasterService doctorMasterService, IGenericService<LvwDoctorMasterList> repository1)
//        {
//            _repository = repository;
//            _repository1 = repository1;
//            _IDoctorService = doctorMasterService;
//        }
//        //List API
//        [HttpPost]
//        [Route("[action]")]
//        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
//        public async Task<IActionResult> List(GridRequestModel objGrid)
//        {
//            IPagedList<DoctorMaster> DoctorMasterList = await _IDoctorService.GetAllPagedAsync(objGrid);
//            return Ok(DoctorMasterList.ToGridResponse(objGrid, "DoctorMaster List "));
//        }
//        //List API Get By Id
//        [HttpGet("{id?}")]
//        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
//        public async Task<ApiResponse> Get(int id)
//        {
//            if (id == 0)
//            {
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
//            }
//            var data = await _repository.GetById(x => x.DoctorId == id);
//            return data.ToSingleResponse<DoctorMaster, DoctoreMasterModel>("DoctorMaster");
//        }



//        //Add API
//        [HttpPost]
//        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
//        public async Task<ApiResponse> Post(DoctoreMasterModel obj)
//        {
//            DoctorMaster model = obj.MapTo<DoctorMaster>();
//            model.IsActive = true;
//            if (obj.DoctorId == 0)
//            {
//                model.CreatedBy = CurrentUserId;
//                model.CreatedDate = DateTime.Now;
//                await _repository.Add(model, CurrentUserId, CurrentUserName);
//            }
//            else
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
//            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorMaster  added successfully.");
//        }
//        //Edit API
//        [HttpPut("{id:int}")]
//        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
//        public async Task<ApiResponse> Edit(DoctoreMasterModel obj)
//        {
//            DoctorMaster model = obj.MapTo<DoctorMaster>();
//            model.IsActive = true;
//            if (obj.DoctorId == 0)
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
//            else
//            {
//                model.ModifiedBy = CurrentUserId;
//                model.ModifiedDate = DateTime.Now;
//                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
//            }
//            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorMaster  updated successfully.");
//        }
//        //Delete API
//        [HttpDelete]
//        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
//        public async Task<ApiResponse> Delete(int Id)
//        {
//            DoctorMaster model = await _repository.GetById(x => x.DoctorId == Id);
//            if ((model?.DoctorId ?? 0) > 0)
//            {
//                model.IsActive = false;
//                model.ModifiedBy = CurrentUserId;
//                model.ModifiedDate = DateTime.Now;
//                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorMaster  deleted successfully.");
//            }
//            else
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
//        }


//        [HttpGet]
//        [Route("get-Doctor")]
//        [Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
//        public async Task<ApiResponse> GetDropdown()
//        {
//            var List = await _repository1.GetAll();
//            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor dropdown", List.Select(x => new { x.DoctorId, x.FirstName, x.MiddleName, x.LastName }));
//        }
//    }
//}
////[HttpGet]
////[Route("get-Doctor")]
//////[Permission(PageCode = "DoctorMaster", Permission = PagePermission.View)]
////public async Task<ApiResponse> GetDropdown()
////{
////    var List = await _repository1.GetAll(x);
////    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor dropdown", List.Select(x => new { x.DoctorId, x.DepartmentId, x.FirstName }));
////}


