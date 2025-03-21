using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class VehicleRentalService : IVehicleRentalService
    {
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        public VehicleRentalService(IPaymentProcessorFactory paymentProcessorFactory, IBookingService bookingService, INotificationService notificationService)
        {
            _paymentProcessorFactory = paymentProcessorFactory;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }

        public void RentVehicle(IVehicle vehicle, ICustomer customer)
        {
            if (vehicle.IsRented) return;

            Console.Write("Enter number of days: ");
            if (int.TryParse(Console.ReadLine(), out int days) && days > 0)
            {
                decimal totalAmount = vehicle.PricePerDay * days;
                Console.WriteLine("Choose Payment Method:");
                Console.WriteLine("1. Card");
                Console.WriteLine("2. UPI ID");
                if (int.TryParse(Console.ReadLine(), out int paymentChoice))
                {
                    IPaymentProcessor paymentProcessor = _paymentProcessorFactory.CreatePaymentProcessor(paymentChoice, totalAmount);
                    if (paymentProcessor.ProcessPayment(totalAmount))
                    {
                        vehicle.IsRented = true;
                        var booking = new Booking(customer, vehicle, days);
                        _bookingService.AddBooking(booking);
                        _notificationService.SendNotification(
                            $"Vehicle {vehicle.Model} rented successfully",
                            customer.Email,
                            "Email"
                        );
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
    }
}
