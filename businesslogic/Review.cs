using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class Review
    {
        public string CustomerName { get; }
        public string VehicleModel { get; }
        public string Comments { get; }
        public int Rating { get; }

        public Review(string customerName, string vehicleModel, string comments, int rating)
        {
            CustomerName = customerName;
            VehicleModel = vehicleModel;
            Comments = comments;
            Rating = rating;
        }
    }
}
