using System;
using AuthorizeNet;
using Dial8ed.Billing.Entities;
using Dial8ed.Billing.Gateway;

namespace Dial8ed.Billing.Services
{
    public class PaymentService
    {
        private IGatewayService _authorizeGateway;

        // Constructor Injection
        internal PaymentService(IGatewayService authorizeGateway)
        {
            _authorizeGateway = authorizeGateway;
        }

        public PaymentService()
        {
            _authorizeGateway = GatewayServiceFactory.CreateFromConfig();
        }

        public GatewayResult Authorize(AuthorizationInfo authorization, bool includeCapture)
        {
            var requestType = includeCapture == true ? "AuthorizeAndCapture" : "AuthorizeOnly";

            return SendRequest(new AuthorizationRequest(authorization.CardNumber, authorization.ExpirationDate, authorization.Amount,
                                              authorization.Description, includeCapture), requestType);
        }

        public GatewayResult AuthorizeOnly(AuthorizationInfo payment)
        {
            return Authorize(payment, includeCapture:false);
        }

        public GatewayResult AuthorizeAndCapture(AuthorizationInfo payment)
        {
            return Authorize(payment, includeCapture:true);
        }

        public GatewayResult CapturePriorAuthorization(decimal amount, TransactionInfo transaction)
        {
            return SendRequest(new CardPresentPriorAuthCapture(transaction.TransactionId, amount) 
                { AuthCode = transaction.AuthorizationCode}, "CaptureOnly");            
        }

        public GatewayResult Refund(TransactionInfo transaction)
        {
            return new GatewayResult() { Message = "Not Implemented", Succeeded = false };
        }

        private GatewayResult BuildExceptionGatewayResult(decimal amount, Exception ex, string methodName)
        {
            return new GatewayResult()
                       {
                           Amount = amount,
                           Approved = false,
                           AuthorizationCode = "",
                           Exception = ex,                           
                           InvoiceNumber = "",
                           Message = methodName + " Request Failed",
                           ResponseCode = ""
                       };
        }

        private GatewayResult BuildGatewayResult(decimal amount, IGatewayResponse result, string methodName)
        {
            return new GatewayResult()
                       {
                           Amount = amount,
                           Approved = result.Approved,
                           AuthorizationCode = result.AuthorizationCode,                           
                           InvoiceNumber = result.InvoiceNumber,
                           Message = methodName + " : " + result.Message,
                           Succeeded = result.Approved,
                           ResponseCode = result.ResponseCode,
                           TransactionId = result.TransactionID
                       };
        }

        private GatewayResult SendRequest(IGatewayRequest request, string methodName)
        {
            var amount = decimal.Parse(request.Amount);
            try
            {
                var result = _authorizeGateway.Payments.Send(request);
                return BuildGatewayResult(amount, result, methodName);
            }
            catch (Exception exception)
            {
                return BuildExceptionGatewayResult(amount, exception, methodName);
            }    
        }
    }
}