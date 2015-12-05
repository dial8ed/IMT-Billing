using AuthorizeNet;

namespace Dial8ed.Billing.Gateway
{
    internal class GatewayConfigSettings
    {        
        public GatewayConfigSettings()
        {            
            SetGatewayCredentials();            
        }
       
        private void SetGatewayCredentials()
        {
           switch(GatewayConfig.Default.GatewayServiceMode)
           {
               case "Dev":
                   ApiLoginId = GatewayConfig.Default.DevApiLoginId;
                   TransactionId = GatewayConfig.Default.DevTransactionId;
                   ServiceMode = ServiceMode.Test;
                   break;
               case "Test":
                   ApiLoginId = GatewayConfig.Default.LiveApiLoginId;
                   TransactionId = GatewayConfig.Default.LiveTransactionId;
                   ServiceMode = ServiceMode.Test;
                   break;
               case "Live":
                    ApiLoginId = GatewayConfig.Default.LiveApiLoginId;
                   TransactionId = GatewayConfig.Default.LiveTransactionId;
                   ServiceMode = ServiceMode.Live;
                   break;               
           }           
        }

        public string ApiLoginId { get; private set; }

        public string TransactionId { get; private set; }

        public ServiceMode ServiceMode { get; private set; }
    }
}