using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification(string message, string contactInfo, string method)
        {
            Console.WriteLine($"Email notification sent: {message} to {contactInfo}");
        }
    }
}
