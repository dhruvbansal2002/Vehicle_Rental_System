using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class Booking : IBooking
    {
        public IVehicle RentedVehicle { get; }
        public ICustomer Customer { get; }
        public DateTime BookingDate { get; }
        public decimal TotalPrice { get; }

        public Booking(ICustomer customer, IVehicle vehicle, int numberOfDays)
        {
            Customer = customer;
            RentedVehicle = vehicle;
            BookingDate = DateTime.Now;
            TotalPrice = vehicle.PricePerDay * numberOfDays;
        }
    }
}
