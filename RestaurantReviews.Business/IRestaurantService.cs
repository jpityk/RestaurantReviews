using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Models;

namespace RestaurantReviews.Business
{
    public interface IRestaurantService
    {
        List<Restaurant> GetRestaurantsByCity(string city);
        Restaurant SaveRestaurant(Restaurant restaurant);
    }
}
