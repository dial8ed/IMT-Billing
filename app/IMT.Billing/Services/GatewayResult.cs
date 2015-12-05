namespace Dial8ed.Billing.Services
{
    public class GatewayResult : ServiceResult
    {              
        public decimal Amount { get; internal set; }
        public bool Approved { get; internal set; }
        public string AuthorizationCode { get; internal set; }
        public string InvoiceNumber { get; internal set; }
        public string CardNumber { get; internal set; }
        public string ResponseCode { get; internal set; }
        public string TransactionId { get; internal set; }
    }
}