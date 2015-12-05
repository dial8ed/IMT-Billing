using AuthorizeNet;

namespace Dial8ed.Billing.Gateway
{
    internal class DefaultGatewayService : IGatewayService
    {       
        public DefaultGatewayService(ICustomerGateway customers, IGateway payments, ISubscriptionGateway subscriptions)
        {            
            Subscriptions = subscriptions;
            Customers = customers;
            Payments = payments;
        }

        public ISubscriptionGateway Subscriptions { get; private set; }
        public ICustomerGateway Customers { get; private set; }
        public IGateway Payments { get; private set; }
    }
}