
using System.ComponentModel.DataAnnotations;

namespace din_im_liau.Request;

public class ShopSearchRequest : BaseConditionRequest
{
    [Required]
    public string? Keyword { get; set; }
}
