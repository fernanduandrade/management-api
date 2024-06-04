using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Manager.Presentation.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);    
        }
        catch(ValidationException ex)
        {
            var validationErrors = GetBadRequestValidation(ex);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsync(JsonConvert.SerializeObject(validationErrors));
        }
        catch (Exception ex)
        {
            var responseError = new
            {
                Message = ex.Message
            };
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsync(JsonConvert.SerializeObject(responseError));
        }
        
    }
    private ValidationProblemDetails GetBadRequestValidation(ValidationException ex)
    {
        var traceId = Guid.NewGuid().ToString();

        var errors = new Dictionary<string, string[]>();

        foreach(var error in ex.Errors)
        {
            errors.Add(error.PropertyName, [error.ErrorMessage]);
        }

        var validationErrors = new ValidationProblemDetails(errors)
        {
            Status = (int)HttpStatusCode.BadRequest,
            Type = "https://httpstatuses.com/400",
            Title = "Validação fahou",
            Detail = "Um ou mais errors ocorreram por favor e precisam ser corrigidos. Por favor olhe os detalhes",
            Instance = traceId
        };


        return validationErrors;

    }
}