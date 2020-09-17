using ContactsApi.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ContactsApi.Presentation.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message) { StatusCode = (int)DetermineStatusCode(context.Exception) };
            context.ExceptionHandled = true;
        }

        HttpStatusCode DetermineStatusCode(Exception exception)
        {
            return exception switch
            {
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                NoResourceFoundException _ => HttpStatusCode.NotFound,
                ValidationException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
