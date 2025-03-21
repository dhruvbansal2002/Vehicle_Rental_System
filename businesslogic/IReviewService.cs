using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IReviewService
    {
        void AddReview(string customerName, string vehicleModel, string comments, int rating);
        void DeleteReview(string customerName, string vehicleModel);
        void ViewReviews();
    }
}
