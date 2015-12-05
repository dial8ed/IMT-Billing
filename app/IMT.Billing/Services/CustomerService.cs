using System;
using Dial8ed.Billing.Entities;
using Dial8ed.Billing.Gateway;

namespace Dial8ed.Billing.Services
{    
    public class CustomerService
    {
        private IGatewayService _gatewayService;

        internal CustomerService(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        public CustomerService()
        {
            _gatewayService = GatewayServiceFactory.CreateFromConfig();
        }

        public CustomerResult CreateCustomer(CustomerInfo customerInfo)
        {
            try
            {
                var cust = _gatewayService.Customers.CreateCustomer(customerInfo.Contact.Email, customerInfo.Description);
                if(!string.IsNullOrEmpty(cust.ProfileID))
                {
                    return new CustomerResult()
                               {Message = "Customer Created", ProfileId = cust.ProfileID, Succeeded = true};
                }

                return new CustomerResult()
                           {
                               Message = "Customer Creation Failed. No Further Information",
                               Succeeded = false,
                               ProfileId = ""
                           };
            }
            catch(Exception exception)
            {
                return new CustomerResult()
                           {
                               Exception = exception,
                               Message = "Customer Creation Failed",
                               ProfileId = "",
                               Succeeded = false
                           };
            }
            
        }      
    }
}