

using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
// [Route("[controller]")]
[Produces("application/json")]
[ApiController]
public class BaseController : ControllerBase
{

}
