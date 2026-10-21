using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArgumentException = System.ArgumentException;

namespace Clinica.API.Middlewares
{
    public class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Ocorreu uma excepção na API: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno no servidor.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Detail = "Ocorreu um erro inesperado. A nossa equipa já foi notificada."
            };

            // Tratamento de erros de domínio (ArgumentException lançado pelas entidades)
            if (exception is ArgumentException argEx)
            {
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Erro de validação de regra de negócio.";
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                problemDetails.Detail = argEx.Message;
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; // Indica que a excepção foi tratada
        }
    }
}