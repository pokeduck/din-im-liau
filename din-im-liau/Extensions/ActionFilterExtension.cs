using Microsoft.AspNetCore.Mvc.Filters;

namespace din_im_liau.Extensions;

public static class ActionFilterExtension
{
    static public bool HasActionAttribute<T>(this ActionExecutingContext context) where T : Attribute
    {
        var result = context.ActionDescriptor.EndpointMetadata.Any(x => x.GetType() == typeof(T));
        return result;
    }
}
