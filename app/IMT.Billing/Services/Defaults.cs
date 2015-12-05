using AuthorizeNet;

namespace Dial8ed.Billing.Services
{
    public static class SubscriptionDefaults 
    {
        public const int BillingInterval = 1;       
        public static BillingIntervalUnits BillingIntervalUnits = BillingIntervalUnits.Months;           
    }
}