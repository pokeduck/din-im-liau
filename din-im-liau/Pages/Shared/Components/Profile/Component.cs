using Microsoft.AspNetCore.Mvc;


namespace din_im_liau.Pages.Components
{
    // [ViewComponent(Name = "Profile")]
    public class Profile : BaseViewComponents
    {
        public Profile() { }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

        public class ProfileViewModel { }
    }

}

