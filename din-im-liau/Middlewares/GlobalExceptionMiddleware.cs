using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Models.Exceptions;
using Models.Responses;

namespace din_im_liau.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower, WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //await LogRequest(context);
            await _next(context);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = ex.Message;
            var errorCode = 999;
            object? payload = null;
            if (ex is CustomPayloadException customerExp)
            {
                statusCode = (int)customerExp.HttpStatusCode;
                message = customerExp.Message;
                errorCode = (int)customerExp.ResultCode;
            }
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
            }
            var defaultResponse = new DefaultResponse { Code = errorCode, Message = message, Data = payload };
            var response = JsonSerializer.Serialize(defaultResponse, _jsonSerializerOptions);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
            await context.Response.CompleteAsync();
            //await LogResponse(context, ex);
        }
    }
}
