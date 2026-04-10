using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models;
using HIMS.API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.RIS
{
    /// <summary>
    /// Handles outbound calls from this HMIS/EMR system to the Aikenist RIS.
    /// Endpoints: Create / Update / Delete Radiology Order, Send Patient History.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RisController : BaseController
    {
        private readonly IRisApiHelper _risHelper;
        private readonly ILogger<RisController> _logger;

        public RisController(IRisApiHelper risHelper, ILogger<RisController> logger)
        {
            _risHelper = risHelper;
            _logger = logger;
        }

        // ──────────────────────────────────────────────────
        //  POST api/ris/radiology-order
        //  Submits a new radiology order to Aikenist RIS
        // ──────────────────────────────────────────────────

        [HttpPost("radiology-order")]
        public async Task<IActionResult> CreateRadiologyOrder([FromBody] CreateRadiologyOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Creating radiology order. AccessionNumber: {AccessionNumber}", request.AccessionNumber);

            try
            {
                var result = await _risHelper.CreateRadiologyOrderAsync(request);
               // return result.Status ? Ok(result) : BadRequest(result);

                if (result.Status)
                {
                    return Ok(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology order created successfully", result));
                }
                else
                {
                    return BadRequest(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, result.Message ?? "Failed to create radiology order", result));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating radiology order for AccessionNumber: {AccessionNumber}", request.AccessionNumber);
                return StatusCode(StatusCodes.Status500InternalServerError, new RisApiResponse
                {
                    Status = false,
                    Message = "Unexpected error while creating radiology order."
                });
            }
        }

        // ──────────────────────────────────────────────────
        //  PUT api/ris/radiology-order/{checkId}
        //  checkId = accession_number OR external_id
        //  - If accession_number → updates full order including comments
        //  - If external_id     → updates patient demographics only
        // ──────────────────────────────────────────────────

        [HttpPut("radiology-order/{checkId}")]
        public async Task<IActionResult> UpdateRadiologyOrder([FromRoute] string checkId, [FromBody] UpdateRadiologyOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(checkId))
                return BadRequest(new RisApiResponse { Status = false, Message = "checkId (accession_number or external_id) is required." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Updating radiology order. CheckId: {CheckId}", checkId);

            try
            {
                var result = await _risHelper.UpdateRadiologyOrderAsync(checkId, request);
                return result.Status ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating radiology order. CheckId: {CheckId}", checkId);
                return StatusCode(StatusCodes.Status500InternalServerError, new RisApiResponse
                {
                    Status = false,
                    Message = "Unexpected error while updating radiology order."
                });
            }
        }

        // ──────────────────────────────────────────────────
        //  DELETE api/ris/radiology-order
        //  Note: RIS will NOT delete if scan is already completed in PACS
        // ──────────────────────────────────────────────────

        [HttpDelete("radiology-order")]
        public async Task<IActionResult> DeleteRadiologyOrder([FromBody] DeleteRadiologyOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Deleting radiology order. AccessionNumber: {AccessionNumber}, ExternalId: {ExternalId}",
                request.AccessionNumber, request.ExternalId);

            try
            {
                var result = await _risHelper.DeleteRadiologyOrderAsync(request);
                return result.Status ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting radiology order. AccessionNumber: {AccessionNumber}", request.AccessionNumber);
                return StatusCode(StatusCodes.Status500InternalServerError, new RisApiResponse
                {
                    Status = false,
                    Message = "Unexpected error while deleting radiology order."
                });
            }
        }

        // ──────────────────────────────────────────────────
        //  POST api/ris/patient-history
        //  Sends prior patient reports/history to Aikenist RIS
        // ──────────────────────────────────────────────────

        [HttpPost("patient-history")]
        public async Task<IActionResult> SendPatientHistory([FromBody] PatientHistoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ExternalId))
                return BadRequest(new RisApiResponse { Status = false, Message = "external_id is required." });

            _logger.LogInformation("Sending patient history. ExternalId: {ExternalId}", request.ExternalId);

            try
            {
                var result = await _risHelper.SendPatientHistoryAsync(request);
                return result.Status ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending patient history. ExternalId: {ExternalId}", request.ExternalId);
                return StatusCode(StatusCodes.Status500InternalServerError, new RisApiResponse
                {
                    Status = false,
                    Message = "Unexpected error while sending patient history."
                });
            }
        }
    }
}
