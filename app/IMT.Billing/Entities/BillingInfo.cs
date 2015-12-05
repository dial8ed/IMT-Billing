namespace Dial8ed.Billing.Entities
{
    public class BillingInfo
    {
        public AddressInfo Address { get; private set; }
        public CustomerInfo Customer { get; private set; }

        public BillingInfo(CustomerInfo customer, AddressInfo address)
        {
            Address = address;
            Customer = customer;
        }                                   
    }
}