using AuthorizeNet;

namespace Dial8ed.Billing.Gateway
{
    internal class GatewayServiceFactory
    {        
        public static IGatewayService CreateFromConfig()
        {
            var config = new GatewayConfigSettings();            

            var customerGateway = new CustomerGateway(config.ApiLoginId, config.TransactionId, config.ServiceMode);
            var paymentGateway = new AuthorizeNet.Gateway(config.ApiLoginId, config.TransactionId, (config.ServiceMode == ServiceMode.Test));
            var subscriptionGateway = new SubscriptionGateway(config.ApiLoginId, config.TransactionId,
                                                              config.ServiceMode);

            return new DefaultGatewayService(customerGateway,paymentGateway, subscriptionGateway);
        }                
    }
}