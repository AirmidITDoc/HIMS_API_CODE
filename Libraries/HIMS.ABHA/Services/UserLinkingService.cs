using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using HIMS.Data;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace HIMS.ABHA.Services
{
    public class UserLinkingService : IUserLinkingService
    {
        private readonly AbdmHttpClient _client;
        private readonly ILogger<AbhaService> _logger;
        private readonly Data.Models.HIMSDbContext _context;

        public UserLinkingService(AbdmHttpClient client, ILogger<AbhaService> logger, Data.Models.HIMSDbContext hIMSDbContext)
        {
            _client = client;
            _logger = logger;
            _context = hIMSDbContext;
        }

        public async Task<ApiResult<HttpResponseMessage>> OnDiscoverAsync(string TransactionId, string AbhaAddress, string RequestId)
        {
            try
            {
                var qry = from r in _context.Registrations
                          join p in _context.TPatientAbhaInformations on r.AbhaTranId equals p.AbhaTranId
                          join v in _context.VisitDetails on r.RegId equals v.RegId
                          where p.AbhaAddress == AbhaAddress
                          select new Carecontext()
                          {
                              display = p.AbhaFullName,
                              referenceNumber = v.Opdno,
                          };
                var objPatient = await qry.ToListAsync();
                OnDiscoverRequestDto obj = new()
                {
                    transactionId = TransactionId,
                    matchedBy = new string[] { "MR" },
                    patient = new PatientDto[] {
                    new PatientDto {
                        referenceNumber = objPatient.FirstOrDefault()?.referenceNumber,
                        display = objPatient.FirstOrDefault()?.display  ,
                        hiType = "Prescription",
                        count = objPatient.Count(),
                        careContexts = objPatient.Select(x => new Carecontext { referenceNumber = x.referenceNumber, display = "Prescription" }).ToArray()
                    }
                },
                    response = new Response
                    {
                        requestId = RequestId
                    }

                };
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.DiscoverReqUrl}";
                return await _client.PostAsync<HttpResponseMessage>(url, obj, new() { ["X-CM-ID"] = AppSettings.Current.Credentials.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnDiscoverAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<HttpResponseMessage>> OnLinkInitAsync(LinkOnInitRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.UserInitLinking}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnLinkInitAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<HttpResponseMessage>> OnLinkConfirmAsync(LinkOnConfirmRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.UserConfirmLinking}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnLinkConfirmAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }
    }
}
