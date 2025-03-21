using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor CreatePaymentProcessor(int paymentChoice, decimal amount)
        {
            return paymentChoice switch
            {
                1 => new CardPaymentProcessor("User", amount),
                2 => new UPIPaymentProcessor("User", amount),
                _ => throw new InvalidOperationException("Invalid payment method.")
            };
        }
    }
}
