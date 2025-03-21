namespace Vehicle_Management_System.BusinessLogic
{ 
    public class Program
    {
        static void Main()
        {
            var vehicleRepository = new DefaultVehicleRepository();
            var reviewService = new DefaultReviewService();
            var bookingService = new DefaultBookingService();
            var notificationService = new EmailNotificationService();
            var paymentProcessorFactory = new PaymentProcessorFactory();
            var vehicleRentalService = new VehicleRentalService(paymentProcessorFactory, bookingService, notificationService);
            var customers = new List<Customer>();

            while (true)
            {
                Console.WriteLine("----------DRIVE SAFE----------");
                Console.WriteLine("\nWelcome to Vehicle Rental System");
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int roleChoice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (roleChoice)
                {
                    case 1:
                        HandleCustomerMenu(customers, notificationService, reviewService, bookingService, vehicleRepository, vehicleRentalService);
                        break;
                    case 2:
                        HandleAdminMenu(vehicleRepository, bookingService);
                        break;
                    case 3:
                        Console.WriteLine("Thank you for using the Vehicle Rental System!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private static void HandleCustomerMenu(List<Customer> customers, INotificationService notificationService, IReviewService reviewService, IBookingService bookingService, IVehicleRepository vehicleRepository, IVehicleRentalService vehicleRentalService)
        {
            while (true)
            {
                Console.WriteLine("\n1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Back");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        RegisterCustomer(customers, notificationService, reviewService, bookingService, vehicleRentalService);
                        break;
                    case 2:
                        LoginCustomer(customers, vehicleRepository, vehicleRentalService, notificationService, reviewService, bookingService);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static void RegisterCustomer(List<Customer> customers, INotificationService notificationService, IReviewService reviewService, IBookingService bookingService, IVehicleRentalService vehicleRentalService)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter User ID: ");
            string userId = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Password: ");
            string password = Console.ReadLine() ?? string.Empty;
            var customer = new Customer(name, email, phoneNumber, userId, password, vehicleRentalService, notificationService, reviewService, bookingService);
            customers.Add(customer);
            Console.WriteLine("Registration successful! You can now log in.");
        }

        private static void LoginCustomer(List<Customer> customers, IVehicleRepository vehicleRepository, IVehicleRentalService vehicleRentalService, INotificationService notificationService, IReviewService reviewService, IBookingService bookingService)
        {
            Console.Write("Enter User ID: ");
            string loginId = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Password: ");
            string loginPassword = Console.ReadLine() ?? string.Empty;
            Customer? customer = customers.FirstOrDefault(c => c.UserId == loginId);
            if (customer != null && customer.Login(loginId, loginPassword))
            {
                while (true)
                {
                    Console.WriteLine("\nCustomer Menu");
                    Console.WriteLine("1. View Available Vehicles");
                    Console.WriteLine("2. Rent a Vehicle");
                    Console.WriteLine("3. Add a Review");
                    Console.WriteLine("4. View Reviews");
                    Console.WriteLine("5. Delete a Review");
                    Console.WriteLine("6. Complain");
                    Console.WriteLine("7. Return a Vehicle");
                    Console.WriteLine("8. SOS Call");
                    Console.WriteLine("9. Logout");
                    Console.Write("Enter your choice: ");
                    if (!int.TryParse(Console.ReadLine(), out int customerChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (customerChoice)
                    {
                        case 1:
                            customer.ListAvailableVehicles(vehicleRepository);
                            break;
                        case 2:
                            customer.RentVehicleFromList(vehicleRepository);
                            break;
                        case 3:
                            AddCustomerReview(customer);
                            break;
                        case 4:
                            customer.ViewReviews();
                            break;
                        case 5:
                            customer.DeleteReview();
                            break;
                        case 6:
                            RegisterCustomerComplaint(customer);
                            break;
                        case 7:
                            ReturnVehicle(customer);
                            break;
                        case 8:
                            SOSCall();
                            break;
                        case 9:
                            customer.Logout();
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Login failed.");
            }
        }

        private static void AddCustomerReview(Customer customer)
        {
            var rentedVehicles = customer.RentedVehicles.ToList();
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

            Console.Write("Enter the number of the vehicle you want to review: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleNumber) && vehicleNumber > 0 && vehicleNumber <= rentedVehicles.Count)
            {
                var rentedVehicle = rentedVehicles[vehicleNumber - 1];
                Console.Write("Enter your review comments: ");
                string comments = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter rating (1-5): ");
                if (int.TryParse(Console.ReadLine(), out int rating))
                {
                    customer.AddReview(rentedVehicle, comments, rating);
                }
                else
                {
                    Console.WriteLine("Invalid rating. Please enter a number between 1 and 5.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private static void RegisterCustomerComplaint(Customer customer)
        {
            var rentedVehicles = customer.RentedVehicles.ToList();
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

            Console.Write("Enter the number of the vehicle you want to complain about: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleNumber) && vehicleNumber > 0 && vehicleNumber <= rentedVehicles.Count)
            {
                var rentedVehicle = rentedVehicles[vehicleNumber - 1];
                Console.Write("Enter your complaint: ");
                string complaint = Console.ReadLine() ?? string.Empty;
                customer.Complain(rentedVehicle, complaint);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private static void ReturnVehicle(Customer customer)
        {
            var rentedVehicles = customer.RentedVehicles.ToList();
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

            Console.Write("Enter the number of the vehicle you want to return: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleNumber) && vehicleNumber > 0 && vehicleNumber <= rentedVehicles.Count)
            {
                var rentedVehicle = rentedVehicles[vehicleNumber - 1];
                Console.Write("Enter User ID: ");
                string userId = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter Password: ");
                string password = Console.ReadLine() ?? string.Empty;

                if (customer.UserId == userId && customer.Login(userId, password))
                {
                    customer.ReturnVehicle(rentedVehicle);
                    Console.WriteLine("Vehicle returned successfully!");
                }
                else
                {
                    Console.WriteLine("Authentication failed. Vehicle return unsuccessful.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private static void SOSCall()
        {
            Console.WriteLine("CALLING 108 AND EMERGENCY CONTACT NUMBER.");
        }

        private static void HandleAdminMenu(IVehicleRepository vehicleRepository, IBookingService bookingService)
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Panel - Manage Vehicles");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Remove Vehicle");
                Console.WriteLine("3. List Vehicles");
                Console.WriteLine("4. Print Company Details");
                Console.WriteLine("5. Generate Company Report");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddVehicle(vehicleRepository);
                        break;
                    case 2:
                        RemoveVehicle(vehicleRepository);
                        break;
                    case 3:
                        ListVehicles(vehicleRepository);
                        break;
                    case 4:
                        PrintCompanyDetails();
                        break;
                    case 5:
                        bookingService.GenerateReport();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void AddVehicle(IVehicleRepository vehicleRepository)
        {
            Console.Write("Enter vehicle type: ");
            string type = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter vehicle model: ");
            string model = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter price per day: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal pricePerDay))
            {
                var vehicle = new Vehicle(type, model, pricePerDay);
                vehicleRepository.AddVehicle(vehicle);
                Console.WriteLine("Vehicle added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
            }
        }

        private static void RemoveVehicle(IVehicleRepository vehicleRepository)
        {
            Console.Write("Enter model of vehicle to remove: ");
            string model = Console.ReadLine() ?? string.Empty;
            var vehicleToRemove = vehicleRepository.GetAllVehicles().FirstOrDefault(v => v.Model == model);
            if (vehicleToRemove != null)
            {
                vehicleRepository.RemoveVehicle(vehicleToRemove);
                Console.WriteLine("Vehicle removed successfully!");
            }
            else
            {
                Console.WriteLine("Vehicle not found.");
            }
        }

        private static void ListVehicles(IVehicleRepository vehicleRepository)
        {
            var allVehicles = vehicleRepository.GetAllVehicles();
            foreach (var vehicle in allVehicles)
            {
                vehicle.DisplayVehicleDetails();
            }
        }

        private static void PrintCompanyDetails()
        {
            Console.WriteLine("\nCompany Details:");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Company Name: ABC Car Rental Service ");
            Console.WriteLine("Address: Kengeri, Bangalore, India");
            Console.WriteLine("Contact: +91-9876543210");
        }
    }
}