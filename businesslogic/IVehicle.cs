using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IVehicle
    {
        string Type { get; }
        string Model { get; }
        decimal PricePerDay { get; }
        bool IsRented { get; set; }
        void DisplayVehicleDetails();
    }
}
