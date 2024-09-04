using System.Net;
using Common.Enums;

namespace Models.Exceptions;

public class NotFoundException : CustomPayloadException
{
    private const HttpStatusCode _HttpStatusCode = HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(_HttpStatusCode, message) { }


    public NotFoundException(string message, ResultErrorCode resultErrorCode) : base(_HttpStatusCode, message, resultErrorCode) { }

    public NotFoundException(string message, ResultErrorCode resultErrorCode, object? resultPayload) : base(_HttpStatusCode, message, resultErrorCode,resultPayload) {}

}
