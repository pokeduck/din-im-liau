using Microsoft.AspNetCore.Mvc;
using din_im_liau.Pages.Components;

namespace din_im_liau.Pages.Components.Profile
{
    [ViewComponent(Name = "Profile")]
    public class Component : BaseViewComponents
    {
        public Component() { }

        // public IViewComponentResult Invoke(ProfileViewModel viewModel)
        // {
        //     return View(viewModel);
        // }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

        public class ProfileViewModel { }
    }

}

