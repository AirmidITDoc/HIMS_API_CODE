using HIMS.Core.Infrastructure;
using Microsoft.OpenApi.Validations;

namespace HIMS.API.PaymentGateway
{
    public class MpesaAuthService
    {
        private readonly HttpClient _client;
        private readonly string _consumerKey = "";
        private readonly string _consumerSecret = "";
        private readonly IConfiguration _configuration;
        public MpesaAuthService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _consumerKey = _configuration["MPesa:Key"];
            _consumerSecret = _configuration["MPesa:Secret"];
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string baseUrl = _configuration["MPesa:AccessTokenUrl"];

            var authToken = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{_consumerKey}:{_consumerSecret}")
            );

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);
            request.Headers.Add("Authorization", $"Basic {authToken}");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = System.Text.Json.JsonDocument.Parse(content);

            return json.RootElement.GetProperty("access_token").GetString();
        }
    }
    public class MpesaStkService
    {
        private readonly MpesaAuthService _authService;
        private readonly HttpClient _client;

        private readonly string BusinessShortCode = ""; // Test Paybill/Till
        private readonly string PassKey = "";
        private readonly IConfiguration _configuration;

        public MpesaStkService(HttpClient client, MpesaAuthService auth, IConfiguration configuration)
        {
            _client = client;
            _authService = auth;
            _configuration = configuration;
            BusinessShortCode = _configuration["MPesa:BusinessShortCode"];
            PassKey = _configuration["MPesa:PassKey"];
        }
        public async Task<string> RegisterUrls()
        {
            string token = await _authService.GetAccessTokenAsync();

            var payload = new
            {
                ShortCode = _configuration["MPesa:ShortCode"],
                ResponseType = _configuration["MPesa:ResponseType"],
                ConfirmationURL = _configuration["MPesa:ConfirmationUrl"],
                ValidationURL = _configuration["MPesa:ValidationUrl"]
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _configuration["MPesa:RegisterUrl"]);

            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> CheckStkStatusAsync(string checkoutRequestId)
        {
            string token = await _authService.GetAccessTokenAsync();

            string timestamp = AppTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{BusinessShortCode}{PassKey}{timestamp}"));

            var payload = new
            {
                BusinessShortCode,
                Password = password,
                Timestamp = timestamp,
                CheckoutRequestID = checkoutRequestId
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://sandbox.safaricom.co.ke/mpesa/stkpushquery/v1/query");

            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(payload),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> StkPushAsync(string phoneNumber, decimal amount,long Opdipdid, string callbackUrl, string reference)
        {
            string token = await _authService.GetAccessTokenAsync();
            string timestamp = AppTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{BusinessShortCode}{PassKey}{timestamp}"));

            var payload = new
            {
                BusinessShortCode,
                Password = password,
                Timestamp = timestamp,
                TransactionType = _configuration["MPesa:TransactionType"],
                Amount = amount,
                PartyA = phoneNumber,
                PartyB = BusinessShortCode,
                PhoneNumber = NormalizePhone(phoneNumber),
                CallBackURL = callbackUrl,
                AccountReference = reference,
                TransactionDesc = "Payment",
                ResponseType = _configuration["MPesa:ResponseType"]
            };
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration["MPesa:StkPushUrl"]);
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(payload),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        private static string NormalizePhone(string phone)
        {
            phone = phone.Trim().Replace("+", "");

            // Local: 07XXXXXXXX
            if (phone.StartsWith("07"))
                return "254" + phone[1..];

            // Local no zero: 7XXXXXXXX
            if (phone.StartsWith("7"))
                return "254" + phone;

            // Already correct?
            if (phone.StartsWith("2547"))
                return phone;

            throw new Exception("Invalid Kenyan phone number");
        }

    }

}
