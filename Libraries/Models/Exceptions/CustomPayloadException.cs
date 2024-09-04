using System.Net;
using Common.Enums;

namespace Models.Exceptions;

public class CustomPayloadException : Exception
{

    public HttpStatusCode HttpStatusCode;
    public ResultErrorCode ResultCode;
    public object? ResultPayload;

    public CustomPayloadException(HttpStatusCode statusCode, string message) : base(message)
    {
        HttpStatusCode = statusCode;
        ResultCode = ResultErrorCode.CommonError;
        ResultPayload = null;
    }

    public CustomPayloadException(HttpStatusCode statusCode, string message, ResultErrorCode resultCode) : base(message)
    {
        HttpStatusCode = statusCode;
        ResultCode = resultCode;
        ResultPayload = null;
    }

    public CustomPayloadException(HttpStatusCode statusCode, string message, ResultErrorCode resultCode, object? resultPayload) : base(message)
    {
        HttpStatusCode = statusCode;
        ResultCode = resultCode;
        ResultPayload = resultPayload;
    }

}
