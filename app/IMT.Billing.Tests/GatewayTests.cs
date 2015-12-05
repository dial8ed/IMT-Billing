using Dial8ed.Billing.Entities;
using NUnit.Framework;

namespace Dial8ed.Billing.Tests
{
    [TestFixture]
    public class GatewayTests
    {

        [Test]
        public void when_the_gateway_service_is_created_it_is_initialized_from_the_app_config_file()
        {
            
           
        }


        [Test]
        public void card_exp_months_test()
        {
            var card = new CardInfo();
            card.ExpirationYear = 2012;
            card.ExpirationMonth = 5;
            var result = card.GetMonthsUntilCardExpires();
            Assert.IsTrue(result == 2);
        }
         
    }
}