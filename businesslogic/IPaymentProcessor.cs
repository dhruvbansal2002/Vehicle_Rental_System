using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
        bool ValidatePayment();
        void RefundPayment();
    }
}
