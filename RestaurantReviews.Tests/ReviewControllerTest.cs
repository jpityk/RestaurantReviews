using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RestaurantReviews.Api;
using RestaurantReviews.Api.Controllers;
using System.Web.Http.Results;
using RestaurantReviews.Business;
using RestaurantReviews.Models;
using System.Net;
using System.Net.Http;
using System.Web.Routing;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http;
using Moq;

namespace RestaurantReviews.Tests
{
    [TestClass]
    public class ReviewControllerTest
    {
        [TestMethod]
        public void GetReviewsByUser_Success()
        {
            //arrange
            var userID = 1;
            var serviceMock = new Mock<IReviewService>();
            serviceMock.Setup(rs => rs.GetReviewsByUser(userID)).Returns(GetReviewList);
            var controller = new ReviewController(serviceMock.Object);

            //act
            var actionResult = controller.Get(userID);
            var contentResult = actionResult as OkNegotiatedContentResult<List<Review>>;

            //assert
            Assert.IsNotNull(contentResult);
            var list = contentResult.Content;
            Assert.AreEqual(true, list.Count > 0);
            Assert.AreEqual(list[0].UserID, userID);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void GetReviewsByUser_DivideByZeroException()
        {
            //arrange
            var userID = 1;
            var serviceMock = new Mock<IReviewService>();
            serviceMock.Setup(rs => rs.GetReviewsByUser(userID)).Throws<DivideByZeroException>();
            var controller = new ReviewController(serviceMock.Object);

            //act
            var actionResult = controller.Get(userID);
        }

        [TestMethod]
        public void PostReview_Success()
        {
            //arrange
            var review = new Review() { RestaurantID = 1, UserID = 1, ReviewText = "Best sandwich in town." };
            var serviceMock = new Mock<IReviewService>();
            serviceMock.Setup(rs => rs.SaveReview(review)).Returns(GetSavedReview(review));
            var controller = new ReviewController(serviceMock.Object);

            //act
            var actionResult = controller.Post(review);
            var contentResult = actionResult as NegotiatedContentResult<Review>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Best sandwich in town.", contentResult.Content.ReviewText);
            Assert.AreEqual(true, contentResult.Content.ReviewID > 0);
        }

        [TestMethod]
        public void DeleteReview_Success()
        {
            //arrange
            var reviewID = 3;
            var serviceMock = new Mock<IReviewService>();
            serviceMock.Setup(rs => rs.DeleteReview(reviewID)).Returns(true);
            var controller = new ReviewController(serviceMock.Object);

            //act
            var actionResult = controller.Delete(reviewID);
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(true, contentResult.Content);
        }

        protected List<Review> GetReviewList()
        {
            var list = new List<Review>();

            list.Add(new Review() { ReviewID = 1, RestaurantID = 1, UserID = 1, ReviewText = "This place rocks." });
            list.Add(new Review() { ReviewID = 2, RestaurantID = 2, UserID = 1, ReviewText = "Never eat here!" });

            return list;
        }

        protected Review GetSavedReview(Review review)
        {
            review.ReviewID = 3;
            return review;
        }
    }
}
