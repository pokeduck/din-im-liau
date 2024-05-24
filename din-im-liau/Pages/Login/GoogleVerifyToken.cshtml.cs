
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace din_im_liau.Pages.Login;

public class GoogleVerifyTokenModel : PageModel
{

    public async Task<IActionResult> OnGet()
    {
        Console.WriteLine("OnGet");
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        Console.WriteLine("OnPost");
        return Page();
    }
}
