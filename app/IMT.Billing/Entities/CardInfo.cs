using System;

namespace Dial8ed.Billing.Entities
{
    public class CardInfo
    {        
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int ExpirationYear { get; set; }
        public int ExpirationMonth { get; set; }

        public string CardType { get; set; }
        public string CardCode { get; set; }

        public short GetMonthsUntilCardExpires()
        {
            var expDate = new DateTime(FourDigitExpirationYear, ExpirationMonth, 1);

            var months = expDate.MonthDifference(DateTime.UtcNow);
            return short.Parse(months.ToString());
        }

        public int FourDigitExpirationYear
        { 
            get {
                if (ExpirationYear < 2000)
                return ExpirationYear + 2000;
    
            return ExpirationYear;
            }
        }
    }
}