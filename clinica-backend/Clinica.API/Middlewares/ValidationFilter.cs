using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clinica.API.Middlewares
{
    public class ValidationFilter<T>(
        IValidator<T> validator) 
        : IAsyncActionFilter where T : class
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, 
            ActionExecutionDelegate next)
        {
            // Tenta encontrar o DTO correspondente nos argumentos da rota/body
            var param = context.ActionArguments.Values.FirstOrDefault(v => v is T);

            if (param != null)
            {
                var validationResult = await validator.ValidateAsync((T)param);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                        .ToDictionary(g => g.Key, g => g.ToArray());

                    var problemDetails = new ValidationProblemDetails(errors)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Um ou mais erros de validação ocorreram.",
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                    return;
                }
            }

            await next();
        }
    }
}