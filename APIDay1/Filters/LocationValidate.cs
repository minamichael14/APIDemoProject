using Microsoft.AspNetCore.Mvc.Filters;

namespace APIDay1.Filters
{
    public class LocationValidate : ActionFilterAttribute
    {
        private readonly string _propertyName;
        public LocationValidate(string propertyName )
        {
            _propertyName = propertyName;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var x = context.HttpContext.Request.Body.ToString();
            //System.Configuration.appse
            
            //if != "USA")
            //{
            //    throw new Exception("Not allowed Location");
            //}
        }
    }
}
