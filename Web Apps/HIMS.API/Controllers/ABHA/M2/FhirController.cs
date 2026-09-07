using HIMS.ABHA.Interface;
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
        public async Task<IActionResult> GetPrescription(string registrationId)
        {
            var result = await _fhirService
                .CreatePrescriptionBundle(registrationId);

            return Ok(result);
        }

    }
}
