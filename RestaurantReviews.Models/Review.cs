using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int RestaurantID { get; set; }
        public int UserID { get; set; }
        public string ReviewText { get; set; }
    }
}
