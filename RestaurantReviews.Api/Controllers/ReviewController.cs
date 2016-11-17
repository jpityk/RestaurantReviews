using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Api.Filters;
using RestaurantReviews.Business;
using RestaurantReviews.Models;

namespace RestaurantReviews.Api.Controllers
{
    [UnhandledExceptionFilter]
    public class ReviewController : ApiController
    {
        private IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IHttpActionResult Get(int userID)
        {
            var reviewList = _reviewService.GetReviewsByUser(userID);

            if (reviewList.Count == 0)
            {
                return NotFound();
            }

            return Ok(reviewList);
        }

        [HttpPost]
        public IHttpActionResult Post(Review review)
        {
            var newReview = _reviewService.SaveReview(review);

            return Content(HttpStatusCode.Created, newReview);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deleteResult = _reviewService.DeleteReview(id);

            return Ok(deleteResult);
        }
    }
}
