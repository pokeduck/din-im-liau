
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Controllers.Login;

[Route("user/google2")]
public class Google2Controller : ControllerBase
{
    private HttpContext _httpContext;
    public Google2Controller(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }
    [Route("")]
    [HttpPost]
    public IActionResult Index()
    {
        Console.WriteLine(_httpContext.Request.Cookies);
        return new JsonResult(new { controller = true });
    }
}
