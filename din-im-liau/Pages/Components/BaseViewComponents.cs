
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Pages.Components;

public class BaseViewComponents : ViewComponent
{
    public BaseViewComponents()
    {

    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
