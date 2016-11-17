using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Models;

namespace RestaurantReviews.Business
{
    public interface IReviewService
    {
        List<Review> GetReviewsByUser(int userID);
        Review SaveReview(Review review);
        bool DeleteReview(int reviewID);
    }
}
