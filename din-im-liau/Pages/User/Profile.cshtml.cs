
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using Amazon;
using Common.Helper;
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
    public string AvatarUrl { get; set; }
    [Display(Name = "Email 位址")]
    public string? Email { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "長度太長")]
    [Display(Name = "您的暱稱")]
    public string NickName { get; set; }

}
public class ProfileModel : BasePageModel
{
    [BindProperty]
    public IFormFile Upload { get; set; }


    [BindProperty]
    public ProfileVM ProfileVM { get; set; }

    private IWebHostEnvironment _environment;
    public ProfileModel(IHttpContextAccessor accessor, IWebHostEnvironment environment) : base(accessor)
    {
        _environment = environment;
    }

    public void OnGet()
    {

        ProfileVM = new ProfileVM
        {
            NickName = Account?.NickName ?? "",
            Email = Account?.Email ?? "",
            AvatarUrl = Account?.ThumbnailUrl ?? "https://localhost:8888/assets/profile-placeholder.png"

        };

    }

    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine("sss");
        Console.WriteLine("sss");
        // var folderName = "uploads";
        // var fileExtension = Path.GetExtension(Upload.FileName);
        var test = new List<IFormFile>();
        var size = test.Sum(f => f.Length);

        Stream st = Upload.OpenReadStream();
        MemoryStream mst = new MemoryStream();
        st.CopyTo(mst);
        var hashValue = HashHelper.Generate(mst.ToArray());

        Console.WriteLine($"hash:{hashValue}");
        // var photoUrl = "localhost:8888/localhost"
        var ext = System.IO.Path.GetExtension(Upload.FileName);
        var file = Path.Combine(_environment.ContentRootPath, "uploads", $"{hashValue}{ext}");
        var thumbnailPath = Path.Combine("/","uploads", $"{hashValue}{ext}");

        if (System.IO.File.Exists(file))
        {
            Console.WriteLine("Exist!");
        }
        else
        {
            Console.WriteLine("Create!");
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

        }

        IgnoreFieldValidation(nameof(ProfileVM.Email));
        IgnoreFieldValidation(nameof(ProfileVM.AvatarUrl));
        // foreach (var k in ModelState.Keys)
        // {
        //     var v = ModelState[k];
        //     Console.WriteLine($"{k}");
        // }
        // var page = Page();
        // page.Page.BadRequest


        if (!ModelState.IsValid)
        {
            ProfileVM.Email = Account.Email;
            ProfileVM.AvatarUrl = Account?.ThumbnailUrl ?? $"https://localhost:8888{thumbnailPath}";
            return Page();
        }
        // ModelState.Add("", "");
        // ModelState.AddModelError("ProfileVM.NickName", "Wrong!");
        // return Page(new {});
        if (Account != null)
        {
            await _accountService.UpdateNickName(Account.Id, ProfileVM.NickName);
            return RedirectToIndexPage();

        }
        else
        {
            return Page();
        }
    }


    // public async Task<(string fileName, string name, string url)> UploadFile(IFormFile file)
    // {
    //     if (file.Length <= 0)
    //         throw new InvalidDataException();
    //     //throw new ForbiddenException($"{nameof(UploadFile)}, name: {file.Name} file size is less than or equal to 0");

    //     var defaultPath = "uploads";
    //     var fileExtension = Path.GetExtension(file.FileName);
    //     var folderPath = fileExtension switch
    //     {
    //         ".jpg" or ".jpeg" or ".png" => Path.Combine(defaultPath, "images"),
    //         ".pdf" => Path.Combine(defaultPath, "pdf"),
    //         _ => throw new NotImplementedException("不支援上傳此副檔名"),
    //     };

    //     var path = Path.Combine(_environment.WebRootPath, folderPath);

    //     if (!Directory.Exists(path))
    //         Directory.CreateDirectory(path);

    //     using var memoryStream = new MemoryStream();
    //     await file.CopyToAsync(memoryStream);

    //     var hash = HashHelper.Generate("SHA1", memoryStream.ToArray());
    //     var fileName = $"{hash}{fileExtension}";
    //     var url = $"/{folderPath.Replace("\\", "/")}/{fileName}";

    //     using var fileStream = File.Create(Path.Combine(path, fileName));
    //     await file.CopyToAsync(fileStream);

    //     return (fileName, file.FileName, url);
    // }

}
