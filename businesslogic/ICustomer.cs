using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface ICustomer
    {
        string Name { get; }
        string Email { get; }
        void RentVehicle(IVehicle vehicle);
        void ReturnVehicle(IVehicle vehicle);
        void AddReview(IVehicle vehicle, string comments, int rating);
        void Complain(IVehicle vehicle, string complaint);
    }
}
