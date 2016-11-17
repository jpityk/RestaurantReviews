using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;

namespace RestaurantReviews.Api.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = "Oops"
            };
            throw new HttpResponseException(resp);
        }
    }
}