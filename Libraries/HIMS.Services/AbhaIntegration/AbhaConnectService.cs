using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HIMS.Services.AbhaIntegration
{
    public class AbhaConnectService : IAbhaConnectService
    {
        public async Task<object> InitiateClient(object model)
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.kanaad.co.in/api/user/v1/abdm/clientSession/saveSessionByUserIdAndClientId"
            );

            request.Headers.Add("accept", "*/*");
            request.Headers.Add("x-org-id", "1");

            var json = JsonSerializer.Serialize(model);

            request.Content = new StringContent(json,Encoding.UTF8,"application/json");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonNode.Parse(result).AsObject();
            var responseCode = jsonObject["responseCode"]?.GetValue<int>();

            if (responseCode != 200)
            {
                return jsonObject;
            }
            var transactionId = jsonObject["responseData"]?["transactionId"]?.GetValue<string>();
            var callbackUrl =$"http://13.207.45.186:8090/abdm/client-login/1/{transactionId}";

            jsonObject["responseData"]["callbackUrl"] = callbackUrl;
            return jsonObject;
        }
    }
}
