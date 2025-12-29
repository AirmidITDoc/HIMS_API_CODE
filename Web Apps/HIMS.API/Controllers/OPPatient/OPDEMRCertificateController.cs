using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Common;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPDEMRCertificateController : BaseController
    {
        private readonly IOPBillingService _oPBillingService;
        private readonly IOPCreditBillService _IOPCreditBillService;
        private readonly IOPSettlementService _IOPSettlementService;
        private readonly IAdministrationService _IAdministrationService;
        private readonly IVisitDetailsService _IVisitDetailsService;
        private readonly IGenericService<MCertificateMaster> _radiorepository1;

        public OPDEMRCertificateController(IOPBillingService repository, IOPCreditBillService repository1, IOPSettlementService repository2, IAdministrationService repository3, IVisitDetailsService repository4, IGenericService<MCertificateMaster> pathrepository1)
        {
            _oPBillingService = repository;
            _IOPCreditBillService = repository1;
            _IOPSettlementService = repository2;
            _IAdministrationService = repository3;
            _IVisitDetailsService = repository4;
            _radiorepository1 = pathrepository1;
        }
        [HttpPost("CertificateInformationList")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CertificateInformationListDto> CertificateInformationList = await _oPBillingService.GetListAsync(objGrid);
            return Ok(CertificateInformationList.ToGridResponse(objGrid, "CertificateInformation List"));
        }
        //Add API
        [HttpPost("TCertificateInformationSave")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(TCertificateInformationParamModel obj)
        {
            TCertificateInformation model = obj.MapTo<TCertificateInformation>();
            //model.IsActive = true;
            if (obj.CertificateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _oPBillingService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }


        [HttpPut("TCertificateInformationUpdate")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(TCertificateInformationParamModel obj)
        {
            TCertificateInformation model = obj.MapTo<TCertificateInformation>();
            if (obj.CertificateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _oPBillingService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model);
        }

        [HttpGet]
        [Route("get-CertificateMaster")]
        //[Permission(PageCode = "MedicalRecords", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown2()
        {
            var MMasterList = await _radiorepository1.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Certificate Master  dropdown", MMasterList.Select(x => new { x.CertificateId, x.CertificateName, x.CertificateDesc }));
        }

    }
}
