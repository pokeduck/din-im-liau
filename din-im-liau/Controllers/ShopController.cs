using din_im_liau.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace din_im_liau.Controllers;


[SwaggerTag("店家")]
public class ShopController : BaseController
{

    /// <summary>
    /// 店家列表
    /// </summary>
    /// <param name="condition">篩選條件</param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<IActionResult> List([FromBody] BaseConditionRequest condition)
    {
        return Ok(condition);
    }

    /// <summary>
    /// 店家詳細資料
    /// </summary>
    /// <param name="id">店家id</param>
    /// <returns></returns>
    [HttpGet("detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        return Ok(new { id });
    }

    /// <summary>
    /// 搜尋店家
    /// </summary>
    /// <param name="keyword">店家條件</param>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] ShopSearchRequest keyword)
    {
        return Ok(new { keyword });
    }
}

