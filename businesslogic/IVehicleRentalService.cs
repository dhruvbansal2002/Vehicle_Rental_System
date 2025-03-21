using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IVehicleRentalService
    {
        void RentVehicle(IVehicle vehicle, ICustomer customer);
    }
}
