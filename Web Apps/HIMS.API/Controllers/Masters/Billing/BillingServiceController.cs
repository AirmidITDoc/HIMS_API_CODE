using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.API.Models.Inventory;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BillingServiceController : BaseController
    {
        private readonly IGenericService<ServiceMaster> _repository;
        private readonly IGenericService<ClassMaster> _ClassRepository;
        private readonly IGenericService<TariffMaster> _TariffRepository;
        private readonly IBillingService _BillingService;
        public BillingServiceController(IBillingService repository, IGenericService<ServiceMaster> repository1, IGenericService<ClassMaster> classRepository, IGenericService<TariffMaster> tariffRepository)
        {
            _BillingService = repository;
            _repository = repository1;
            _ClassRepository = classRepository;
            _TariffRepository = tariffRepository;
        }

        [HttpPost("BillingList")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<BillingServiceDto> BillingList = await _BillingService.GetListAsync(objGrid);
            return Ok(BillingList.ToGridResponse(objGrid, "Billing List"));
        }

        [HttpPost("PackageServiceInfoList")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<PackageServiceInfoListDto> ServiceList = await _BillingService.GetListAsync1(objGrid);
            return Ok(ServiceList.ToGridResponse(objGrid, "PackageService List"));
        }

        [HttpGet("PathologyServicesearch")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public ApiResponse PathologyServicesearch(string Keyword)
        {
            var data = _BillingService.PathologyServicesearch(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathologyService search  data", data);
        }

        [HttpGet("RadiologyServicesearch")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public ApiResponse RadiologyServicesearch(string Keyword)
        {
            var data = _BillingService.RadiologyServicesearch(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RadiologyService search  data", data);
        }



        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(BillingServiceModel obj)
        {
            ServiceMaster model = obj.MapTo<ServiceMaster>();
            if (obj.ServiceId == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                await _BillingService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("Edit/{id:int}/{tariffId:int}")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Edit)]

        public async Task<ApiResponse> Edit(int id, int tariffId, BillingServiceModel obj)
        {
            if (id != obj.ServiceId || obj.ServiceId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            ServiceMaster model = obj.MapTo<ServiceMaster>();

            model.ModifiedDate = AppTime.Now;
            model.ModifiedBy = CurrentUserId;

            await _BillingService.UpdateAsync(model, CurrentUserId, CurrentUserName, tariffId, new string[] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpDelete("ServicDelete")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            ServiceMaster model = await _repository.GetById(x => x.ServiceId == Id);
            if ((model?.ServiceId ?? 0) > 0)
            {
                model.IsActive = model.IsActive != true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

        }
        [HttpGet("GetServiceListwithGroupWise")]
        public async Task<ApiResponse> GetServiceListwithGroupWise(int TariffId, int ClassId, string IsPathRad, string ServiceName)
        {
            var resultList = await _BillingService.GetServiceListwithGroupWise(TariffId, ClassId, IsPathRad, ServiceName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get ServiceList with Group Wise List.", resultList);
        }

        [HttpGet("GetServicewithGroupWiseList")]
        public ApiResponse GetServicewithGroupWiseList(int TariffId, int ClassId, string SrvcName)
        {
            var resultList = _BillingService.GetServicewithGroupWiseList(TariffId, ClassId, SrvcName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get ServiceList with Group Wise List.", resultList);
        }






        [HttpPut("UpdateDifferTariff")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Edit)]
        public ApiResponse Update(DifferTraiffModel obj)
        {
            if (obj.OldTariffId == 0 || obj.NewTariffId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid parameters: Tariff IDs cannot be zero.");
            }

            var model = new ServiceDetail
            {
                TariffId = obj.OldTariffId
            };

            int userId = 1;
            string userName = "admin";

            _BillingService.UpdateDifferTariff(model, obj.OldTariffId, obj.NewTariffId, userId, userName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update successfully.");
        }
        [HttpPost("ServiceWiseCompanyCode")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        public ApiResponse Insert(ServiceWiseCompanyCodeModel obj)
        {
            ServiceWiseCompanyCode model = obj.MapTo<ServiceWiseCompanyCode>();
            if (obj.TariffId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                _BillingService.InsertS(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpGet("GetServiceListwithTraiff")]
        public async Task<ApiResponse> GetServiceListwithTraiff(int TariffId, string ServiceName)
        {
            var resultList = await _BillingService.GetServiceListwithTraiff(TariffId, ServiceName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service List With Tariff Id.", resultList.Select(x => new
            {
                x.FormattedText,
                x.ServiceId,
                x.GroupId,
                x.ServiceShortDesc,
                x.ServiceName,
                x.ClassRate,
                x.TariffId,
                x.ClassId,
                x.IsEditable,
                x.CreditedtoDoctor,
                x.IsPathology,
                x.IsRadiology,
                x.IsActive,
                x.PrintOrder,
                x.IsPackage,
                x.DoctorId,
                x.IsDocEditable
            }));
        }
        //Add API
        [HttpPost("PackageDetailsInsert")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        public ApiResponse Insert(PackageDetModel obj)
        {
            List<MPackageDetail> model = obj.packageDetail.MapTo<List<MPackageDetail>>();

            if (model.Count > 0)
            {
                _BillingService.Insert(model, CurrentUserId, CurrentUserName, obj.PackageTotalDays, obj.PackageIcudays, obj.PackageMedicineAmount, obj.PackageConsumableAmount);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   added successfully.");
        }

        [HttpPost("PackageDetailList")]
        //[Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListD(GridRequestModel objGrid)
        {
            IPagedList<PackageDetListDto> BillingList = await _BillingService.GetListAsyncD(objGrid);
            return Ok(BillingList.ToGridResponse(objGrid, "PackageDetail List"));
        }

        [HttpGet("GetServicesNew")]
        public ApiResponse GetServices(int TariffId)
        {
            var resultList = _BillingService.GetServiceListNew(TariffId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get ServiceList with Group Wise List.", resultList);
        }
        [HttpPost("save-services-new")]
        public async Task<ApiResponse> SaveServices(BillingServiceNewDto Data)
        {
            await _BillingService.SaveServicesNew(Data.TariffId, Data.Data);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service list updated successfully.");
        }

        [HttpPost("import-preview")]
        [Permission]
        public async Task<ApiResponse> PreviewData([FromForm] IFormFile file, [FromForm] string mapping)
        {
            if (file == null || file.Length == 0)
                return ApiResponseHelper.GenerateResponse(
                    ApiStatusCode.Status400BadRequest,
                    "File is missing",
                    null);

            try
            {
                var data = await ExcelImportHelper.ParseExcelAsync<BillingServiceImportDto>(file, mapping, x =>
                {

                    if (x.PatientRate < 0)
                        return (0, "Invalid PatientRate");

                    if (x.CpRate < 0)
                        return (0, "Invalid CpRate");

                    if (string.IsNullOrWhiteSpace(x.Service))
                        return (0, "Service is required");

                    if (string.IsNullOrWhiteSpace(x.Tariff))
                        return (0, "Tariff is required");

                    if (string.IsNullOrWhiteSpace(x.Class))
                        return (0, "Class is required");

                    if (x.ClassRate <= 0)
                        return (0, "ClassRate must be greater than 0");

                    return (1, "Valid");
                });

                var distinctServices = data.DistinctBy(x => x.Service);
                var matchedServices = (await _repository.GetAll(x => distinctServices.Select(s => s.Service).Contains(x.ServiceName))).ToList();
                foreach (var service in matchedServices)
                    data.Where(x => x.Service == service.ServiceName).ToList().ForEach(x =>
                    {
                        x.ServiceId = service.ServiceId;
                    });
                var distinctClasses = data.DistinctBy(x => x.Class);
                var matchedClasses = (await _ClassRepository.GetAll(x => distinctClasses.Select(s => s.Class).Contains(x.ClassName))).ToList();
                foreach (var cls in matchedClasses)
                    data.Where(x => x.Class == cls.ClassName).ToList().ForEach(x =>
                    {
                        x.ClassId = cls.ClassId;
                    });
                var distinctTariffs = data.DistinctBy(x => x.Tariff);
                var matchedTariffs = (await _TariffRepository.GetAll(x => distinctTariffs.Select(s => s.Tariff).Contains(x.TariffName))).ToList();
                foreach (var tariff in matchedTariffs)
                    data.Where(x => x.Tariff == tariff.TariffName).ToList().ForEach(x =>
                    {
                        x.TariffId = tariff.TariffId;
                    });
                data.Where(x => x.Status == 1).ToList().ForEach(x =>
                {
                    x.Status = x.ClassId > 0 && x.ServiceId > 0 && x.TariffId > 0 ? 1 : 0;
                    x.Message = x.Status == 1 ? "Valid" : $"{(x.ServiceId > 0 ? "" : "Service not found. ")}{(x.ClassId > 0 ? "" : "Class not found. ")}{(x.TariffId > 0 ? "" : "Tariff not found.")}".Trim();
                });

                return ApiResponseHelper.GenerateResponse(
                    ApiStatusCode.Status200OK,
                    "Files are processed successfully.",
                    data);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse(
                    ApiStatusCode.Status500InternalServerError,
                    ex.Message,
                    null);
            }
        }
        [HttpPost("import-data")]
        [Permission]
        public async Task<ApiResponse> ImportData([FromForm] IFormFile file, [FromForm] string mapping)
        {
            var data = await ExcelImportHelper.ParseExcelAsync<BillingServiceImportDto>(file, mapping, x =>
            {

                if (x.PatientRate < 0)
                    return (0, "Invalid PatientRate");

                if (x.CpRate < 0)
                    return (0, "Invalid CpRate");

                if (string.IsNullOrWhiteSpace(x.Service))
                    return (0, "Service is required");

                if (string.IsNullOrWhiteSpace(x.Tariff))
                    return (0, "Tariff is required");

                if (string.IsNullOrWhiteSpace(x.Class))
                    return (0, "Class is required");

                if (x.ClassRate <= 0)
                    return (0, "ClassRate must be greater than 0");

                return (1, "Valid");
            });
            if (!data.Any(x => x.Status == 1))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No valid data to import.", null);
            else
            {
                var importData = data.Where(x => x.Status == 1);
                // Add here logic for import data into tables. above data should be inserted into tables. you can map this data into your entity and then save into database.

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Data imported successfully.", null);
            }
        }
    }
}