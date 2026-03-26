using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Models;
using HIMS.API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.RIS
{
    /// <summary>
    /// Inbound endpoints that Aikenist RIS will POST to when reports are ready.
    /// HMIS/EMR must expose these URLs to the RIS and provide its own Bearer/token.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HmisReportController : ControllerBase
    {
        private readonly ILogger<HmisReportController> _logger;

        public HmisReportController(ILogger<HmisReportController> logger)
        {
            _logger = logger;
        }

        // ──────────────────────────────────────────────────
        //  POST api/hmis/report
        //  RIS calls this endpoint to push finalized report details
        //  Expected response: { "status": true, "message": "Scan and report received" }
        // ──────────────────────────────────────────────────

        [HttpPost("report")]
        public IActionResult ReceiveReport([FromBody] ReportDetailsRequest request)
        {
            if (request == null)
                return BadRequest(new RisApiResponse { Status = false, Message = "Invalid request body." });

            _logger.LogInformation(
                "Report received from RIS. AccessionNumber: {AccessionNumber}, PatientId: {PatientId}, Modality: {Modality}",
                request.AccessionNumber, request.PatientId, request.Modality);

            // ── TODO: persist / process the report in your HMIS/EMR ──
            // e.g. save to database, trigger workflow, notify clinician, etc.

            return Ok(new RisApiResponse
            {
                Status = true,
                Message = "Scan and report received"
            });
        }

        // ──────────────────────────────────────────────────
        //  POST api/hmis/report/share-status
        //  RIS calls this endpoint to notify about report share status
        //  Expected response: { "status": true, "message": "Scan and report share status updated" }
        // ──────────────────────────────────────────────────

        [HttpPost("report/share-status")]
        public IActionResult ReceiveReportShareStatus([FromBody] ReportShareStatusRequest request)
        {
            if (request == null)
                return BadRequest(new RisApiResponse { Status = false, Message = "Invalid request body." });

            _logger.LogInformation(
                "Report share status received from RIS. AccessionNumber: {AccessionNumber}, ShareMode: {ShareMode}, SharedWithPatient: {SharedWithPatient}, SharedWithRefPhy: {SharedWithRefPhy}",
                request.AccessionNumber, request.ShareMode, request.ShareWithPatient, request.ShareWithRefPhy);

            // ── TODO: update share status in your HMIS/EMR ──

            return Ok(new RisApiResponse
            {
                Status = true,
                Message = "Scan and report share status updated"
            });
        }
    }
}
