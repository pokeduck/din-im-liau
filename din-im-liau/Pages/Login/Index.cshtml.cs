using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace din_im_liau.Pages.Login;
public class IndexModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostVerifyGoogleToken(object response)
    {
        Console.WriteLine(response);
        return Page();
    }
}
