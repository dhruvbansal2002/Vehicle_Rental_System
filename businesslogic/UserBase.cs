using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public abstract class UserBase : IUserAuthentication
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string UserId { get; protected set; }
        protected string Password { get; set; }
        protected bool IsLoggedIn { get; private set; }

        protected UserBase(string name, string email, string phoneNumber, string userId, string password)
        {
            ValidateUsername(userId);
            ValidatePassword(password);

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            UserId = userId;
            Password = password;
        }

        private void ValidateUsername(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId) || userId.Length < 5)
            {
                throw new ArgumentException("Username must be at least 5 characters long.");
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || !password.Any(char.IsDigit) || !password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                throw new ArgumentException("Password must be at least 8 characters long, contain at least one digit, one uppercase letter, and one lowercase letter.");
            }
        }

        public virtual bool Login(string userId, string password)
        {
            if (UserId == userId && Password == password)
            {
                IsLoggedIn = true;
                Console.WriteLine("Login successful!");
                return true;
            }
            Console.WriteLine("Invalid credentials.");
            return false;
        }

        public virtual void Logout()
        {
            IsLoggedIn = false;
            Console.WriteLine("Logged out successfully.");
        }

        public void ListAvailableVehicles(IVehicleRepository vehicleRepository)
        {
            var availableVehicles = vehicleRepository.GetAvailableVehicles();
            if (availableVehicles.Count == 0)
            {
                Console.WriteLine("No vehicles available.");
                return;
            }
            Console.WriteLine("\nAvailable Vehicles:");
            Console.WriteLine("-------------------------------");

            int index = 1;
            foreach (var vehicle in availableVehicles)
            {
                Console.WriteLine($"{index}. Type: {vehicle.Type}, Model: {vehicle.Model}, Price per Day: ₹{vehicle.PricePerDay} (Available)");
                index++;
            }
        }
    }
}
