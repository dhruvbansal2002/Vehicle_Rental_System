using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public class DefaultReviewService : IReviewService
    {
        private readonly List<Review> _reviews = new();

        public void AddReview(string customerName, string vehicleModel, string comments, int rating)
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }
            _reviews.Add(new Review(customerName, vehicleModel, comments, rating));
        }

        public void DeleteReview(string customerName, string vehicleModel)
        {
            var review = _reviews.FirstOrDefault(r =>
                r.CustomerName == customerName && r.VehicleModel == vehicleModel);
            if (review != null)
            {
                _reviews.Remove(review);
            }
        }

        public void ViewReviews()
        {
            foreach (var review in _reviews)
            {
                Console.WriteLine($"Customer: {review.CustomerName}");
                Console.WriteLine($"Vehicle: {review.VehicleModel}");
                Console.WriteLine($"Rating: {review.Rating}/5");
                Console.WriteLine($"Comments: {review.Comments}");
                Console.WriteLine("------------------------");
            }
        }
    }
}
