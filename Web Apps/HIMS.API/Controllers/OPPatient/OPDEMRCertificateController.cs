using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.API.Models.OutPatient;
using HIMS.Services.Administration;
using HIMS.Services.Common;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

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
        public OPDEMRCertificateController(IOPBillingService repository, IOPCreditBillService repository1, IOPSettlementService repository2, IAdministrationService repository3, IVisitDetailsService repository4)
        {
            _oPBillingService = repository;
            _IOPCreditBillService = repository1;
            _IOPSettlementService = repository2;
            _IAdministrationService = repository3;
            _IVisitDetailsService = repository4;
        }

        //Add API
        [HttpPost("TCertificateInformationSave")]
        //[Permission(PageCode = "TCertificateInformationSave", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(TCertificateInformationParamModel obj)
        {
            TCertificateInformation model = obj.MapTo<TCertificateInformation>();
            //model.IsActive = true;
            if (obj.CertificateId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.CertificateDate = DateTime.Now;
                await _oPBillingService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }


        [HttpPost("TCertificateInformationUpdate")]
        //[Permission(PageCode = "TCertificateInformationUpdate", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(TCertificateInformationParamModel obj)
        {
            TCertificateInformation model = obj.MapTo<TCertificateInformation>();
            if (obj.CertificateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
               
                model.CertificateDate = DateTime.Now;
                model.CertificateTime = DateTime.Now;
                await _oPBillingService.UpdateAsync(model, CurrentUserId, CurrentUserName);
                //await _oPBillingService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model);
        }

    }
}
