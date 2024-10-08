using Models.Attributes;
using Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Responses;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Models.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using din_im_liau.Middlewares;

namespace din_im_liau.Controllers;


[SwaggerTag("會員")]
public class UserController : BaseController
{

    private readonly AccountService AccountService;

    public UserController(AccountService accountService)
    {
        AccountService = accountService;
    }
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
    [SwaggerSuccessResponse(typeof(GenericResponse<AccountDTO>))]
    public async Task<IActionResult> Profile()
    {

        var currentAccountId = Account?.Id ?? throw new NotFoundException(" user not found");
        var currentAccount = await AccountService.GetAccountDTOById(currentAccountId) ?? throw new NotFoundException(" user not found");

        Response200.Data = currentAccount;

        return Ok(Response200);
    }

    /// <summary>
    /// 更新會員資料
    /// </summary>
    /// <param name="body">會員內容</param>
    /// <returns></returns>
    [HttpPut("profile")]
    [SwaggerSuccessResponse(typeof(GenericResponse<AccountDTO>))]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest body)
    {
        Response200.Data = await AccountService.UpdateAccount(Account.Id, body);
        return Ok(Response200);
    }


    /// <summary>
    /// 會員列表
    /// </summary>
    /// <param name="condition">列表條件</param>
    /// <returns></returns>
    [AuthorizeRoleAdmin]
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
