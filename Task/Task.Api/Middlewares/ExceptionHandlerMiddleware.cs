using Task.Api.Helpers;
using Task.Service.Exceptions;

namespace Task.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate request;
        public ExceptionHandlerMiddleware(RequestDelegate request)
        {
            this.request = request;
        }
        public async System.Threading.Tasks.Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                });
            }
            catch (AlreadyExistException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                });
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                });
            }
        }
    }
}
