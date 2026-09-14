using HIMS.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Transactions;
using HIMS.Services.AbhaIntegration;

namespace HIMS.Services.AbhaIntegration
{
    public class AbhaConnectService : IAbhaConnectService
    {

        private readonly IConfiguration _configuration;
        private readonly HIMSDbContext _context;
        //private readonly IAbhaConnectService _abhaConnectService;
        public AbhaConnectService(IConfiguration configuration, HIMSDbContext context //, IAbhaConnectService abhaConnectService
            )
        {
            _configuration = configuration;
            _context = context;
            //_abhaConnectService = abhaConnectService;
        }
        public async Task<object> InitiateClient(object model)
        {
            using var client = new HttpClient();

            var abhaClientSessionUrl = _configuration["ABDMIntegration:ClientSessionUrl"];
            var abhaBaseUrl = _configuration["ABDMIntegration:BaseUrl"];

            var request = new HttpRequestMessage(HttpMethod.Post, abhaClientSessionUrl);

            request.Headers.Add("accept", "*/*");
            request.Headers.Add("x-org-id", "1");

            var json = JsonSerializer.Serialize(model);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonNode.Parse(result).AsObject();
            var responseCode = jsonObject["responseCode"]?.GetValue<int>();
            var responseMessage = jsonObject["responseMessage"]?.GetValue<string>();

            if (responseCode != 200)
            {
                return new
                {
                    ResponseCode = responseCode,
                    ResponseMessage = responseMessage,
                    ResponseData = jsonObject["responseData"]
                };
            }

            var transactionId = jsonObject["responseData"]?["transactionId"]?.GetValue<string>();
            var callbackUrl = $"{abhaBaseUrl}/{transactionId}";

            jsonObject["responseData"]["callbackUrl"] = callbackUrl;

            return new
            {
                ResponseCode = responseCode,
                ResponseMessage = responseMessage,
                ResponseData = jsonObject["responseData"]
            };
        }

        public virtual async Task InsertAsync(TAbhaCallbackformation obj)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TAbhaCallbackformations.Add(obj);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

        public async Task<object> AuthenticateUserAsync()
        {
            using var client = new HttpClient();
            var authenticateUserUrl = _configuration["ABDMIntegration:AuthenticateUserUrl"];
            var authenticateUserName = _configuration["ABDMIntegration:username"];
            var authenticatePassword = _configuration["ABDMIntegration:password"];

            var request = new HttpRequestMessage(HttpMethod.Post, authenticateUserUrl);

            var obj = new { username = authenticateUserName, password = authenticatePassword };

            var jsonContent = JsonSerializer.Serialize(obj);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonNode.Parse(result).AsObject();
            var responseCode = jsonObject["responseCode"]?.GetValue<int>();
            var responseMessage = jsonObject["responseMessage"]?.GetValue<string>();

            var jwtToken = jsonObject["jwttoken"]?.GetValue<string>();
            var roles = jsonObject["roles"];


            return new
            {
                ResponseCode = responseCode ?? 200,
                ResponseMessage = responseMessage ?? "Authentication successful.",
                ResponseData = new
                {
                    jwttoken = jwtToken,
                    roles = roles
                }
            };
        }

        //public async Task<string> CareContextAsync(CareContextModel model)
        //{
        //    var url = _configuration["ABDMIntegration:LinkCareContextUrl"];

        //    using var client = new HttpClient();

        //    using var request = new HttpRequestMessage(HttpMethod.Post, url);

        //    //Authorization header

        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        //    //HIP ID header
        //    request.Headers.Add("X-HIP-ID", model.hipId);

        //    //            Serialize request model
        //    var jsonContent = JsonSerializer.Serialize(model);

        //    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //    //          Send request
        //    var response = await client.SendAsync(request);

        //    var result = await response.Content.ReadAsStringAsync();

        //    response.EnsureSuccessStatusCode();

        //    //        Capture Kanaad response
        //    var responseObject =
        //        JsonSerializer.Deserialize<string>(
        //            result,
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            });

        //    return responseObject;
        //}


    }
}
