using din_im_liau.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace din_im_liau.Controllers;


[SwaggerTag("會員")]
public class UserController : BaseController
{
    /// <summary>
    /// 會員資料 by id
    /// </summary>
    /// <param name="id">會員id</param>
    /// <returns></returns>
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> ProfileById(int id)
    {

        return Ok(new { Id = id });
    }

    /// <summary>
    /// 會員資料 by token
    /// </summary>
    /// <returns></returns>
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {

        return Ok(Account);
    }

    /// <summary>
    /// 更新會員資料
    /// </summary>
    /// <param name="body">會員內容</param>
    /// <returns></returns>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest body)
    {

        return Ok(body);
    }


    /// <summary>
    /// 會員列表
    /// </summary>
    /// <param name="condition">列表條件</param>
    /// <returns></returns>
    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] UserListRequest condition)
    {
        return Ok(condition);
    }

    /// <summary>
    /// 追蹤商店
    /// </summary>
    /// <param name="id">商店id</param>
    /// <returns></returns>

    [HttpPost("shop/follow")]
    public async Task<IActionResult> Follow([FromForm] string id)
    {
        return Ok(Account);
    }

    /// <summary>
    /// 列出已追蹤商店
    /// </summary>
    /// <returns></returns>

    [HttpGet("shop/following")]
    public async Task<IActionResult> Following([FromBody] BaseConditionRequest condition)
    {
        return Ok(condition);
    }


}
