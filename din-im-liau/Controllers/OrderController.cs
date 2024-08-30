
using din_im_liau.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace din_im_liau.Controllers;


[SwaggerTag("團購單")]
public class OrderController : BaseController
{

    /// <summary>
    /// 團購單列表
    /// </summary>
    /// <param name="condition">條件</param>
    /// <returns></returns>
    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] BaseConditionRequest condition)
    {
        return Ok(new { condition });
    }

    /// <summary>
    /// 取得團購單資料
    /// </summary>
    /// <param name="id">團購單id</param>
    /// <returns></returns>
    [HttpGet("detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        return Ok(new { id });
    }

    /// <summary>
    /// 產生該筆團購單的統計報表
    /// </summary>
    /// <returns></returns>
    [HttpPost("sheet")]
    public async Task<IActionResult> Sheet([FromBody] BaseIdRequest request)
    {
        return Ok(request);
    }


    /// <summary>
    /// 建立一筆新的團購單
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] BaseIdRequest request)
    {
        return Ok(new { request });
    }

    /// <summary>
    /// 關閉團購單
    /// </summary>
    /// <param name="request">團購單id</param>
    /// <returns></returns>
    [HttpPost("close")]
    public async Task<IActionResult> Close([FromBody] BaseIdRequest request)
    {
        return Ok(new { request });
    }

    /// <summary>
    /// 刪除團購單
    /// </summary>
    /// <param name="request">團購單名稱</param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] BaseIdRequest request)
    {
        return Ok(new { request });
    }

    /// <summary>
    /// 刪除團購單內一筆飲料
    /// </summary>
    /// <param name="request">飲料</param>
    /// <returns></returns>
    [HttpDelete("record/remove")]
    public async Task<IActionResult> RemoveRecord([FromBody] BaseIdRequest request)
    {
        return Ok(new { request });
    }

    /// <summary>
    /// 加入飲料到團購單
    /// </summary>
    /// <param name="request">飲料資料</param>
    /// <returns></returns>
    [HttpPost("record/create")]
    public async Task<IActionResult> CreateRecord([FromBody] BaseIdRequest request)
    {
        return Ok(new { request });
    }


    /// <summary>
    /// 更新內容品項
    /// </summary>
    /// <param name="request">品項</param>
    /// <returns></returns>
    [HttpPut("record/update")]
    public async Task<IActionResult> UpdateRecord([FromBody] BaseIdRequest request)
    {
        return Ok(new { request });
    }
}
