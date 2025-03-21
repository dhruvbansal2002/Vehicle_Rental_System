using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class Customer : UserBase, ICustomer
    {
        private readonly List<IVehicle> _rentedVehicles = new();
        private readonly IVehicleRentalService _vehicleRentalService;
        private readonly INotificationService _notificationService;
        private readonly IReviewService _reviewService;
        private readonly IBookingService _bookingService;

        public Customer(
            string name,
            string email,
            string phoneNumber,
            string userId,
            string password,
            IVehicleRentalService vehicleRentalService,
            INotificationService notificationService,
            IReviewService reviewService,
            IBookingService bookingService)
            : base(name, email, phoneNumber, userId, password)
        {
            _vehicleRentalService = vehicleRentalService;
            _notificationService = notificationService;
            _reviewService = reviewService;
            _bookingService = bookingService;
        }

        public void ViewReviews()
        {
            _reviewService.ViewReviews();
            Console.WriteLine("Viewed reviews.");
        }

        public void RentVehicle(IVehicle vehicle)
        {
            _vehicleRentalService.RentVehicle(vehicle, this);
            _rentedVehicles.Add(vehicle); // Ensure the vehicle is added to the rented list
            Console.WriteLine($"Rented vehicle: {vehicle.Model}");
        }

        public void ReturnVehicle(IVehicle vehicle)
        {
            if (_rentedVehicles.Contains(vehicle))
            {
                vehicle.IsRented = false;
                _rentedVehicles.Remove(vehicle);
                _notificationService.SendNotification(
                    $"Vehicle {vehicle.Model} returned successfully",
                    Email,
                    "Email"
                );
                Console.WriteLine($"Returned vehicle: {vehicle.Model}");
            }
        }

        public void AddReview(IVehicle vehicle, string comments, int rating)
        {
            if (_rentedVehicles.Contains(vehicle))
            {
                _reviewService.AddReview(Name, vehicle.Model, comments, rating);
                Console.WriteLine("Review added successfully!");
                Console.WriteLine($"Added review for vehicle: {vehicle.Model}");
            }
            else
            {
                Console.WriteLine("You can only review vehicles you have rented.");
            }
        }

        public void Complain(IVehicle vehicle, string complaint)
        {
            if (_rentedVehicles.Contains(vehicle))
            {
                Console.WriteLine($"Complaint registered for {vehicle.Model}: {complaint}");
                // Assuming a payment processor is available for refund
                // _paymentProcessor.RefundPayment();
                Console.WriteLine($"Complaint registered for vehicle: {vehicle.Model}");
            }
            else
            {
                Console.WriteLine("You have not rented this vehicle.");
            }
        }

        public IEnumerable<IVehicle> RentedVehicles => _rentedVehicles;

        public void RentVehicleFromList(IVehicleRepository vehicleRepository)
        {
            ListAvailableVehicles(vehicleRepository);
            var availableVehicles = vehicleRepository.GetAvailableVehicles();
            if (availableVehicles.Count == 0) return;

            Console.Write("\nEnter the number of the vehicle you want to rent: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= availableVehicles.Count)
            {
                IVehicle selectedVehicle = availableVehicles[choice - 1];
                RentVehicle(selectedVehicle);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        public void DeleteReview()
        {
            var rentedVehicles = RentedVehicles.ToList();
            if (rentedVehicles.Count == 0)
            {
                Console.WriteLine("You have not rented any vehicles.");
                return;
            }

            Console.WriteLine("Rented Vehicles:");
            for (int i = 0; i < rentedVehicles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rentedVehicles[i].Model}");
            }

            Console.Write("Enter the number of the vehicle for which you want to delete your review: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleNumber) && vehicleNumber > 0 && vehicleNumber <= rentedVehicles.Count)
            {
                var vehicleModel = rentedVehicles[vehicleNumber - 1].Model;
                _reviewService.DeleteReview(Name, vehicleModel);
                Console.WriteLine($"Deleted review for vehicle: {vehicleModel}");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}
