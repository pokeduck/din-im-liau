
using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

public class ShopSearchRequest : BaseConditionRequest
{
    [Required]
    public string? Keyword { get; set; }
}
