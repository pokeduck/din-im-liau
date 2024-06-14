
using din_im_liau.Page;
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Pages.User;

public class LogoutModel : BasePageModel
{
    public LogoutModel(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

    public async Task<IActionResult> OnGet()
    {
        await SignOut();
        return new RedirectToPageResult("/Index");
    }
}
