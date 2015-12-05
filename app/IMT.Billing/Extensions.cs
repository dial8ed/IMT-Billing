using System;
using AuthorizeNet;
using AuthorizeNet.APICore;
using Dial8ed.Billing.Entities;

namespace Dial8ed.Billing
{
    public static class Extensions
    {        
        public static Address ToAuthorizeAddress(this BillingInfo billingInfo)
        {
            var address = new Address(new customerAddressType())
                              {
                                  City = billingInfo.Address.City,
                                  Company = billingInfo.Customer.Company,
                                  Country = billingInfo.Address.Country,
                                  Fax = billingInfo.Customer.Contact.Fax,
                                  First = billingInfo.Customer.FirstName,
                                  Last = billingInfo.Customer.LastName,
                                  ID = "",
                                  Phone = billingInfo.Customer.Contact.Phone,
                                  State = billingInfo.Address.State,
                                  Street = billingInfo.Address.Street
                              };

            return address;
        }

        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }
    }
}