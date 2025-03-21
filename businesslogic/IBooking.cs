using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IBooking
    {
        IVehicle RentedVehicle { get; }
        ICustomer Customer { get; }
        DateTime BookingDate { get; }
        decimal TotalPrice { get; }
    }
}
