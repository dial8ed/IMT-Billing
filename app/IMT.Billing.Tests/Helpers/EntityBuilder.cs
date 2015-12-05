using System;
using Dial8ed.Billing.Entities;

namespace Dial8ed.Billing.Tests.Helpers
{
    public class EntityBuilder
    {        
        public static CustomerInfo TestCustomer()
        {
            return new CustomerInfo()
                       {
                           Company = "Test Company",
                           Description = "This is a test customer",
                           FirstName = "Bob",
                           LastName = "Smith",
                           Subdomain = "test " + new Guid().ToString(),
                           Tier = Guid.NewGuid(),
                           Contact = TestContact()
                       };
        } 

        public static BillingInfo TestBilling(){        
            return new BillingInfo(TestCustomer(), TestAddress());            
        }

        public static AddressInfo TestAddress()
        {
            return new AddressInfo()
                       {
                           Street = "123 Test Street",
                           City = "Austin",
                           State = "TX",
                           Country = "USA",
                           Postal = "78728"
                       };
        }

        public static CardInfo TestCard()
        {
            return new CardInfo()
                       {
                           CardCode = "777",
                           CardNumber = "4007000000027",
                           CardType = "visa",
                           ExpirationDate = "1215",
                           ExpirationMonth = 12,
                           ExpirationYear = 15
                       };
        }

        public static ContactInfo TestContact()
        {
            return new ContactInfo()
                       {
                           Email = "test@dial8ed.com",
                           Fax = "512-555-5555",
                           Phone = "512-555-6666"
                       };
        }

        public static PaymentInfo TestPayment()
        {
            return new PaymentInfo()
                       {
                           Amount = 42.95M,
                           Card = TestCard(),
                           Description = "Test Payment"
                       };
        }

        public static PaymentInfo TestInvalidPayment()
        {
            return new PaymentInfo()
                       {
                           Amount = 49.95M,
                           Card = TestInvalidCard(),
                           Description = "Test Invalid Payment"
                       };
        }

        public static CardInfo TestInvalidCard()
        {
            return new CardInfo()
                       {
                           CardCode = "000",
                           CardNumber = "11111111111111111",
                           ExpirationDate = "0909",
                           CardType = "Invalid"
                       };
        }


        public static AuthorizationInfo TestValidAuthorization()
        {
            return new AuthorizationInfo()
                       {
                           Amount = 42.95M,
                           CardNumber = "4007000000027",
                           Description = "Test Valid Authorization",
                           ExpirationDate = "1215"
                       };
        }

        public static AuthorizationInfo TestInvalidAuthorization()
        {
            return new AuthorizationInfo()
                       {
                           Amount = 42.95M,
                           CardNumber = "1111111111111111",
                           Description = "Test Invalid Authorization",
                           ExpirationDate = "0909"
                       };
        }
    }
}