using System;
using AuthorizeNet;
using AuthorizeNet.APICore;
using NUnit.Framework;
namespace Dial8ed.Billing.Tests
{
    [TestFixture]
    public class AuthorizeNetApiTests
    {
        private const string ApiLoginId = "87dfdCVP5K";
        private const string TransactionKey = "6NBCfu86ez7755Tr";

        [Test]
        public void CreateCustomer()
        {
            var customerGateway = new CustomerGateway(ApiLoginId, TransactionKey);
            var customer =  customerGateway.CreateCustomer("justin@dial8ed.com", "jdial");
            Assert.IsNotNull(customer);
        }

        [Test]
        public void AuthorizeCard()
        {
            var gateway = new AuthorizeNet.Gateway(ApiLoginId, TransactionKey);
            var response = gateway.Send(new AuthorizationRequest("4007000000027", "1220", 3, Guid.NewGuid().ToString()));
            Assert.IsTrue(response.Approved);
        } 

        [Test]
        public void CreateCustomerBillingAddress()
        {
            var gateway = new CustomerGateway(ApiLoginId, TransactionKey);
            var customer = gateway.GetCustomer("6933900");   
            customer.BillingAddress = new Address()
                                          {
                                              Company = "Dial",
                                              City = "Austin",
                                              First = "Justin",
                                              Last = "Dial",
                                              Street = "45 STI Blvd",
                                              Zip = "78728",
                                              State = "TX",
                                              ID = customer.ProfileID                                              
                                          };

            bool updated = gateway.UpdateCustomer(customer);
            Assert.IsTrue(updated);
        }
        
        [Test]
        public void CreatePaymentSubscription()
        {           
            var subscriptionRequest = SubscriptionRequest.CreateNew();
            subscriptionRequest.CustomerID = "6933900";
            subscriptionRequest.SubscriptionName = "Tier 1 Monthly";
            subscriptionRequest.SubscriptionID = "Tier Id Guid";
            subscriptionRequest.UsingCreditCard("Justin", "Dial", "4007000000027",2020, 12);            
            subscriptionRequest.Amount = 49.99M;
            subscriptionRequest.BillingInterval = 1;
            subscriptionRequest.BillingCycles = 24;
            subscriptionRequest.BillingIntervalUnits =BillingIntervalUnits.Months;
            subscriptionRequest.BillingAddress = new Address()
            {
                Company = "Dial",
                City = "Austin",
                First = "Justin",
                Last = "Dial",
                Street = "45 STI Blvd",
                Zip = "78728",
                State = "TX",
                ID = "6933900"
            };
            
            var response = new SubscriptionGateway(ApiLoginId, TransactionKey, ServiceMode.Test).CreateSubscription(subscriptionRequest);
            
            Assert.IsTrue(response.SubscriptionID != "");

        }

        [Test]
        public void CreatePaymentProfile()
        {                      
            var cc = new creditCardMaskedType()
                         {    
                             cardType = "visa",
                             cardNumber = "4007000000027",
                             expirationDate = "1212"
                         };

            var paymentType = new paymentMaskedType() {Item = cc};

            var addressType = new customerAddressExType()
                                  {                                      
                                      address = "123 Street",
                                      city = "Austin",
                                      company = "Dial Test",
                                      country = "USA",
                                      firstName = "Justin",
                                      lastName = "Dial",
                                      state = "TX",
                                      zip = "78728"
                                  };


            var profile = new customerPaymentProfileMaskedType()
                              {
                                  billTo = addressType,
                                  customerPaymentProfileId = "00001",
                                  payment = paymentType
                              };

            var paymentProfile = new PaymentProfile(profile);
            
            var gateway = new CustomerGateway(ApiLoginId, TransactionKey, ServiceMode.Test);
            
            var profileResult = gateway.UpdatePaymentProfile("00001", paymentProfile);

            var message = "Payment Profile was " + (profileResult ? "" : "not" + " created successfully");

            Assert.IsTrue(profileResult);
            
        }
    }
}
