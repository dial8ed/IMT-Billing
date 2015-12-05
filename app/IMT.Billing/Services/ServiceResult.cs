using System;

namespace Dial8ed.Billing.Services
{
    public class ServiceResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }        
    }    
}