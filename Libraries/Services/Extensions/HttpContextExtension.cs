
using Microsoft.AspNetCore.Http;
using Models.DataModels;

namespace Services.Extensions;

public static class HttpContextExtension {
    public static Account GetAccount(this HttpContext httpContext) {
        var result = httpContext.Items.TryGetValue("Account", out var value);

        if (!result)
            return default!;

        var newAcc = (Account?)value;
        return newAcc ?? default!;
    }
}
