using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class DefaultBookingService : IBookingService
    {
        private readonly List<IBooking> _bookings = new();

        public void AddBooking(IBooking booking) => _bookings.Add(booking);

        public IBooking GetBookingByVehicle(IVehicle vehicle) =>
            _bookings.FirstOrDefault(b => b.RentedVehicle == vehicle) ?? throw new InvalidOperationException("Booking not found");

        public decimal GetTotalRevenue() => _bookings.Sum(b => b.TotalPrice);

        public void GenerateReport()
        {
            Console.WriteLine("\nBooking Report");
            Console.WriteLine("-------------");
            foreach (var booking in _bookings)
            {
                Console.WriteLine(
                    $"Customer: {booking.Customer.Name}, " +
                    $"Vehicle: {booking.RentedVehicle.Model}, " +
                    $"Amount: ₹{booking.TotalPrice}"
                );
            }
            Console.WriteLine($"Total Revenue: ₹{GetTotalRevenue()}");
        }
    }
}
