
using System.ComponentModel.DataAnnotations;

namespace din_im_liau.Request;

#nullable disable warnings

public class UserListRequest : BaseConditionRequest
{
    [Required]
    public string? Keyword { get; set; }
}
