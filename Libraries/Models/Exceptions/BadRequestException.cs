using System.Net;
using Common.Enums;

namespace Models.Exceptions;

public class BadRequestException : CustomPayloadException
{

    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }


    public BadRequestException(string message, ResultErrorCode resultErrorCode) : base(HttpStatusCode.BadRequest, message, resultErrorCode) { }

    public BadRequestException(string message, ResultErrorCode resultErrorCode, object? resultPayload) : base(HttpStatusCode.BadRequest, message, resultErrorCode, resultPayload) { }
}
