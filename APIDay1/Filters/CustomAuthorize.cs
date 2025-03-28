using System.Security.Claims;
using APIDay1.Models;
using APIDay1.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIDay1.Filters
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        private string _role;
        public CustomAuthorize(string role) 
        {
            _role = role;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User;
            var claimRole = claims.FindFirst(ClaimTypes.Role);
            if (claimRole == null || string.IsNullOrEmpty(claimRole.Value))
            {
                throw new UnauthorizedAccessException();
            }
            if(_role != claimRole.Value)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
