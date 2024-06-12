

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace din_im_liau.Page;

[Authorize]
public class BasePageModel : PageModel
{

    protected readonly HttpContext _httpContext;
    public BasePageModel(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }
}
