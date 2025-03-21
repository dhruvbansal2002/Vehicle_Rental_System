using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IVehicleRepository
    {
        List<IVehicle> GetAllVehicles();
        List<IVehicle> GetAvailableVehicles();
        void AddVehicle(IVehicle vehicle);
        void RemoveVehicle(IVehicle vehicle);
    }
}
