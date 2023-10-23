using MiniProcurement.Exceptions;

namespace MiniProcurement.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotAuthorizedException ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(ex.Message);

            }
            catch(ResourceExistsException ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal server error");
            }
        }

    }
}
