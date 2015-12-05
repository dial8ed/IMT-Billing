using System;

namespace Dial8ed.Billing.Entities
{
    public class CustomerInfo
    {       
        public CustomerInfo()
        {
            Contact = new ContactInfo();            
        }

        public ContactInfo Contact { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Company { get; set; } 
        public string Description { get; set; }
        public string Subdomain { get; set; }
        public Guid Tier { get; set; }        
        public string ProfileId { get; set; }
       
    }
}