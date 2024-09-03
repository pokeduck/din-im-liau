
using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

#nullable disable warnings

public class UserListRequest : BaseConditionRequest
{
    [Required]
    public string? Keyword { get; set; }
}
