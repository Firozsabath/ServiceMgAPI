using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ServiceManagement.WebAPI
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            this.logger.LogError(exception,"Exception Ocuured {Message}",exception.Message);

            var probDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error"
            };

            httpContext.Response.StatusCode = probDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(probDetails, cancellationToken);

            return true;
        }
    }
}
