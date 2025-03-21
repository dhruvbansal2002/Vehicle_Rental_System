using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class CardPaymentProcessor : PaymentProcessorBase
    {
        public CardPaymentProcessor(string userName, decimal amount) : base(userName, amount) { }

        public override bool ProcessPayment(decimal amount)
        {
            Console.Write("Enter Card Number: ");
            string cardNumber = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Card Expiry Date (MM/YY): ");
            string expiryDate = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter CVV: ");
            string cvv = Console.ReadLine() ?? string.Empty;

            if (!ValidateCardNumber(cardNumber) || !ValidateExpiryDate(expiryDate) || !ValidateCVV(cvv))
            {
                Console.WriteLine("Details not entered properly. Payment failed.");
                return false;
            }

            // Simulate payment processing
            _isPaymentCompleted = true;
            Console.WriteLine($"Card Payment of ₹{amount} processed for {_userName}");
            return true;
        }

        private bool ValidateCardNumber(string cardNumber)
        {
            return cardNumber.Length == 16 && cardNumber.All(char.IsDigit);
        }

        private bool ValidateExpiryDate(string expiryDate)
        {
            if (expiryDate.Length != 5 || expiryDate[2] != '/')
            {
                return false;
            }

            if (!int.TryParse(expiryDate.Substring(0, 2), out int month) || !int.TryParse(expiryDate.Substring(3, 2), out int year))
            {
                return false;
            }

            if (month < 1 || month > 12)
            {
                return false;
            }

            var currentYear = DateTime.Now.Year % 100;
            var currentMonth = DateTime.Now.Month;

            return year > currentYear || (year == currentYear && month >= currentMonth);
        }

        private bool ValidateCVV(string cvv)
        {
            return cvv.Length == 3 && cvv.All(char.IsDigit);
        }
    }
}
