using System.Net;
using Common.Enums;

namespace Models.Exceptions;

public class ForbiddenException : CustomPayloadException
{
    private const HttpStatusCode _HttpStatusCode = HttpStatusCode.Forbidden;

    public ForbiddenException(string message) : base(_HttpStatusCode, message) { }


    public ForbiddenException(string message, ResultErrorCode resultErrorCode) : base(_HttpStatusCode, message, resultErrorCode) { }

    public ForbiddenException(string message, ResultErrorCode resultErrorCode, object? resultPayload) : base(_HttpStatusCode, message, resultErrorCode, resultPayload) { }

}
