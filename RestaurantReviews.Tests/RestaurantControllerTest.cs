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
    public class RestaurantControllerTest
    {
        [TestMethod]
        public void GetRestaurantsByCity_Success()
        {
            //arrange
            var city = "Pittsburgh";
            var serviceMock = new Mock<IRestaurantService>();
            serviceMock.Setup(rs => rs.GetRestaurantsByCity(city)).Returns(GetRestaurantList);
            var controller = new RestaurantController(serviceMock.Object);
            
            //act
            var actionResult = controller.Get(city);
            var contentResult = actionResult as OkNegotiatedContentResult<List<Restaurant>>;
            
            //assert
            Assert.IsNotNull(contentResult);
            var list = contentResult.Content;
            Assert.AreEqual(true, list.Count > 0);
            Assert.AreEqual(list[0].City, city);
        }

        [TestMethod]
        public void GetRestaurantsByCity_ReturnsNotFound()
        {
            //arrange
            var city = "Pittsburgh";
            var serviceMock = new Mock<IRestaurantService>();
            serviceMock.Setup(rs => rs.GetRestaurantsByCity(city)).Returns(new List<Restaurant>());
            var controller = new RestaurantController(serviceMock.Object);

            //act
            var actionResult = controller.Get(city);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostRestaurant_Success()
        {
            //arrange
            var restaurant = new Restaurant() { Name = "Bill's Bakery", City = "Pittsburgh" };
            var serviceMock = new Mock<IRestaurantService>();
            serviceMock.Setup(rs => rs.SaveRestaurant(restaurant)).Returns(GetSavedRestaurant(restaurant));
            var controller = new RestaurantController(serviceMock.Object);

            //act
            var actionResult = controller.Post(restaurant);
            var contentResult = actionResult as NegotiatedContentResult<Restaurant>;

            //assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Bill's Bakery", contentResult.Content.Name);
            Assert.AreEqual(true, contentResult.Content.RestaurantID > 0);
        }

        protected List<Restaurant> GetRestaurantList()
        {
            var list = new List<Restaurant>();

            list.Add(new Restaurant() { RestaurantID = 1, Name = "Sea Sharp", City = "Pittsburgh" });
            list.Add(new Restaurant() { RestaurantID = 2, Name = "Food N'at", City = "Pittsburgh" });

            return list;
        }

        protected Restaurant GetSavedRestaurant(Restaurant restaurant)
        {
            restaurant.RestaurantID = 3;
            return restaurant;
        }
    }
}
