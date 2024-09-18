

using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using Models.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace din_im_liau.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
// [Route("[controller]")]
[Produces("application/json")]
[ApiController]
[Authorize]
[SwaggerResponse((int)(HttpStatusCode.BadRequest), BadRequestMessage, typeof(ErrorResponse))]
[SwaggerResponse((int)(HttpStatusCode.Unauthorized), UnauthorizedMessage, typeof(ErrorResponse))]
[SwaggerResponse((int)(HttpStatusCode.Forbidden), ForbiddenMessage, typeof(ErrorResponse))]
[SwaggerResponse((int)(HttpStatusCode.NotFound), NotFoundMessage, typeof(ErrorResponse))]
[SwaggerResponse((int)(HttpStatusCode.InternalServerError), InternalErrorMessage, typeof(ErrorResponse))]
public class BaseController : ControllerBase
{

    private const string BadRequestMessage = "Bad Request.";
    private const string NotFoundMessage = "Not found.";
    private const string UnauthorizedMessage = "Unauthorized.";
    private const string ForbiddenMessage = "Forbidden.";

    private const string InternalErrorMessage = "Internal Server Error.";
    private const string SuccessMessage = "Success.";


    protected GenericResponse<object> Response200 = new()
    {
        Code = 0,
        Message = SuccessMessage
    };

    protected GenericResponse<object> Response404 = new()
    {
        Code = 0,
        Message = NotFoundMessage
    };

    protected GenericResponse<object> Response500 = new()
    {
        Code = 0,
        Message = InternalErrorMessage
    };

    protected class ErrorResponse : DefaultResponse
    {
        [DataMember(Order = 1)]
        [DefaultValue(200)]
        public override int Code { get; set; }

    }

    protected Account Account
    {
        get
        {
            var result = HttpContext.Items.TryGetValue("Account", out var value);

            if (!result)
                return new Account();

            var newAcc = (Account?)value ?? new Account();
            return newAcc;

        }
    }

}
