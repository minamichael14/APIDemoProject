using Microsoft.AspNetCore.Mvc.Filters;

namespace APIDay1.Filters
{
    public class LocationValidate : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var property in context.ActionArguments.Values)
            {
                var z = property;
            }
            var arguments = context.ActionArguments.Values.FirstOrDefault();
            var location = arguments.GetType().GetProperty("Location").GetValue(arguments);

            if (location.ToString() != "USA")
            {
                throw new Exception("Not allowed Location");
            }
        }
    }
}
