using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Filters
{
    public class XCorsFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Request.Headers.Add("Access-Control-Allow-Origin", "*");
            context.HttpContext.Request.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
            context.HttpContext.Request.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");


            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");
            base.OnActionExecuted(context);

        }
    }
}
