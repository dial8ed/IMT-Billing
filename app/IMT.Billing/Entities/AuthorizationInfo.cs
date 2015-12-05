namespace Dial8ed.Billing.Entities
{
    public class AuthorizationInfo
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
         
    }
}