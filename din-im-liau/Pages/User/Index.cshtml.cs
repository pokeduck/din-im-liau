

using Amazon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace din_im_liau.Pages.User;
public class IndexModel : PageModel {
    public IndexModel(){}

    public IActionResult OnGet() {
        return new RedirectToPageResult("/user/profile");
    }
}
