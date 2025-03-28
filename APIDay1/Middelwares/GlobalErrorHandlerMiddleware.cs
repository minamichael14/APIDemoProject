namespace APIDay1.Middelwares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    message = ex.Message is null ?
                            "An unexpected error occurred." : ex.Message,
                    stackTrace = ex.StackTrace,                        
                };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
