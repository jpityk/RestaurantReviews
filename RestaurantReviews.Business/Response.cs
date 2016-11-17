using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Business
{
    public class Response
    {
        public enum StatusEnum
        {
            Success,
            Fail
        }

        public StatusEnum Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }

    public class Response<T> : Response
    {
        T Data { get; set; }
    }
}
