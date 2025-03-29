using Microsoft.AspNetCore.Mvc.Filters;

namespace APIDay1.Filters
{
    public class CustomResponseHeader : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("AppName", "WebAppName..");
        }

    }
}
