using Microsoft.OpenApi.Validations;

namespace HIMS.API.PaymentGateway
{
    public class MpesaAuthService
    {
        private readonly HttpClient _client;
        private readonly string _consumerKey = "qpVprT8a2CqQXHAqUvP21lPlSTQHRjNIXzF7Mr3CQ8O6lFgE";
        private readonly string _consumerSecret = "gy3AdSg2pxbCI116PEmbaEEfZjHgWpqg8ctD8r2C79hIdJhYWNlAI61lcHkIz5tS";

        public MpesaAuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string baseUrl = "https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";

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

        private readonly string BusinessShortCode = "174379"; // Test Paybill/Till
        private readonly string PassKey = "YOUR_PASSKEY";

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
                ShortCode = "600978",
                ResponseType = "Completed",
                ConfirmationURL = "https://api.airmid.co.in/api/payment/confirmation",
                ValidationURL= "https://api.airmid.co.in/api/payment/validation"
            };

            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://sandbox.safaricom.co.ke/mpesa/c2b/v1/registerurl");

            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(payload),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> CheckStkStatusAsync(string checkoutRequestId)
        {
            string token = await _authService.GetAccessTokenAsync();

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{BusinessShortCode}{PassKey}{timestamp}")
            );

            var payload = new
            {
                BusinessShortCode,
                Password = password,
                Timestamp = timestamp,
                CheckoutRequestID = checkoutRequestId
            };

            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://sandbox.safaricom.co.ke/mpesa/stkpushquery/v1/query");

            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(payload),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> StkPushAsync(string phoneNumber, decimal amount, string callbackUrl)
        {
            string token = await _authService.GetAccessTokenAsync();

            //string endpoint = "https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest";
            //string endpoint = "https://sandbox.safaricom.co.ke/mpesa/qrcode/v1/generate";
            string endpoint = "https://sandbox.safaricom.co.ke/mpesa/c2b/v1/registerurl";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{BusinessShortCode}{PassKey}{timestamp}")
            );

            //var payload = new
            //{
            //    BusinessShortCode,
            //    Password = password,
            //    Timestamp = timestamp,
            //    TransactionType = "CustomerPayBillOnline",
            //    Amount = amount,
            //    PartyA = phoneNumber,
            //    PartyB = BusinessShortCode,
            //    PhoneNumber = phoneNumber,
            //    CallBackURL = callbackUrl,
            //    AccountReference = "Test123",
            //    TransactionDesc = "Payment"
            //};
            var payload = new
            {
                ShortCode = 600986,
                CommandID = "CustomerPayBillOnline",
                Amount = amount,
                Msisdn = phoneNumber,
                BillRefNumber = "57567",
                ResponseType = "Completed",
            };
            //var payload = new
            //{
            //    MerchantName = "TEST SUPERMARKET",
            //    RefNo = "Invoice Test",
            //    Amount = amount,
            //    TrxCode = "BG",
            //    CPI = phoneNumber,
            //    Size = "300"
            //};

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(payload),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }

}
