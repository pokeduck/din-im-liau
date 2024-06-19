
using System.ComponentModel.DataAnnotations;
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUglify.JavaScript.Syntax;

namespace din_im_liau.Pages.User;

public class ProfileVM
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "")]
    public string NickName { get; set; }
}
public class ProfileModel : BasePageModel
{
    [BindProperty]
    public ProfileVM ProfileVM { get; set; }
    public ProfileModel(IHttpContextAccessor accessor) : base(accessor) { }

    public async Task<IActionResult> OnGet()
    {
        ProfileVM = new ProfileVM
        {
            Email = Account?.Email ?? "",
            NickName = Account?.NickName ?? "",
            Id = Account?.Id ?? 0
        };
        return Page();
    }

    public async Task<IActionResult> OnPost() {
        if (ModelState.IsValid) {

        }
        Console.WriteLine(ProfileVM.NickName);
        Console.WriteLine(ProfileVM.Email);
        return Page();
    }
}
