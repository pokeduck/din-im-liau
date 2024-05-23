using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace din_im_liau.Pages.Login;

public class GoogleModel : PageModel
{
    public GoogleModel() { }

    public async Task<IActionResult> OnGet()
    {
        return Page();

    }

    public async Task<IActionResult> OnPost([FromBody] object payload)
    {
        Console.WriteLine(payload);
        return Page();

    }
}
