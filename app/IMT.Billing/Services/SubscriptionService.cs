using System;
using AuthorizeNet;
using AuthorizeNet.APICore;
using Dial8ed.Billing.Entities;
using Dial8ed.Billing.Gateway;


namespace Dial8ed.Billing.Services
{
    public class SubscriptionService
    {
        private IGatewayService _gatewayService;

        internal SubscriptionService(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public SubscriptionService()
        {
            _gatewayService = GatewayServiceFactory.CreateFromConfig();
        }

        public SubscriptionResult CreatePaymentSubscription(CustomerInfo customerInfo, PaymentInfo paymentInfo,
                                                    BillingInfo billingInfo)
        {
            var subscriptionRequest = SubscriptionRequest.CreateNew();
            subscriptionRequest.CustomerID = customerInfo.ProfileId;
            subscriptionRequest.SubscriptionName = customerInfo.Description;
            subscriptionRequest.SubscriptionID = customerInfo.Tier.ToString();
            subscriptionRequest.UsingCreditCard(customerInfo.FirstName, customerInfo.LastName,
                                                paymentInfo.Card.CardNumber,
                                                (paymentInfo.Card.ExpirationYear + 2000),
                                                paymentInfo.Card.ExpirationMonth);
            subscriptionRequest.Amount = paymentInfo.Amount;

            subscriptionRequest.BillingInterval = SubscriptionDefaults.BillingInterval;
            subscriptionRequest.BillingCycles = paymentInfo.Card.GetMonthsUntilCardExpires();
            subscriptionRequest.BillingIntervalUnits = SubscriptionDefaults.BillingIntervalUnits;
            subscriptionRequest.BillingAddress = billingInfo.ToAuthorizeAddress();

            try
            {
                var subscription = _gatewayService.Subscriptions.CreateSubscription(subscriptionRequest);
                var status = _gatewayService.Subscriptions.GetSubscriptionStatus(subscription.SubscriptionID);
                var message = subscription.SubscriptionName + " was " +
                              (status == ARBSubscriptionStatusEnum.active ? "" : "not") + " created successfully";

                var succeeded = status == ARBSubscriptionStatusEnum.active;
                return new SubscriptionResult()
                {
                    Message = message,
                    SubscriptionId = subscription.SubscriptionID,
                    Succeeded = succeeded,
                    SubscriptionStatus = status.ToString()
                };
            }
            catch (Exception exception)
            {
                return new SubscriptionResult()
                {
                    Message = "Subscription Creation Failed",
                    Exception = exception,
                    Succeeded = false,
                    SubscriptionId = "",
                    SubscriptionStatus = ""
                };
            }
        }         
    }
}