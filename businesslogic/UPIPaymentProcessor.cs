using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class UPIPaymentProcessor : PaymentProcessorBase
    {
        public UPIPaymentProcessor(string userName, decimal amount) : base(userName, amount) { }

        public override bool ProcessPayment(decimal amount)
        {
            Console.Write("Enter UPI ID: ");
            string upiId = Console.ReadLine() ?? string.Empty;

            if (!ValidateUPIId(upiId))
            {
                Console.WriteLine("Details not entered properly. Payment failed.");
                return false;
            }

            // Simulate payment processing
            _isPaymentCompleted = true;
            Console.WriteLine($"UPI Payment of ₹{amount} processed for {_userName}");
            return true;
        }

        private bool ValidateUPIId(string upiId)
        {
            return upiId.Contains('@') && upiId.Length > 3;
        }
    }
}
