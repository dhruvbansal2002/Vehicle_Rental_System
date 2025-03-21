using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public abstract class PaymentProcessorBase : IPaymentProcessor
    {
        protected readonly string _userName;
        protected readonly decimal _amount;
        protected bool _isPaymentCompleted;

        protected PaymentProcessorBase(string userName, decimal amount)
        {
            _userName = userName ?? throw new ArgumentNullException(nameof(userName));
            _amount = amount;
            _isPaymentCompleted = false;
        }

        public virtual bool ProcessPayment(decimal amount)
        {
            _isPaymentCompleted = true;
            Console.WriteLine($"Payment of ₹{amount} processed for {_userName}");
            return true;
        }

        public bool ValidatePayment() => _isPaymentCompleted;

        public void RefundPayment()
        {
            _isPaymentCompleted = false;
            Console.WriteLine($"Refund of ₹{_amount} processed for {_userName}");
        }
    }
}
