using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Models;

namespace RestaurantReviews.Business
{
    public class ReviewService : IReviewService
    {
        public List<Review> GetReviewsByUser(int userID)
        {
            throw new NotImplementedException();
        }

        public Review SaveReview(Review review)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReview(int reviewID)
        {
            throw new NotImplementedException();
        }
    }
}
