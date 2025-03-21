using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class Vehicle : IVehicle
    {
        public string Type { get; }
        public string Model { get; }
        public decimal PricePerDay { get; }
        public bool IsRented { get; set; }

        public Vehicle(string type, string model, decimal pricePerDay)
        {
            Type = type;
            Model = model;
            PricePerDay = pricePerDay;
            IsRented = false;
        }

        public void DisplayVehicleDetails()
        {
            string status = IsRented ? "(Rented)" : "(Available)";
            Console.WriteLine($"Type: {Type}, Model: {Model}, Price per Day: ₹{PricePerDay} {status}");
        }
    }
}
