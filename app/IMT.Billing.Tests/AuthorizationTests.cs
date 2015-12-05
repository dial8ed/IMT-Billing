using Dial8ed.Billing.Entities;
using Dial8ed.Billing.Services;
using Dial8ed.Billing.Tests.Helpers;
using NUnit.Framework;

namespace Dial8ed.Billing.Tests
{
    [TestFixture]
    public class AuthorizationTests
    {
        private PaymentService _paymentService;

        [SetUp]
        public void setup()
        {
            _paymentService = new PaymentService();    
        }

        [Test]
        public void when_authorizing_a_valid_card()
        {            
            var result  = _paymentService.AuthorizeOnly(EntityBuilder.TestValidAuthorization());
            Assert.IsTrue(result.Approved);
        }

        [Test]
        public void when_authorizing_an_invalid_card()
        {
            var result = _paymentService.AuthorizeOnly(EntityBuilder.TestInvalidAuthorization());
            Assert.IsFalse(result.Approved);
        }

        [Test]
        public void when_authorizing_and_capturing_a_valid_card()
        {
            var result = _paymentService.AuthorizeAndCapture(EntityBuilder.TestValidAuthorization());
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public void when_authorizing_and_capturing_an_invalid_card()
        {
            var result = _paymentService.AuthorizeAndCapture(EntityBuilder.TestInvalidAuthorization());
            Assert.IsFalse(result.Approved);
        }

        [Test] // Run this test by itself. If you run the suite then it fail due to a duplicate transaction
        public void when_capturing_an_authorized_payment()
        {
            var authResult = _paymentService.AuthorizeOnly(EntityBuilder.TestValidAuthorization());

            var captureResult = _paymentService.CapturePriorAuthorization(authResult.Amount, new TransactionInfo()
                                                         {AuthorizationCode = authResult.AuthorizationCode, TransactionId = authResult.TransactionId});

            Assert.IsTrue(captureResult.Approved);
        }

    }
}