

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace din_im_liau.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
// [Route("[controller]")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
    protected Account? Account {
        get {
            var result = HttpContext.Items.TryGetValue("Account", out var value);

            if (!result)
                return null;

            var  newAcc = (Account?)value;
            return newAcc;

         }
    }

}
