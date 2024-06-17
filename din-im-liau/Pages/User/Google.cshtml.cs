
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Pages.User;
[AllowAnonymous]
[IgnoreAntiforgeryToken(Order = 1001)]
public class GoogleModel(IHttpContextAccessor httpContextAccessor) : BasePageModel(httpContextAccessor)
{
    public async Task<IActionResult> OnPost()
    {
        return new JsonResult(new { Success = true });

    }
}

