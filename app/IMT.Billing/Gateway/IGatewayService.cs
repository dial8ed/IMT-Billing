using AuthorizeNet;

namespace Dial8ed.Billing.Gateway
{
    internal interface IGatewayService
    {
        ISubscriptionGateway Subscriptions { get; }
        ICustomerGateway Customers { get; }
        IGateway Payments { get; }
    }
}