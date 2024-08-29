using System.ComponentModel.DataAnnotations;

namespace din_im_liau.Request;
#nullable disable warnings

public class BaseConditionRequest
{
    [Required]
    public int? Offset { get; set; }

    [Required]

    public int? PageSize { get; set; }
}
