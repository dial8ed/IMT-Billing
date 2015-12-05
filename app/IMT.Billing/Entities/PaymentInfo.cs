namespace Dial8ed.Billing.Entities
{
    public class PaymentInfo
    {     
        public PaymentInfo()
        {
            Card = new CardInfo();
        }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public CardInfo Card { get; set; }           
    }
}