
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Amazon;
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.DataModels;
using NUglify.JavaScript.Syntax;
using Services;

namespace din_im_liau.Pages.User;

public class ProfileVM
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Email 位址")]
    public string Email { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "長度太長")]
    [Display(Name = "您的暱稱")]
    public string NickName { get; set; }
}
public class ProfileModel : BasePageModel
{
    [BindProperty]
    public ProfileVM ProfileVM { get; set; }
    public ProfileModel(IHttpContextAccessor accessor) : base(accessor) { }

    public async Task OnGet()
    {

        ProfileVM = new ProfileVM
        {
            Email = Account?.Email ?? "",
            NickName = Account?.NickName ?? "",
            Id = Account?.Id ?? 0
        };
        Console.WriteLine(ProfileVM.Id);
    }

    public async Task<IActionResult> OnPost()
    {
        // IgnoreFieldValidation(nameof(ProfileVM.Email));
        // foreach (var k in ModelState.Keys)
        // {
        //     var v = ModelState[k];
        //     Console.WriteLine($"{k}");
        // }
        // var page = Page();
        // page.Page.BadRequest
        if (!ModelState.IsValid)
        {
            return Page();
        }
        // ModelState.Add("", "");
        // ModelState.AddModelError("ProfileVM.NickName", "Wrong!");
        // return Page(new {});
        if (Account != null)
        {
            await _accountService.UpdateNickName(ProfileVM.Id, ProfileVM.NickName);
            return RedirectToPage("index");

        }
        else
        {
            return Page();
        }
    }
}
