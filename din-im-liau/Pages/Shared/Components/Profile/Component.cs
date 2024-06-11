using Microsoft.AspNetCore.Mvc;
using Models.DataModels;


namespace din_im_liau.Pages.Components
{
    // [ViewComponent(Name = "Profile")]
    public class Profile : BaseViewComponents
    {

        private readonly DataContext _dataContext;
        private readonly HttpContext _httpContext;
        public Profile(DataContext dataContext, HttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

        public class ProfileViewModel { }
    }

}

