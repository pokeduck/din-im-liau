using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace Common.Extensions;

public static class CookieExtensions
{
    const string GoogleStateCookieName = "google_oauth_state_token";
    public static string? GetGoogleStateRequestCookie(this HttpContext context)
    {
        var result = context.Request.Cookies[GoogleStateCookieName];
        return result;
    }

    public static void SetGoogleStateResponseCookie(this HttpContext context, string value)
    {
        context.Response.Cookies.Append(GoogleStateCookieName, value);
    }

    public static void DeleteGoogleState(this HttpContext context)
    {
        var options = new CookieOptions();
        options.Expires = DateTime.Now.AddSeconds(-1);
        context.Response.Cookies.Append(GoogleStateCookieName, "", options);
    }

    public static bool IsGoogleStateTokenValid(this HttpContext context, string value, bool isDeleteAfterSearch = true)
    {

        var state = context.GetGoogleStateRequestCookie();
        if (state != null)
        {
            if (state == value)
            {
                if (isDeleteAfterSearch)
                    context.DeleteGoogleState();
                return true;
            }
        }
        if (isDeleteAfterSearch)
            context.DeleteGoogleState();



        return false;
    }
}


