
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUglify.JavaScript.Syntax;

namespace din_im_liau.Pages.User;

public class ProfileVM
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
}
public class ProfileModel : BasePageModel
{
    [BindProperty]
    public ProfileVM ProfileVM { get; set; }
    public ProfileModel(IHttpContextAccessor accessor) : base(accessor) { }

    public async Task<IActionResult> OnGet()
    {

    }
}
