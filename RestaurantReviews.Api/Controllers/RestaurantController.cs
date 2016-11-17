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
    public class RestaurantController : ApiController
    {
        private IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public IHttpActionResult Get(string city)
        {
            var restaurantList = _restaurantService.GetRestaurantsByCity(city);

            if (restaurantList.Count == 0)
            {
                return NotFound();
            }

            return Ok(restaurantList);
        }

        [HttpPost]
        public IHttpActionResult Post(Restaurant restaurant)
        {
            var newRestaurant = _restaurantService.SaveRestaurant(restaurant);

            return Content(HttpStatusCode.Created, newRestaurant);
        }

    }
}
