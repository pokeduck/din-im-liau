
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;

namespace din_im_liau.Pages.User;

public class ProfileModel : BasePageModel
{
    public ProfileModel(IHttpContextAccessor accessor) : base(accessor) { }
}