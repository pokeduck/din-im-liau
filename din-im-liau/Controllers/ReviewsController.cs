
using Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace din_im_liau.Controllers;

[SwaggerTag("評論")]
public class ReviewsController : BaseController
{

    /// <summary>
    /// 產品評論總表
    /// </summary>
    /// <param name="condition">條件</param>
    /// <returns></returns>
    [HttpPost("product/list")]
    public async Task<IActionResult> ProductList([FromBody] BaseConditionRequest condition)
    {
        return Ok(condition);
    }


    /// <summary>
    /// 產品評論
    /// </summary>
    /// <param name="id">產品id</param>
    /// <returns></returns>
    [HttpGet("product/{id}")]
    public async Task<IActionResult> ProductDetail(int id)
    {
        return Ok(new { id = id });
    }


    /// <summary>
    /// 店家評論
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("shop/{id}/rating")]
    public async Task<IActionResult> ShopRating(int id)
    {
        return Ok(new { id = id });
    }


}
