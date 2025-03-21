using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IBookingService
    {
        void AddBooking(IBooking booking);
        IBooking GetBookingByVehicle(IVehicle vehicle);
        decimal GetTotalRevenue();
        void GenerateReport();
    }
}
