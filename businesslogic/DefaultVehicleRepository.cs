using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class DefaultVehicleRepository : IVehicleRepository
    {
        private readonly List<IVehicle> _vehicles;

        public DefaultVehicleRepository()
        {
            _vehicles = new List<IVehicle>
            {
                new Vehicle("Car", "Toyota Corolla", 1500),
                new Vehicle("Bike", "Honda CB350", 700),
                new Vehicle("SUV", "Mahindra XUV500", 2500),
                new Vehicle("Truck", "Tata Ace", 3000)
            };
        }

        public List<IVehicle> GetAllVehicles() => _vehicles;
        public List<IVehicle> GetAvailableVehicles() => _vehicles.Where(v => !v.IsRented).ToList();
        public void AddVehicle(IVehicle vehicle) => _vehicles.Add(vehicle);
        public void RemoveVehicle(IVehicle vehicle) => _vehicles.Remove(vehicle);
    }
}
