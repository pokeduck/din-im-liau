
using din_im_liau.Page;
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Pages.User;
public class OrdersModel(IHttpContextAccessor httpContextAccessor) : BasePageModel(httpContextAccessor)
{
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }
}
