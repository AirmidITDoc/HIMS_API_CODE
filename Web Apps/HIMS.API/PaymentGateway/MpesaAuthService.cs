using HIMS.Core.Domain.Common;
using HIMS.Core.Infrastructure;

namespace HIMS.API.PaymentGateway
{
    public class MpesaAuthService
    {
        private readonly HttpClient _client;
        public MpesaAuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var authToken = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{AppSettings.Settings.MPesa.Key}:{AppSettings.Settings.MPesa.Secret}")
            );

            var request = new HttpRequestMessage(HttpMethod.Get, AppSettings.Settings.MPesa.AccessTokenUrl);
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


        public MpesaStkService(HttpClient client, MpesaAuthService auth)
        {
            _client = client;
            _authService = auth;
        }
        public async Task<string> RegisterUrls()
        {
            string token = await _authService.GetAccessTokenAsync();

            var payload = new
            {
                AppSettings.Settings.MPesa.ShortCode,
                AppSettings.Settings.MPesa.ResponseType,
                ConfirmationURL = AppSettings.Settings.MPesa.ConfirmationUrl,
                ValidationURL = AppSettings.Settings.MPesa.ValidationUrl
            };

            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.Settings.MPesa.RegisterUrl);

            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> CheckStkStatusAsync(string checkoutRequestId)
        {
            string token = await _authService.GetAccessTokenAsync();

            string timestamp = AppTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{AppSettings.Settings.MPesa.BusinessShortCode}{AppSettings.Settings.MPesa.PassKey}{timestamp}"));

            var payload = new
            {
                AppSettings.Settings.MPesa.BusinessShortCode,
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

        public async Task<string> StkPushAsync(string phoneNumber, decimal amount, long Opdipdid, string callbackUrl, string reference)
        {
            string token = await _authService.GetAccessTokenAsync();
            string timestamp = AppTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{AppSettings.Settings.MPesa.BusinessShortCode}{AppSettings.Settings.MPesa.PassKey}{timestamp}"));

            var payload = new
            {
                AppSettings.Settings.MPesa.BusinessShortCode,
                Password = password,
                Timestamp = timestamp,
                AppSettings.Settings.MPesa.TransactionType,
                Amount = amount,
                PartyA = phoneNumber,
                PartyB = AppSettings.Settings.MPesa.BusinessShortCode,
                PhoneNumber = NormalizePhone(phoneNumber),
                CallBackURL = callbackUrl,
                AccountReference = reference,
                TransactionDesc = "Payment",
                AppSettings.Settings.MPesa.ResponseType
            };
            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.Settings.MPesa.StkPushUrl);
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
