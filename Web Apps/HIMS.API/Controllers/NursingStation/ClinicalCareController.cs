using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ClinicalCareController : BaseController
    {
        private readonly IClinicalCareService _ClinicalCareService;

        private readonly IGenericService<TConsentInformation> _repository;
      
        public ClinicalCareController(IClinicalCareService repository,  IGenericService<TConsentInformation> repository4)
        {
            _repository = repository4;
            _ClinicalCareService = repository;

        }

        [HttpPost("AdmisionListNursingList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionReturnList(GridRequestModel objGrid)
        {
            IPagedList<AdmisionListNursingListDto> AdmisionListNursingList = await _ClinicalCareService.GetListAsync(objGrid);
            return Ok(AdmisionListNursingList.ToGridResponse(objGrid, "AdmisionListNursingList "));
        }

        [HttpPost("IPPathologyList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IPPathologyList(GridRequestModel objGrid)
        {
            IPagedList<IPPathologyListDto> IPPathologyList = await _ClinicalCareService.GetListAsync1(objGrid);
            return Ok(IPPathologyList.ToGridResponse(objGrid, "IPPathologyList "));
        }


        [HttpPost("NursingWeightList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingWeightList(GridRequestModel objGrid)
        {
            IPagedList<NursingWeightListDto> NursingWeightList = await _ClinicalCareService.NursingWeightList(objGrid);
            return Ok(NursingWeightList.ToGridResponse(objGrid, "NursingWeightList "));
        }

        [HttpPost("NursingPainAssessmentList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingPainAssessmentList(GridRequestModel objGrid)
        {
            IPagedList<NursingPainAssessmentListDto> NursingPainAssessmentList = await _ClinicalCareService.NursingPainAssessmentList(objGrid);
            return Ok(NursingPainAssessmentList.ToGridResponse(objGrid, "NursingPainAssessmentList "));
        }

        [HttpPost("NursingSugarlevelList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingSugarlevelList(GridRequestModel objGrid)
        {
            IPagedList<NursingSugarlevelListDto> NursingSugarlevelList = await _ClinicalCareService.NursingSugarlevelList(objGrid);
            return Ok(NursingSugarlevelList.ToGridResponse(objGrid, "NursingSugarlevelList "));
        }

        [HttpPost("NursingVitalsList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingVitalsList(GridRequestModel objGrid)
        {
            IPagedList<NursingVitalsListDto> NursingVitalsList = await _ClinicalCareService.NursingVitalsList(objGrid);
            return Ok(NursingVitalsList.ToGridResponse(objGrid, "NursingVitalsList"));
        }

        [HttpPost("NursingOxygenVentilatorList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingOxygenVentilatorList(GridRequestModel objGrid)
        {
            IPagedList<NursingOxygenVentilatorListDto> NursingOxygenVentilatorList = await _ClinicalCareService.NursingOxygenVentilatorList(objGrid);
            return Ok(NursingOxygenVentilatorList.ToGridResponse(objGrid, "NursingOxygenVentilatorList"));
        }
    }
}

