using HIMS.ABHA.Interface;
using HIMS.API.Extensions;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.ABHA.M2
{
    public class FhirController : Controller
    {
        private readonly IFhirPrescriptionService _fhirService;

        public FhirController(IFhirPrescriptionService fhirService)
        {
            _fhirService = fhirService;
        }

        [HttpGet("prescription/{registrationId}")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> GetPrescription(string registrationId)
        {
            var result = await _fhirService.CreatePrescriptionBundle(registrationId);

            return Ok(result);
        }
        [HttpGet("prescription-new/{registrationId}")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> GetPrescriptionNew(string registrationId)
        {
            var result = await _fhirService.CreatePrescriptionBundleNew(registrationId);

            return Ok(result);
        }

    }
}
