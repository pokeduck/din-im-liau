
using Models.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Models.Attributes;

public class SwaggerSuccessResponseAttribute : SwaggerResponseAttribute
{

    private const int SuccessCode = 200;
    private const string SuccessMessage = "Success";
    public SwaggerSuccessResponseAttribute(Type? type = null) : base(SuccessCode, SuccessMessage, type ?? typeof(DefaultResponse))
    {

    }
}
