using HIMS.Data.Models;

namespace HIMS.API.Models.PaymentGateway
{
    public class PaymentRequestDto
    {
        public string phone { get; set; }
        public decimal amount { get; set; }
        public long? Opdipdid { get; set; }


    }
    public class MpesaCallbackRoot
    {
        public MpesaBody Body { get; set; }
    }

    public class MpesaBody
    {
        public MpesaStkCallback stkCallback { get; set; }
    }

    public class MpesaStkCallback
    {
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public CallbackMetadata CallbackMetadata { get; set; }
    }

    public class CallbackMetadata
    {
        public List<MetadataItem> Item { get; set; }
    }

    public class MetadataItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
    public class MPesaResponseDto
    {
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CustomerMessage { get; set; }
        public string ReferenceNo { get; set; }
    }
    //public class TestA
    //{
    //    public long Id { get; set; }
    //    public string MerchantRequestID { get; set; }
    //    public string CheckoutRequestID { get; set; }
    //    public int ResultCode { get; set; }
    //    public string ResultDesc { get; set; }
    //    public decimal Amount { get; set; }
    //    public string MpesaReceiptNumber { get; set; }
    //    public DateTime TransactionDate { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public DateTime ResponseOn { get; set; }
    //}

    public class MPesaResponse
    {
        public static TMpesaResponse MapMpesaToTestA(MpesaCallbackRoot callback)
        {
            var stk = callback?.Body?.stkCallback;
            if (stk == null)
                return null;

            decimal amount = 0;
            string receipt = null;
            long transactionDateRaw = 0;
            string phone = null;

            // Metadata exists only when payment = success
            var items = stk.CallbackMetadata?.Item;
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item?.Name == null)
                        continue;

                    switch (item.Name)
                    {
                        case "Amount":
                            amount = Convert.ToDecimal(item.Value);
                            break;

                        case "MpesaReceiptNumber":
                            receipt = item.Value?.ToString();
                            break;

                        case "TransactionDate":
                            transactionDateRaw = Convert.ToInt64(item.Value);
                            break;

                        case "PhoneNumber":
                            phone = item.Value?.ToString();
                            break;
                    }
                }
            }

            // Convert Mpesa date (yyyymmddhhmmss) → DateTime
            DateTime transactionDate = DateTime.MinValue;
            if (transactionDateRaw > 0)
            {
                string s = transactionDateRaw.ToString();

                transactionDate = DateTime.ParseExact(
                    s,
                    "yyyyMMddHHmmss",
                    System.Globalization.CultureInfo.InvariantCulture
                );
            }

            return new TMpesaResponse
            {
                Id = 0, // you will set or auto-generate this
                MerchantRequestId = stk.MerchantRequestID,
                CheckoutRequestId = stk.CheckoutRequestID,
                ResultCode = stk.ResultCode,
                ResultDesc = stk.ResultDesc,
                Amount = amount,
                MpesaReceiptNumber = receipt,
                TransactionDate = transactionDate,
                PhoneNumber = phone,
                ResponseOn = DateTime.UtcNow
            };
        }

    }
}
